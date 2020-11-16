using ALT.BizService;
using ALT.Framework;
using ALT.Framework.Data;

using ALT.Framework.Mvc;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalWebMng.CommonCS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace loggalWebMng.Controllers
{
    public class AccountController : BaseController
    {
        #region >> 로그인 정보
        [AltloggalAuthorization]
        public JsonResult doLogin(string USER_ID, string PASSWORD, string returnUrl = "/")
        {
            SessionHelper.LOG_NAME = "로그인";
            string msg = string.Empty, focusinput = string.Empty, returnUrlParam = string.Empty;
            IList<T_MEMBER> list = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = USER_ID });

            if (list.Count() != 1)
            {
                msg = "아이디가 잘못되었습니다.";
                focusinput = "USER_ID";
                returnUrl = "";
            }
            else
            {
                if (GlobalMvc.Util.Encrypt_PW(PASSWORD) != list.First().PASSWORD)
                {
                    msg = "비밀번호가 잘못되었습니다.";
                    focusinput = "PASSWORD";
                    returnUrl = "";
                }
                else
                {
                    SessionHelper.LoginInfo.MEMBER = list.First();
                    List<EMPLOYEE_INFO> employeeList = new ALT.BizService.AccountService().GetEmployeeInfoList(new EMPLOYEE_INFO_COND { MEMBER_CODE = (int)list.First().MEMBER_CODE }).OrderBy(o=>o.STORE_CODE).ToList();

                    if (employeeList.Count() > 0)
                    {

                        SessionHelper.LoginInfo.EMPLOYEE_LIST = employeeList;
                        var empData = employeeList.First();
                        SetChangeEmployee(empData.STORE_CODE);
                        //IList<T_STORE> storelist = new ALT.BizService.BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE });
                        //SessionHelper.LoginInfo.STORE = storelist.First();
                        //SessionHelper.LoginInfo.WebMemu = new ALT.BizService.AccountService().GetLoginWebMenuList(new LOGIN_WEBMENU { PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE, STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE, MEMBER_CODE = (int)list.First().MEMBER_CODE });
                        //SessionHelper.LoginInfo.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
                        //SessionHelper.LoginInfo.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
                        //SessionHelper.LoginInfo.COMPANY_ID = SessionHelper.LoginInfo.STORE.COMPANY_ID;
                        //SessionHelper.ScheduleInfo = new loggalServiceBiz.MongoDBService().GetAdPlayShedulerInfo();
                        //SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH = SetEmployeeSearchAuth();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(msg))
                        {
                            SessionHelper.LoginInfo.MEMBER = null;
                            msg = "온라인 회원은 접근할 수 없습니다. 회원가입을 진행하여 주시기 바랍니다.";
                            returnUrl = "/account/memberjoin";
                            returnUrlParam = "USER_ID=" + USER_ID;
                            focusinput = "confirm";
                        }
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        /*로그인시 로그인 정보추가 */
                        Service.commoneService.SaveLog(new T_LOG
                        {
                            STORE_CODE = SessionHelper.LoginInfo.STORE_CODE,
                            LOG_TYPE = 1,
                            LOG_DATA1 = "/account/doLogin",
                            LOG_DATA2 = SessionHelper.LOG_NAME,
                            USE_IP = Request.UserHostAddress,
                            LOG_DESC = "USER_ID:" + USER_ID + " NAME: " + ((SessionHelper.LoginInfo.MEMBER == null) ? "" : SessionHelper.LoginInfo.MEMBER.USER_NAME.ToString("")),
                            INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE
                        });
                    }
                }
            }
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "" : returnUrl;
            return new JsonResult() { Data = new { focus_input = focusinput, error_message = msg, return_url = returnUrl, returnurlparam = returnUrlParam } };
        }

        private void SetChangeEmployee( int? StoreCode) {
            SessionHelper.LoginInfo.EMPLOYEE = SessionHelper.LoginInfo.EMPLOYEE_LIST.Where(w => w.STORE_CODE == StoreCode).FirstOrDefault();
            
            IList<T_STORE> storelist = new ALT.BizService.BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = StoreCode });
            SessionHelper.LoginInfo.STORE = storelist.First();
            SessionHelper.LoginInfo.WebMemu = new ALT.BizService.AccountService().GetLoginWebMenuList(new LOGIN_WEBMENU { PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE, STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE, MEMBER_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE });
            SessionHelper.LoginInfo.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
            SessionHelper.LoginInfo.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            SessionHelper.LoginInfo.COMPANY_ID = SessionHelper.LoginInfo.STORE.COMPANY_ID;
            SessionHelper.ScheduleInfo = new loggalServiceBiz.MongoDBService().GetAdPlayShedulerInfo();
            SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH = SetEmployeeSearchAuth();
        }

        public ActionResult changeEmployee(int STORE_CODE, string rtnUrl = "/") {
            
            
            SetChangeEmployee( STORE_CODE);
            return Redirect(rtnUrl);
           // return View();
        }

        [AltloggalAuthorization]
        public JsonResult GetMemberCheck(T_MEMBER_COND Param)
        {
            string msg = string.Empty, focusinput = string.Empty;
            IList<T_MEMBER> list = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID });

            if (list.Count() != 1)
            {
                msg = "아이디가 잘못되었습니다.";
                focusinput = "LOGIN_USER_ID";
            }
            else
            {
                if (GlobalMvc.Util.Encrypt_PW(Param.PASSWORD) != list.First().PASSWORD)
                {
                    msg = "비밀번호가 잘못되었습니다.";
                    focusinput = "LOGIN_PASSWORD";
                }
            }

            return new JsonResult()
            {
                Data = new
                {
                    focus_input = focusinput,
                    error_message = msg
                }
            };
        }

        /// <summary>
        /// 사용자 서치권한설정
        /// </summary>
        /// <returns></returns>
        private EMPLOYEE_SEARCH_AUTH SetEmployeeSearchAuth()
        {
            EMPLOYEE_SEARCH_AUTH Data = new EMPLOYEE_SEARCH_AUTH();
            if (!(SessionHelper.LoginInfo.STORE_CODE == 1 && (SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4)))
            {
                Data.COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE;
            }
            if (SessionHelper.LoginInfo.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Data.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != null)
            {
                switch (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH)
                {
                    case 2: /*상위부서권한*/
                        Data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Data.DEPT_SEARCH = SessionHelper.LoginInfo.EMPLOYEE.DEPT_SEARCH;
                        break;
                    case 3:/*부서권한*/
                        Data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Data.DEPT_CODE = SessionHelper.LoginInfo.EMPLOYEE.DEPT_CODE;
                        break;
                    case 8:/*상급자권한*/
                        Data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Data.PARENT_MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
                        break;
                    case 9:/*본인권한*/
                        Data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Data.MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
                        break;
                }
            }
            return Data;
        }
        [AltloggalAuthorization]
        public JsonResult doLogOut(string returnUrl = "/")
        {
            SessionHelper.LOG_NAME = "로그아웃";
            SessionHelper.LOG_OUT();
            return new JsonResult() { Data = new { return_url = returnUrl } };
        }
        #endregion
        [AltloggalAuthorization]
        public JsonResult GetMemberList(T_MEMBER_COND Param)
        {
            var list = new AccountService().GetMemberList(Param);
            list.ForEach(i => i.PASSWORD = "****");

            return new JsonResult() { Data = new { list = list } };
        }





        [AltloggalAuthorization]
        public ActionResult PasswordFind()
        {
            return View();
        }

        [AltloggalAuthorization]
        public JsonResult SendEmailPasswordUrl(string USER_ID)
        {
            T_MEMBER Param = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = USER_ID }).First();
            Param.PASSWORD_CHANGE_URL = GlobalMvc.Util.Encrypt_PW(Param.USER_ID.ToString("") + Param.MEMBER_CODE.ToString("") + Param.USER_NAME.ToString());
            RTN_SAVE_DATA data = new AccountService().ReqPasswordChange(Param);
            Param.PASSWORD_AUTH_TIME = Convert.ToDateTime(data.DATA);
            ViewBag.data = Param;
            string sContent = GlobalMvc.Common.RenderPartialViewToString(this, "~/Views/account/Partial/PV_PasswordFindMail.cshtml");
            new MailHelper().SendMail(new MAILINFO
            {
                ACCEPT_ID = USER_ID
                ,
                SUBJECT = "[" + Global.ConfigInfo.ProjectTitle + "] 비밀번호 변경요청을 하였습니다."
                ,
                CONTENT = sContent
            },false);
            return new JsonResult() { Data = data };
        }

        [AltloggalAuthorization]
        public ActionResult PasswordChange(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            ViewBag.data = new AccountService().GetMemberList(new T_MEMBER_COND { PASSWORD_CHANGE_URL = id }).FirstOrDefault();
            return View();
        }

        [AltloggalAuthorization]
        public JsonResult PasswordChangeSave(T_MEMBER Param)
        {
            Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            RTN_SAVE_DATA data = new AccountService().PasswordChangeSave(Param);
            //new MailHelper() {
            //}
            return new JsonResult() { Data = data };
        }
        [AltloggalAuthorization]
        public ActionResult MemberJoin(string id)
        {
            ViewBag.userId = id;
            return View();
        }

        [AltloggalAuthorization]
        public JsonResult MemberJoinSave (MEMBER_JOIN Param)
        {
            
            RTN_SAVE_DATA rtn = new EmployeeService().loggalMng_MEMBER_JOIN(Param);
            return new JsonResult { Data = rtn };
        }
        [AltloggalAuthorization]
        public ActionResult MemberModify(int? id)
        {
            ViewBag.id = id ?? 0;
            EMPLOYEE_INFO Cond = new EMPLOYEE_INFO { MEMBER_CODE = id };
            if (Cond.MEMBER_CODE == null || Cond.MEMBER_CODE == 0)
            {
                Cond.MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
                Cond = Service.employeeService.GetEmployeeList(new EMPLOYEE_COND { MEMBER_CODE = Cond.MEMBER_CODE }).FirstOrDefault();
            }
            if (Cond.STORE_CODE == null) Cond.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;

            T_MEMBER member = new AccountService().GetMemberList(new T_MEMBER_COND { MEMBER_CODE = Cond.MEMBER_CODE }).First();
            T_STORE store   = new ALT.BizService.BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = Cond.STORE_CODE }).First();

            ViewBag.member = member;
            ViewBag.store = store;
            
            return View();
        }

        [AltloggalAuthorization]
        public JsonResult MemberModifySave(T_MEMBER Param,  T_STORE Param2)
        {
            if (Param2 == null || Param2.STORE_CODE == null) Param2 = new T_STORE { COMPANY_TYPE2 = SessionHelper.LoginInfo.STORE.COMPANY_TYPE2
                                                     , BUSI_REG_NUMBER = "" };

            Param2.COMPANY_CODE = Param2.COMPANY_CODE;
            Param2.STORE_CODE = Param2.STORE_CODE;

            string msg = string.Empty;
            //Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            Param.BIRTH = Param.BIRTH.RemoveDateString();

            T_MEMBER memData = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID }).FirstOrDefault();
            RTN_SAVE_DATA data = new RTN_SAVE_DATA();

            if (Param.PASSWORD != "******")
            {
                if (!string.IsNullOrEmpty(Param.PASSWORD)) memData.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            }
            memData.BIRTH = Param.BIRTH;
            memData.USER_NAME = Param.USER_NAME;
            memData.ZIP_CODE = Param.ZIP_CODE;
            memData.ADDRESS1 = Param.ADDRESS1;
            memData.ADDRESS2 = Param.ADDRESS2;
            memData.PHONE = Param.PHONE;
            memData.GENDER = Param.GENDER;
            memData.INSERT_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            ///memData.MEMBER_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            

            data = new AccountService().MemberModify(memData, Param2);

            data.RETURN_URL = "/";

            memData = new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID }).FirstOrDefault();
           // SessionHelper.LoginInfo.STORE = new ALT.BizService.BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE }).First();
           // SessionHelper.LoginInfo.MEMBER = memData;

            return new JsonResult { Data = data };
        }

        #region >> 회원등록
        [AltloggalAuthorization]
        public ActionResult MemberReg(int? id, int? storeCode = 3)
        {
            EMPLOYEE_INFO Cond = new EMPLOYEE_INFO { MEMBER_CODE = id };
            T_MEMBER member = new T_MEMBER();
            if (id != null && id > 0)
            {
                Cond = Service.employeeService.GetEmployeeList(new EMPLOYEE_COND { MEMBER_CODE = Cond.MEMBER_CODE }).FirstOrDefault();

                member = new AccountService().GetMemberList(new T_MEMBER_COND { MEMBER_CODE = Cond.MEMBER_CODE }).First();
            }
            
            if (Cond.STORE_CODE == null) Cond.STORE_CODE = storeCode;

            T_STORE store = new ALT.BizService.BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = Cond.STORE_CODE }).First();

            ViewBag.member = member;
            ViewBag.store = store;
            return View();
        }

        [AltloggalAuthorization]
        public JsonResult SaveMember(EMPLOYEE_INFO param)
        {
            SessionHelper.LOG_NAME = "직원저장";
            string msg = string.Empty;
            param.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            if (param.MEMBER_CODE != null && param.MEMBER_CODE > -1 && param.PASSWORD == "******")  //비밀번호 변경 안할경우
            {
                param.PASSWORD = param.EX_PASSWORD;
            }
            else
            {
                param.PASSWORD = GlobalMvc.Util.Encrypt_PW(param.PASSWORD);
            }

            param.BIRTH = param.BIRTH.RemoveDateString();
            param.INSERT_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            EMPLOYEE_RESPONSE result = Service.employeeService.SaveEmployee(param);

            return new JsonResult { Data = result };
        }
        #endregion
        #region >> 회원조회
        [AltloggalAuthorization]
        public ActionResult MemberList(int? STORE_CODE)
        {


            var store = new BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = STORE_CODE }).FirstOrDefault();
            ViewBag.data = store;
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult PV_MemberList(EMPLOYEE_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE;
            }

            if (!(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != null)
            {
                switch (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH)
                {
                    case 2: /*상위부서권한*/
                        Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Cond.DEPT_SEARCH = SessionHelper.LoginInfo.EMPLOYEE.DEPT_SEARCH.ToString("");
                        break;
                    case 3:/*부서권한*/
                        Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Cond.DEPT_CODE = SessionHelper.LoginInfo.EMPLOYEE.DEPT_CODE;
                        break;
                    case 8:/*상급자권한*/
                        Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Cond.PARENT_MEMBER_CODE = SessionHelper.LoginInfo.EMPLOYEE.MEMBER_CODE;
                        break;
                    case 9:/*본인권한*/
                        Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
                        Cond.MEMBER_CODE = SessionHelper.LoginInfo.EMPLOYEE.MEMBER_CODE;
                        break;
                }
            }
            ViewBag.list = Service.employeeService.GetEmployeeList(Cond);


            return PartialView2();
        }

        [AltloggalAuthorization]
        public ActionResult MemberExcelReport(EMPLOYEE_COND Cond)
        {
            List<string> headers = new List<string>()
            { "이름", "아이디", "전화번호", "생년월일", "상급자", "권한", "수정자", "수정시간"   };


            Cond.PAGE = 1; Cond.PAGE_COUNT = 100000;

            if (SessionHelper.LoginInfo.STORE_CODE != 1)
            {
                Cond.MEMBER_CODE = SessionHelper.LoginInfo.EMPLOYEE.MEMBER_CODE;
            }

            return Service.employeeService.GetEmployeeList(Cond).Select(s => new {
                USER_NAME = s.USER_NAME
               ,
                USER_ID = s.USER_ID
               ,
                MOBILE = s.MOBILE
               ,
                BIRTH = s.BIRTH.ToFormatDate()
               ,
                PARENT_MEMBER_NAME = s.PARENT_MEMBER_NAME
               ,
                EMP_AUTH_NAME = s.EMP_AUTH_NAME
               ,
                UPDATE_MEMBER_NAME = s.UPDATE_NAME
               ,
                UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }
        #endregion


        #region >> 북마크리스트/좋아요리스트
        [AltloggalAuthorization]
        public ActionResult MemberBookList()
        {
            return View();
        }


        [AltloggalAuthorization]
        public ActionResult MemberBookBannerList()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult MemberBookLocalboxList()
        {
            return View();
        }


        [AltloggalAuthorization]
        public ActionResult MemberFavoriteList()
        {
            return View();
        }

        [AltloggalAuthorization]
        public JsonResult MemberBookmarkDelete(List<T_MEMBER_BOOKMARK> savelist)
        {
            RTN_SAVE_DATA rtn = new loggalServiceBiz.AccountService().MemberbookmarkSave(savelist, "D");
            return new JsonResult { Data = rtn };
        }

        [AltloggalAuthorization]
        public PartialViewResult pv_memberbookmark_adlist(BOOKMARK_AD_COND Cond)
        {
            if (SessionHelper.LoginInfo.MEMBER.USER_TYPE != 1) // 관리자가 아닐 경우 자기 데이터만 보여준다
            {
                Cond.USER_ID = SessionHelper.LoginInfo.MEMBER.USER_ID;
            }
            ViewBag.list = new loggalServiceBiz.AccountService().GetBookmarkAdList(Cond);
            return PartialView2();
        }

        [AltloggalAuthorization]
        public PartialViewResult pv_memberbookmark_boxlist(T_MEMBER_BOOKMARK_COND Cond)
        {
            if (SessionHelper.LoginInfo.MEMBER.USER_TYPE != 1) // 관리자가 아닐 경우 자기 데이터만 보여준다
            { 
                Cond.USER_ID = SessionHelper.LoginInfo.MEMBER.USER_ID;
            }
            Cond.TITLE = "";
    
            ViewBag.list = new loggalServiceBiz.AccountService().GetMemberbookmarkList(Cond);
            return PartialView2();
        }

        [AltloggalAuthorization]
        public JsonResult AdShareRequestSave(List<STEP4_SAVE> list)
        {
            RTN_SAVE_DATA rtn = new loggalServiceBiz.RequestADService().AdShareRequest(list);
            return new JsonResult() { Data = rtn };
        }
        #endregion
    }
}