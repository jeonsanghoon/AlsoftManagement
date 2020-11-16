using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 비콘정보(T_BEACON)
    public class T_BEACON_COND
    {
        /// <summary>       
        /// 사인코드 
        /// </summary>	   
        public long? BEACON_CODE { get; set; }
        /// <summary>       
        /// 사인명
        /// </summary>	   
        public string BEACON_NAME { get; set; }
        /// <summary>
        /// 페이지
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 건수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 정렬 순서
        /// </summary>
        public string SORT { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool? HIDE { get; set; }
    }
    /// <summary>       
    /// 비콘정보(T_BEACON)
    /// </summary>	   
    public class T_BEACON
    {
        /// <summary>
        /// 저장유형 N: 추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 비콘고유코드 
        /// </summary>	   
        public Int64 BEACON_CODE { get; set; }
        /// <summary>       
        /// 비콘명
        /// </summary>	   
        public string BEACON_NAME { get; set; }
        /// <summary>       
        /// 로컬박스 고유번호 T_DEVICE_AUTH_NUMBER 테이블과 연계
        /// </summary>	   
        public string DEVICE_NUMBER { get; set; }
        /// <summary>
        /// 로고 URL
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>       
        /// 주소1
        /// </summary>	   
        public string ADDRESS1 { get; set; }
        /// <summary>       
        /// 주소2
        /// </summary>	   
        public string ADDRESS2 { get; set; }
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
        /// 0:사용 1:미사용 T_COMMON테이블의 U004
        /// </summary>	   
        public bool HIDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 조회건수
        /// </summary>
        public int? TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 비콘정보(T_BEACON) END 

}
