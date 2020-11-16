using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 웹메뉴 사용자별 메뉴권한 테이블(T_STORE_WEBMENU_EMPLOYEE_MENU)
    /// <summary>       
    /// 웹메뉴 사용자별 메뉴권한 테이블(T_STORE_WEBMENU_EMPLOYEE_MENU)
    /// </summary>	   
    public class T_STORE_WEBMENU_EMPLOYEE_MENU
    {
        /// <summary>
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>
        public int? PROJECT_SITE { get; set; }
        /// <summary>       
        /// 매장(사업장)코드 T_STORE 테이블의 STORE_CODE
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 메뉴그룹코드=>T_MEMBER 테이블의 직원(USER_TYPE이 5)인것중 MEMBER_CODE
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 메뉴코드(T_STORE_WEBMENU테이블)
        /// </summary>	   
        public int? MENU_CODE { get; set; }
        /// <summary>       
        /// 등록권한 여부 1:있음 0 없음
        /// </summary>	   
        public bool? INSERT_AUTH { get; set; }
        /// <summary>       
        /// 수정권한 여부 1:있음 0 없음
        /// </summary>	   
        public bool? UPDATE_AUTH { get; set; }
        /// <summary>       
        /// 엑셀다운로드권한 여부 1:있음 0 없음
        /// </summary>	   
        public bool? EXCEL_AUTH { get; set; }
        /// <summary>       
        /// 출력여부
        /// </summary>	   
        public bool? PRINT_AUTH { get; set; }
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
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
        public string SEARCH_CODE { get; set; }
        public string MENU_NAME { get; set; }
        public string MENU_URL { get; set; }
        public string UPDATE_NAME { get; set; }
    }
    #endregion >> 웹메뉴 사용자별 메뉴권한 테이블(T_STORE_WEBMENU_EMPLOYEE_MENU) END 
}
