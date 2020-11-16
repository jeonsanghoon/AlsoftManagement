using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    #region >> 모바일 배너 상세

    /// <summary>
    /// 모바일배너상세 조건
    /// </summary>
    public class MOBILE_AD_DETAIL_COND
    {
        /// <summary>
        /// 배너코드
        /// </summary>
        public long AD_CODE { get; set; }
        /// <summary>
        /// 사용자아이디(이메일)
        /// </summary>
        public string USER_ID { get; set; }
        
    }
    /// <summary>
    /// 광고상세
    /// </summary>
    public class MOBILE_AD_DETAIL_DATA
    {
        /// <summary>
        /// 배너아이디
        /// </summary>
        public long? AD_CODE { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set; }
        /// <summary>
        /// 로고 URL
        /// </summary>
        public string LOGO_URL { get; set; }
        /// <summary>
        /// 광고상세 URL
        /// </summary>
        public string CONTENT_URL { get; set; }

        /// <summary>
        /// 북마크유무
        /// </summary>
        public bool BOOKMARK_YN { get; set; }
        /// <summary>
        /// 좋아요
        /// </summary>
        public bool FAVORITE_YN { get; set; }
    }
    #endregion

    #region >> 모바일 배너 검색리스트
    public class MOBILE_AD_SEARCH_COND
    {
        /// <summary>
        /// 페이지당 데이터건수
        /// </summary>
        public int? PAGE_COUNT { get; set; }
        /// <summary>
        /// 페이지번호
        /// </summary>
        public int? PAGE { get; set; }
        /// <summary>
        /// 위도
        /// </summary>
        public decimal? LATITUDE { get; set; }
        /// <summary>
        /// 경도
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
        /// UTC Time Zone 분단위 디폴트 9 * 60 = 540
        /// </summary>
        public int? TIME_ZONE { get; set; }

        /// <summary>
        /// 카테고리코드
        /// </summary>
        public int? CATEGORY_CODE { get; set; }
        /// <summary>
        /// 카테고리코드들 콤마로 구분
        /// </summary>
        public String CATEGORY_CODES { get; set; }
        /// <summary>
        /// 키워드 코드
        /// </summary>
        public int? KEYWORD_CODE { get; set; }
        /// <summary>
        /// 검색어
        /// </summary>
        public string KEYWORD_NAME { get; set; }
        /// <summary>
        /// 검색위치로 부터 거리
        /// </summary>
        public decimal? DISTANCE { get; set; }
        /// <summary>
        /// 로그인 아이디(이메일)
        /// </summary>
        public string USER_ID { get; set; }
    }

    public class MOBILE_AD_SEARCH_DATA
    {
        /// <summary>
        /// 순번
        /// </summary>
        public long IDX { get; set; }
        /// <summary>
        /// 광고코드
        /// </summary>
        public long AD_CODE { get; set;}
        /// <summary>
        /// 제목
        /// </summary>
        public string TITLE { get; set;}
        /// <summary>
        /// 부제목
        /// </summary>
        public string SUB_TITLE { get; set; }
        /// <summary>
        /// 회사명
        /// </summary>
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 로고이미지 URL
        /// </summary>
        public string LOGO_URL { get; set; }
        public int BANNER_TYPE { get; set; }
        /// <summary>
        /// 검색위치로 부터 거리
        /// </summary>
        public decimal DISTANCE { get; set; }

        /// <summary>
        /// 실제배너구분 1:일반배너 2:로컬박스의 배너
        /// </summary>
        public int REAL_AD_TYPE { get; set; }
        /// <summary>
        /// 상세페이지 URL
        /// </summary>
        public string CONTENT_URL { get; set; }
        public bool BOOKMARK_YN { get; set; }

    }
    #endregion
}
