using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    public class LOGGAL_AD_COND
    {
        public long? DEVICE_CODE { get; set; }
        public long? AD_CODE { get; set; }
        public string DEVICE_NUMBER { get; set; }

        public DateTime? UPDATE_DATE { get; set; }

        /// <summary>
        /// 사용자 아이디 T_MEMBER 테이블
        /// </summary>
        public string USER_ID { get; set; }
    }

    public class LOGGAL_AD_MAIN
    {
        public long DEVICE_CODE { get; set; }
        public string DEVICE_NAME { get; set; }

        public IList<LOGGAL_AD_DATA> list { get; set; }
    }


    public class LOGGAL_AD_DATA
    {
        /// <summary>
        /// 로컬박스코드
        /// </summary>
        public long DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 카테고리코드
        /// </summary>
        public int CATEGORY_CODE { get; set; }

        /// <summary>
        /// 검색을 위한 키조함  최상위코드부터 하위 코드 순으로 |로구분 예) 1|12|100
        /// </summary>
        public string SEARCH_CATEGORY_CODE { get; set; }
        /// <summary>
        /// 카테고리명
        /// </summary>
        public string CATEGORY_NAME { get; set; }
        /// <summary>
        /// 카테고리순번
        /// </summary>
        public int CATEGORY_ORDER_SEQ { get; set; }
        /// <summary>
        /// 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역 3:내배너
        /// </summary>
        public int? CATEGORY_TYPE { get; set; }
        /// <summary>
        /// 광고코드
        /// </summary>
        public long AD_CODE { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 광고부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005) 1:제목+이미지 2:제목 3:이미지
        /// </summary>
        public int BANNER_TYPE { get; set; }
        /// <summary>
        /// 로고(썸네일) URL
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>
        /// 내용 URL
        /// </summary>
        public string CONTENT_URL { get; set; }
        /// <summary>
        /// 서버현재시간
        /// </summary>
        public DateTime SERVER_DATE { get; set; }
        /// <summary>
        /// 등록한 사람명
        /// </summary>
        public string REG_NAME { get; set; }
        /// <summary>
        /// 최종업데이트시간
        /// </summary>
        public DateTime? UPDATE_DATE { get; set; }
        /// <summary>
        /// 광고주회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 북마크유무
        /// </summary>
        public bool BOOKMARK_YN { get; set; }
    }
    #region >>

    public class AdDeviceSaveData
    {
        public string SAVE_TYPE { get; set; }
        public List<T_AD_DEVICE> list { get; set; }
    }
    public class AdDeviceSaveData2
    {
        public string SAVE_TYPE { get; set; }
    }
    #endregion 
    #region >> 광고&단말기테이블(T_AD_DEVICE)
    /// <summary>       
    /// 광고&단말기테이블(T_AD_DEVICE)
    /// </summary>	   
    public class T_AD_DEVICE
    {

        /// <summary>
        /// 저장유형 N:신규 U:수정 D:삭제
        /// </summary>
        public string SAVE_MODE { get; set; }
        /// <summary>       
        /// 일련번호(기본키)
        /// </summary>	   
        public Int64 AD_DEVICE_CODE { get; set; }
        /// <summary>       
        /// 광고코드
        /// </summary>	   
        public Int64? AD_CODE { get; set; }
        /// <summary>       
        /// 광고테이블
        /// </summary>	   
        public Int64? DEVICE_CODE { get; set; }
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
        /// UTC 시작일(yyyyMMdd)
        /// </summary>	   
        public string FR_UTC_DATE { get; set; }
        /// <summary>       
        /// UTC 종료일(yyyyMMdd)
        /// </summary>	   
        public string TO_UTC_DATE { get; set; }
        /// <summary>       
        /// UTC 시작일(HH:mm)
        /// </summary>	   
        public string FR_UTC_TIME { get; set; }
        /// <summary>       
        /// UTC 종료일(HH:mm)
        /// </summary>	   
        public string TO_UTC_TIME { get; set; }
        /// <summary>       
        /// 광고종료시간
        /// </summary>	   
        public int? CLICK_CNT { get; set; }
        /// <summary>
        /// 승인회사코드(T_COMPANY 테이블 참조)
        /// </summary>
        public int? APPROVAL_COMPANY_CODE { get; set; }

        /// <summary>
        /// 승인상태 T_COMMON테이블( MAIN_CODE : A009) 0:일반 1:요청 2:반려 9:승인 11:취소
        /// </summary>
        public int? STATUS { get; set; }
        /// <summary>       
        /// 숨김처리(0:보임 1:숨김)
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
		/// <summary>       
		/// 모바일 공개 여부
		/// </summary>	
		public bool IS_MOBILE { get; set; }
    }
    #endregion >> 광고&단말기테이블(T_AD_DEVICE) END 

    public class AD_DEVICE_LIST
    {
        public long SEQ { get; set; }
        public long? AD_DEVICE_CODE { get; set; }
        public long? DEVICE_CODE { get; set; }
        public long AD_CODE { get; set; }
        public string LOGO_URL { get; set; }
        public string TITLE { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public bool? HIDE { get; set; }
        public int? CONTENT_TYPE { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }
    }

    public class AdOnDeviceList
    {
        public long SEQ { get; set; }
        public long AD_CODE { get; set; }
        public string LOGO_URL { get; set; }
        public string TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string STORE_NAME { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public string CATEGORY_NAME { get; set; }
        public long? ORI_DEVICE_CODE { get; set; }
        public string ORI_DEVICE_NAME { get; set; }
        public string MEMBER_NAME { get; set; }
        public int? IS_BOOKMARK { get; set; }
        public int? AD_FRAME_TYPE;
        public string AD_FRAME_TYPE_NAME { get; set; }
        public int? AD_TYPE { get; set; }
        public string AD_TYPE_NAME { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }

    }

    public class AdOnDeviceList2
    {
        public long SEQ { get; set; }
        public bool? HIDE { get; set; }
        public long AD_CODE { get; set; }
        public long? AD_DEVICE_CODE { get; set; }
        public long DEVICE_CODE { get; set; }
        public int? COMPANY_CODE { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public int AD_FRAME_TYPE {get;set;} 
        public string AD_FRAME_TYPE_NAME { get; set; }
        public string DEVICE_NAME { get; set; }
        public long AUITH_NUMBER { get; set; }
        public string ADDRESS { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public int? STATUS { get; set; }
        public string STATUS_NAME { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }

    }

    public class AD_DEVICE_MOBILE_COND
    {
        /// <summary>
        /// 검색페이지 디폴트 1
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 데이터 건수 10000
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        public long DEVICE_CODE { get; set; }
        public string USER_ID { get; set; }
        /// <summary>
        /// 대표카테고리
        /// </summary>
        public int? REP_CATEGORY_CODE { get; set; }

        /// <summary>
        /// T_CATEGORY 테이블의 CATEGORY_TYPE 카테고리유형 T_COMMON 테이블의 B004 코드 사용, 1:광고 2:지역 3:내배너
        /// </summary>
        public int? CATEGORY_TYPE { get; set; }
        /// <summary>
        /// 배너코드
        /// </summary>
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 키워드명 >> 값이 있으면 조건에추가 LIKE '%검색조건%'
        /// </summary>
        public string KEYWORD_NAME { get; set; }

        /// <summary>
        /// 카테고리와 키워드 연결 테이블의 기본키
        /// </summary>
        public int? CK_CODE { get; set; }

        /// <summary>
        /// 0:보임 1:숨김 2:모두 디폴트는 보임(0)
        /// </summary>
        public int? HIDE { get; set; }
  
    }
    public class AD_DEVICE_MOBILE_M
    {
        /// <summary>
        /// 로컬박스코드
        /// </summary>
        public long DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 로컬박스 회사코드
        /// </summary>
        public int DEVICE_COMPANY_CODE { get; set; }
        /// <summary>
        /// 로컬박스 회사명
        /// </summary>
        public string DEVICE_COMPANY_NAME { get; set; }
        /// <summary>
        /// 북마크여부
        /// </summary>
        public bool BOOKMARK_YN { get; set; }
        public bool FAVORITE_YN { get; set; }
        public IList<AD_DEVICE_MOBILE_LIST> AD_LIST { get; set; }
    }

    public class AD_DEVICE_MOBILE_LIST
    {
        /// <summary>
        /// 로컬박스코드
        /// </summary>
        public long DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 조회순번
        /// </summary>
        public long IDX { get; set; }
        /// <summary>
        /// 배너코드
        /// </summary>
        public long AD_CODE { get; set; }

        /// <summary>
        /// 광고배너표시유형(T_COMMON : A005) (1:제목+이미지, 2:제목,3:이미지)
        /// </summary>
        public int? BANNER_TYPE { get; set; }
        /// <summary>
        /// 광고 콘텐츠 유형(T_COMMON : A008, 1:HTML, 2:서브배너)
        /// </summary>
        public int? CONTENT_TYPE { get; set; }
        /// <summary>
        /// 회사코드(T_COMPANY)
        /// </summary>
        public int COMPANY_CODE { get; set; }
        /// <summary>
        /// 회사명(T_COMPANY)
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 로컬박스 회사코드(T_COMPANY)
        /// </summary>
        public int DEVICE_COMPANY_CODE { get; set; }
        /// <summary>
        /// 로컬박스 회사명(T_COMPANY)
        /// </summary>
        public string DEVICE_COMPANY_NAME { get; set; }
        /// <summary>
        /// 지점코드(T_STORE)
        /// </summary>
        public int STORE_CODE { get; set; }
        /// <summary>
        /// 지점명
        /// </summary>
        public string STORE_NAME { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 로고 URL
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>
        /// 클릭수
        /// </summary>
        public int? CLICK_CNT { get; set; }
        /// <summary>
        /// 별포인트 5점 만점 평점으로 계산
        /// </summary>
        public decimal? GRADE_POINT { get; set; }

        /// <summary>
        /// 요청한사용자코드 T_MEMBER 테이블의 MEMBER_CODE
        /// </summary>
        public int MEMBER_CODE { get; set; }
        /// <summary>
        /// 요청한사용자명
        /// </summary>
        public string MEMBER_NAME { get; set; }
        /// <summary>
        /// 대표카테고리 코드
        /// </summary>
        public int CATEGORY_CODE { get; set; }
        /// <summary>
        /// 대표카테고리명
        /// </summary>
        public string CATEGORY_NAME { get; set; }
        /// <summary>
        /// 디바이스 북마크 여부
        /// </summary>
        public bool BOOKMARK_YN { get; set; }
        /// <summary>
        /// 배너 북마크 여부
        /// </summary>
        public bool BANNER_BOOKMARK_YN { get; set; }
        /// <summary>
        /// 디바이스 북마크 여부
        /// </summary>
        public bool FAVORITE_YN { get; set; }
        /// <summary>
        /// 배너 북마크 여부
        /// </summary>
        public bool BANNER_FAVORITE_YN { get; set; }
        /// <summary>
        /// 공유승인상태(T_COMMON : A009) 0:미승인, 8:반려, 9:승인
        /// </summary>
        public int? SHARE_STATUS { get; set; }

    }

    public class APPROVAL_GRAPE_COND
    {
        public int? COMPANY_CODE { get; set; }
        public int? MNG_COMPANY_CODE { get; set; }
    }
    public class APPROVAL_GRAPE
    {
        public int STATUS { get; set; }
        public string STATUS_NAME { get; set; }
        public int CNT { get; set; }
    }

    public class MY_BANNER_SAVE {

        public string SAVE_MODE { get; set; }
        /// <summary>
        /// 배너코드 T_AD 테이블코드
        /// </summary>
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 선택된 로컬박스코드(콤마로 코드 구분) T_DEVICE 테이블 코드
        /// </summary>
        public string DEVICE_CODES { get; set; }
        /// <summary>
        /// 등록자코드
        /// </summary>
        public int? REG_CODE { get; set; }
    }


    public class AD_DEVICE_SHARE_TOTAL_COND
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string SORT { get; set; }
        public int COLUMN_COUNT { get; set; }
        public string GUBUN { get; set; }
        public string SEARCH_TEXT { get; set; }

        public int? COMPANY_CODE { get; set; }
    }

    public class AD_DEVICE_SHARE_TOTAL_LIST
    {
        public long IDX { get; set; }
        public string GUBUN { get; set; }
        public int COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public long AD_CODE { get; set; }
        public string LOGO_URL { get; set; }
        public string TITLE { get; set; }

        public int CNT1 { get; set; }
        public int CNT2 { get; set; }
        public int CNT3 { get; set; }
        public int CNT4 { get; set; }
        public int CNT5 { get; set; }
        public int CNT6 { get; set; }
        public DateTime? REQ_DATE { get; set; }
        public int TOT_CNT { get; set; }
        public DateTime UPDATE_DATE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }

}
