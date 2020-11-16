using ALT.Framework.Mvc.Helpers;
using ALT.VO.loggal;
using loggalServiceBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWeb.Controllers
{
    public class SearchController : Controller
    {
        [Compress]
        public JsonResult KeywordList(string q,string type)
        {
           IList<CODE_DATA> list = new KeywordService().GetKeywordKoreanList(new ALT.VO.loggal.KEYWORD_COND { KEYWORD_TYPE = type, KEYWORD_NAME = q });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}