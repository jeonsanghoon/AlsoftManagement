using ALT.VO.Common;
using ALT.VO.loggal;
using loggalServiceBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class ShareController : BaseController
    {
        // GET: Share
        public ActionResult BannerShareList()
        {
            ViewBag.gubun = "banner";

            return View("ShareList");
        }
        public ActionResult LocalboxShareList()
        {
            ViewBag.gubun = "localbox";

            return View("ShareList");
        }
        public PartialViewResult pv_shareList(T_SHARE_COND Param)
        {
            ViewBag.list = new ShareService().GetShareList(Param);
            return PartialView2();
        }

        public JsonResult shareSave(T_SHARE list)
        {
            RTN_SAVE_DATA rtn = new ShareService().ShareSave(list);
            return new JsonResult { Data = rtn };
        }
    }
}