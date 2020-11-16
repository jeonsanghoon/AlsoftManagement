
using ALT.VO.loggal;
using loggalServiceBiz;
using loggalWeb.CommonCS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.Framework.Mvc;
using ALT.Framework.Mvc.Helpers;

namespace loggalWeb.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        [Compress]
        public ActionResult KendoUI()
        {
            return View();
        }

        [Compress]
        public ActionResult TinyMCESample()
        {
            return View(new EditTempleateModel
            {
                TinyMCE_Editor = "This editor instance is using the 'tinymce_jquery_basic_compressed' template.",
            });
        }




        public class keyClassCond
        {
            public string id { get; set; }
            public string name { get; set; }
            public string q { get; set; }
            public string callback { get; set; }
        }

        public class RtnkeyClass
        {
            public string callback { get; set; }
        }
        public class keyClassList
        {
            public string id { get; set; }
            public string name { get; set; }
        }



        
        public ActionResult keyList(keyClassCond Cond)
        {


           ViewBag.data = Cond.callback + "(" + JsonConvert.SerializeObject(new List<keyClassList> { new keyClassList { id = "1111", name = "111111" }, new keyClassList { id = "22222", name = "222222" } }) + ")";
            return View();
        }


     

        public ActionResult TagInput()
        {
            return View();
        }

        public ActionResult MrcDatagrid()
        {
            return View();
        }
    }
}