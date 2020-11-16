using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using loggalServiceBiz;
using ALT.VO.loggal;
using ALT.VO.Common;
using ALT.Framework.Mvc.Helpers;
using loggalWeb.CommonCS;

namespace loggalWeb.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [Compress]
        public ActionResult Index()
        {
            return View();
        }

        [Compress]
        public JsonResult getKeywordAutoCompleate(KEYWORD_COND Cond)
        {
            return new JsonResult { Data = new KeywordService().GetKeywordKoreanList(Cond) };
        }

        [Compress]
        public PartialViewResult PV_KeywordAutoList(KEYWORD_COND Cond)
        {
            ViewBag.Cond = Cond;
           ViewBag.list= new KeywordService().GetKeywordKoreanList(Cond);
            return PartialView();
        }


        [Compress]
        public PartialViewResult PV_LocalNameList(CODE_DATA Cond)
        {
            ViewBag.Cond = Cond;
            ViewBag.list = new KeywordService().GetLocalNameList(Cond);
            return PartialView();
        }


        [Compress]
        public ActionResult Login(string returnUrl)
        {
            SessionHelper.returnUrl = returnUrl;
            return View();
        }
    }
}