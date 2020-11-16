using ALT.BizService;
using ALT.Framework.Mvc.Helpers;
using loggalWebMng.CommonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class TempController : BaseController
    {
        // GET: Temp
        [AltloggalAuthorization]
        public ActionResult Index()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult DaumMap()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult DaumMap2()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult GoogleMap()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult KakaoLogin()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult NaverLogin()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult NaverLogincallback()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult FacebookLogin()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult GoogleLogin()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult KakaoMap()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult KakaoMap2()
        {
            return View();
        }

        [AltloggalAuthorization]
        public ActionResult DaumMapPolyGon()
        {
            return View();
        }

		public ActionResult TableDesc()
		{
			ViewBag.htmlList = new CommonService().GetTableDesc();
			return View();
		}

		public ActionResult ShoppingCart(){

			return View();
		}
    }
}