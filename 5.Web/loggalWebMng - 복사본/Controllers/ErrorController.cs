using ALT.Framework.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class ErrorController : Controller
    {
        [Compress]
        public ActionResult ErrorPage(int? id)
        {
            Response.StatusCode = Convert.ToInt32((id== null) ? 404 : id);
           
            return View();
        }
    }
}