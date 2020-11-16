using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ALT.Framework.Data;
using System.Web.Http.Controllers;
using ALT.Framework.Mvc.Helpers;

namespace loggalApi2.Controllers
{

    public class BaseController : ApiController
    {
        protected readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public BaseController() {
           // logger.Debug("테스트");
        }
        [AltAuthorizationFilter]
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            string controllerName = controllerContext.RouteData.Values["controller"].ToString().ToLower();
        
            string actionName = controllerContext.RouteData.Values["action"].ToString().ToLower();
            logger.Debug("/" + controllerName + "/" + actionName );
            try
            {
                base.Initialize(controllerContext);
            }
            catch (Exception ex)
            {
                logger.Debug("/" + controllerName + "/" + actionName + " : " + ex.Message);
            }
            
        }
		
    }
}
