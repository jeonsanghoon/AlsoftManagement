using ALT.Framework.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ALT.Framework.Mvc
{
    public class EditTempleateModel
    {
        [AllowHtml]
        [UIHint("tinymce_default")]
        public string TinyMCE_Editor { get; set; }

        [AllowHtml]
        [UIHint("Daum_address")]
        public string Daum_address { get; set; }

        //[AllowHtml]
        //[UIHint("Daum_MapMarker")]
        //public string Daum_MapMarker { get; set; }

        [AllowHtml]
        [UIHint("Kakao_address")]
        public string Kakao_address { get; set; }

        [AllowHtml]
        [UIHint("Kakao_MapDrawing")]
        public string Kakao_MapDrawing { get; set; }

        [AllowHtml]
        [UIHint("Kakao_MapMakerCircle")]
        public string Kakao_MapMakerCircle { get; set; }

        [AllowHtml]
        [UIHint("Kakao_MapMakerCircle_Range")]
        public string Kakao_MapMakerCircle_Range { get; set; }
		[AllowHtml]
		[UIHint("Kakao_MapMakerCircle_Range_PlaceItem")]
		public string Kakao_MapMakerCircle_Range_PlaceItem { get; set; }
		[AllowHtml]
        [UIHint("Employee_Popup")]
        public string Employee_Popup { get; set; }
    }

    public class DaumAddressOption
    {
        /// <summary>
        /// 아이디
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 우편번호
        /// </summary>
        public string POST_CODE { get; set; }

        public string ADDRESS1 { get; set; }
        public string ADDRESS2 { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }
        private bool _bReadOnly = false;
        public bool bReadOnly { get { return _bReadOnly; } set { _bReadOnly = value; } }
        SemanticUIHelper.Size _size = SemanticUIHelper.Size.mini;
        public SemanticUIHelper.Size Size { get { return _size; } set { _size = value; } }

        private bool _bPostSearch = false;
        /// <summary>
        /// 우편번호 검색 여부
        /// </summary>
        public bool bPostSearch { get { return _bPostSearch; } set { _bPostSearch = value; } }
    }

    public class Daum_MapMultiMarker
    {
        /// <summary>
        /// 팝업제목
        /// </summary>
        public string TITLE { get; set; }
        public string ID { get; set; }
        private decimal _default_LATITUDE = (decimal)37.566826;
        /// <summary>
        /// 지도 기준점 위도
        /// </summary>
        public decimal default_LATITUDE { get { return _default_LATITUDE; } set { _default_LATITUDE = value; } }
        private decimal _default_longitude = (decimal)126.9786567;
        /// <summary>
        /// 지도 기준점 경도
        /// </summary>
        public decimal default_longitude { get { return _default_longitude; } set { _default_longitude = value; } }

        private int _default_LEVEL = 8;
        /// <summary>
        /// 지도확대레벨
        /// </summary>
        public int default_LEVEL { get { return _default_LEVEL; } set { _default_LEVEL = value; } }
        /// <summary>
        /// 마커표시된 지도 정보
        /// </summary>
        public List<Daum_MapAddress> list { get; set; }

        private bool _bReadOnly = false;
        public bool bReadOnly { get { return _bReadOnly; } set { _bReadOnly = value; } }

        private int? _MAKER_MAX_COUNT = 20;
        /// <summary>
        /// 마커최대 등록 갯수
        /// </summary>
        public int? MAKER_MAX_COUNT { get { return _MAKER_MAX_COUNT; } set { _MAKER_MAX_COUNT = value; } }
        SemanticUIHelper.Size _size = SemanticUIHelper.Size.mini;
        /// <summary>
        /// 버튼크기
        /// </summary>
        public SemanticUIHelper.Size Size { get { return _size; } set { _size = value; } }
        private bool _bCircle = true;
        public bool bCircle { get { return _bCircle; } set { _bCircle = value; } }

        private bool _bRange = false;
        public bool bRange { get { return _bRange; } set { _bRange = value; } }

        private bool _bPostSearch = false;
        /// <summary>
        /// 우편번호 검색 여부
        /// </summary>
        public bool bPostSearch { get { return _bPostSearch; } set { _bPostSearch = value; } }

        public bool _bScript = true;
        public bool bScript { get { return _bScript; } set { _bScript = value; } }
    }
    public class Daum_MapAddress
    {
        public string JIBUN_ADDRESS { get; set; }
        public string REGION { get; set; }
        public string ZIP_CODE { get; set; }
        public decimal? LATITUDE { get; set; }
        public decimal? LONGITUDE { get; set; }

        public string ROAD_ADDRESS { get; set; }
        
    }


    /// <summary>
    /// Html Editor 옵션사항
    /// </summary>
    public class TinyMCEOption
    {
        public string MAINFORM_ID { get; set; }
        public string ID { get; set; }
        public string CONTENT { get; set; }
        public string WIDTH { get; set; }
        public string HEIGHT { get; set; }
        public int[] templeate_Index { get; set; }

        public enLanguage LANGUAGE { get; set; }
    }

    public enum enLanguage
    {
        ko, en
    }
}
