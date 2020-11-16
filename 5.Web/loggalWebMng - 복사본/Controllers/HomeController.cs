using ALT.Framework.Mvc.Helpers;
using loggalWebMng.CommonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        [Compress]
        public ActionResult Index()
        {
            int? companyCode = SessionHelper.LoginInfo.COMPANY_CODE;

            if (SessionHelper.LoginInfo.COMPANY_CODE != 1)
            {
                ViewBag.chart1List = new loggalServiceBiz.DeviceService().GetApprovalGrape(new ALT.VO.loggal.APPROVAL_GRAPE_COND { COMPANY_CODE = companyCode });
                ViewBag.chart2List = new loggalServiceBiz.DeviceService().GetApprovalGrape(new ALT.VO.loggal.APPROVAL_GRAPE_COND { MNG_COMPANY_CODE = companyCode });
            }
            else
            {
                ViewBag.chart1List = new loggalServiceBiz.DeviceService().GetApprovalGrape(new ALT.VO.loggal.APPROVAL_GRAPE_COND { MNG_COMPANY_CODE = companyCode });
                ViewBag.chart2List = new loggalServiceBiz.DeviceService().GetApprovalGrape(new ALT.VO.loggal.APPROVAL_GRAPE_COND {  });
            }

            
            return View();
        }
        [Compress]
        public ActionResult Login(string id)
        {
            id = string.IsNullOrEmpty(id) ? "" : id;

            id = Server.UrlDecode(id);
            ViewBag.id = id;
            return View();
        }
        [Compress]
        public ActionResult index2()
        {
            return View();
        }
    }
}