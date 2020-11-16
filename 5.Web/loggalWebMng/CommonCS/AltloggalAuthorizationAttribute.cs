using ALT.Framework.Mvc.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
namespace loggalWebMng.CommonCS
{
	public class AltloggalAuthorizationAttribute : CompressAttribute
	{

		public override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			string controllerName = filterContext.RouteData.Values["controller"].ToString().ToLower();
			string actionName = filterContext.RouteData.Values["action"].ToString().ToLower();
			if ((SessionHelper.LoginInfo.MEMBER == null || SessionHelper.LoginInfo.MEMBER.MEMBER_CODE == null)
				&& SessionHelper.LoginNonCheckPageList.PAGE.Where(w => ("/" + controllerName + "/" + actionName).Contains(w.ToLower())).Count() == 0
				&& SessionHelper.LoginNonCheckPageList.ControllerNames.Where(w => w.ToLower() == controllerName).Count() == 0
			)
			{
				if (filterContext.HttpContext.Request.IsAjaxRequest())
				{
					filterContext.Result = new JsonResult
					{
						Data = new
						{
							logout = true,
							// put whatever data you want which will be sent
							// to the client
							ERROR_MESSAGE = "죄송합니다. 당신은 로그아웃되었습니다."
						},
						JsonRequestBehavior = JsonRequestBehavior.AllowGet
					};
				}
				else
					filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "home", action = "login", message = ((controllerName == "home" && actionName == "index") ? "" : "sessionout") }));
			}
			base.OnActionExecuting(filterContext);
		}
	}
}