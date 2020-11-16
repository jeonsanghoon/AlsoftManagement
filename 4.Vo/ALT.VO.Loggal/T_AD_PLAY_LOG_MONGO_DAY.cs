using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{

    /// <summary>
    /// 로컬박스별 광고 플레이 로그
    /// </summary>
    public class T_AD_PLAY_LOG_MONGO
    {

        public ObjectId _id { get; set; }
        /// <summary>       
        /// 등록일자 yyyyMMddHHmmddssfff
        /// </summary>	   
        //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime REG_DATE { get; set; }
        /// <summary>
        /// 저장일자 yyyyMMdd
        /// </summary>
        public string REG_DAY { get; set; }
        /// <summary>       
        /// 디바이스종류(T_COMMON MAIN_CODE : L003) 1:모바일, 2:로컬박스
        /// </summary>	   
        public int? DEVICE_KIND { get; set; }
        /// <summary>
        /// 디바이스종류
        /// </summary>
        public string DEVICE_KIND_NAME { get; set; }
        /// <summary>
        /// 화면분할(T_COMMON => MAIN_CODE: H002 1:1Frame, 12:12Frame)
        /// </summary>
        public int? FRAME_TYPE { get; set; }
        /// <summary>
        /// 화면분할(T_COMMON => MAIN_CODE: H002 1:1Frame, 12:12Frame)
        /// </summary>
        public string FRAME_TYPE_NAME { get; set; }
        /// <summary>
        /// 재생유형(T_COMON P004, 1:조회,2:클릭,3:재생)
        /// </summary>
        public int? PLAY_TYPE { get; set; }
        /// <summary>
        /// 재생유형(T_COMON P004, 1:조회,2:클릭,3:재생)
        /// </summary>
        public string PLAY_TYPE_NAME { get; set; }
        /// <summary>       
        /// 로컬박스코드(T_DEVICE)
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>z
        public string DEVICE_NAME { get; set; }
        /// <summary>       
        /// 광고코드(T_AD)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }

        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }

        /// <summary>       
        /// 플레이시간 NUMERIC(7,3) PLAY_TYPE 클릭(2) 일경우 60초로 등록
        /// </summary>	  
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal PLAY_TIME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        ///  T_AD : BANNER_TYPE2 >> 배너유형(1:이미지,2:동영상,3:유튜브-T_COMMON : A010)
        /// </summary>
        public int BANNER_TYPE2 { get; set; }
        /// <summary>
        ///  T_AD : BANNER_TYPE2 >> 배너유형(1:이미지,2:동영상,3:유튜브-T_COMMON : A010)
        /// </summary>
        public string BANNER_TYPE2_NAME { get; set; }

        /// <summary>
        /// 배너구분(B008 1:내배너 2:일반배너
        /// </summary>
        public int BANNER_KIND { get; set; }
        public string BANNER_KIND_NAME { get; set; }

    }



    /// <summary>
    /// 로컬박스별 광고 플레이 로그
    /// </summary>
    
    [DataContract]
    public class T_AD_PLAY_LOG_MONGO_DAY
    {
        /// <summary>       
        /// 등록일자 yyyyMMdd
        /// </summary>	   
        public String REG_DAY { get; set; }
        public DateTime REG_DATE { get; set; }
        /// <summary>       
        /// 로컬박스코드(T_DEVICE)
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>       
        /// 광고코드(T_AD)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        ///  T_AD : BANNER_TYPE2 >> 배너유형(1:이미지,2:동영상,3:유튜브-T_COMMON : A010)
        /// </summary>
        public int BANNER_TYPE2 { get; set; }
        /// <summary>       
        /// 디바이스종류(T_COMMON MAIN_CODE : L003) 1:모바일, 2:로컬박스
        /// </summary>	   
        public int? DEVICE_KIND { get; set; }
        /// <summary>
        /// 배너구분(B008 1:내배너 2:일반배너
        /// </summary>
        public int BANNER_KIND { get; set; }
        /// <summary>
        /// 화면분할(T_COMMON => MAIN_CODE: H002 1:1Frame, 12:12Frame)
        /// </summary>
        public int? FRAME_TYPE { get; set; }
        /// <summary>
        /// 재생유형(T_COMON P004, 1:조회,2:클릭,3:재생)
        /// </summary>
        public int? PLAY_TYPE { get; set; }
        /// <summary>       
        /// 총플레이시간
        /// </summary>	   
        
        public Decimal128 TOT_PLAY_TIME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }

        /// <summary>
        /// 저장건수
        /// </summary>
        public int TOT_QTY { get; set; } = 0;
        /// <summary>
        /// 저장시간
        /// </summary>
        public DateTime INSERT_DATE { get; set; }
    }

    public class T_AD_PLAY_LOG_MONGO_LIST
    {
        public long TOTAL_ROWCOUNT { get; set; } = 0;
        public List<T_AD_PLAY_LOG_MONGO> list { get; set; }
    }


    #region >> 일자별배너재생집계
    /// <summary>
    /// 일자볇배너재생집계조건
    /// </summary>
    public class AdPlayTotalCond
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT_ORDER { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public int? DEVICE_KIND { get; set; }
        public long? AD_CODE { get; set; }
        public string TITLE { get; set; }
        
        public int? BANNER_TYPE2 { get; set; }
        public int? BANNER_KIND { get; set; }
        public int? PLAY_TYPE { get; set; }
        public int? FRAME_TYPE { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
    }
    /// <summary>
    /// 일자별배너재생집계리스트
    /// </summary>
    public class AdPlayTotalList
    {
        public long ROW_IDX { get; set; }
        public string REG_MONTH { get; set; }
        public string REG_DAY { get; set; }
        public string DEVICE_KIND_NAME { get; set; }
        public long AD_CODE { get; set; }
        public string TITLE { get; set; }
        public int? DEVICE_CODE { get; set; }
     
        public string BANNER_TYPE2_NAME { get; set; }
        public string PLAY_TYPE_NAME { get; set; }
        public string FRAME_TYPE_NAME { get; set; }
        public string BANNER_KIND_NAME { get; set; }
        public int STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public decimal SUB_PLAY_TIME { get; set; }
        public int SUB_QTY { get; set; }
        public decimal TOT_PLAY_TIME { get; set; }
        public int TOT_QTY { get; set; }
        public int TOTAL_ROWCOUNT { get; set; } = 0;
    }

    public class AdPlayLocalboxTotalCond {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT_ORDER { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public long? DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }

        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }

        public int? BANNER_KIND { get;set; }
    }
    public class AdPlayLocalboxTotalList {
        public long ROW_IDX { get; set; }
        public string REG_DAY { get; set; }
        public long DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public int BANNER_KIND { get; set; }
        public string BANNER_KIND_NAME { get; set; }
        public int STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public decimal IMAGE1_PLAY_TIME { get; set; }
        public decimal IMAGE6_PLAY_TIME { get; set; }
        public decimal IMAGE12_PLAY_TIME { get; set; }
        public decimal CLICK_PLAY_TIME { get; set; }
        public decimal VIDEO_PLAY_TIME { get; set; }
        public decimal YOUTUBE_PLAY_TIME { get; set; }
        public int IMAGE1_PLAY_QTY { get; set; }
        public int IMAGE6_PLAY_QTY { get; set; }
        public int IMAGE12_PLAY_QTY { get; set; }
        public int CLICK_PLAY_QTY { get; set; }
        public int VIDEO_PLAY_QTY { get; set; }
        public int YOUTUBE_PLAY_QTY { get; set; }
        public int TOT_QTY { get; set; }
        public decimal TOT_PLAY_TIME { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }


    public class AdPlayBusiTotalList
    {
        public string REG_MONTH { get; set; }
        public long ROW_IDX { get; set; }
        public int AD_CNT { get; set; }
        public int DEVICE_CNT { get; set; }
        public int BANNER_KIND { get; set; }
        public string BANNER_KIND_NAME { get; set; }
        public int STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public decimal IMAGE1_PLAY_TIME { get; set; }
        public decimal IMAGE6_PLAY_TIME { get; set; }
        public decimal IMAGE12_PLAY_TIME { get; set; }
        public decimal CLICK_PLAY_TIME { get; set; }
        public decimal VIDEO_PLAY_TIME { get; set; }
        public decimal YOUTUBE_PLAY_TIME { get; set; }
        public int IMAGE1_PLAY_QTY { get; set; }
        public int IMAGE6_PLAY_QTY { get; set; }
        public int IMAGE12_PLAY_QTY { get; set; }
        public int CLICK_PLAY_QTY { get; set; }
        public int VIDEO_PLAY_QTY { get; set; }
        public int YOUTUBE_PLAY_QTY { get; set; }
        public int TOT_QTY { get; set; }
        public decimal TOT_PLAY_TIME { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }


    #endregion
}
