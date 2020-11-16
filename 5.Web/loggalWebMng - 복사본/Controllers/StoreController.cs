using ALT.Framework.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.Framework.Data;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalServiceBiz;
using Newtonsoft.Json;
using loggalWebMng.CommonCS;
using System.Data;

namespace loggalWebMng.Controllers
{
    public class StoreController : BaseController
    {
        #region >> 업체별통계리스트 Start
        /// <summary>
        /// 업체별 통계리스트
        /// </summary>
        /// <returns></returns>
        [Compress]
        public ActionResult StoreStatisticsList(STORE_STATISTICS_COND Cond)
        {
            
            if (SessionHelper.LoginInfo.STORE_CODE != 1)
            {
                ViewBag.data =  new STORE_STATISTICS_COND()
                {
                    COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE
                    ,
                    COMPANY_NAME = SessionHelper.LoginInfo.STORE.COMPANY_NAME
                    ,
                    STORE_CODE = SessionHelper.LoginInfo.STORE_CODE
                        ,
                    STORE_NAME = SessionHelper.LoginInfo.STORE.STORE_NAME

                };
                
            }
            ViewBag.data = Cond ?? new STORE_STATISTICS_COND();
            return View();
        }
        /// <summary>
        /// 업체별통계리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public PartialViewResult pv_StoreStatisticsList(STORE_STATISTICS_COND Cond)
        {

            ViewBag.combolist = Request["combolist"] == null ? new List<T_COMMON>() : JsonConvert.DeserializeObject<List<T_COMMON>>(Request["combolist"]);
            ViewBag.list = new StoreService().GetStoreStatisticsList(Cond);

            return PartialView2();
        }

        [Compress]
        public ActionResult StoreStatisticsExcelReport(STORE_STATISTICS_COND Cond)
        {
            

            List<string> headers = new List<string>()
            { "업체명",  "로컬박스수", "실제갯수", "가상갯수", "1 Frame(박스)", "6 Frame(박스)", "내부배너", "외부배너", "내부+외부"
             , "배너수",  "1 Frame(배너)", "6 Frame(배너)", "모바일배너", "로컬박스배너", "모바일+로컬박스"  };

            Cond.PAGE = 1;
            Cond.PAGE_COUNT = 100000;

         

            return new StoreService().GetStoreStatisticsList(Cond).Select
                (s=> new {
                             s.STORE_NAME 
                           , s.LOCALBOX_CNT
                           , s.REAL_CNT
                           , s.VIRTUAL_CNT
                           , s.LOCALBOX_FRAME1_CNT
                           , s.LOCALBOX_FRAME6_CNT
                           , s.DEVICE_TYPE1
                           , s.DEVICE_TYPE2
                           , s.DEVICE_TYPE3
                           , s.AD_CNT
                           , s.AD_FRAME_TYPE1_CNT
                           , s.AD_FRAME_TYPE6_CNT
                           , s.AD_TYPE1
                           , s.AD_TYPE2
                           , s.AD_TYPE3

                    
                    }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);



            //return .Select(s => new
            //{
            //    DEVICE_CODE = s.DEVICE_CODE
            //   ,
            //    DEVICE_NAME = s.DEVICE_NAME
            //   ,
            //    STORE_NAME = s.STORE_NAME
            //   ,
            //    BUSI_TYPE_NAME = s.BUSI_TYPE_NAME
            //   ,
            //    BUSI_TYPE_NAME2 = s.BUSI_TYPE_NAME2
            //   ,
            //    CONTACT_NAME = s.CONTACT_NAME
            //   ,
            //    CONTACT_PHONE = s.CONTACT_PHONE
            //   ,
            //    AUTH_NUMBER = s.AUTH_NUMBER
            //   ,
            //    ADDRESS = s.ADDRESS
            //   ,
            //    UPDATE_NAME = s.UPDATE_NAME
            //   ,
            //    UPDATE_DATE = s.UPDATE_DATE
            //}).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
            //;
        }

        #endregion >> 업체별통계리스트 End
    }
}