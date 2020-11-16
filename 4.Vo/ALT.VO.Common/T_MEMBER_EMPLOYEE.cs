using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{

   

    #region >> 임직원테이블(T_MEMBER_EMPLOYEE)
    /// <summary>       
    /// 임직원테이블(T_MEMBER_EMPLOYEE)
    /// </summary>	   
    public class T_MEMBER_EMPLOYEE
    {
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 직원코드(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 매장코드(T_STORE_DEPT의 STORE_CODE => T_STORE의 STORE_CODE임)
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 부서코드(T_STORE_DEPT의 DEPT_CODE
        /// </summary>	   
        public int? DEPT_CODE { get; set; }
        /// <summary>       
        /// 상위부서코드
        /// </summary>	   
        public int? PARENT_MEMBER_CODE { get; set; }
        /// <summary>       
        /// 직급코드(T_STORE_POSITION테이블의 POSITION_TYPE : 1인 POSITION_CODE)
        /// </summary>	   
        public int? COMP_POSITION { get; set; }
        /// <summary>       
        /// 직책코드(T_STORE_POSITION테이블의 POSITION_TYPE : 2인 POSITION_CODE)
        /// </summary>	   
        public int? COMP_TITLE { get; set; }
        /// <summary>       
        /// 직원관리권한(T_COMMON의 MAIN_CODE:A002)
        /// </summary>	   
        public int? EMP_AUTH { get; set; }
        /// <summary>       
        /// 메뉴그룹(T_STORE_WEBMENU_GROUP의 GROUP_CODE)
        /// </summary>	   
        public int? MENU_GROUP { get; set; }
        /// <summary>       
        /// 메모
        /// </summary>	   
        public string MEMO { get; set; }
        /// <summary>       
		/// 텔레그램 채팅아이디(알람 보낼때 사용), 해당데이터 변경시 T_ALAM에 받는 사람 RECEIVE_MEMBER_CODE TELEGRAM_CHAT_ID 자동업데이트
		/// </summary>	   
		public string TELEGRAM_CHAT_ID { get; set; }
        /// <summary>       
		/// 직원관리권한 검색조건('|' 로 구분)
		/// </summary>	   
		public string STR_EMP_AUTH { get; set; }
    }
    #endregion >> 임직원테이블(T_MEMBER_EMPLOYEE) END 

    #region >> 직원정보-로그인시 사용
    /// <summary>       
    /// 직원정보-로그인시 사용
    /// </summary>	   
    public class EMPLOYEE_INFO
    {
        public int? PROJECT_SITE { get; set; }
        /// <summary>
        /// T_COMPANY 테이블의 COMPANY_CODE(회사코드0
        /// </summary>
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }

        /// <summary>
        /// 지점코드
        /// </summary>
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 지점명
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>
        /// 회원코드 T_MEBER테이블의 MEMBER_CODE    
        /// </summary>
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 사용자유형 T_COMMON 테이블의 MAIN_CODE:U001 사용
        /// </summary>	   
        public int? USER_TYPE { get; set; }
        /// <summary>
        /// 사용자유형 명
        /// </summary>
        public string USER_TYPE_NAME { get; set; }

        /// <summary>
        /// 사용자아이디
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 암호
        /// </summary>
        public string PASSWORD { get; set; }
        public string EX_PASSWORD { get; set; }
        /// <summary>
        /// 회원명
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>
        /// 이메일
        /// </summary>
        public string EMAIL { get; set; }
        /// <summary>
        /// 전화번호
        /// </summary>
        public string PHONE { get; set; }
        /// <summary>
        /// 휴대폰
        /// </summary>
        public string MOBILE { get; set; }
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
        /// 생년월일(yyyyMMdd)
        /// </summary>
        public string BIRTH { get; set; }
        /// <summary>
        /// T_COMMON 테이블의 MAIN_CODE : H001(1:남, 2:여) 사용 
        /// </summary>
        public int? GENDER { get; set; }
        /// <summary>
        /// 비밀번호변경 URL
        /// </summary>
        public string PASSWORD_CHANGE_URL { get; set; }
        /// <summary>
        /// 비밀번호 변경 유효시간
        /// </summary>
        public DateTime? PASSWORD_AUTH_TIME { get; set; }
        /// <summary>
        /// 부서코드
        /// </summary>
        public int? DEPT_CODE { get; set; }
        /// <summary>
        /// 부서명
        /// </summary>
        public string DEPT_NAME { get; set; }

        /// <summary>
        /// 조회용부서코드
        /// </summary>
        public string DEPT_SEARCH { get; set; }
        /// <summary>
        /// 상급자코드
        /// </summary>
        public int? PARENT_MEMBER_CODE { get; set; }
        /// <summary>
        /// 상급자이름
        /// </summary>
        public string PARENT_MEMBER_NAME { get; set; }
        /// <summary>
        /// 직급(T_STORE_POSITION 테이블과 Join- POSITION_CODE), POSITION_TYPE = 1
        /// </summary>
        public int? COMP_POSITION { get; set; }
        /// <summary>
        /// 직급명
        /// </summary>
        public string COMP_POSITION_NAME { get; set; }
        /// <summary>
        /// 직책(T_STORE_POSITION 테이블과 Join- POSITION_CODE), POSITION_TYPE = 2
        /// </summary>
        public int? COMP_TITLE { get; set; }
        /// <summary>
        /// 직책명
        /// </summary>
        public string COMP_TITLE_NAME { get; set; }
        /// <summary>
        /// 직원권한 T_COMMON 테이블의 A002코드
        /// 1	전체권한
        /// 2	상급부서권한
        /// 3	부서권한
        /// 8	상급자권한
        /// 9	본인권한
        /// </summary>
        public int? EMP_AUTH { get; set; }
        /// <summary>
        /// 직원권한명
        /// </summary>
        public string EMP_AUTH_NAME { get; set; }

        private int? _MAKER_MAX_COUNT = 20;
        /// <summary>
        /// 지도 마커최대등록갯수
        /// </summary>
        public int? MAKER_MAX_COUNT { get { return _MAKER_MAX_COUNT; } set { _MAKER_MAX_COUNT = value; } }
        /// <summary>
        /// 할당메뉴그룹(T_STORE_WEBMENU_GROUP와 JOIN)
        /// </summary>
        public int? MENU_GROUP { get; set; }
        /// <summary>
        /// 할당그룹명
        /// </summary>
        public string MENU_GROUP_NAME { get; set; }
        /// <summary>
        /// 메모
        /// </summary>
        public string MEMO { get; set; }
        /// <summary>       
        /// 텔레그램 채팅아이디(알람 보낼때 사용), 해당데이터 변경시 T_ALAM에 받는 사람 RECEIVE_MEMBER_CODE TELEGRAM_CHAT_ID 자동업데이트
        /// </summary>	   
        public string TELEGRAM_CHAT_ID { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool HIDE { get; set; }
        /// <summary>
        /// 비고
        /// </summary>
        public string REMARK { get; set; }
        public int? INSERT_CODE { get; set; }
        public string INSERT_NAME { get; set; }
        public DateTime? INSERT_DATE { get; set; }
        public int? UPDATE_CODE { get; set; }
        public string UPDATE_NAME { get; set; }

        public int AD_CNT { get; set; }
        public int DEVICE_CNT { get; set; }
        public int CONTACT_AD_CNT { get; set; }
        public int CONTACT_DEVICE_CNT { get; set; }

        public DateTime? UPDATE_DATE { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> (TMP_EMPLOYEE_INFO) END 

    /// <summary>
    /// 조회권한
    /// </summary>
    public class EMPLOYEE_SEARCH_AUTH
    {
        /// <summary>
        /// 회사전체권한
        /// </summary>
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 지점권한
        /// </summary>
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 상위부서권한
        /// </summary>
        public string DEPT_SEARCH { get; set; }
        /// <summary>
        /// 부서권한
        /// </summary>
        public int? DEPT_CODE { get; set; }
        /// <summary>
        /// 상급자권한
        /// </summary>
        public int? PARENT_MEMBER_CODE { get; set; }
        /// <summary>
        /// 본인권한
        /// </summary>
        public int? MEMBER_CODE { get; set; }
    }

    public class EMPLOYEE_INFO_COND
    {

        /// <summary>
        /// 프로젝트사이트(T_COMMON : MAIN_CODE = P001)
        /// </summary>
        public int? PROJECT_SITE { get; set; }
        /// <summary>
        /// 회원코드 T_MEBER테이블의 MEMBER_CODE    
        /// </summary>
        public int? MEMBER_CODE { get; set; }
        /// <summary>
        /// 지점코드
        /// </summary>
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 부서코드
        /// </summary>
        public int? DEPT_CODE { get; set; }
        /// <summary>
        /// 직책(T_STORE_POSITION 테이블과 Join- POSITION_CODE), POSITION_TYPE = 2
        /// </summary>
        public int? COMP_TITLE { get; set; }
    }
    public class EMPLOYEE_RESPONSE : COMMON_RESPONSE
    {
        public int? MEMBER_CODE { get; set; }
    }
   
    public class EMPLOYEE_COND
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
        public int? COMPANY_CODE { get; set; }
        public int? STORE_CODE { get; set; }
        public int? PARENT_MEMBER_CODE { get; set; }
        public int? MEMBER_CODE { get; set;}
        public string MOBILE { get; set; }
        public string USER_NAME { get; set; }
        /// <summary>
        /// 권한 2:전체권한 3: 부서권한 8:상급자권한 9:본인권한
        /// </summary>
        public string EMP_AUTH { get; set; }
        
        public int? DEPT_CODE { get; set; }
        public string DEPT_SEARCH { get; set; }
        public int? COMP_POSITION { get; set; }
        public int? COMP_TITLE { get; set; }
        public string FR_BIRTH { get; set; }
        public string TO_BIRTH { get; set; }
        public string STR_EMP_AUTH { get; set; }
        public bool? HIDE { get; set; }
    }


    public class EMPLOYEE_P_DATA
    {
        public long? SEQ { get; set; }
        public int? COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public int? MEMBER_CODE { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string MOBILE { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }
    }

}
