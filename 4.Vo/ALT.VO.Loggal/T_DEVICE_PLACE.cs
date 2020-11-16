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
    public class T_DEVICE_PLACE_COND
    {
        /// <summary>       
        /// 광고장소 기본키(순번)
        /// </summary>	   
        public long? IDX { get; set; }
        /// <summary>       
        /// 로컬박스코드 T_DEVICE 테이블의 DEVICE_CODE
        /// </summary>	   
        public long? DEVICE_CODE { get; set; }
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
    public class T_DEVICE_PLACE
    {
        /// <summary>       
        /// 로컬박스장소 기본키(순번)
        /// </summary>	   
        public long? IDX { get; set; }
        /// <summary>       
        /// 로컬박스코드 T_DEVICE 테이블의 DEVICE_CODE
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
		/// <summary>
		/// 로컬박스가상영역구분(T_COMMON : P005 1:기본영역, 2:가상영역)
		/// </summary>
		public int? PLACE_TYPE { get; set; }
		/// <summary>
		/// 로컬박스가상영역구분코드명
		/// </summary>
		public string PLACE_TYPE_NAME { get; set; }
		/// <summary>
		/// 순번
		/// </summary>
		public int SEQ { get; set; }
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
