using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
	#region >> 광고 검색 조회 조건 - AD_SEARCH_COND
	/// <summary>
	/// 광고 검색 조회 조건 - AD_SEARCH_COND
	/// </summary>
	public class AD_SEARCH_COND
	{
		/// <summary>
		/// 카테고리 멀티 선택시 사용, 검색요청시 5자리로 만듦, 코드가 240 일경우 요청코드 : 00240
		/// </summary>
		public string[] SEARCH_CATEGORY_CODE { get; set; }
		/// <summary>
		/// 경도(클라이언트단말기 위치정보) >> 값이 있으면 조건에추가 dbo.FN_TO_DISTANCE(@LATITUDE, @LONGITUDE, C.LATITUDE, C.LONGITUDE,'KM') <= @RADIUS
		/// </summary>
		public decimal? LATITUDE { get; set; }
		/// <summary>
		/// 위도(클라이언트단말기 위치정보) >> 값이 있으면 조건에추가 dbo.FN_TO_DISTANCE(@LATITUDE, @LONGITUDE, C.LATITUDE, C.LONGITUDE,'KM') <= @RADIUS
		/// </summary>
		public decimal? LONGITUDE { get; set; }

		/// <summary>
		/// AES256으로 암호화된 위도
		/// </summary>
		public string SEARCH_LAT;
		/// <summary>
		/// AES256으로 암호화된 경도
		/// </summary>
		public string SEARCH_LONG;
		/// <summary>
		/// 반경(km) : 클라이언트 단말기로 부터 검색 반경
		/// </summary>
		public int? RADIUS { get; set; }
		/// <summary>
		/// 키워드코드 >> 값이 있으면 조건에추가 IN (5,10)
		/// </summary>
		public string[] SEARCH_KEYWORD_CODE { get; set; }
		/// <summary>
		/// 키워드명 >> 값이 있으면 조건에추가 LIKE '%검색조건%'
		/// </summary>
		public string KEYWORD_NAME { get; set; }
		/// <summary>
		/// 광고코드 >> 값이 잇으면 조건에추가
		/// </summary>
		public Int64? AD_CODE { get; set; }
		/// <summary>
		/// 비콘기기번호
		/// </summary>
		public string BEACON_DEVICE_NUMBER { get; set; }
		/// <summary>
		/// 카테고리와 키워드 연결 테이블의 기본키
		/// </summary>
		public int? CK_CODE { get; set; }
		public int? COMPANY_CODE { get; set; }
		public int? STORE_CODE { get; set; }
		public int? MEMBER_CODE { get; set; }
		/// <summary>
		/// 광고숨김여부 (0:보이기:default, 1:숨김)
		/// </summary>
		public bool? HIDE { get; set; }
		/// <summary>
		/// 모바일로그인아이디
		/// </summary>
		public string USER_ID { get; set; }
		/// <summary>
		/// 페이지당 건수 (기본 20건)
		/// </summary>
		public int? PageCount { get; set; }
		/// <summary>
		/// 선택된 페이지 기본 1
		/// </summary>
		public int? Page { get; set; }

		/// <summary>
		/// 광고구분(A006) => 1:모바일배너, 2:로컬박스배너, 3:모바일+로컬박스
		/// </summary>
		public int? AD_TYPE { get; set; }

		/// <summary>
		/// 광고구분 T_COMMON : A006 (1:배너 2:로컬배너, 3:배너+로컬배너, 4:통배너), 콤마로구분
		/// </summary>
		public string AD_TYPES { get; set; } = "0,1,3";
		public string SORT_ORDER { get; set; }

	}

	public class AD_PAGE_COND
    {
        public int? PAGE_COUNT { get; set; } = 10;
        public int? PAGE { get; set; } = 1;
        public string SORT { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        public int? AD_CODE { get; set; }
        public int? REP_CATEGORY_CODE { get; set; }
        public string TITLE { get; set; }
        public int? COMPANY_CODE { get; set; }
        public int? STORE_CODE { get; set; }
        public string DEPT_SEARCH { get; set; }
        public int? DEPT_CODE { get; set; }
        public int? PARENT_MEMBER_CODE { get; set; }
        public int? MEMBER_CODE { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public int? GROUP_CODE { get; set; }
        public int? BANNER_TYPE { get; set; }
        public int? STATUS { get; set; }
        public int? BASE_DATE { get; set; }
        public bool? HIDE { get; set; }
		public int? AD_FRAME_TYPE { get; set; }
        
       
    }

    public class AD_COND
    {
        public long? AD_CODE { get; set; }
        public int? REP_CATEGORY_CODE { get; set; }
        public bool? HIDE { get; set; }
    }
    #endregion

    #region >> 광고 조회 클래스
    /// <summary>
    /// 광고 조회결과 클래스
    /// </summary>
  
	
	public class AD_LIST
    {
		public long? ROW_INDEX { get; set; }
        /// <summary>
        /// 광고코드
        /// </summary>
        public Int64 AD_CODE { get; set; }

        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005) (1:제목+이미지, 2:제목,3:이미지)
        /// </summary>
        public int? BANNER_TYPE { get; set; }
        /// <summary>
        /// 콘텐츠 유형(T_COMMON : A008, 1:HTML, 2:서브배너)
        /// </summary>
        public int? CONTENT_TYPE { get; set; }
		/// <summary>
		/// 배너가상영역유형(M003) 1000:1km 1000:10km 1000:100km
		/// </summary>
		public int? ITEM_TYPE { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 광고부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 광고로고
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>
        /// 광고 클릭 횟수
        /// </summary>
        public int CLICK_CNT { get; set; }
        /// <summary>
        /// 평가점수
        /// </summary>
        public decimal GRADE_POINT { get; set; }
        /// <summary>
        /// 광고요청한 회사 코드 T_COMPANY 테이블의 COMPANY_CODE
        /// </summary>
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
        /// </summary>
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 광고요청한 회사명 T_COMPANY테이블의 COMPANY_NAME
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 요청자 코드 T_MEMBER테이블의 MEMBER_CODE
        /// </summary>
        public int? MEMBER_CODE { get; set; }
        /// <summary>
        /// 요청자명 T_MEMBER테이블의 MEMBER_CODE
        /// </summary>
        public string MEMBER_NAME { get; set; }
        /// <summary>
        /// 대표카테고리코드
        /// </summary>
        public int? REP_CATEGORY_CODE { get; set; }
        /// <summary>
        /// 대표카테고리명
        /// </summary>
        public int? REP_CATEGORY_NAME { get; set; }
        /// <summary>
        /// 해당 광고가 있는 로컬박스로 부터 거리
        /// </summary>
        public decimal? DEVICE_DISTANCE { get; set; }
        /// <summary>
        /// 광고로 부터 거리
        /// </summary>
        public decimal? AD_DISTANCE { get; set; }

        /// <summary>
        /// 자신의 위치로 부터 광고장소와 로컬박스위치와 비교하여 가까운곳 거리 리턴
        /// </summary>
        public decimal? DISTANCE { get; set; }
		/// <summary>
        /// 비콘 장치번호
        /// </summary>
        public string BEACON_DEVICE_NUMBER { get; set; }
        /// <summary>
        /// 비콘명
        /// </summary>
        public string BEACON_NAME { get; set; }
        /// <summary>
        /// 북마크여부
        /// </summary>
        public bool BOOKMARK_YN { get; set; }
        /// <summary>
        /// 좋아요여부
        /// </summary>
        public bool FAVORITE_YN { get; set; }
        
    }
	#endregion


	public class MOBILE_AD_LIST
	{
		public long? IDX { get; set; }
		/// <summary>
		/// 광고코드
		/// </summary>
		public Int64 AD_CODE { get; set; }

		/// <summary>
		/// 광고배너표시유형(T_COMMON : A005) (1:제목+이미지, 2:제목,3:이미지)
		/// </summary>
		public int? BANNER_TYPE { get; set; }
		/// <summary>
		/// 배너가상영역유형(M003) 1000:1km 1000:10km 1000:100km
		/// </summary>
		public int? ITEM_TYPE { get; set; }
		/// <summary>
		/// 매장명
		/// </summary>
		public string STORE_NAME { get; set; }
		/// <summary>
		/// 광고제목
		/// </summary>
		public string TITLE { get; set; }
		/// <summary>
		/// 광고부제목
		/// </summary>
		public string SUB_TITLE { get; set; }
		/// <summary>
		/// 광고로고
		/// </summary>
		public string LOGO_URL { get; set; }
		/// <summary>
		/// 광고 클릭 횟수
		/// </summary>
		public int CLICK_CNT { get; set; }
		/// <summary>
		/// 평가점수
		/// </summary>
		public decimal GRADE_POINT { get; set; }
		/// <summary>
		/// 광고요청한 회사 코드 T_COMPANY 테이블의 COMPANY_CODE
		/// </summary>
		public int? COMPANY_CODE { get; set; }
		/// <summary>
		/// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
		/// </summary>
		public int? STORE_CODE { get; set; }

		public int? CONTENT_TYPE { get; set; }
		/// <summary>
		/// 광고요청한 회사명 T_COMPANY테이블의 COMPANY_NAME
		/// </summary>
		public string COMPANY_NAME { get; set; }
		/// <summary>
		/// 요청자 코드 T_MEMBER테이블의 MEMBER_CODE
		/// </summary>
		public int? MEMBER_CODE { get; set; }
		/// <summary>
		/// 요청자명 T_MEMBER테이블의 MEMBER_CODE
		/// </summary>
		public string MEMBER_NAME { get; set; }
		
		/// <summary>
		/// 광고로 부터 거리
		/// </summary>
		public decimal? AD_DISTANCE { get; set; }

		/// <summary>
		/// 북마크여부
		/// </summary>
		public bool BOOKMARK_YN { get; set; }
		/// <summary>
		/// 좋아요여부
		/// </summary>
		public bool FAVORITE_YN { get; set; }

	}
	#region >> 광고테이블(T_AD)
	/// <summary>       
	/// 광고테이블(T_AD)
	/// </summary>	   
	public class T_AD
    {
        /// <summary>
        /// 저장모드 N:추가 U:수정 D:삭제
        /// </summary>
        public string SAVE_MODE { get; set; }
        /// <summary>
        /// 조회순번
        /// </summary>
        public long SEQ { get; set; }
        /// <summary>       
        /// 일련번호(기본키)
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>
        /// 매장별 그룹코드 T_STORE_GROUP 테이블의 GROUP_CODE
        /// </summary>
        public int? GROUP_CODE { get; set; }
        /// <summary>
        /// 매장별 그룹코드 T_STORE_GROUP 테이블의 GROUP_NAME
        /// </summary>
        public string GROUP_NAME { get; set; }
        /// <summary>
        /// 광고등록일(yyyyMMdd)
        /// </summary>
        public string REG_DATE { get; set; }
        /// <summary>
        /// 광고구분 T_COMMON : A006 1	일반광고, 2	로컬광고
        /// </summary>
        public int? AD_TYPE { get; set; }
        public string AD_TYPE_NAME { get; set; }
        /// <summary>
        /// 모바일에서 메인광고유형 (MAIN_CODE : A011) 1:카테고리배너 2:메인배너
        /// </summary>
        public int? AD_TYPE2 { get; set; }

        /// <summary>
        /// 모바일에서 메인광고유형 (MAIN_CODE : A011) 1:카테고리배너 2:메인배너
        /// </summary>
        public string AD_TYPE2_NAME { get; set; }

        /// <summary>
        /// 배너 화면분할(T_COMMON => MAIN_CODE: H002) 1:1Frame 6:6Frame
        /// </summary>
        public int? AD_FRAME_TYPE { get; set; } = 1;
		public string AD_FRAME_TYPE_NAME { get; set; }

		/// <summary>
		/// 콘텐츠 유형(T_COMMON : A008, 1:HTML, 2:서브배너)
		/// </summary>
		public int CONTENT_TYPE { get; set; }
        /// <summary>       
        /// 로고 URL
        /// </summary>	   
        public string LOGO_URL { get; set; }
        /// <summary>       
        /// 제목
        /// </summary>	   
        public string TITLE { get; set; }
        /// <summary>       
        /// 부제목
        /// </summary>	   
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005)
        /// </summary>
        public int? BANNER_TYPE { get; set; }

        /// <summary>
        /// 배너유형(1:이미지,2:동영상,3:유튜브-T_COMMON : A010)
        /// </summary>
        public int? BANNER_TYPE2 { get; set; }
        /// <summary>
        /// 배너파일 URL
        /// </summary>
        public string FILE_URL { get; set; }

        /// <summary>
        /// 광고배너표시유형
        /// </summary>
        public string BANNER_TYPE_NAME { get; set; }
        /// <summary>       
        /// 내용
        /// </summary>	   
        public string CONTENT { get; set; }
        /// <summary>       
        /// 광고시작일(yyyyMMdd)
        /// </summary>	   
        public string FR_DATE { get; set; }
        /// <summary>       
        /// 광고종료일(yyyyMMdd)
        /// </summary>	   
        public string TO_DATE { get; set; }
        /// <summary>       
        /// 광고시작시간(HH:mm)
        /// </summary>	   
        public string FR_TIME { get; set; }
        /// <summary>       
        /// 광고시작시간(HH:mm)
        /// </summary>	   
        public string TO_TIME { get; set; }
        /// <summary>       
        /// 광고클릭수
        /// </summary>	   
        public int CLICK_CNT { get; set; }
        /// <summary>       
        /// 별포인트 5점 만점 평점으로 계산
        /// </summary>	   
        public decimal GRADE_POINT { get; set; }
        /// <summary>       
        /// T_COMPANY 테이블의 COMPANY_CODE
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>
        /// T_COMPANY 테이블의 COMPANY_NAME
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>       
        /// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// T_STORE의 STORE_NAME 값이 없으면 전체광고
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>       
        /// 요청한사용자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>
        /// 요청자명
        /// </summary>
        public string MEMBER_NAME { get; set; }
        /// <summary>
        /// 요청자 연락처
        /// </summary>
        public string MOBILE { get; set; }
        /// <summary>
        /// 대표카테고리코드
        /// </summary>
        public int? REP_CATEGORY_CODE { get; set; }
        public string REP_CATEGORY_NAME { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        /// 광고요청상태(T_COMMON테이블의 A001 코드)
        /// </summary>
        public int? STATUS { get; set; }
        public string STATUS_NAME { get; set; }
        /// <summary>       
        /// 담당업체코드 T_COMPANY테이블의 COMPANY_CODE
        /// </summary>	   
        public int? CONTACT_COMPANY_CODE { get; set; }
        /// <summary>       
        /// 담당업체명
        /// </summary>	   
        public string CONTACT_COMPANY_NAME { get; set; }
        /// <summary>       
        /// 담당지점코드 T_STORE테이블의 STORE_CODE
        /// </summary>	   
        public int? CONTACT_STORE_CODE { get; set; }
        /// <summary>       
        /// 담당지점명
        /// </summary>	   
        public string CONTACT_STORE_NAME { get; set; }
        /// <summary>       
        /// 담당자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>	   
        public int? CONTACT_CODE { get; set; }
        /// <summary>       
        /// 담당자명
        /// </summary>	   
        public string CONTACT_NAME { get; set; }
        /// <summary>       
        /// 담당자핸드폰
        /// </summary>	   
        public string CONTACT_MOBILE { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김)
        /// </summary>	   
        public bool HIDE { get; set; }

        public string HIDE_NAME { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        public string INSERT_NAME { get; set; }

        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 광고테이블
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        public string UPDATE_NAME { get; set; }
        public List<KEYWORD_DATA> KEYWORDS { get; set; }
        /// <summary>       
        /// 광고테이블
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        public int TOTAL_ROWCOUNT { get; set; }
        public string SORT { get; set; }

        /// <summary>
        /// 로컬박스 내배너 원본 코드
        /// </summary>
        public long? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스 내배너 원본 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 로컬박스 내배너 공유갯수
        /// </summary>
        public int? SHARE_CNT { get; set; }
        /// <summary>
        /// 로컬박스에 모바일배너 공유갯수
        /// </summary>
        public int? AD_SHARE_CNT { get; set; }
		/// <summary>
        /// 검색위치로 부터거리
        /// </summary>
        public decimal? PLACE_DISTANCE { get; set; }
		/// <summary>
        /// 배너가 노출되는 로컬박스 갯수
        /// </summary>
        public int DEVICE_CNT { get; set; }

		/// <summary>
		/// 가상영역유형 아이템 타입
		/// </summary>
		public int? ITEM_TYPE { get; set; }
	}
    #endregion >> 광고테이블(T_AD) END 





    public class AD_STATUS_COND
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary>+
		/// 
        public string USER_ID { get; set; }
        /// <summary>
        /// 요청건 예 TOP_CNT ="TOP 2" 2건만 가져옴 빈값일 경우 모두 조회
        /// </summary>
        public string TOP_CNT { get; set; }

        /// <summary>
        /// 광고테이블(T_AD) 조회조건 추가
        /// </summary>
        public string AD_COND_STRING { get; set; }
    }

    /// <summary>
    /// 광고진행상태
    /// </summary>
    public class AD_STATUS
    {
        public long? IDX { get; set; }
        public long? AD_CODE { get; set; }
        public string TITLE { get; set; }
        public int STATUE { get; set; }
        public string NEXT_PAGE_URL { get; set; }
        public string NEXT_PAGE_NAME { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public DateTime? REG_DATE { get; set; }
    }


    #region >> 광고즐겨찾기테이블(T_AD_FAVORITE)

    public class T_AD_FAVORITE_COND
    {
        /// <summary>
        /// 기본키(순번)
        /// </summary>
        public long? IDX { get; set; }
        /// <summary>       
        /// 즐겨찾기유형(T_COMMON : L003)
        /// </summary>	   
        public int? FAV_TYPE { get; set; }
        /// <summary>       
        /// 광고(T_AD) 기본키
        /// </summary>	   
        public long? AD_CODE { get; set; }
        /// <summary>       
        /// 로컬박스(T_DEVICE_MAIN) DEVICE_CODE
        /// </summary>	   
        public long? DEVICE_CODE { get; set; }
        /// <summary>       
        /// 로컬박스(T_DEVICE_MAIN) SEQ
        /// </summary>	   
        public int? DEVICE_SEQ { get; set; }
        /// <summary>       
        /// 모바일번호
        /// </summary>	   
        public string MOBILE { get; set; }
        /// <summary>       
        /// SNS 유형(T_COMMON : L004)
        /// </summary>	   
        public int? SNS_TYPE { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
        /// <summary>
        /// 사용자IP
        /// </summary>
        public string USER_IP { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string DEVICE_NAME { get; set; }
        public string AD_TITLE { get; set; }

        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }

    }
    /// <summary>       
    /// 광고즐겨찾기테이블(T_AD_FAVORITE)
    /// </summary>	   
    public class T_AD_FAVORITE
    {
        /// <summary>
        /// 기본키(순번)
        /// </summary>
        public long IDX { get; set; }
        /// <summary>       
        /// 즐겨찾기유형(T_COMMON : L003)
        /// </summary>	   
        public int FAV_TYPE { get; set; }
        /// <summary>
        /// 즐겨찾기 유형명
        /// </summary>
        public string FAV_TYPE_NAME { get; set; }
        /// <summary>       
        /// 광고(T_AD) 기본키
        /// </summary>	   
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string AD_TITLE { get; set; }
        /// <summary>       
        /// 로컬박스(T_DEVICE_MAIN) DEVICE_CODE
        /// </summary>	   
        public long? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>       
        /// 로컬박스(T_DEVICE_MAIN) SEQ
        /// </summary>	   
        public int? DEVICE_SEQ { get; set; }
        /// <summary>       
        /// 모바일번호
        /// </summary>	   
        public string MOBILE { get; set; }
        /// <summary>       
        /// SNS 유형(T_COMMON : L004)
        /// </summary>	   
        public int? SNS_TYPE { get; set; }
        /// <summary>
        /// SNS유형명
        /// </summary>
        public string SNS_TYPE_NAME { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
        /// <summary>       
        /// 사용자아이피
        /// </summary>	   
        public string USER_IP { get; set; }
        /// <summary>
        /// 즐겨찾기 수
        /// </summary>
        public int CNT { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 업데이트시간
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }

        public int TOTAL_ROWCOUNT { get; set; }

    }


    #endregion >> 광고즐겨찾기테이블(T_AD_FAVORITE) END 

    public class AD_DEVICE_SHARE_COND
    {
        public long DEVICE_CODE { get; set; }
    }

    /// <summary>
    /// 로컬박스별 모바일배너 승인정보 리스트
    /// </summary>
    public class AD_DEVICE_SHARE_LIST
    {
        public long AD_CODE { get; set; }
        public string TITLE { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public int STATUS { get; set; }
        public string STATUS_NAME { get; set; }

    }

    public class T_AD_STATUS
    {
        public long AD_CODE { get; set; }
        public int STATUS { get; set; }
        public int REG_CODE { get; set; }
    }

	public class AD_MOBILE
	{
		/// <summary>
		/// 광고코드
		/// </summary>
		public Int64 AD_CODE { get; set; }
		/// <summary>
		/// 광고배너표시유형(T_COMMON : A005) (1:제목+이미지, 2:제목,3:이미지)
		/// </summary>
		public int? BANNER_TYPE { get; set; }
		/// <summary>
		/// 광고제목
		/// </summary>
		public string STORE_NAME { get; set; }
		/// <summary>
		/// 광고제목
		/// </summary>
		public string TITLE { get; set; }
		/// <summary>
		/// 광고부제목
		/// </summary>
		public string SUB_TITLE { get; set; }
		/// <summary>
		/// 광고로고
		/// </summary>
		public string LOGO_URL { get; set; }
		/// <summary>
		/// 광고 클릭 횟수
		/// </summary>
		public int? CLICK_CNT { get; set; }
		/// <summary>
		/// 평가점수
		/// </summary>
		public decimal? GRADE_POINT { get; set; }
		/// <summary>
		/// 광고요청한 회사 코드 T_COMPANY 테이블의 COMPANY_CODE
		/// </summary>
		public int? COMPANY_CODE { get; set; }
		/// <summary>
		/// 요청한매장코드들 T_SOTRE 테이블의 STORE_CODE , 구분자 => | 값이 없으면 업체 전체 광고
		/// </summary>
		public int? STORE_CODE { get; set; }
		/// <summary>
		/// 콘텐츠 유형(T_COMMON : A008, 1:HTML, 2:서브배너)
		/// </summary>
		public int? CONTENT_TYPE { get; set; }
		/// <summary>
		/// 광고요청한 회사명 T_COMPANY테이블의 COMPANY_NAME
		/// </summary>
		public string COMPANY_NAME { get; set; }
		/// <summary>
		/// 요청자 코드 T_MEMBER테이블의 MEMBER_CODE
		/// </summary>
		public int? MEMBER_CODE { get; set; }
		/// <summary>
		/// 요청자명 T_MEMBER테이블의 MEMBER_CODE
		/// </summary>
		public string MEMBER_NAME { get; set; }
		/// <summary>
		/// 광고로 부터 거리
		/// </summary>
		public int? AD_DISTANCE { get; set; }
		/// <summary>
		/// 북마크여부
		/// </summary>
		public bool BOOKMARK_YN { get; set; }
		/// <summary>
		/// 좋아요여부
		/// </summary>
		public bool FAVORITE_YN { get; set; }
	}
}
