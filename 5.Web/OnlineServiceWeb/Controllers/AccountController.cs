using System;
using ALT.Framework.Mvc;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using OnlineServiceBiz;


using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineServiceWeb.CommonCS;

namespace OnlineServiceWeb.Controllers
{
    public class AccountController : BaseController
    {
        public ActionResult Login(string returnUrl = "/")
        {
            ViewBag.returnUrl = returnUrl;
            return ReturnView();
        }
        #region >> 로그인 정보
        public JsonResult doLogin(string USER_ID, string PASSWORD, string returnUrl = "/")
        {
            string msg = string.Empty, focusinput = string.Empty;
            IList<T_MEMBER> list = new HomePageService().GetMemberList(new T_MEMBER { USER_ID = USER_ID });

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
            return new JsonResult() { Data = new { focus_input = focusinput, error_message = msg, return_url = returnUrl } };
        }
        public JsonResult doLogOut(string returnUrl = "/")
        {
            SessionHelper.LoginInfo.MEMBER = null;
            return new JsonResult() { Data = new { return_url = returnUrl } };
        }
        #endregion
        public JsonResult MemberList(T_MEMBER Param)
        {
            var list = new HomePageService().GetMemberList(Param);
            list.ForEach(i => i.PASSWORD = "****");
           
            return new JsonResult() { Data = new { list = list } };
        }

        #region >> 회원가입
        public ActionResult memberjoin() {
            return ReturnView();
        }
        public JsonResult SaveAddMember(T_MEMBER Param)
        {
            string msg = string.Empty;
            Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);

            msg = new AccountService().SaveMember(Param);
            return new JsonResult { Data = new { error_message = msg } };
        }
        #endregion
    }
}