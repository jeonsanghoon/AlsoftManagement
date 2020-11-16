
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.VO.Common;
using loggalServiceBiz;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.Framework.Mvc.Helpers;
using ALT.Framework.Mvc;
using ALT.VO.loggal;
using loggalWeb.CommonCS;

namespace loggalWeb.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        [Compress]
        public ActionResult Index()
        {
            return View();
        }

        [Compress]
        public JsonResult MemberList(T_MEMBER_COND Param)
        {
            var list = new AccountService().GetMemberList(Param);
            list.ForEach(i => i.PASSWORD = "****");

            return new JsonResult() { Data = new { list = list } };
        }

        #region >> 로그인 정보
        [Compress]
        public JsonResult doLogin(string USER_ID, string PASSWORD, string returnUrl= null)
        {
            string msg = string.Empty, focusinput = string.Empty;
            IList<T_MEMBER> list = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = USER_ID });

            if (list.Count() != 1)
            {
                msg = "아이디가 잘못되었습니다.";
                focusinput = "LOGIN_USER_ID";
            }
            else
            {
                if (GlobalMvc.Util.Encrypt_PW(PASSWORD) != list.First().PASSWORD)
                {
                    msg = "비밀번호가 잘못되었습니다.";
                    focusinput = "LOGIN_PASSWORD";
                }
                else
                {
                    SessionHelper.LoginInfo.MEMBER = list.First();
                }
            }


            if (returnUrl != "/" && SessionHelper.returnUrl != null && returnUrl == "")
            {
                returnUrl = SessionHelper.returnUrl;
            }
            if (!string.IsNullOrEmpty(msg)) {
                return new JsonResult() { Data = new { focus_input = focusinput, error_message = msg, returnUrl = returnUrl } };
            }
            IList<AD_STATUS> adStatusList = new AdvertisingService().GetAdStatusList(new AD_STATUS_COND() { USER_ID = SessionHelper.LoginInfo.MEMBER.USER_ID, TOP_CNT= "TOP 2", AD_COND_STRING = " AND B.STATUS <= 9" });
            if(adStatusList.Count > 1)
            {
                returnUrl = "/advstep/steplist";
            }else if (adStatusList.Count == 1)
            {
                returnUrl = adStatusList.First().NEXT_PAGE_URL + "?id=" + adStatusList.First().AD_CODE;
            }

            if(string.IsNullOrEmpty(returnUrl))
            {
                returnUrl = "/";
            }

            return new JsonResult() { Data = new { focus_input = focusinput, error_message = msg, returnUrl = returnUrl } };
        }
        [Compress]
        public JsonResult doLogOut()
        {
            SessionHelper.LoginInfo.MEMBER = null;
            string returnUrl = "/home/login?returnUrl=" + SessionHelper.returnUrl;
            return new JsonResult() { Data = new { returnUrl = returnUrl } };
        }
        #endregion
        #region >> 회원가입
        [Compress]
        public ActionResult Memberjoin()
        {
            return View();
        }
        [Compress]
        public JsonResult SaveMember(T_MEMBER Param)
        {
            string msg = string.Empty;
            Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            Param.BIRTH = Param.BIRTH.RemoveDateString();
            Param.INSERT_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            RTN_SAVE_DATA data = new AccountService().SaveMember(Param);
            return new JsonResult { Data = new { data = data } };
        }
        #endregion
    }
}