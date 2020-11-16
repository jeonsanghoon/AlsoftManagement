using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace loggalWebMng
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Web.config")));
        }
        protected void Application_BeginRequest()
        {

            if (!Request.Url.AbsoluteUri.Contains("localhost") )
            {
                if(Request.Url.AbsoluteUri.Contains("http://")) Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            }
        }

    }
}
