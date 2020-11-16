using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

using System.Xml.Linq;



using ALT.Framework;
using ALT.VO.Common;


namespace ALT.Framework.Mvc.Helpers
{
    public static class MVCHelper
    {

        /// <summary>
        /// View Page 언어 변환
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="root">xml 로드</param>
        /// <param name="CodeID">언어코드</param>
        /// <param name="langtype">언어유형(ko, en....)</param>
        /// <param name="abbr">약어 여부</param>
        /// <returns></returns>
        /// 
        public static MvcHtmlString Language(this HtmlHelper helper, string CodeID, bool abbr = false, string rootUrl = null, string defaultTitle = null)
        {
            XElement root;
            
            string sControllerName = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            string sActionName = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            string slang = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["lang"].ToString();
            if (string.IsNullOrEmpty(rootUrl))
            {

                root = XElement.Load(System.Web.HttpContext.Current.Server.MapPath("/views/" + sControllerName + "/language/" + sActionName + ".xml"));
            }
            else
            {
                root = XElement.Load(System.Web.HttpContext.Current.Server.MapPath(rootUrl));
            }

            string sVal = string.Empty;
            IEnumerable<XElement> langs =
            from el in root.Elements("Code")
            where (string)el.Attribute("CodeId") == CodeID
            select el;


            string langtype = slang;

            if (langtype.Length != 2)
            {
                langtype = "en";
            }
            if (langs.Count() > 0)
            {
                if (abbr)
                    sVal = "<abbr title=\"" + langs.First().Element(langtype).Value.Trim() + "\" >" + langs.First().Element(langtype).Element("abbr").Value.Trim() + "</abbr>";
                else
                    sVal = System.Web.HttpUtility.HtmlEncode(langs.First().Element(langtype).FirstNode.ToString().Trim());
            }
            else
            {
                return new MvcHtmlString(defaultTitle);
            }
            return new MvcHtmlString(sVal.Trim());
        }

        public static MvcHtmlString LanguageCommon(this HtmlHelper helper, string CodeID, bool abbr = false, string defaultTitle = null)
        {
            return new MvcHtmlString(Language(CodeID, abbr, "/views/common/language/common.xml", defaultTitle));
        }

        public static string Language(string CodeID, bool abbr = false, string rootUrl = null, string defaultTitle = null)
        {

            string sControllerName = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            string sActionName = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            string slang = System.Web.HttpContext.Current.Request.RequestContext.RouteData.Values["lang"].ToString();
            XElement root;
            
            if (string.IsNullOrEmpty(rootUrl))
            {

                root = XElement.Load(System.Web.HttpContext.Current.Server.MapPath("/views/" + sControllerName + "/language/" + sActionName + ".xml"));
            }
            else
            {
                root = XElement.Load(System.Web.HttpContext.Current.Server.MapPath(rootUrl));
            }

            string sVal = string.Empty;
            IEnumerable<XElement> langs =
            from el in root.Elements("Code")
            where (string)el.Attribute("CodeId") == CodeID
            select el;


            string langtype = slang;
            if (langtype.Length != 2)
            {
                langtype = "en";
            }
            if (langs.Count() > 0)
            {
                if (abbr)
                    sVal = "<abbr title=\"" + langs.First().Element(langtype).Value.Trim() + "\" >" + langs.First().Element(langtype).Element("abbr").Value.Trim() + "</abbr>";
                else
                    sVal = langs.First().Element(langtype).FirstNode.ToString().Trim();
            }
            else
            {
                sVal = defaultTitle;
            }
            return sVal.Trim();
        }

        public static string LanguageCommon(string CodeID, bool abbr = false, string defaultTitle = null)
        {
            return Language(CodeID, abbr, "/views/common/language/common.xml", defaultTitle);
        }




        /// <summary>
        /// 문자열 변환/객체가 Null 일 경우 공백을 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted text</returns>
        public static string SafeString(object origin)
        {
            if (origin == null)
            {
                return "";
            }
            else
            {
                return origin.ToString();
            }
        }

        /// <summary>
        /// 정수 변환/객체가 Null 일 경우 0을 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted integer</returns>
        public static int SafeInt(object origin)
        {
            try
            {
                if (origin == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(origin);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 정수 변환/객체가 Null 일 경우 0을 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted integer</returns>
        public static long SafeInt64(object origin)
        {
            if (origin == null || origin.Equals(""))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(origin);
            }
        }

        /// <summary>
        /// 실수 변환/객체가 Null 일 경우 0을 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted decimal</returns>
        public static decimal SafeDecimal(object origin)
        {
            try
            {
                if (origin == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDecimal(origin);
                }
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 실수 변환/객체가 Null 일 경우 0을 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted double</returns>
        public static double SafeDouble(object origin)
        {
            if (origin == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(origin);
            }
        }

        /// <summary>
        /// 날짜 변환/객체가 Null 일 경우 현재날짜를 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted datetime</returns>
        public static DateTime SafeDatetime(object origin)
        {
            if (origin == null)
            {
                return DateTime.Now;
            }
            else
            {
                return Convert.ToDateTime(origin);
            }
        }


        /// <summary>
        /// Bool 변환/객체가 Null 일 경우 false를 반환
        /// </summary>
        /// <param name="orgin">Original value</param>
        /// <returns>Returns converted datetime</returns>
        public static bool SafeBool(object origin)
        {
            if (origin == null || origin.ToString() == "")
            {
                return false;
            }
            else
            {
                if (origin.ToString() == "1" || origin.ToString().ToLower() == "true")
                    return true;
                else
                    return false;
            }
        }


        public static List<SelectListItem> GetTimeZoneCombo(double selectMin )
        {
            DateTime utcDateTime = DateTime.Now.ToUniversalTime();

            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var data in TimeZoneInfo.GetSystemTimeZones())
            {
                SelectListItem item = new SelectListItem();

                
                double totMin = data.BaseUtcOffset.TotalMinutes;
                if (totMin == selectMin)
                {
                    item.Selected = true;
                }
                string name = data.DisplayName;

                item.Value = totMin.ToString();
                item.Text = name;

                list.Add(item);
            }
            return list;
        }

		
    }
}
