﻿using ALT.Framework.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class PopupController : Controller
    {
        // GET: Popup
        [Compress]
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult DeviceListP()
        //{
        //    return View();
        //}
    }
}