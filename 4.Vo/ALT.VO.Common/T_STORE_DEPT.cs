using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 직급/직책테이블(T_STORE_POSITION)
    /// <summary>       
    /// 직급/직책테이블(T_STORE_POSITION)
    /// </summary>	   
    public class T_STORE_POSITION
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>
        /// 기본키(자동증가)
        /// </summary>
        public int? POSITION_CODE { get; set; }
        /// <summary>       
        /// 매장코드(T_STORE 테이블의 STORE_CODE)
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 유형(T_COMMON테이블의 MAIN_CODE : A003 => 1:직급, 2:직책)
        /// </summary>	   
        public int? POSITION_TYPE { get; set; }
        /// <summary>
        /// 유형명
        /// </summary>
        public string POSITION_TYPE_NAME { get; set; }
        /// <summary>       
        /// 등급
        /// </summary>	   
        public int SEQ { get; set; }
        /// <summary>       
        /// 이름
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 숨김여부(0:보임 1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
		/// <summary>
		/// 수정자명
		/// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 직급/직책테이블(T_STORE_POSITION) END 

    #region >> 매장별부서테이블(T_STORE_DEPT)
    /// <summary>       
    /// 매장별부서테이블(T_STORE_DEPT)
    /// </summary>	   
    public class T_STORE_DEPT
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// 매장코드(T_STORE 테이블의 STORE_CODE)
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 부서코드(매장별 1부터 순차적으로 증가)
        /// </summary>	   
        public int? DEPT_CODE { get; set; }
        /// <summary>       
        /// 상위부서코드
        /// </summary>	   
        public int? PARENT_DEPT_CODE { get; set; }
        /// <summary>
        /// 상위부서명
        /// </summary>
        public string PARENT_DEPT_NAME { get; set; }
        /// <summary>       
        /// 부서명
        /// </summary>	   
        public string DEPT_NAME { get; set; }
        public int? LEVEL { get; set; }
        /// <summary>       
        /// 2자리씩 유일키, 상위코드 검색시 len-2 = DEPT_SEARCH, 하위 검색시 DEPT_SEARCH LIKE '해당코드%'
        /// </summary>	   
        public string DEPT_SEARCH { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool? HIDE { get; set; }
        public string HIDE_NAME { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
        /// </summary>
        public string INSERT_NMAE { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 매장별부서테이블(T_STORE_DEPT) END 
}
