using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.Framework.Mvc.Contoller;
using ALT.Framework.Mvc.Helpers;
using loggalWebMng.CommonCS;
using ALT.Framework.Mvc;
using ALT.BizService;
using ALT.VO.Common;
using loggalServiceBiz;
using System.IO;
using ALT.Framework;
using ALT.Framework.Data;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Runtime.Serialization;
using System.Net;
using System.Text;

namespace loggalWebMng.Controllers
{
    
    
    public class BaseController : MVCBaseController
    {
        /**
          * 이벤트 순서
          * 1) Initialize
          * 2) OnAuthorization
          * 3) OnActionExecuting
          * 4) 실제 Contoller
          * 5) OnActionExecuted
          */
		protected override void Initialize(System.Web.Routing.RequestContext requestContext)
		{

			//GlobalMvc.Util.BrowerCheckRedirect("http://m.nave.com/", "http://naver.com");
			base.Initialize(requestContext);

			string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
			string actionName = this.RouteData.Values["action"].ToString().ToLower();

			/// 5분간격으로 세션 재셋팅
			SessionHelper.LoginInfo.EDIT_MODE = enEditMode.WRITE;
			Response.Buffer = true;

			GlobalMvc.Common.HttpCheckRedirect();
		}

		protected override void OnAuthorization(AuthorizationContext filterContext)
		{
			base.OnAuthorization(filterContext);
		}
		


		protected override void OnException(ExceptionContext filterContext)
        {
			

			base.OnException(filterContext);
        }
		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			base.OnActionExecuting(filterContext);
            string data = base.GetParameter();
            if (!string.IsNullOrEmpty(data))
            {
                SessionHelper.LOG_PARAM = data;
            }

			
		}
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            string controllerName = this.RouteData.Values["controller"].ToString().ToLower();
            string actionName = this.RouteData.Values["action"].ToString().ToLower();

            #region >> 로그저장
            if (!(controllerName == "account" && (actionName == "dologin" || actionName == "dologout")))
            {
                LOGIN_WEBMENU menu = SessionHelper.LoginInfo.WebMemu.Where(w => w.MENU_URL.ToLower().Equals("/" + controllerName + "/" + actionName)).FirstOrDefault();
                if(menu != null)
                {
                    SessionHelper.LOG_NAME = menu.NAME;
                    SessionHelper.LoginInfo.CURRENT_MENU_NAME = menu.NAME;
                }
                if (!string.IsNullOrEmpty(SessionHelper.LOG_NAME))
                {
                    Service.commoneService.SaveLog(new T_LOG
                    {
                        STORE_CODE = SessionHelper.LoginInfo.STORE_CODE ?? 0,
                        LOG_TYPE = 2,
                        LOG_DATA1 = "/" + controllerName + "/" + actionName,
                        LOG_DATA2 = SessionHelper.LOG_NAME,
                        USE_IP = Request.UserHostAddress,
                        LOG_DESC = SessionHelper.LOG_PARAM,
                        INSERT_CODE = (SessionHelper.LoginInfo.MEMBER == null || SessionHelper.LoginInfo.MEMBER.MEMBER_CODE == null) ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE
                    });
                }
            }
            SessionHelper.LOG_NAME = "";
            SessionHelper.LOG_PARAM = "";
            #endregion >> 로그저장

            #region >> 세션아웃시 로그아웃 처리
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                if ((SessionHelper.LoginInfo.MEMBER == null || SessionHelper.LoginInfo.MEMBER.MEMBER_CODE == null || SessionHelper.LoginInfo.WebMemu == null) 
                    && SessionHelper.LoginNonCheckPageList.PAGE.Where(w => w.ToLower().Contains("/" + controllerName + "/" + actionName)).Count() == 0
                    && SessionHelper.LoginNonCheckPageList.PAGE.Where(w => w.ToLower().Contains("/" + controllerName + "/")).Count() == 0
                    && SessionHelper.LoginNonCheckPageList.ControllerNames.Where(w => w.ToLower() == controllerName ).Count() == 0
                    )
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
            }
            #endregion >> 세션아웃시 로그아웃 처리

            SessionHelper.SetSession();
        }

        /// <summary>
        /// 이미지업로드
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult UploadImage()
        {
            string url = GlobalMvc.FileInfo.FileUpload(Request.Files[0], DateTime.Now.ToString("yyyyMM"));
            string fileName = url.Split('|')[0];
            url = url.Split('|')[1];
            string full_url = System.Web.HttpContext.Current.Request.Url.Scheme + "://" + System.Web.HttpContext.Current.Request.Url.Authority + url;
            full_url = full_url.ConvertHttpsUrl();
            return new JsonResult { Data = new { url = full_url, filename = url.Split('|')[0] } };
        }


        /// <summary>
        /// 파일업로드
        /// </summary>
        /// <param name="FolderName"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public JsonResult FileUpload(FILE_COND Cond)
        {
            string msg = string.Empty;
            string folderName = Request["FolderName"] == null ? DateTime.Now.ToString("yyyyMM") : Request["FolderName"];
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                #region >> 글로벌 함수로 빼서 검증할 경우 file.contentlength 가 0 이 되는 현상이 있음
                int bLocalBoxImage = Convert.ToInt32(Request["bLocalBoxImage"] == null ? "0" : Request["bLocalBoxImage"]);
                if (bLocalBoxImage == 1)
                {
                    Size size = System.Drawing.Image.FromStream(file.InputStream).Size;
                    double dSize = Convert.ToDouble(size.Width) / Convert.ToDouble(size.Height);

                    if (!(dSize > 1.6 && dSize < 1.8))
                    {
                        throw new Exception("로컬박스 이미지는 1920(가로) * 1080(세로) 이미지를 등록하셔야 합니다. ");
                    }
                }
                #endregion
                FILE_INFO rtn = GlobalMvc.FileInfo.FileUpload2(file, folderName);
                return new JsonResult { Data = rtn };

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return new JsonResult { Data = new { FULL_URL = "", URL = "", FILE_NAME = "", FILE_EXT = "", return_msg = msg } };
            }
        }
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		protected override void EndExecute(IAsyncResult asyncResult)
		{
		
			base.EndExecute(asyncResult);
		}
		/* 
        [AltloggalAuthorization]
        public JsonResult FileUpload(FILE_COND Cond)
        {
            string msg = string.Empty;
            string folderName = Request["FolderName"] == null ? DateTime.Now.ToString("yyyyMM") : Request["FolderName"];
            try
            {
                HttpPostedFileBase file = Request.Files[0];

                string fileName = System.IO.Path.GetFileName(file.FileName);
                string targetFilename = DateTime.Now.ToString("yyyyMMddHHmmss") + System.IO.Path.GetExtension(file.FileName);
                bool bRtn = new FTPclient().Upload2(file.InputStream, targetFilename);


                FILE_INFO rtn = GlobalMvc.FileInfo.FileUpload2(file, folderName);
                return new JsonResult { Data = rtn };

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return new JsonResult { Data = new { FULL_URL = "", URL = "", FILE_NAME = "", FILE_EXT = "", return_msg = msg } };
            }
        }*/
	}
	
}

