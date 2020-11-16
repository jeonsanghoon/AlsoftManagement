using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    public class T_STORE_BUSINESSHOURS_COND
    {
        public int? STORE_CODE { get; set; }
        public string SEARCH_DATE { get; set; }
    }

    #region >> 매장별 영업시간 설정(T_STORE_BUSINESSHOURS)
    /// <summary>       
    /// 매장별 영업시간 설정(T_STORE_BUSINESSHOURS)
    /// </summary>	   
    public class T_STORE_BUSINESSHOURS
    {
        /// <summary>       
        /// T_STORE 테이블의 STORE_CODE
        /// </summary>	   
        public int STORE_CODE { get; set; }
        /// <summary>       
        /// T_COMMON테이블의 GROUP_CODE : S002참고
        /// </summary>	   
        public int SCHEDULE_TYPE { get; set; }
        /// <summary>       
        /// 요일(DATEPART-int형) 또는 날짜(yyyyMMdd)
        /// </summary>	   
        public string SCHEDULE_TYPE_DTL { get; set; }
        /// <summary>       
        /// 영업시작시간(24시간으로 표시 - 09:00-HH:mm)
        /// </summary>	   
        public string FR_TIME { get; set; }
    
        /// <summary>       
        /// 영업종료시간(24시간으로 표시 - 21:00-HH:mm)
        /// </summary>	   
        public string TO_TIME { get; set; }

        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// T_COMMON 테이블의 MAIN_CODE:S003
        /// </summary>	   
        public int? STATUS { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

    }


    /// <summary>
    /// 영업시간 조회 리스트
    /// </summary>
    public class T_STORE_BUSINESSHOURS_LIST
    {
        /// <summary>       
        /// T_STORE 테이블의 STORE_CODE
        /// </summary>	   
        public int STORE_CODE { get; set; }
        /// <summary>       
        /// T_COMMON테이블의 GROUP_CODE : S002참고
        /// </summary>	   
        public int SCHEDULE_TYPE { get; set; }
        /// <summary>       
        /// 요일(DATEPART-int형) 또는 날짜(yyyyMMdd)
        /// </summary>	   
        public string SCHEDULE_TYPE_DTL { get; set; }
        /// <summary>       
        /// 영업시작시간(24시간으로 표시 - 09:00-HH:mm)
        /// </summary>	   
        public string FR_TIME { get; set; }
        public string FR_TIME2 { get; set; }
        /// <summary>       
        /// 영업종료시간(24시간으로 표시 - 21:00-HH:mm)
        /// </summary>	   
        public string TO_TIME { get; set; }
        public string TO_TIME2 { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// T_COMMON 테이블의 MAIN_CODE:S003
        /// </summary>	   
        public int? STATUS { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        public string WEEK_NAME { get; set; }
        public string STATUS_NAME { get; set; }
        public string SCHEDULE_TYPE_NAME { get; set; }
    }
    #endregion >> 매장별 영업시간 설정(T_STORE_BUSINESSHOURS) END 

}
