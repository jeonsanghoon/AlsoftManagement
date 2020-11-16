using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.Framework.Data;
using ALT.Framework.Mvc.Contoller;
using OnlineServiceBiz;
using ALT.VO.Common;
using ALT.Framework.Mvc.Helpers;
using ALT.Framework;

using ALT.Framework.Mvc.Data;
using ALT.Framework.Mvc;
using OnlineServiceWeb.CommonCS;

namespace OnlineServiceWeb.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        /// <summary>
        /// 첫로딩페이지
        /// </summary>
        /// <param name="id">Store Code</param>
        /// <returns></returns>
        public ActionResult Index(string id)
        {

            IList<T_MEMBER> list = new HomePageService().GetMemberList(new T_MEMBER { USER_ID = "admin" });

            id = string.IsNullOrEmpty(id) ? "1" : "";
            int storeCode = (int)(id.isNumeric() ? Convert.ToInt32(id) : 1);



            IList<T_STORE> StoreList = new HomePageService().GetStoreList(new T_STORE_COND { STORE_CODE = storeCode });

            if (StoreList.Count() == 1)
            {
                SessionHelper.LoginInfo.STORE = StoreList.FirstOrDefault();
                if (!string.IsNullOrEmpty(SessionHelper.LoginInfo.STORE.CULTURE_NAME))
                    SessionHelper.LoginInfo.CultureInfo = new CULTURE_INFO(SessionHelper.LoginInfo.STORE.CULTURE_NAME);
                else
                    SessionHelper.LoginInfo.CultureInfo = new CULTURE_INFO(Global.ConfigInfo.DefaultCultureName);

                SessionHelper.LoginInfo.COMPANY_ID = SessionHelper.LoginInfo.STORE.COMPANY_ID;
                SessionHelper.LoginInfo.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;

            }
            SessionHelper.LoginInfo.StoreImageList = new HomePageService().GetStoreImageList(new T_STORE_IMAGE { STORE_CODE = storeCode, SERVICE_TYPE = 1, HIDE = false });


            // SessionHelper.LoginInfo.MEMBER = new HomePageService().GetMemberList(new T_MEMBER {USER_ID="mem01" }).FirstOrDefault();
            return View();
        }


        public ActionResult DrawView(string path)
        {
            path = (string.IsNullOrEmpty(path) ? "" : path).ToLower();
            if (path.Contains("home.cshtml"))
            {
                return this.HomeSlider(path);
            }
            else if (path.Contains("menu.cshtml"))
            {
                return this.Menu(path);
            }
            else if (path.Contains("contact.cshtml"))
            {
                return this.Contact(path);
            }
            return View(path);
        }

        public ActionResult HomeSlider(string path)
        {

            return View(path);
        }

        public ActionResult Menu(string path)
        {
            ViewBag.ItemGroupList = new HomePageService().GetItemGroupList(new T_ITEM_GROUP_COND { STORE_CODE = SessionHelper.LoginInfo.STORE_CODE, HIDE = false });
            ViewBag.ItemList = new HomePageService().GetItemList(new T_ITEM_COND { STORE_CODE = SessionHelper.LoginInfo.STORE_CODE, HIDE = false });
            return View(path);
        }

        public ActionResult Contact(string path)
        {
            ViewBag.SearchDate = DateTime.Now.ToString("yyyyMMdd");
            ViewBag.BusinessHourList = new HomePageService().GetGetStoreBusinessHourList(new T_STORE_BUSINESSHOURS_COND { STORE_CODE = SessionHelper.LoginInfo.STORE_CODE, SEARCH_DATE = ViewBag.SearchDate });
            return View(path);
        }

        public JsonResult StoreContactSave(T_STORE_CONTACT param)
        {
            param.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;
            string msg = new HomePageService().StoreContactSave(param);

            new MailHelper().SendMail(new MAILINFO()
            {
                ACCEPT_ID = param.EMAIL + ";" + SessionHelper.LoginInfo.STORE.EMAIL
                //  ,SENDER_ID="jsh0147@naver.com"
                ,
                SENDER_NAME = SessionHelper.LoginInfo.STORE.STORE_NAME
                ,
                SUBJECT = param.TITLE
                ,
                CONTENT = param.CONTENT

            });
            return new JsonResult { Data = new { error_message = msg } };
        }

        public JsonResult StoreReservationSave(T_STORE_RESERVATION param)
        {
            param.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;
            string msg = new HomePageService().StoreReservationSave(param);


            if (string.IsNullOrEmpty(msg))
            {
                new MailHelper().SendMail(new MAILINFO()
                {
                    ACCEPT_ID = param.EMAIL + ";" + SessionHelper.LoginInfo.STORE.EMAIL
                  //  ,SENDER_ID="jsh0147@naver.com"
                  ,
                    SENDER_NAME = SessionHelper.LoginInfo.STORE.STORE_NAME
                  ,
                    SUBJECT = "[예약메일] - (" + param.REQUEST_DATE + ")" + param.NAME + "님이 예약을 요청하였습니다."
                  ,
                    CONTENT = param.CONTENT
                });
            }

            return new JsonResult { Data = new { error_message = msg } };
        }


       
    }
}