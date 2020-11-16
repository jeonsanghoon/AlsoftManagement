using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 지점별 그룹관리(T_STORE_GROUP)
    /// <summary>       
    /// 지점별 그룹관리(T_STORE_GROUP)
    /// </summary>	   
    public class T_STORE_GROUP
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// 기본키
        /// </summary>	   
        public int? GROUP_CODE { get; set; }
        /// <summary>
        /// 지점명 : T_STORE테이블의 STORE_CODE
        /// </summary>
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 그룹유형 : T_COMMON(B007) 1:광고 2:로컬박스
        /// </summary>	   
        public int? GROUP_TYPE { get; set; }
        /// <summary>       
        /// 그룹명
        /// </summary>	   
        public string GROUP_NAME { get; set; }
        /// <summary>
        /// 순번
        /// </summary>
        public int SEQ { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
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
    #endregion >> 지점별 그룹관리(T_STORE_GROUP) END 
}

