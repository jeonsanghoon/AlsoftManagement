using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    public class T_DEVICE_AUTH_NUMBER_COND
    {
        public int AUTH_TYPE { get; set; }
        /// <summary>
        /// 로컬박스번호
        /// </summary>
        public string DEVICE_NUMBER { get; set; }
    }

    public class T_DEVICE_COND
    {
        /// <summary>
        /// 매장별 그룹코드 T_STORE_GROUP 테이블의 GROUP_CODE
        /// </summary>
        public int? GROUP_CODE { get; set; }
        /// <summary>       
        /// 단말기번호(일련번호), 기본키
        /// </summary>	   
        public long? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스번호
        /// </summary>
        public string DEVICE_NUMBER { get; set; }
        /// <summary>
        /// 로컬박스인증번호
        /// </summary>
        public long? AUTH_NUMBER { get; set; }
        /// <summary>       
        /// 매장코드 T_STORE 테이블 참조, NULL일경우 매장에 종속 된것이 아님
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 회사코드 T_COMPANY테이블의 COMPANY_CODE
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 장비명
        /// </summary>	   
        public string DEVICE_NAME { get; set; }
        public string SAVE_MODE { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        /// <summary>
        /// 인증 유형(2:로컬박스 3:로컬사인) T_COMMON 테이블 L003 참조
        /// </summary>
        public int? AUTH_TYPE { get; set; }
        /// <summary>
        /// 로컬사이니지 (T_SIGNAGE) 테이블의 SIGN_CODE 참조
        /// </summary>
        public long? SIGN_CODE { get; set; }
        
    }
    #region >> 로컬단말기기정보(T_DEVICE)
    /// <summary>       
    /// 로컬단말기기정보(T_DEVICE)
    /// </summary>	   
    public class T_DEVICE
    {

        /// <summary>
        /// 저장모드 N:추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_TYPE { get; set; }
        /// <summary>       
        /// 단말기번호(일련번호), 기본키
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스 스테이션코드(T_DEVICE_STATION 테이블)
        /// </summary>
        public int? STATION_CODE { get; set; }
        /// <summary>
        /// 등록된 하드웨어(기기) T_HARDWARE
        /// </summary>
        public int? HARDWARE_CODE { get; set; }
        /// <summary>
        /// 배너종류 D003 1:내부배너, 2: 외부배너, 3: 내부+외부
        /// </summary>
        public int? DEVICE_TYPE { get; set; }
        /// <summary>
        /// 로컬박스 스테이션명(T_DEVICE_STATION 테이블)
        /// </summary>
        public string STATION_NAME { get; set; }
        /// <summary>
        /// 사업자 업체코드
        /// </summary>
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 사업자 지점코드
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>       
        /// 매장코드 T_STORE 테이블 참조, NULL일경우 매장에 종속 된것이 아님
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 지점(매장)명
        /// </summary>
        public string STORE_NAME { get; set; }
        public int? MEMBER_CODE { get; set; }
        public string MEMBER_NAME { get; set; }
        public string MOBILE { get; set; }
        /// <summary>       
        /// 장비번호(MAC_ADDRESS)
        /// </summary>	   
        public string DEVICE_NUMBER { get; set; }
        /// <summary>       
        /// 인증번호 : T_DEVICE_AUTH_NUMBER 테이블의 AUTH_NUMBER
        /// </summary>	   
        public Int64? AUTH_NUMBER { get; set; }
        /// <summary>       
        /// 장비명
        /// </summary>	   
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 그룹코드(지점별) T_STORE_GROUP의 GROUP_CODE
        /// </summary>
        public int? GROUP_CODE { get; set; }
        /// <summary>       
        /// 장비설명
        /// </summary>	   
        public string DEVICE_DESC { get; set; }
        /// <summary>       
        /// 영리여부(0:영리, 1:비영리) T_COMMON의 MAIN_CODE : B001
        /// </summary>	   
        public int? BUSI_TYPE { get; set; }
        /// <summary>       
        /// 사업자여부(0:일반, 1:사업자) T_COMMON의 MAIN_CODE : B002
        /// </summary>	   
        public int? BUSI_TYPE2 { get; set; }
        /// <summary>       
        /// 주소
        /// </summary>	   
        public string ADDRESS1 { get; set; }
        /// <summary>       
        /// 상세주소
        /// </summary>	   
        public string ADDRESS2 { get; set; }
        /// <summary>       
        /// 우편번호
        /// </summary>	   
        public string ZIP_CODE { get; set; }
        /// <summary>       
        /// 위도
        /// </summary>	   
        public decimal? LATITUDE { get; set; }
        /// <summary>       
        /// 경도
        /// </summary>	   
        public decimal? LONGITUDE { get; set; }
        /// <summary>       
        /// 담당업체명
        /// </summary>	   
        public int? CONTACT_COMPANY_CODE { get; set; }
        /// <summary>       
        /// 담당업체명
        /// </summary>	   
        public string CONTACT_COMPANY_NAME { get; set; }
        /// <summary>       
        /// 담당지점
        /// </summary>	   
        public int? CONTACT_STORE_CODE { get; set; }
        /// <summary>       
        /// 담당지점
        /// </summary>	   
        public string CONTACT_STORE_NAME { get; set; }
        /// <summary>       
        /// 담당자코드(T_MEMBER 테이블의 MEMBER_CODE)
        /// </summary>	   
        public int? CONTACT_CODE { get; set; }
        /// <summary>       
        /// 담당자이름
        /// </summary>	   
        public string CONTACT_NAME { get; set; }
        /// <summary>       
        /// 담당자전화
        /// </summary>	   
        public string CONTACT_PHONE { get; set; }
        /// <summary>       
        /// 담당자이메일
        /// </summary>	   
        public string CONTACT_EMAIL { get; set; }
        /// <summary>
        /// 담당자연락처
        /// </summary>
        public string CONTACT_MOBILE { get; set; }
        /// <summary>
        /// 로컬박스에 표시할 배너 카테고리
        /// </summary>
        public string CATEGORY_CODES { get; set; }
        /// <summary>
        /// 로컬박스에 표시할 로컬박스에서 배너까지의 거리 기본 3km(3000m) 등록단위 m
        /// </summary>
        public int? AD_DISTANCE { get; set; }
        /// <summary>
        /// 데이터수신주기- 등록 초단위 기본 1시간 3600초
        /// </summary>
        public int? DATA_CYCLE_TIME { get; set; }
        /// <summary>
        /// 광고카테고리변경주기(초단위등록) 기본 3분 180
        /// </summary>
        public int? AD_CYCLE_TIME { get; set; }
        /// <summary>
        /// 모바일 배너 화면분할(T_COMMON => MAIN_CODE: H002)
        /// </summary>
        public int? AD_FRAME_TYPE { get; set; }
        /// <summary>
        /// 페이지대기시간 기본 1분(초단위 등록)
        /// </summary>
        public int? PAGE_WAITING_TIME { get; set; }
        /// <summary>       
        /// 로컬박스상태(T_COMMON : S006)
        /// </summary>	   
        public int? STATUS { get; set; }
        /// <summary>       
        /// 숨김여부(0:보임 1숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        /// 기계작동시간
        /// </summary>
        public DateTime? WORKING_TIME { get; set; }
        /// <summary>       
        /// UTC 기준 시간(시간단위 30분은 0.5로 표현)
        /// </summary>	   
        public decimal? TIME_ZONE { get; set; }
        
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
        public List<KEYWORD_DATA> KEYWORDS { get; set; }
        /// <summary>
        /// 내배너갯수
        /// </summary>
        public int MAIN_CNT { get; set; }
        /// <summary>
        /// 가상역역설정 갯수
        /// </summary>
        public int PLACE_CNT { get; set; }

        
        public string HARDWARE_NAME { get; set; }
        public string MDDEL_NAME { get; set; }
        public string LOGO_URL { get; set; }

        public List<T_DEVICE_PLACE> devicePlaceList { get; set; }

		/// <summary>
		/// 모바일 공개
		/// </summary>
		public bool? IS_MOBILE { get; set; }
		public int? ITEM_TYPE { get; set; }
		/// <summary>
		/// 대표/서브 로컬 박스 코드
		/// </summary>
		public long? PARENT_DEVICE_CODE { get; set; }
		public long? SELECT_DEVICE_CODE { get; set; }
		public long? CUR_DEVICE_CODE { get; set; }
		public string JIBUN_ADDRESS { get; set; }
		public int? TOTAL_ROWCOUNT { get; set; }
	}
    #endregion >> 로컬단말기기정보(T_DEVICE) END 

    public class STEP_LOCAL_COND
    {
        public Int64? AD_CODE { get; set; }
        public string SEARCH_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
    }
    public class STEP_LOCAL_LIST
    {
        public Int64? AD_DEVICE_CODE { get; set; }
        public Int64 DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public string STORE_NAME { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string PHONE { get; set; }
        public string EMAIL { get; set; }
        public int TOTAL_ROWCOUNT { get; set; } 
    }

    public class STEP4_SAVE
    {
        public string SAVE_TYPE { get; set; }
        public long? AD_CODE { get; set; }
        public List<int> DEVICE_CODE { get; set; }
        public List<int> AD_DEVICE_CODE { get; set; }
        int? _timezoneOffset = 9;
        public int? TIMEZONE_OFFSET { get { return _timezoneOffset == null ? 9 : _timezoneOffset; } set { _timezoneOffset = value; } }
        public int? REG_CODE { get; set; }
        /// <summary>
        /// 승인상태 T_COMMON테이블( MAIN_CODE : A009) 1:요청 2:반려 9:승인
        /// </summary>
        public int? STATUS { get; set; }
    }

    public class STEP5_SAVE {
        public long? AD_CODE { get; set; }
        public int? REG_CODE { get; set; }
    }

    public class STEPLIST_COND
    {
        public int? MEMBER_CODE { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string TITLE { get; set; }
    }

    public class STEPLIST
    {
        public long AD_CODE { get; set; }
        public string LOGO_URL { get; set; }
        public string TITLE { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        /// <summary>
        /// T_COMMON : A001
        /// </summary>
        public int STATUS { get; set; }
        public string STATUS_NAME { get; set; }

        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005)
        /// </summary>
        public int? BANNER_TYPE { get; set; }

        /// <summary>
        /// 광고배너표시유형
        /// </summary>
        public string BANNER_TYPE_NAME { get; set; }
        public string STATUS_PAGE { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }

    /// <summary>
    /// 로컬박스조회 클래스
    /// </summary>
    public class DEVICE_LIST
    {

        public long? DEVICE_CODE { get; set; }
        public int? DEVICE_TYPE { get; set; }
		public string DEVICE_TYPE_NAME { get; set; }
        public int? STATION_CODE { get; set; }
        public string STATION_NAME { get; set; }
        public int? GROUP_CODE { get; set; }
        public string GROUP_NAME { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public string DEVICE_NAME { get; set; }
        public string BUSI_TYPE_NAME { get; set; }
        public string BUSI_TYPE_NAME2 { get; set; }
        public string STATUS_NAME { get; set; }
        public int? CONTACT_CODE { get; set; }
        public string CONTACT_NAME { get; set; }
        public string CONTACT_PHONE { get; set; }
        public long? AUTH_NUMBER { get; set; }
        public string ADDRESS { get; set; }
        public int? RADIUS { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        public string JIBUN_ADDRESS { get; set; }
        public int? AD_FRAME_TYPE { get; set; }
        public string AD_FRAME_TYPE_NAME { get; set; }
        /// <summary>
        /// 최종작동시간과 현재시간 차이(분단위)
        /// </summary>
        public int WORKING_DIFF { get; set; }
        public DateTime? WORKING_TIME { get; set; }
        public string CATEGORY_CODES { get; set; }
        public int? HARDWARE_CODE { get; set; }
        public string HARDWARE_NAME { get; set; }
        public string MODEL_NAME { get; set; }
        public int PLACE_CNT { get; set; }
        public int MAIN_CNT { get; set; }
        public string LOGO_URL { get; set; }
        public string UPDATE_NAME { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
		public string STATUS { get; set; }
		public long? PARENT_DEVICE_CODE { get; set; }
		public bool? IS_MOBILE { get; set; }
		public string VIRTUAL_DEVICE_NAME { get; set; }
	}

    public class DEVICE_LIST_COND {
        public long? DEVICE_CODE { get; set; }
		public int? STATION_CODE { get; set; }
        public string STATION_NAME { get; set; }
        public int? COMPANY_CODE { get; set; }
        public int? GROUP_CODE { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public string DEVICE_NAME { get; set; }
        public string STORE_DEVICE_NAME { get; set; }
        public int? BUSI_TYPE { get; set; }
        public int? BUSI_TYPE2 { get; set; }
        public int? AUTH_YN { get; set; }
        public int? STATUS { get; set; }
      
        public string SEARCH_CATEGORY_CODE { get; set; }

        public string CONTACT_DEPT_SEARCH { get; set; }
        public int? CONTACT_DEPT_CODE { get; set; }
        public int? CONTACT_PARENT_MEMBER_CODE { get; set; }
        public int? CONTACT_CODE { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT { get; set; }

        public int? AD_CODE { get; set; }
		/// <summary>
		/// 내배너 코드
		/// </summary>
		public int? MY_AD_CODE { get; set; }
        public int? MY_STORE_CODE { get; set; }


        private string _disPlayMode = "Normal";
        /// <summary>
        /// Normal, Popup, Total
        /// </summary>
        public string DISPLAY_MODE { get { return _disPlayMode; } set { _disPlayMode = (value == null || value == "" ? _disPlayMode : value); }  }

        public long? NOT_DEVICE_CODE { get; set; }
        public long? NOT_AD_CODE { get; set; }
		public long? NOT_MY_AD_CODE { get; set; }
		public bool? HIDE { get; set; }

        public bool? IS_VIRTUAL_DEVICE { get; set; }
     
        /// <summary>
        /// 위도
        /// </summary>
        public decimal? LATITUDE { get; set; }
        /// <summary>
        /// 경도
        /// </summary>
        public decimal? LONGITUDE { get; set; }
        /// <summary>
        /// 광고가 포함된 로컬박스를 가져오는 광고코드
        /// </summary>
        public long? DEVICE_CONTAINING_AD_CODE { get; set; }

        public int? MEMBER_CODE { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        /// <summary>
        /// 배너 프레임 유형
        /// </summary>
        public int? AD_FRAME_TYPE { get; set; }
        public int? REAL_DEVICE { get; set; }
		/// <summary>
		/// 대표 로컬박스 코드
		/// </summary>
		public long? PARENT_DEVICE_CODE { get; set; }
		/// <summary>
		/// 대표/서브 구분
		/// </summary>
		public bool? RELATIVE { get; set; }
		public bool? IS_MOBILE { get; set; }
		public string VIRTUAL_DEVICE_NAME { get; set; }

	}



	/// <summary>
	/// 로컬 메인 광고 리스트
	/// </summary>
	public class LOGGAL_MAIN_CONTENTLIST
    {
        /// <summary>
        /// 로컬박스코드 T_DEVICE 테이블 참조
        /// </summary>
        public long DEVICE_CODE { get; set; }
        /// <summary>
        /// Display 순번
        /// </summary>
        public int SEQ { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get;set;}
        /// <summary>
        /// 부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005)
        /// </summary>
        public int BANNER_TYPE { get; set; }
        /// <summary>
        /// 콘텐츠 유형 0: 이미지 1:Html
        /// </summary>
        public int CONTENT_TYPE { get; set; }
        /// <summary>
        /// 내용
        /// </summary>
        public string CONTENT { get; set; }

        public string CONTENT_DETAIL_URL { get; set; }

        /// <summary>
        /// 최종업데이트시간
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }
    }

    public class DEVICE_LOCATION_COND
    {
        public string MODE { get; set; }
        public int? STATION_CODE { get; set; }
        public int? COMPANY_CODE { get; set; }
        public string SEARCH_CODE { get; set; }
        
        /// <summary>
        /// 위도(검색위치 기준:현위치)
        /// </summary>
        public decimal? LATITUDE { get; set; }
        /// <summary>
        /// 경도(검색위치 기준:현위치)
        /// </summary>
        public decimal? LONGITUDE { get; set; }
        /// <summary>
        /// AES256으로 암호화된 위도
        /// </summary>
        public String SEARCH_LAT;
        /// <summary>
        /// AES256으로 암호화된 경도
        /// </summary>
        public String SEARCH_LONG;
        /// <summary>
        /// 거리 (m 기준)
        /// </summary>
        public decimal? DISTANCE { get; set; }
        public string LOCATION_NAME { get; set; }

        /// <summary>
        /// 로컬박스명 또는 소유주로 검색 %
        /// </summary>
        public string SEARCH_TEXT { get; set; }
        /// <summary>
        /// 모바일 로그인 아이디
        /// </summary>
        public string USER_ID { get; set; }
        public string SORT { get; set; }

        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
    } 

    public class DEVICE_LOCATION
    {
        
        public string SEARCH_CODE { get; set; }
        public string LOCATION_NAME { get; set; }
        public long? DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }
        public int? STATION_CODE { get; set; }
        public long? SIGN_CODE { get; set; }
        public string DEVICE_DESC { get; set; }
        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public string ZIP_CODE { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        public long? AUTH_NUMBER { get; set; }
        public string COMPANY_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string CONTACT_COMPANY_NAME { get; set; }
        public string CONTACT_NAME { get; set; }
        /// <summary>
        /// 검색기준 위치로부터 거리
        /// </summary>
        public decimal? DISTANCE { get; set; }
        public bool BOOKMARK_YN { get; set; }
        public bool FAVORITE_YN { get; set; }
        public String LOGO_URL { get; set; }
    }

    public class AD_DEVICE_SHARE
    {
        public long DEVICE_CODE { get; set; }
        public long AD_CODE { get; set; }
        public int? TIMEZONE_OFFSET { get; set; } = 9;
		public string COMMENT { get; set; }
    }

}
