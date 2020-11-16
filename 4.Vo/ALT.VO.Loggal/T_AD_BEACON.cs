using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 배너비콘정보(T_AD_BEACON)
    public class T_AD_BEACON_COND
    {

        /// <summary>       
        /// 순번(기본키)
        /// </summary>	
        public int? IDX { get; set; }
        /// <summary>       
        /// 배너코드(T_AD 테이블의 AD_CODE)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 비콘기기번호
        /// </summary>
        public string DEVICE_NUBMER { get; set; }
        /// <summary>
        /// 비콘기기번호 멀티조건 
        /// </summary>
        public string DEVICE_NUMBERS { get; set; }
        /// <summary>       
        /// 비콘테이블(T_BEACON의 BEACON_CODE)
        /// </summary>	   
        public Int64? BEACON_CODE { get; set; }

        /// <summary>
        /// 비콘코드 구분자 , IN 조건에 포함
        /// </summary>
        public string BEACON_CODES { get; set; }
        /// <summary>
        /// 비콘명
        /// </summary>
        public string BEACON_NAME { get; set; }
        /// <summary>
        /// 검색일자
        /// </summary>
        public string SEARCH_DATE { get; set; }
        /// <summary>       
        /// 배너 제목
        /// </summary>	   
        public string TITLE { get; set; }
        /// <summary>       
        /// 0:사용 1:미사용 T_COMMON테이블의 U004
        /// </summary>	   
        public bool? HIDE { get; set; }
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

    }
    /// <summary>       
    /// 배너비콘정보(T_AD_BEACON)
    /// </summary>	   
    public class T_AD_BEACON
    {
        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 순번(기본키)
        /// </summary>	   
        public Int64 IDX { get; set; }
        /// <summary>       
        /// 배너코드(T_AD 테이블의 AD_CODE)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 로고 URL
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>       
        /// 비콘테이블(T_BEACON의 BEACON_CODE)
        /// </summary>	   
        public Int64? BEACON_CODE { get; set; }
        /// <summary>
        /// 비콘명
        /// </summary>
        public string BEACON_NAME { get; set; }
        /// <summary>
        /// 비콘기기 고유번호
        /// </summary>
        public string DEVICE_NUMBER { get; set; }
        /// <summary>
        /// 비콘로고 URL
        /// </summary>
        public string BEACON_LOGO_URL { get; set; }
        /// <summary>       
        /// 시작일자(yyyyMMdd)
        /// </summary>	   
        public string FR_DATE { get; set; }
        /// <summary>       
        /// 종료일자(yyyyMMdd)
        /// </summary>	   
        public string TO_DATE { get; set; }
        /// <summary>       
        /// 시작시간(HH:mm)
        /// </summary>	   
        public string FR_TIME { get; set; }
        /// <summary>       
        /// 종료시간(HH:mm)
        /// </summary>	   
        public string TO_TIME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 0:사용 1:미사용 T_COMMON테이블의 U004
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int INSERT_CODE { get; set; }
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
        public int UPDATE_CODE { get; set; }

        /// <summary>
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
        /// <summary>
        /// 수신시간
        /// </summary>
        public DateTime RECEIVE_TIME { get; set; }

        public int TOTAL_ROWCOUNT { get; set; }
        public string SORT { get; set; }
    }
    #endregion >> 배너비콘정보(T_AD_BEACON) END 

}
