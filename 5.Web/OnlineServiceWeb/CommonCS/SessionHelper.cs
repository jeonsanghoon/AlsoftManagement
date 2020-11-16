using ALT.Framework;
using ALT.Framework.Mvc;
using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineServiceWeb.CommonCS
{
    /// <summary>
    /// 세션 클래스
    /// </summary>
    public static class SessionHelper
    {
        public static string SessionName = Global.ConfigInfo.LoginSessionName;
        #region >> 쿠키 기준
        /*public static LOGIN_INFO LoginInfo
        {
            get
            {

                LOGIN_INFO rtnData;

                if (HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
                {

                    rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];
                    if (rtnData.LOGIN_ID != null)
                    {
                        GlobalMvc.Util.SetCookie(SessionName + "_LOGININFO", JsonConvert.SerializeObject(rtnData));
                        return rtnData;
                    }
                }
                rtnData = JsonConvert.DeserializeObject<LOGIN_INFO>(GlobalMvc.Util.getCookie(SessionName + "_LOGININFO"));

                if (rtnData == null && HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
                {
                    rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];
                }
                else if (rtnData != null)
                {
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = rtnData;
                }

                if (rtnData == null) rtnData = new LOGIN_INFO { LANGUAGE = "en" };

                return rtnData;
            }
            set
            {
                {
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = value;
                    GlobalMvc.Util.SetCookie(SessionName + "_LOGININFO", JsonConvert.SerializeObject((LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"]));
                }
            }
        }*/
        #endregion

        #region >> 세션 기준
        /// <summary>
        /// 로그인 관련 세션
        /// </summary>
        public static LOGIN_INFO LoginInfo
        {
            get
            {
                LOGIN_INFO rtnData;

                if (HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
                {
                    rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];
                }
                else
                    rtnData = new LOGIN_INFO { LANGUAGE = "en" };
                HttpContext.Current.Session[SessionName + "_LOGININFO"] = rtnData;
                return rtnData;
            }
            set
            {
                {
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = value;
                    LOGIN_INFO rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];

                }
            }
        }



        /// <summary>
        /// 세션재셋팅
        /// </summary>
        /// <param name="nSec">300 = 60*5 => 5분</param>
        public static void SetSession(int nSec = 300)
        {
            LOGIN_INFO rtnData;
            if (HttpContext.Current.Session[SessionName + "_LOGININFO"] != null)
            {
                rtnData = (LOGIN_INFO)HttpContext.Current.Session[SessionName + "_LOGININFO"];

                if ((DateTime.Now - rtnData.SETTING_TIME).TotalSeconds >= nSec)
                {
                    rtnData.SETTING_TIME = DateTime.Now;
                    HttpContext.Current.Session[SessionName + "_LOGININFO"] = rtnData;
                }
            }
        }
        #endregion




        public static LOGIN_NONCHECK_PAGE LoginNonCheckPageList
        {
            get
            {
                LOGIN_NONCHECK_PAGE LoginNonCheckPageList;
                if (HttpContext.Current.Session[SessionName + "_LOGIN_NONCHECK_PAGE"] == null)
                {
                    LoginNonCheckPageList = new LOGIN_NONCHECK_PAGE();
                    LoginNonCheckPageList.PAGE = new List<string>();
                }
                else
                {
                    LoginNonCheckPageList = (LOGIN_NONCHECK_PAGE)HttpContext.Current.Session[SessionName + "_LOGIN_NONCHECK_PAGE"];
                    if (LoginNonCheckPageList.PAGE == null)
                    {
                        LoginNonCheckPageList.PAGE = new List<string>();
                    }
                }

                LoginNonCheckPageList.PAGE.Add("/advstep/step1");
                LoginNonCheckPageList.PAGE.Add("/home/login");
                return LoginNonCheckPageList;

            }
            set { HttpContext.Current.Session[SessionName + "_LOGIN_NONCHECK_PAGE"] = value; }
        }

        public static string returnUrl { get { return GlobalMvc.Util.getCookie("returnURL"); } set { GlobalMvc.Util.SetCookie("returnURL", value); } }


        /// <summary>
        /// decimal 형을 String 포맷으로 변경
        /// </summary>
        /// <param name="val"></param>
        /// <param name="format">디폴트는 통화형</param>
        /// <returns></returns>
        public static string ToFormatAmt(this decimal val, string format = null)
        {

            format = "n" + SessionHelper.LoginInfo.CultureInfo.NumberFormat.CurrencyDecimalDigits;
            return val.ToString(format);
        }

    }
}