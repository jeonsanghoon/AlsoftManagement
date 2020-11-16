using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    public class T_LOG_COND
    {
        public int? COMPANY_CODE { get; set; }
        public int? STORE_CODE { get; set; }
        public string DEPT_SEARCH { get; set; }
        public int? PARENT_MEMBER_CODE { get; set; }
        public int? LOGIN_MEMBER_CODE { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public int? LOG_TYPE { get; set; }
        public string LOG_DATA1 { get; set; }
        public string LOG_DATA2 { get; set; }
        public string LOG_DESC { get; set; }
        public string INSERT_NAME { get; set; }
        public string SORT_ORDER { get; set; }
        //public string LOG_DAT2 { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
    }

    #region >> 로그테이블(T_LOG)
    /// <summary>       
    /// 로그테이블(T_LOG)
    /// </summary>	   
    public class T_LOG
    {
        /// <summary>       
        /// 기본키(순번)
        /// </summary>	   
        public Int64 IDX { get; set; }
        /// <summary>
        /// 매장코드(T_STORE 테이블참조)
        /// </summary>
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 지점명
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>
        /// 부서명
        /// </summary>
        public string DEPT_NAME { get; set; }
        /// <summary>       
        /// 로그일시(yyyyMMddHHmmss 24시간으로 표시)
        /// </summary>	   
        public string LOG_DATE { get; set; }
        /// <summary>       
        /// 로그유형(T_COMMON 테이블의 MAIN_CODE:L002
        /// </summary>	   
        public int LOG_TYPE { get; set; }

        public string LOG_TYPE_NAME { get; set; }
        /// <summary>       
        /// 사용자정의1
        /// </summary>	   
        public string LOG_DATA1 { get; set; }
        /// <summary>       
        /// 사용자정의2
        /// </summary>	   
        public string LOG_DATA2 { get; set; }
        /// <summary>       
        /// 사용자정의3
        /// </summary>	   
        public string LOG_DATA3 { get; set; }
        /// <summary>       
        /// 로그상세정보
        /// </summary>	   
        public string LOG_DESC { get; set; }
        /// <summary>       
        /// 사용 IP 
        /// </summary>	   
        public string USE_IP { get; set; }
        /// <summary>       
        /// 로그 관련 테이블
        /// </summary>	   
        public string LOG_TABLE { get; set; }
        /// <summary>       
        /// 등록자 T_MEMER의 MEMBER_CODE
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        public string INSERT_NAME { get; set; }
        public string INSERT_ID { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 로그테이블(T_LOG) END 

}
