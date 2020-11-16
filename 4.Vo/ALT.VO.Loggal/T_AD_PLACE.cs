using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 광고장소(T_AD_PLACE)


    /// <summary>       
    /// 광고장소(T_AD_PLACE)
    /// </summary>	   
    public class T_AD_PLACE_COND
    {
        /// <summary>       
        /// 광고장소 기본키(순번)
        /// </summary>	   
        public long? IDX { get; set; }
        /// <summary>       
        /// 광고코드 T_AD 테이블의 AD_CODE
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
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
    /// 광고장소(T_AD_PLACE)
    /// </summary>	   
    public class T_AD_PLACE
    {
        /// <summary>       
        /// 광고장소 기본키(순번)
        /// </summary>	   
        public long IDX { get; set; }
        /// <summary>       
        /// 광고코드 T_AD 테이블의 AD_CODE
        /// </summary>	   
        public long? AD_CODE { get; set; }
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
        /// 등록자
        /// </summary>	   
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
    }
    #endregion >> 광고장소(T_AD_PLACE) END 


    //public class DAUM_MAP_API
    //{
    //    public string 
    //}

}
