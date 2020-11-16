using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{

    #region >> 회원별 북마크정보(T_MEMBER_BOOKMARK)

    /// <summary>
    /// 회원별 북마크 조회 조건
    /// </summary>
    public class T_MEMBER_BOOKMARK_COND
    {
        /// <summary>
        /// 순번
        /// </summary>
        public int? BOOKMARK_CODE { get; set; }
        /// <summary>       
        /// 회원코드(T_MEMBER테이블의 MEMBER_CODE)
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>
        /// B009 : 북마크종류 1:북마크 2:좋아요
        /// </summary>
        public int? BOOKMARK_KIND { get; set; }
        /// <summary>
        /// 북마크유형 (1:웹페이지 2: 로컬박스) T_COMON 테이블의 MAIN_CODE : A007
        /// </summary>
        public int? BOOKMARK_TYPE { get; set; }
        /// <summary>
        /// 사용자아이디
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 등록자 이름
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>       
        /// 북마크URL
        /// </summary>	   
        public string BOOKMARK_URL { get; set; }
        /// <summary>
        /// 페이지번보
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 갯수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 디바이스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 정렬순서
        /// </summary>
        public string SORT_ORDER { get; set; }
    }


    /// <summary>       
    /// 회원별 북마크정보(T_MEMBER_BOOKMARK)
    /// </summary>	   
    public class T_MEMBER_BOOKMARK
    {
        /// <summary>
        /// 저장유형 D:일경우 삭제
        /// </summary>
        public string SAVE_MODE { get; set; }
        /// <summary>
        /// 순번
        /// </summary>
        public int BOOKMARK_CODE { get; set; }
        /// <summary>       
        /// 회원코드(T_MEMBER테이블의 MEMBER_CODE)
        /// </summary>	   
        public int MEMBER_CODE { get; set; }
        /// <summary>
        /// 회원명
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>
        /// 사용자아이디
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 북마크 명칭( 기본 : 유형이 1일 경우 URL, 2일경우 로컬박스명, 3일경우 광고제목)
        /// </summary>
        public string BOOKMARK_NAME { get; set; }
        /// <summary>
        /// B009 : 북마크종류 1:북마크 2:좋아요
        /// </summary>
        public int? BOOKMARK_KIND { get; set; }
        /// <summary>
        /// 마크유형(A007) 1:웹페이지, 2:로컬박스 3:광고
        /// </summary>
        public int? BOOKMARK_TYPE { get; set; }
        /// <summary>
        /// 로컬박스코드(T_DEVICE 테이블 참조)
        /// </summary>
        public long? DEVICE_CODE { get; set; }
        /// <summary>
        /// 로컬박스명
        /// </summary>
        public string DEVICE_NAME { get; set; }
        /// <summary>
        /// 로컬박스설치 주소
        /// </summary>
        public string DEVICE_ADDRESS { get; set; }
        /// <summary>
        /// 광고코드(T_AD 테이블 참조)
        /// </summary>
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 광고제목
        /// </summary>
        public string TITLE { get; set; }
        
        /// <summary>       
        /// 북마크URL
        /// </summary>	   
        public string BOOKMARK_URL { get; set; }
        /// <summary>       
        /// 메모
        /// </summary>	   
        public string MEMO { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>
        /// 배너상태 T_AD테이블의 STATUS T_COMMON(MAIN_CODE:A001)
        /// </summary>
        public int? STATUS { get; set; }
        /// <summary>
        /// 사용상태
        /// </summary>
        public string STATUS_NAME { get; set; }
        /// <summary>
        /// 로컬박스 또는 배너 회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 배너공유건수
        /// </summary>
        public int AD_CNT { get; set; }
        /// <summary>
        /// 로컬박스공유 건수
        /// </summary>
        public int DEVICE_CNT { get; set; }
        /// <summary>
        /// 로컬박스인증번호
        /// </summary>
        public long? AUTH_NUMBER { get; set; }
        /// <summary>
        /// 로컬박스설명
        /// </summary>
        public string DEVICE_DESC { get; set; }
        public string LOGO_URL { get; set; }
        public string SUB_TITLE { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }
    #endregion >> 회원별 북마크정보(T_MEMBER_BOOKMARK) END 

    #region >> 북마크 배너 정보
    /// <summary>
    /// 북마크배너리스트 조건
    /// </summary>
    public class BOOKMARK_AD_COND
    {
        /// <summary>
        /// 사용자 아이디 이메일
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        /// 등록자이름
        /// </summary>
        public string USER_NAME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? BOOKMARK_KIND { get; set; }
        /// <summary>
        /// 페이지 번호 Default 1
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 페이지당 데이터 건수 Default 15
        /// </summary>
        public int? PAGE_COUNT { get; set; }

        public string TITLE { get; set; }
        public string SORT_ORDER { get; set; }
    }
    /// <summary>
    /// 북마크 배너 리스트
    /// </summary>
    public class BOOKMARK_AD_LIST
    {
        public long IDX { get; set; }
        public int BOOKMARK_CODE { get; set; }
        public long AD_CODE { get; set; }
        public int STATUS { get; set; }
        public int MEMBER_CODE { get; set; }
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string LOGO_URL { get; set; }
        public int BANNER_TYPE { get; set; }
        public string BANNER_TYPE_NAME { get; set; }
        public int AD_TYPE { get; set; }
        public string AD_TYPE_NAME { get; set; }
        public string COMPANY_NAME { get; set; }
        public string CONTENT_URL { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public DateTime INSERT_DATE { get; set; }
        /// <summary>
        /// 공유건수
        /// </summary>
        public int AD_CNT { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }
    #endregion
}
