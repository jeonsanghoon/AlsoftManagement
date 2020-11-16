using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using ALT.VO.Common;
using System.Web.Routing;

namespace ALT.Framework.Mvc
{
    public class Common
    {
        #region >> 공통코드 콤보

        /// <summary>
        /// 공통코드 콤보 호출
        /// </summary>
        /// <param name="Cond"></param>
        /// <param name="selectData"></param>
        /// <param name="bBlank"></param>
        /// <param name="bEncode"></param>
        /// <returns></returns>
        //public List<SelectListItem> GetCommcdeCombo(T_COMMON Cond, string selectData = "", bool bBlank = false, bool bEncode = false, string blankName = "선택")
        //{
        //    BaseSr.BaseServiceClient client = new BaseSr.BaseServiceClient();
        //    Cond.USEYN = "Y";
        //    string sJson = JsonConvert.SerializeObject(Cond);
        //    IList<T_COMMON> list = JsonConvert.DeserializeObject<IList<T_COMMON>>(client.GetCommCode(sJson));
        //    return GetCombo(list, selectData, bEncode, bBlank, blankName);
        //}

        //public List<SelectListItem> GetCommcdeCombo(T_COMMON Cond, bool USEYN_all, string selectData = "", bool bBlank = false, bool bEncode = false, string blankName = "선택")
        //{
        //    BaseSr.BaseServiceClient client = new BaseSr.BaseServiceClient();
        //    if (!USEYN_all)
        //        Cond.USEYN = "Y";
        //    string sJson = JsonConvert.SerializeObject(Cond);
        //    IList<T_COMMON> list = JsonConvert.DeserializeObject<IList<T_COMMON>>(client.GetCommCode(sJson));
        //    return GetCombo(list, selectData, bEncode, bBlank, blankName);
        //}
        #endregion

        #region >> 콤보박스 셋팅
        /// <summary>
        /// 콤보박스 셋팅
        /// </summary>
        /// <param name="list"></param>
        /// <param name="selectedData"></param>
        /// <param name="bEncode"></param>
        /// <param name="bBlank"></param>
        /// <returns></returns>
        private List<SelectListItem> GetCombo(IList<T_COMMON> list, string selectedData, bool bEncode = false, bool bBlank = false, string blankName = "선택")
        {
            List<SelectListItem> combo = new List<SelectListItem>();

            // this.GetCommcdeCombo(new COMMCODE() { MAINCODE = "" }, bBlank: false);
            SelectListItem data;
            int nRow = 0;
            if (bBlank)
            {
                data = new SelectListItem();
                data.Value = "";
                data.Text = blankName;
                if (string.IsNullOrEmpty(selectedData))
                    data.Selected = true;
                combo.Add(data);
                nRow++;
            }
            foreach (T_COMMON data1 in list)
            {

                combo.Add(new SelectListItem()
                {
                    Value = data1.SUB_CODE.ToString(),
                    Text = (bEncode) ? HttpUtility.HtmlEncode(data1.NAME) : HttpUtility.HtmlDecode(data1.NAME),
                    Selected = (nRow == 0 && string.IsNullOrEmpty(selectedData)) ? true : ((selectedData == data1.SUB_CODE.ToString()) ? true : false)
                });
                nRow++;
            }
            return combo;
        }
        #endregion


        public string RenderPartialViewToString(Controller controller, string viewName, object model = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                viewName = controller.ControllerContext.RouteData.GetRequiredString("action");
            }

            controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                // Find the partial view by its name and the current controller context.
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);

                // Create a view context.
                var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);

                // Render the view using the StringWriter object.
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        /* Get Route /controller/action */
        public RouteValueDictionary GetRouteValues()
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values;
        }


        /// <summary>
        /// http를 https 로 url 전환
        /// </summary>
        /// <param name="res"></param>
        /// <param name="redirectUrl"></param>
        public void HttpCheckRedirect( string redirectUrl = null)
        {
            HttpRequest res =  HttpContext.Current.Request;
            string scheme = (res.Url.Host.ToLower().Contains("localhost")) ? res.Url.Scheme : "https";

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = scheme + "://" + res.Url.Authority.ToLower() + redirectUrl;
            }
            else if (!res.Url.Host.ToLower().Contains("localhost") && res.Url.Scheme == "http")
            {
                redirectUrl = res.Url.OriginalString.ToLower().Replace("http://", "https://");
            }

            if (!string.IsNullOrEmpty(redirectUrl)) redirectUrl = redirectUrl.Replace("//www.", "//");
            else if (res.Url.OriginalString.ToLower().Contains("//www."))
            {
                redirectUrl = res.Url.OriginalString.Replace("//www.", "//");
            }
            else { redirectUrl = ""; }

            if (!string.IsNullOrEmpty(redirectUrl)) HttpContext.Current.Response.Redirect(redirectUrl.Replace(":80","").Replace(":433",""));
        }
    }
}
