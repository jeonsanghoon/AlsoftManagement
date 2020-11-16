using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineServiceWeb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "Properties",
             url: "{comp}/{lang}/{controller}/{action}/{id}",
             defaults: new
             {
                 comp = "altsoft",
                 lang = "en",
                 controller = "home",
                 action = "index",
                 id = UrlParameter.Optional
             }
         );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
