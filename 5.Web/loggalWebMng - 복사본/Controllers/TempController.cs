using ALT.Framework.Mvc.Helpers;
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
        [Compress]
        public ActionResult Index()
        {
            return View();
        }
        [Compress]
        public ActionResult DaumMap()
        {
            return View();
        }
        [Compress]
        public ActionResult DaumMap2()
        {
            return View();
        }
        [Compress]
        public ActionResult GoogleMap()
        {
            return View();
        }
        [Compress]
        public ActionResult KakaoLogin()
        {
            return View();
        }
        [Compress]
        public ActionResult NaverLogin()
        {
            return View();
        }
        [Compress]
        public ActionResult NaverLogincallback()
        {
            return View();
        }
        [Compress]
        public ActionResult FacebookLogin()
        {
            return View();
        }
        [Compress]
        public ActionResult GoogleLogin()
        {
            return View();
        }
        [Compress]
        public ActionResult KakaoMap()
        {
            return View();
        }
        [Compress]
        public ActionResult KakaoMap2()
        {
            return View();
        }

        [Compress]
        public ActionResult DaumMapPolyGon()
        {
            return View();
        }
    }
}