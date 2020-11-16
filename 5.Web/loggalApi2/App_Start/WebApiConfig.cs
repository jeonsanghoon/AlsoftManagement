using Microsoft.AspNet.WebApi.Extensions.Compression.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Extensions.Compression.Core.Compressors;
using System.Web.Http;

namespace loggalApi2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 구성 및 서비스

            // Web API 경로
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
            GlobalConfiguration.Configuration.MessageHandlers.Insert(0, new ServerCompressionHandler(new System.Net.Http.Extensions.Compression.Core.Compressors.GZipCompressor(), new DeflateCompressor()));
        }
    }
}
