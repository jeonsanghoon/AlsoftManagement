using ALT.Framework;
using ALT.Framework.Mvc;
using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace loggalWebMng.CommonCS
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
                {
                    rtnData = new LOGIN_INFO { LANGUAGE = "en" };
                    rtnData.EMPLOYEE = new EMPLOYEE_INFO();
                    rtnData.MEMBER = new T_MEMBER();
                }
                HttpContext.Current.Session[SessionName + "_LOGININFO"] = rtnData;
                rtnData.CURRENT_MENU_NAME = HttpContext.Current.Request["currentmenuName"] ?? rtnData.CURRENT_MENU_NAME;             
				
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
        public static void LOG_OUT() {
            HttpContext.Current.Session[SessionName + "_LOGININFO"] = null;
        }

        public static SCHEDULER_INFO ScheduleInfo
        {
            get { return (SCHEDULER_INFO)(HttpContext.Current.Session[SessionName + "SCHEDULE_INFO"] == null ? "" : HttpContext.Current.Session[SessionName + "SCHEDULE_INFO"]); }
            set { HttpContext.Current.Session[SessionName + "SCHEDULE_INFO"] = value; }
        }
        /// <summary>
        /// 로그명 : 해당명이 있을 경우 로그를 저장한다.
        /// </summary>
        public static string LOG_NAME
        {
            get { return (string)(HttpContext.Current.Session[SessionName + "LOG_NAME"] == null ? "" : HttpContext.Current.Session[SessionName + "LOG_NAME"]); }
            set { HttpContext.Current.Session[SessionName + "LOG_NAME"] = value; }
        }
        /// <summary>
        /// 러컬박스 상세페이지 대기시간 T_DEVICE 테이브에있음
        /// </summary>
        public static string PAGE_WAITING_TIME
        {
            get { return (string)(HttpContext.Current.Session[SessionName + "PAGE_WAITING_TIME"] == null ? "" : HttpContext.Current.Session[SessionName + "PAGE_WAITING_TIME"]); }
            set { HttpContext.Current.Session[SessionName + "PAGE_WAITING_TIME"] = value; }
        }
        /// <summary>
        /// 로그 파라미터
        /// </summary>
        public static string LOG_PARAM
        {
            get { return (string)(HttpContext.Current.Session[SessionName + "LOG_PARAM"] == null ? "" : HttpContext.Current.Session[SessionName + "LOG_PARAM"]); }
            set { HttpContext.Current.Session[SessionName + "LOG_PARAM"] = value; }
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

                //LoginNonCheckPageList.PAGE.Add("/advstep/step1");
                
                LoginNonCheckPageList.PAGE.Add("/home/login");
                LoginNonCheckPageList.PAGE.Add("/home/GoogleMapMaker");
                LoginNonCheckPageList.PAGE.Add("/basic/deviceMainDetail");
                LoginNonCheckPageList.PAGE.Add("/basic/AdFavoriteSave");
                LoginNonCheckPageList.PAGE.Add("/account/");
                LoginNonCheckPageList.PAGE.Add("/account/dologin");
                LoginNonCheckPageList.PAGE.Add("/account/memberjoin");
                LoginNonCheckPageList.PAGE.Add("/account/memberjoinsave");
                LoginNonCheckPageList.PAGE.Add("/account/GetMemberList");

                LoginNonCheckPageList.PAGE.Add("/common/getdaummapapiaddresstolnglat");
                LoginNonCheckPageList.PAGE.Add("/advertise/adsub");
                LoginNonCheckPageList.PAGE.Add("/advertise/contentview");
                LoginNonCheckPageList.PAGE.Add("/advertise/adsigninfo");
                LoginNonCheckPageList.PAGE.Add("/basic/signagecontrol");
                LoginNonCheckPageList.PAGE.Add("/basic/signagecontrolsave");
                LoginNonCheckPageList.PAGE.Add("/basic/getsignagecontrollist");
                LoginNonCheckPageList.PAGE.Add("/basic/signagecontrolplayupdate");

                LoginNonCheckPageList.PAGE.Add("/base/fileupload");
                LoginNonCheckPageList.PAGE.Add("/temp/");
                LoginNonCheckPageList.PAGE.Add("/device/");

                if (LoginNonCheckPageList.ControllerNames == null) LoginNonCheckPageList.ControllerNames = new List<string> { };
                LoginNonCheckPageList.ControllerNames.Add("temp");
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