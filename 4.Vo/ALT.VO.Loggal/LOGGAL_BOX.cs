using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{

    public class LOGGAL_BOX_COND
    {
        public int GUBUN { get; set; } = 1;
        public long? DEVICE_CODE { get; set; }
        public int?    DISTANCE { get; set; }
        public string DEVICE_NUMBER { get; set; }
        public int? STORE_CODE { get; set; }
        public string TITLE { get; set; }
        public int? AD_FRAME_TYPE { get; set; }
        public bool bWorkingTime { get; set; } = false;
        public int? CATEGORY_CODE { get; set; }
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string HOST { get; set; }
    }
   
    public class LOGGAL_BOX_INFO
    {
        public int SEQ { get; set; }
        public long? GROUP_SEQ { get; set; }
		public int CATEGORY_CODE { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int? CONTENT_TYPE { get; set; }
		public int? BANNER_TYPE2 { get; set; }
        public int? FRAME_TYPE { get; set; }
        public int? PLAY_TIME { get; set; }
        public List<LOGGAL_BOX_INFO_DETAIL> list {get;set;}
    }


    public class LOGGAL_BOX_INFO_DETAIL
    {
        public long? AD_CODE { get; set; }
        public int BANNER_TYPE { get; set; }
        public string LOGO_URL { get; set; }
        public string BANNER_IMAGE { get; set; }
        public string TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string COMPANY_NAME { get; set; }
        public string CONTENT_URL { get; set; }
        public int CLICK_CNT { get; set; }
        public int BOOKMARK_CNT { get; set; }
        public int FAVORITE_CNT { get; set; }
    }


    public class LOGGAL_BOX_DATA
    {
        public long DEVICE_CODE { get; set; } 
        public string DEVICE_NAME { get; set; }
        public int CATEGORY_CODE { get; set; }
        public long? GROUP_SEQ { get; set; }
        public string GROUP_NAME { get; set; }
        public int? CONTENT_TYPE { get; set; }
        /// <summary>
        /// CONTENT_TYPE가 2일경우 동영상 URL 3일경우 youtube ID
        /// </summary>
        public string REF_DATA1 { get; set; }
        /// <summary>
        /// 보기그룹별 재생시간(이미지일 경우에만 적용)
        /// </summary>
        public int? PLAY_TIME { get; set; }
        public int? FRAME_TYPE { get; set; }
        public string CATEGORY_NAME { get; set; }
        public int CATEGORY_TYPE { get; set; }
        public string GLYPH { get; set; }
        public int BANNER_TYPE { get; set; }
        public int ORDER_SEQ { get; set; }
        public long AD_CODE { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public string TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        public string BANNER_IMAGE { get; set; }
        public string LOGO_URL { get; set; }
        public decimal LATITUDE { get; set; }
        public decimal LONGITUDE { get; set; }
        public decimal DISTANCE { get; set; }
        public string CONTENT_URL { get; set; }
        public string COMPANY_NAME { get; set; }
        /// <summary>
        /// 배너 검색 기준 주소
        /// </summary>
        public string JIBUN_ADDRESS { get; set; }
        /// <summary>
        /// 공유승인상태 A009 0	일반, 1	요청, 8	반려, 9	승인,11	취소
        /// </summary>
        public int? SHARE_STATUS { get; set; }
        /// <summary>
        /// 공유승인상태명
        /// </summary>
        public string SHARE_STATUS_NAME { get; set; }
        /// <summary>
        /// 화면분할
        /// </summary>
        public int? AD_FRAME_TYPE { get; set; }
        /// <summary>
        /// 화면분할명
        /// </summary>
        public string AD_FRAME_TYPE_NAME { get; set; }
        public int? PAGE_WAITING_TIME { get; set; } = 60;

        /// <summary>
        /// 구분 1:거리순으로 포함 2:배너공유
        /// </summary>
        public int GUBUN { get; set; }
        /// <summary>
        /// 공유원본로컬박스
        /// </summary>
        public string ORI_DEVICE_NAME { get; set; }
        /// <summary>
        /// 공유원본업체명
        /// </summary>
        /// 
        public string ORI_COMPANY_NAME { get; set; }

        /// <summary>
        /// 상세보기건수
        /// </summary>
        public int CLICK_CNT { get; set; }
        /// <summary>
        /// 북마크한 건수
        /// </summary>
        public int BOOKMARK_CNT { get; set; }
        /// <summary>
        /// 좋아요 건수
        /// </summary>
        public int FAVORITE_CNT { get; set; }
        public int TOTAL_ROWCOUNT { get; set; }
    }


	public class LOGGAL_BOX_COND2
	{
		public int GUBUN { get; set; } = 1;
		
		public long? DEVICE_CODE { get; set; }
		public int? DISTANCE { get; set; }
		public string DEVICE_NUMBER { get; set; }
		public int? CATEGORY_CODE { get; set; }
		public string TITLE { get; set; }
		
		public bool bWorkingTime { get; set; } = false;
		
		public int? PAGE { get; set; }
		public int? PAGE_COUNT { get; set; }
		public string HOST { get; set; }
		public int? STATUS { get; set; }
		public int? STORE_CODE { get; set; }
		public int? AD_FRAME_TYPE { get; set; }
		/// <summary>
        /// 화면분할명
        /// </summary>
        public string AD_FRAME_TYPE_NAME { get; set; }
		/// <summary>
		/// 내배너에서 IS_MOBILE 인것만 로컬박스에 노출함
		/// </summary>
		public bool? IS_MOBILE { get; set; }
		public int? OUT_AD_STATUS { get; set; }
	}


	public class LOGGAL_BOX_DATA2
	{
		public long ROW_IDX { get; set; }
        public string AD_KIND_NAME { get; set; }

        public int GROUP_SEQ { get; set; }
		public long CATEGORY_SEQ { get; set; }
		public long DEVICE_CODE { get; set; }
		public string DEVICE_NAME { get; set; }
		public int AD_TYPE { get; set; }
		public int? STATUS { get; set; }
		public string STATUS_NAME { get; set; }
		public int AD_FRAME_TYPE { get; set; }
		/// <summary>
		/// 화면분할명
		/// </summary>
		public string AD_FRAME_TYPE_NAME { get; set; }
		public int BANNER_TYPE { get; set; }
		public int? BANNER_TYPE2 { get; set; }
		public int? DISPLAY_SEQ { get; set; }
		public decimal? PLACE_DISTANCE { get; set; }
		public int CATEGORY_CODE { get; set; }
		public string CATEGORY_NAME { get; set; }
		public int CATEGORY_ORDER_SEQ { get; set; }
		public long AD_CODE { get; set; }
		public string TITLE { get; set; }
		public string SUB_TITLE { get; set; }
		public string FR_DATE { get; set; }
		public string TO_DATE { get; set; }
		public string FR_TIME { get; set; }
		public string TO_TIME { get; set; }
		public string LOGO_URL { get; set; }
		public string BANNER_IMAGE { get; set; }
		public int CLICK_CNT { get; set; }
		public int? STORE_CODE { get; set; }
		public string STORE_NAME { get; set; }
		public int? MEMBER_CODE { get; set; }
		public string MEMBER_NAME { get; set; }
		public string STORE_PHONE { get; set; }
		public long? AD_DEVICE_CODE { get; set; }
		public int? DEVICE_TYPE { get; set; }
		public int? DATA_CYCLE_TIME { get; set; }
		public int? AD_CYCLE_TIME { get; set; }
		public int? PAGE_WAITING_TIME { get; set; }
		public int BOOKMARK_CNT { get; set; }
		public int FAVORITE_CNT { get; set; }
		public string CONTENT_URL { get; set; }
		public bool IS_MOBILE { get; set; }
		public int TOTAL_ROWCOUNT { get; set; }
		
	}


	public class DAUM_MAP_COND
    {
        public string TITLE { get; set; }
    }
    public enum enMapType
    {
        Nomal, Station
    }
    public class DAUM_MAPLIST
    {
        public string KEY_CODE { get; set; }
        public string TITLE { get; set; }
        public string SUB_TITLE { get; set; }
        private bool? _activeYn = true;
        public bool? ACTIVE_YN { get { return _activeYn; } set { _activeYn = value; } }
        public string CONTENT { get; set; }
        public string LINK_URL { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        private Int32? _radius = 500;
        public int? RADIUS { get { return _radius; } set { _radius = value; } }
        /// <summary>
        /// 범위설정여부(마스터데이터 여부 default : 1), 마스터일 경우 반경등록 마스터가 아닌 경우 마스터 범위안에 등록되어야함
        /// </summary>
        public bool IS_RANGE { get; set; }

        public enMapType EnMapType { get; set; }
    }

    public class LOGGAL_BOX_AUTH
    {
        public long? AUTH_NUMBER { get; set; }
        /// <summary>
        /// 인증 유형(2:로컬박스 3:로컬사인), T_COMMON 테이블의 L003
        /// </summary>
        public int?  AUTH_TYPE { get; set; }
        public string DEVICE_NUMBER { get; set; }
        public long? SIGN_CODE { get; set; }
        public long? DEVICE_CODE { get; set; }
    }
}
