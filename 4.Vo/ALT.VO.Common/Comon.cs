using ALT.Framework.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{

    public enum enEditMode
    {
        READ, WRITE
    }

	public enum enLoginAuth
	{ 
		Admin, Owner
	}
 
    /// <summary>
    /// 로그인정보
    /// </summary>
    public class LOGIN_INFO
    {
        public T_MEMBER MEMBER { get; set; }
      //  public T_COMPANY COMPANY { get; set; }
        public T_STORE STORE { get; set; }
        public IList<EMPLOYEE_INFO> EMPLOYEE_LIST { get; set; }
        List<LOGIN_WEBMENU> _webMenu;
        public List<LOGIN_WEBMENU> WebMemu { get { return _webMenu == null ? new List<LOGIN_WEBMENU>() : _webMenu; } set { _webMenu = value; } }
        public string LANGUAGE { get; set; }
        public string COMPANY_ID { get; set; }
        
        public List<T_STORE_IMAGE> StoreImageList { get; set; }

        public CULTURE_INFO CultureInfo { get; set; }

        private string _baseUrl = string.Empty;
        public string BASE_URL { set { _baseUrl = value; } get { return ("/" + COMPANY_ID + "/" + LANGUAGE).ToLower(); } }
        public EMPLOYEE_INFO EMPLOYEE { get; set; }
        /// <summary>
        /// 조회권한
        /// </summary>
        public EMPLOYEE_SEARCH_AUTH EMPLOYEE_SEARCH_AUTH { get; set; }
        SHOPPING_CART _cart;
        public SHOPPING_CART SHOPPING_CART { get { return ((_cart == null) ? new SHOPPING_CART() : _cart); } set { _cart = value; } }

        public DateTime _settingTime = DateTime.Now;
        public DateTime SETTING_TIME { get { return _settingTime; } set { _settingTime = value; } }
        public long? AD_CODE { get; set; }

        public int? _companyCode = 1;
        public int? COMPANY_CODE { get { return _companyCode; } set { _companyCode = value; } }
        public int? _storeCode = 1;
        public int? STORE_CODE { get { return _storeCode; } set { _storeCode = value; } }

        public enEditMode _EDIT_MODE = enEditMode.WRITE;
        public enEditMode EDIT_MODE { get { return _EDIT_MODE; } set { _EDIT_MODE = value; } }
        /// <summary>
        /// 현재메뉴
        /// </summary>
        public string CURRENT_MENU_NAME { get; set; }
	}
    /// <summary>
    /// 스케쥴러정보
    /// </summary>
    public class SCHEDULER_INFO
    {
        public string MONGODB_BASE_DATE { get; set; }
        public DateTime MONGODB_APPLY_TIME { get; set; }
    }

    public class LOGIN_NONCHECK_PAGE
    {
        public List<string> PAGE { get; set; }
        public List<string> ControllerNames { get; set; }
        
    }


    #region >> 공통코드 테이블(항상 첫번째 데이터는 SUB_CODE가 *이고 필드 설명이 들어감)(T_COMMON)

    public class T_COMMON_COND
    {
        /// <summary>
        /// 페이지
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 조회 Row 건수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 정렬
        /// </summary>
        public string SORT { get; set; }
        /// <summary>       
        /// 메인코드
        /// </summary>	   
        public string MAIN_CODE { get; set; }
        /// <summary>       
        /// 서브코드
        /// </summary>	   
        public int? SUB_CODE { get; set; }
        /// <summary>       
        /// 이름
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 언어코드 T_LANGUAGE테이블의 LANGUAGE_CODE
        /// </summary>	   
        public Int64? LANGUAGE_CODE { get; set; }
        /// <summary>       
        /// 정렬순번
        /// </summary>	   
        public int? ORDER_SEQ { get; set; }
        /// <summary>       
        /// 참조코드1
        /// </summary>	   
        public string REF_DATA1 { get; set; }
        /// <summary>       
        /// 참조코드2
        /// </summary>	   
        public string REF_DATA2 { get; set; }
        /// <summary>       
        /// 참조코드3
        /// </summary>	   
        public string REF_DATA3 { get; set; }
        /// <summary>       
        /// 참조코드4
        /// </summary>	   
        public string REF_DATA4 { get; set; }
        /// <summary>       
        /// 숨김여부
        /// </summary>	   
        public bool? HIDE { get; set; }
        public string ADD_COND { get; set; }
        public string selectedValue { get; set; }
        public string color { get; set; }
        
    }
    /// <summary>       
    /// 공통코드 테이블(항상 첫번째 데이터는 SUB_CODE가 *이고 필드 설명이 들어감)(T_COMMON)
    /// </summary>	   
    public class T_COMMON
    {
        public string MRC_EDIT_MODE { get; set; }
        /// <summary>       
        /// 일련번호(자동순번)
        /// </summary>	   
        public int? IDX { get; set; }
        /// <summary>       
        /// 메인코드
        /// </summary>	   
        public string MAIN_CODE { get; set; }
        /// <summary>       
        /// 서브코드
        /// </summary>	   
        public int? SUB_CODE { get; set; }
        /// <summary>       
        /// 이름
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 언어코드 T_LANGUAGE테이블의 LANGUAGE_CODE
        /// </summary>	   
        public Int64? LANGUAGE_CODE { get; set; }
        /// <summary>       
        /// 정렬순번
        /// </summary>	   
        public int? ORDER_SEQ { get; set; }
        /// <summary>       
        /// 참조코드1
        /// </summary>	   
        public string REF_DATA1 { get; set; }
        /// <summary>       
        /// 참조코드2
        /// </summary>	   
        public string REF_DATA2 { get; set; }
        /// <summary>       
        /// 참조코드3
        /// </summary>	   
        public string REF_DATA3 { get; set; }
        /// <summary>       
        /// 참조코드4
        /// </summary>	   
        public string REF_DATA4 { get; set; }
        /// <summary>       
        /// 숨김여부
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_COE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>
        /// 등록자명
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
        /// 수정자명
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        public int? TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 공통코드 테이블(항상 첫번째 데이터는 SUB_CODE가 *이고 필드 설명이 들어감)(T_COMMON) END 

    /// <summary>
    /// 리턴 메세지 데이터
    /// </summary>
    public class RTN_SAVE_DATA
    {
        public string DATA { get; set; } = string.Empty;
        public string DATA2 { get; set; } = string.Empty;
        public string FOCUS_TAG_ID { get; set; }
        string _errorMessage = string.Empty;
        public string ERROR_MESSAGE { get { return string.IsNullOrEmpty(_errorMessage) ? "" : _errorMessage; } set { _errorMessage = value; } }
        public string MESSAGE { get; set; } = string.Empty;
        public string RETURN_URL { get; set; } = string.Empty;
    }

    /// <summary>
    /// 공통 RESPONSE 객체
    /// </summary>
    public class COMMON_RESPONSE
    {
        public string RESPONSE_CODE { get; set; }
        public string RESPONSE_MSG { get; set; }
        public string RESULT_VALUE { get; set; }
    }

    public class PAGE_PARAM
    {
        public PAGE_PARAM()
        {
            PAGE = 1;
            TOTAL = 1;
            PAGE_SIZE = 10;
            PER_PAGE = 15;
            SORT_COLUMN = "";
            SORT_ORDER = "asc";
        }
        public int? PAGE { get; set; } //현재 페이지
        //{
        //    get { return this.PAGE == null ? 1 : this.PAGE; }
        //    set { this.PAGE = value; }
        //}       
        public int? TOTAL { get; set; } //전체 건수
        //{
        //    get { return this.TOTAL == null ? 1 : this.TOTAL; }
        //    set { this.TOTAL = value; }
        //}      
        public int? PAGE_SIZE { get; set; } //pagination에 표시될 페이지 수
        //{
        //    get { return this.PAGE_SIZE == null ? 10 : this.PAGE_SIZE; }
        //    set { this.PAGE_SIZE = value; }
        //} 
        public int? PER_PAGE { get; set; } //페이지당 출력수
        //{
        //    get { return this.PER_PAGE == null ? 15 : this.PER_PAGE; }
        //    set { this.PER_PAGE = value; }
        //}
        public string SORT_COLUMN { get; set; } //sorting할 컬럼
        public string SORT_ORDER { get; set; }  //asc, desc
    }

    public class SEARCH_COND
    {
        public int? PAGE { get; set; } //현재 페이지
        public int? PAGE_COUNT { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        public string SEARCH_DATA { get; set; }
        public string SEARCH_DATA1 { get; set; }
        public string SEARCH_DATA2 { get; set; }
        public string SEARCH_DATA3 { get; set; }

        public string SEARCH_DATA4 { get; set; }
        public string SEARCH_DATA5 { get; set; }
        public string SEARCH_DATA6 { get; set; }
        public string SEARCH_DATA7 { get; set; }
        public string SEARCH_DATA8 { get; set; }
        public string SEARCH_DATA9 { get; set; }
        public string SEARCH_DATA10 { get; set; }
        public string SEARCH_DATA11 { get; set; }
        public string SEARCH_DATA12 { get; set; }
        public bool? HIDE { get; set; }

        public string SORT { get; set; }
    }

}

