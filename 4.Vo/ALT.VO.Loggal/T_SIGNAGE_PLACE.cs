using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{

    /// <summary>       
    /// 로컬사이니지장소(T_SIGNAGE_PLACE)
    /// </summary>	   
    public class T_SIGNAGE_PLACE_COND
    {
        /// <summary>       
        /// 광고장소 기본키(순번)
        /// </summary>	   
        public long? IDX { get; set; }
        /// <summary>       
        /// 로컬사인코드 T_SIGNAGE 테이블의 SIGN_CODE
        /// </summary>	   
        public Int64? SIGN_CODE { get; set; }
        /// <summary>
        /// 장소유형(1:기준영역, 2:제어영역)
        /// </summary>
        public int? PLACE_TYPE { get; set; }
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

    #region >> 로컬사인장소(T_SIGNAGE_PLACE)

    public class SIGNAGE_PLACE_SAVE
    {
        public long? SIGN_CODE { get; set; } = 0;
        public int? PLACE_TYPE { get; set; } = 1;
        public int? REG_CODE { get; set; }
        public List<T_SIGNAGE_PLACE> list { get; set; }
    }
    /// <summary>       
    /// 로컬사인장소(T_SIGNAGE_PLACE)
    /// </summary>	   
    public class T_SIGNAGE_PLACE
    {
        /// <summary>       
        /// 로컬사인장소 기본키(순번)
        /// </summary>	   
        public Int64 IDX { get; set; }
        /// <summary>       
        /// 로컬사인코드 T_SIGNAGE 테이블의 SIGN_CODE
        /// </summary>	   
        public Int64? SIGN_CODE { get; set; }
        /// <summary>
        /// 장소유형(1:기준영역, 2:제어영역)
        /// </summary>
        public int? PLACE_TYPE { get; set; }
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
        /// 반경(기준 M)
        /// </summary>	   
        public int? RADIUS { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
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
    #endregion >> 로컬사인장소(T_SIGNAGE_PLACE) END 

}
