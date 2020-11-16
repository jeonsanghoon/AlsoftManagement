using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Profile;
using System.Web.Routing;

using Newtonsoft.Json;
using log4net;
using log4net.Config;

using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using OnlineServiceBiz;


namespace ALT.Framework.Mvc.Contoller
{

  
    public class MVCBaseController : System.Web.Mvc.Controller
    {
        /**
        * 이벤트 순서
        * 1) Initialize
        * 2) OnAuthorization
        * 3) OnActionExecuting
        * 4) 실제 Contoller
        * 5) OnActionExecuted
        */
       
        protected readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static bool IgnoreCertificateErrorHandler(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            var certificate = (X509Certificate2)cert;
            
            return true;
        }

        public MVCBaseController()
        {
            ServicePointManager.ServerCertificateValidationCallback += IgnoreCertificateErrorHandler;
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(IgnoreCertificateErrorHandler); 
        }

        
        public PartialViewResult PartialView2()
        {
            return PartialView2(null,null);
        }

        public PartialViewResult PartialView2(string viewName)
        {
            return PartialView2(viewName, null);
        }
        
        public PartialViewResult PartialView2(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = "~/Views/" + this.ControllerContext.RouteData.Values["controller"] + "/Partial/" + this.ControllerContext.RouteData.Values["action"] + ".cshtml";
            return PartialView(viewName, model);
        }


        public PartialViewResult PartialCombo(object model = null)
        {
            string viewName = "~/Views/base/Partial/PV_Combo.cshtml";
            return PartialView(viewName, model);
        }


        protected string GetParameter()
        {
            
            string data = string.Empty;
            if (Request.RequestContext.RouteData.Values["id"] != null)
            {
                data += "id=" + Request.RequestContext.RouteData.Values["id"] + " || ";
            }
            var jsonString = String.Empty;
            Request.InputStream.Position = 0;
        
            using (var inputStream = new StreamReader(Request.InputStream))
            {
                //jsonString = inputStream.ReadToEnd();
                string line;
                while ((line = inputStream.ReadLine()) != null)
                    jsonString += line;
                inputStream.DiscardBufferedData();
                inputStream.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
            }
            Request.InputStream.Position = 0;
            
            data += "JsonString=" + jsonString + " || ";
            data += "QueryString=" + Request.RequestContext.HttpContext.Request.QueryString.ToString();
       
            return data;

        }
        [Compress]
        public JsonResult SetSession<T>(string name, T data)
        {
            Session[name] = data;
            return new JsonResult { Data = "ok" };
        }
        [Compress]
        public JsonResult GetSession(string name)
        {
            RTN_SAVE_DATA data = new RTN_SAVE_DATA();
            data.DATA = JsonConvert.SerializeObject(Session[name]);
            return new JsonResult { Data = data };
        }
    }
}
