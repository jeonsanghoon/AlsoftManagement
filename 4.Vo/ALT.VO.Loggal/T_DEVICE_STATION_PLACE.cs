using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{

    #region >> 로컬박스장소(T_DEVICE_PLACE)


    /// <summary>       
    /// 광고장소(T_DEVICE_PLACE)
    /// </summary>	   
    public class T_DEVICE_STATION_PLACE_COND
    {
        /// <summary>       
        /// 광고장소 기본키(순번)
        /// </summary>	   
        public long? IDX { get; set; }
        /// <summary>       
        /// 스테이션코드 T_DEVICE_STATION 테이블의 T_DEVICE_STATION
        /// </summary>	   
        public int? STATION_CODE { get; set; }
        /// <summary>
        /// 마스터데이터 여부(default : 1), 마스터일 경우 반경등록 마스터가 아닌 경우 마스터 범위안에 등록되어야함
        /// </summary>
        public bool? IS_RANGE { get; set; }
        /// <summary>       
        /// T_CATEGORY_KEYRORD 테이블의 CK_CODE
        /// </summary>	   
        public int? CK_CODE { get; set; }
        /// <summary>       
        /// 지역(시/도 시/군/구 읍/면동) 
        /// </summary>	   
        public string REGION { get; set; }
        /// <summary>       
        /// 지번주소
        /// </summary>	   
        public string JIBUN_ADDRESS { get; set; }
        /// <summary>       
        /// 도로명주소
        /// </summary>	   
        public string ROAD_ADDRESS { get; set; }
        /// <summary>       
        /// 건물명
        /// </summary>	   
        public string BUILDING { get; set; }
        /// <summary>       
        /// 우편번호
        /// </summary>	   
        public string ZIP_CODE { get; set; }
    }

    /// <summary>       
    /// 로컬박스장소(T_DEVICE_PLACE)
    /// </summary>	   
    public class T_DEVICE_STATION_PLACE
    {
        /// <summary>       
        /// 로컬박스장소 기본키(순번)
        /// </summary>	   
        public long IDX { get; set; }
        /// <summary>       
        /// 로컬박스코드 T_DEVICE_STATION 테이블의 STATION_CODE
        /// </summary>	   
        public int? STATION_CODE { get; set; }
        /// <summary>
        /// 마스터데이터 여부(default : 1), 마스터일 경우 반경등록 마스터가 아닌 경우 마스터 범위안에 등록되어야함
        /// </summary>
        public bool IS_RANGE { get; set; }
        /// <summary>       
        /// T_CATEGORY_KEYRORD 테이블의 CK_CODE
        /// </summary>	   
        public int? CK_CODE { get; set; }
        /// <summary>       
        /// 지역(시/도 시/군/구 읍/면동) 
        /// </summary>	   
        public string REGION { get; set; }
        /// <summary>       
        /// 지번주소
        /// </summary>	   
        public string JIBUN_ADDRESS { get; set; }
        /// <summary>       
        /// 도로명주소
        /// </summary>	   
        public string ROAD_ADDRESS { get; set; }
        /// <summary>       
        /// 건물명
        /// </summary>	   
        public string BUILDING { get; set; }
        /// <summary>       
        /// 우편번호
        /// </summary>	   
        public string ZIP_CODE { get; set; }
        /// <summary>       
        /// 위도
        /// </summary>	   
        public decimal? LATITUDE { get; set; }
        /// <summary>       
        /// 경도
        /// </summary>	   
        public decimal? LONGITUDE { get; set; }
        /// <summary>
        /// 반경(m 기준)
        /// </summary>
        public int? RADIUS { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 사용자명
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
    }
    #endregion >> 로컬박스장소(T_DEVICE_PLACE) END 
}
