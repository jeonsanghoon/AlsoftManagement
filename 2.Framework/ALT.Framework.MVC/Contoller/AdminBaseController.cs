using System;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using System.Web.Profile;
using System.Web.Routing;

namespace ALT.Framework.Mvc.Contoller
{
    public class AdminBaseController : MVCBaseController
    {
        protected AdminBaseController()
        {
        }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }
    }
}
