using ALT.Framework;
using ALT.Framework.Data;
using ALT.Framework.Mvc;
using ALT.Framework.Mvc.Contoller;
using ALT.Framework.Mvc.Helpers;
using loggalWeb.CommonCS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWeb.Controllers
{
    public class BaseController : MVCBaseController
    { /**
        * 이벤트 순서
        * 1) Initialize
        * 2) OnAuthorization
        * 3) OnActionExecuting
        * 4) 실제 Contoller
        * 5) OnActionExecuted
        */
        [Compress]
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {

            //GlobalMvc.Util.BrowerCheckRedirect("http://m.nave.com/", "http://naver.com");
            base.Initialize(requestContext);

            string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
            string actionName = this.RouteData.Values["action"].ToString().ToLower();

            /// 5분간격으로 세션 재셋팅
            SessionHelper.SetSession();

            //if (Request["returnUrl"] != null)
            //{
            //     = Request["returnUrl"];
            //}
            //requestContext
            if (!Request.IsAjaxRequest())
            {
                if (SessionHelper.LoginInfo.MEMBER == null && SessionHelper.LoginNonCheckPageList.PAGE.Where(w => w.ToLower().Contains("/" + controllerName + "/" + actionName)).Count() == 0)
                {
                    Response.Redirect("/home/login");
                }
            }
        }

        [Compress]
        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            //if (filterContext.ExceptionHandled)
            //{
            //    return;
            //}
            //filterContext.Result = new ViewResult
            //{

            //    ViewName = "~/Views/Shared/Error.htm"
            //};
            //filterContext.ExceptionHandled = true;

        }

        [Compress]
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
            string actionName = this.RouteData.Values["action"].ToString().ToLower();
            if (Request.IsAjaxRequest())
            {
                #region >> 세션아웃시 로그아웃 처리
                if ((SessionHelper.LoginInfo.MEMBER == null || SessionHelper.LoginInfo.MEMBER.MEMBER_CODE == null) && SessionHelper.LoginNonCheckPageList.PAGE.Where(w => w.ToLower().Contains("/" + controllerName + "/" + actionName)).Count() == 0)
                {
                    filterContext.Result = new JsonResult
                    {
                        Data = new
                        {
                            logout = true,
                            // put whatever data you want which will be sent
                            // to the client
                            ERROR_MESSAGE = "sorry, but you were logged out"
                        },
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                #endregion >> 세션아웃시 로그아웃 처리
            }
            else if(!(controllerName == "home" && actionName =="login"))
            {
                SessionHelper.returnUrl = Request.Url.AbsolutePath; 
                
            }
        }

        [Compress]
        public ActionResult UploadImage()
        {
            string url = GlobalMvc.FileInfo.FileUpload(Request.Files[0], DateTime.Now.ToString("yyyyMM"));
            string fileName = url.Split('|')[0];
            url = url.Split('|')[1];
            string full_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + url;
            full_url = full_url.ConvertHttpsUrl();
            return new JsonResult { Data = new { url = full_url, filename = url.Split('|')[0] } };
        }

        [Compress]
        public JsonResult FileUpload(string FolderName)
        {
            string msg = string.Empty;
            string folderName = Request["FolderName"] == null ? "" : Request["FolderName"];

            try
            {
                string url = GlobalMvc.FileInfo.FileUpload(Request.Files[0], folderName);
                string fileName = url.Split('|')[0];
                url = url.Split('|')[1];
                string full_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + url;
                full_url = full_url.ConvertHttpsUrl();
                return new JsonResult { Data = new { FULL_URL = full_url, URL = url, FILE_NAME = fileName, return_msg = msg } };
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return new JsonResult { Data = new { FULL_URL = "", URL = "", FILE_NAME = "", return_msg = msg } };
            }
        }
    }
}