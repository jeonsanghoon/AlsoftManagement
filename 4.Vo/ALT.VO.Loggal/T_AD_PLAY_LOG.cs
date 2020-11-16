using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 기본기(순번)(T_AD_PLAY_LOG)

    /// <summary>
    /// 로컬박스별 광고 플레이 로그
    /// </summary>
    public class T_AD_PLAY_LOG
    {
        /// <summary>
        /// 검색순번
        /// </summary>
        public Int64 ROW_IDX { get; set; }
        /// <summary>       
        /// 기본키(순번)(T_AD_PLAY_LOG)
        /// </summary>	
        public Int64? IDX { get; set; }
        /// <summary>       
        /// 등록일자
        /// </summary>	   
        public DateTime? REG_DATE { get; set; }
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
        /// 재생유형(T_COMON P004, 1:조회,2:클릭,3:재생)
        /// </summary>
        public int? PLAY_TYPE { get; set; }
        /// <summary>       
        /// 로컬박스코드(T_DEVICE)
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>       
        /// 광고코드(T_AD)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 광고크드 (구분자는 콤마)
        /// </summary>
        public string AD_CODES { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>       
        /// 플레이시간 NUMERIC(7,3)
        /// </summary>	   
        public decimal? PLAY_TIME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>
        ///  T_AD : BANNER_TYPE2 >> 배너유형(1:이미지,2:동영상,3:유튜브-T_COMMON : A010)
        /// </summary>
        public string BANNER_TYPE2_NAME { get; set; }
        /// <summary>
        /// 검색총갯수
        /// </summary>
        public int TOTAL_ROWCOUNT { get; set; }
    }


    #endregion >> 기본기(순번)(T_AD_PLAY_LOG) END 
    #region >> 로컬박스별 광고 플레이 로그(T_AD_PLAY_LOG)조회조건
    /// <summary>       
    /// 로컬박스별 광고 플레이 로그(T_AD_PLAY_LOG)
    /// </summary>	   
    public class T_AD_PLAY_LOG_COND
    {
        [BsonId]
        public ObjectId ID { get; set; }
        /// <summary>
        /// 등록일(From - REG_DATE)
        /// </summary>
        public string FR_DATE { get; set; }
        /// <summary>
        /// 등록일(To - REG_DATE)
        /// </summary>
        public string TO_DATE { get; set; }

        public int? DEVICE_KIND { get; set; }
        public Int64? AD_CODE { get; set; }
        public Int64? DEVICE_CODE { get; set; }
        public string TITLE { get; set; }
        public string DEVICE_NAME { get; set; }

        /// <summary>
        /// 배너구분(내배너/일반배너)
        /// </summary>
        public int? BANNER_KIND { get; set; }

        /// <summary> 
        /// 페이지당 건수 (기본 20건)
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 선택된 페이지 기본 1
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 조회순서
        /// </summary>
        public string SORT_ORDER { get; set; }
        
    }

    #endregion >> 로컬박스별 광고 플레이 로그(T_AD_PLAY_LOG)조회조건 END 




}
