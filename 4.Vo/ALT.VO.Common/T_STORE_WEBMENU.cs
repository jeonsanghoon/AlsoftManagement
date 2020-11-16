using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 매장별웹메뉴(T_STORE_WEBMENU)
    /// <summary>       
    /// 매장별웹메뉴(T_STORE_WEBMENU)
    /// </summary>	   
    public class T_STORE_WEBMENU
    {

        public string MRC_EDIT_MODE { get; set; }
        public int ROW_NUM { get; set; }
        /// <summary>
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>
        public int? PROJECT_SITE { get; set; }
        /// <summary>       
        /// 매장코드
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 웹메뉴코드
        /// </summary>	   
        public int? MENU_CODE { get; set; }
        /// <summary>       
        /// 검색용코드:LEVEL단위로 2자리, 정렬,검색에사용,  LEVEL 순번 상위코드가 정해짐
        /// </summary>	   
        public string SEARCH_CODE { get; set; }
        /// <summary>       
        /// 메뉴유형 T_COMMON : M001
        /// </summary>	   
        public int? MENU_TYPE { get; set; }
        /// <summary>       
        /// 웹메뉴상위코드
        /// </summary>	   
        public int? PARENT_CODE { get; set; }
        /// <summary>       
        /// 메뉴의깊이
        /// </summary>	   
        public int? LEVEL { get; set; }
        /// <summary>       
        /// LEVEL별 웹메뉴의 순번
        /// </summary>	   
        public int? SEQ { get; set; }
        /// <summary>       
        /// 메뉴명(줄임말)
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 메뉴명(전체본명)
        /// </summary>	   
        public string FULL_NAME { get; set; }
        /// <summary>       
        /// 메뉴주소
        /// </summary>	   
        public string MENU_URL { get; set; }
        /// <summary>       
        /// 템플릿페이지명
        /// </summary>	   
        public string MENU_CLASS { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        public string UPDATE_NAME { get; set; }
    }
    #endregion >> 매장별웹메뉴(T_STORE_WEBMENU) END 

   

    public class LOGIN_WEBMENU
    { 
        /// <summary>
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>
        public int? PROJECT_SITE { get; set; }
        public int? STORE_CODE { get; set; }
        public int MEMBER_CODE { get; set; }
        public int MENU_CODE { get; set; }
        public string SEARCH_CODE { get; set; }
        public int? PARENT_CODE { get; set; }
        public int? LEVEL { get; set; }
        public string NAME { get; set; }
        public string MENU_CLASS { get; set; }
        public string MENU_URL { get; set; }
        public int MENU_TYPE { get; set; }
        public string TEMPLEATE_PAGE { get; set; }
        public bool INSERT_AUTH { get; set; }
        public bool UPDATE_AUTH { get; set; }
        public bool EXCEL_AUTH { get; set; }
        public bool PRINT_AUTH { get; set; }
        public bool? HIDE { get; set; }
    }
}
