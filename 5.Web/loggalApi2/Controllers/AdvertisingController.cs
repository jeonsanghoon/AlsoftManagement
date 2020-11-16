using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ALT.VO.loggal;
using loggalServiceBiz;
using Newtonsoft.Json;
using ALT.VO.Common;
using ALT.Framework.Mvc.Helpers;
using ALT.Framework;

namespace loggalApi2.Controllers
{
    public class AdvertisingController : BaseController
    {

        #region >> Post 방식
        /// <summary>
        /// 카테고리정보 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<CATEGORY_LIST> GetCategoryList([FromBody]CATEGORY_COND Cond)
        {
            IList<CATEGORY_LIST> list = new CategoryService().GetCategoryList(Cond);
            return list;
        }

        /// <summary>
        /// 광고리스트 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<MOBILE_AD_LIST> GetAdList([FromBody]AD_SEARCH_COND Cond)
        {
			Cond.LATITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LAT) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LAT)) : Cond.LATITUDE;
            Cond.LONGITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LONG) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LONG)) : Cond.LONGITUDE;

            IList<MOBILE_AD_LIST> list = new CategoryService().GetAdList(Cond);
            return list;
        }
        /// <summary>
        /// 디바이스/광고 리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public AD_DEVICE_MOBILE_M GetMobileAdDeviceList([FromBody]AD_DEVICE_MOBILE_COND Cond)
        {
            Cond.USER_ID = Global.SecurityInfo.Decrypt_data(Cond.USER_ID);
            return new CategoryService().GetMobileAdDeviceList(Cond);
        }

        /// <summary>
        /// 검색어 자동완성 데이터 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<CODE_DATA> GetKeywordAutoCompleateList([FromBody]KEYWORD_COND Cond)
        {
            return new KeywordService().GetKeywordKoreanList(Cond);
        }
        /// <summary>
        /// 지역별 자동완성 리스트 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<CODE_DATA> GetLocalNameList([FromBody] CODE_DATA Cond)
        {
            return new KeywordService().GetLocalNameList(Cond);
        }

        /// <summary>
        /// 카테고리 키워드 리스트 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<CODE_DATA> GetCategoryKeywordList([FromBody]CATEGORY_KEYWORD_COND Cond)
        {
            return new KeywordService().GetCategoryKeywordList(Cond);
        }

        /// <summary>
        /// 광고클릭시 체크 및 클릭수 저장(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public RTN_SAVE_DATA AD_OpenPage([FromBody]long adCode)
        {
            return new AdvertisingService().AD_OpenPageForMobile(adCode);
        }

        #endregion
        /// <summary>
        /// 카테고리정보 가져오기(Get방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public IList<CATEGORY_LIST> GetCategoryList2([FromUri]CATEGORY_COND Cond)
        {

            IList<CATEGORY_LIST> list = new CategoryService().GetCategoryList(Cond);
            return list;
        }

        /// <summary>
        /// 광고리스트 가져오기(Get방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public IList<MOBILE_AD_LIST> GetAdList2([FromUri]AD_SEARCH_COND Cond)
        {
            IList<MOBILE_AD_LIST> list = new CategoryService().GetAdList(Cond);

            return list;
        }

        /// <summary>
        /// 검색어 자동완성 데이터 가져오기(Get방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public IList<CODE_DATA> GetKeywordAutoCompleateList2([FromUri]KEYWORD_COND Cond)
        {
            return new KeywordService().GetKeywordKoreanList(Cond);
        }


        /// <summary>
        /// 검색어 자동완성 데이터 가져오기(Get방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public IList<CODE_DATA> GetLocalNameList2([FromUri]CODE_DATA Cond)
        {
            return new KeywordService().GetLocalNameList(Cond);
        }

        /// <summary>
        /// 검색어 자동완성 데이터 가져오기(Get방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public IList<CODE_DATA> GetCategoryKeywordList2([FromUri]CATEGORY_KEYWORD_COND Cond)
        {
            return new KeywordService().GetCategoryKeywordList(Cond);
        }

        [HttpPost]
        [DeflateCompression]
        public MOBILE_AD_DETAIL_DATA GetMobileAdDetail(MOBILE_AD_DETAIL_COND Cond)
        {
            return new AdvertisingService().GetMobileAdDetail(Cond);
        }

        [HttpPost]
        [DeflateCompression]
        public List<MOBILE_AD_SEARCH_DATA> GetMobileAdSearchList(MOBILE_AD_SEARCH_COND Cond)
        {
            Cond.USER_ID = Global.SecurityInfo.Decrypt_data(Cond.USER_ID);
            Cond.LATITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LAT) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LAT)) : Cond.LATITUDE;
            Cond.LONGITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LONG) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LONG)) : Cond.LONGITUDE;
            return new AdvertisingService().GetMobileAdSearchList(Cond);
        }


        [HttpPost]
        [DeflateCompression]
        public List<T_AD_BEACON> GetAdBeconList(T_AD_BEACON_COND Cond)
        {
            return new BeaconService().GetAdBeaconList(Cond);
        }

        public string getSt(int? id =1) {
            return "getSt" + id.ToString();
        }

    }
}
