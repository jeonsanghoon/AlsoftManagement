using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 매장별웹메뉴(T_WEBMENU)

    public class T_WEBMENU_COND
    {
        /// <summary>       
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>	   
        public int? PROJECT_SITE { get; set; }
        /// <summary>       
        /// 웹메뉴코드
        /// </summary>	   
        public int? MENU_CODE { get; set; }
        /// <summary>       
        /// 조회용 코드(순번을 2자리씩 상위코드 순으로 저장, 하위코드 조회시 해당 데이터 SEARCH_CODE + % 로 조회)
        /// </summary>	   
        public string SEARCH_CODE { get; set; }
        /// <summary>       
        /// 메뉴명(줄임말)
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 메뉴의깊이
        /// </summary>	   
        public int? LEVEL { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }

    }
    /// <summary>       
    /// 매장별웹메뉴(T_WEBMENU)
    /// </summary>	   
    public class T_WEBMENU
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>	   
        public int PROJECT_SITE { get; set; }
        /// <summary>       
        /// 웹메뉴코드
        /// </summary>	   
        public int MENU_CODE { get; set; }
        /// <summary>       
        /// 메뉴유형 T_COMMON : M001
        /// </summary>	   
        public int? MENU_TYPE { get; set; }
        /// <summary>
        /// 메뉴유형명 T_COMMON : M001
        /// </summary>
        public string MENU_TYPE_NAME { get; set; }
        /// <summary>       
        /// 메뉴권한(T_COMMON:U002)
        /// </summary>	   
        public int? MENU_AUTH { get; set; }
        /// <summary>
        /// 메뉴권한명(T_COMMON:U002)
        /// </summary>
        public string MENU_AUTH_NAME { get; set; }
        /// <summary>       
        /// 업체메뉴 => MENU_COMP_CODE에만 있을 경우 해당 업체 전체 메뉴로 할당, MENU_STORE_CODE에도 값이 있을 경우는 해당 지점메뉴로 등록
        /// </summary>	   
        public int? MENU_COMPANY_CODE { get; set; }
        /// <summary>
        /// 참조 업체명
        /// </summary>
        public string MENU_COMPANY_NAME { get; set; }

        /// <summary>       
        /// 지점메뉴 => MENU_COMP_CODE에만 있을 경우 해당 업체 전체 메뉴로 할당, MENU_STORE_CODE에도 값이 있을 경우는 해당 지점메뉴로 등록
        /// </summary>	   
        public int? MENU_STORE_CODE { get; set; }
        /// <summary>
        /// 참조 지점명
        /// </summary>
        public string MENU_STORE_NAME { get; set; }
        /// <summary>       
        /// 조회용 코드(순번을 2자리씩 상위코드 순으로 저장, 하위코드 조회시 해당 데이터 SEARCH_CODE + % 로 조회)
        /// </summary>	   
        public string SEARCH_CODE { get; set; }
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
        public string TEMPLEATE_PAGE { get; set; }
        /// <summary>       
        /// 메뉴아이콘 클래스(loggal Management에서 사용)
        /// </summary>	   
        public string MENU_CLASS { get; set; }

        /// <summary>
        /// 사용자메뉴권한 T_COMMON => U003
        /// </summary>
        public string USER_AUTH { get; set; }
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
        /// 수정자
        /// </summary>
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
    #endregion >> 매장별웹메뉴(T_WEBMENU) END 

}
