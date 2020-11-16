using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.BizService;
using ALT.VO.Common;
using ALT.Framework.Mvc;
using ALT.Framework.Data;
using loggalWebMng.CommonCS;

using ALT.Framework;
using ALT.Framework.Mvc.Helpers;

namespace loggalWebMng.Controllers
{
    public class EmployeeController : BaseController
    {
        [Compress]
        public ActionResult EmployeeReg(EMPLOYEE_COND Cond, int? id = -1)
        {
            int? member_code = id;
            EMPLOYEE_INFO data = new EMPLOYEE_INFO();

            if(SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE != 1)
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
            }
            if (id >= 0)
                data = Service.employeeService.GetEmployeeList(new EMPLOYEE_COND { MEMBER_CODE = id }).FirstOrDefault();

            if(data == null || data.MEMBER_CODE == null)
            {
                data = new EMPLOYEE_INFO() { COMPANY_CODE = Cond.COMPANY_CODE,  STORE_CODE = Cond.STORE_CODE, DEPT_CODE = Cond.DEPT_CODE };
                var storelist = new BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = data.STORE_CODE });
                if (storelist.Count() == 1)
                {
                    data.COMPANY_CODE = storelist[0].COMPANY_CODE;
                    data.COMPANY_NAME = storelist[0].COMPANY_NAME;
                    data.STORE_CODE = storelist[0].STORE_CODE;
                    data.STORE_NAME = storelist[0].STORE_NAME;
                }
            }

            if(SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
            }

            ViewBag.data = data;
            data.STORE_CODE = (data.STORE_CODE == null ? 1 : data.STORE_CODE);
            List <SelectListItem> deptCombo = Service.employeeService.GetStoreDept(new T_STORE_DEPT() { STORE_CODE = data.STORE_CODE }).Select(x => new SelectListItem() { Text = x.DEPT_NAME, Value = x.DEPT_CODE.ToString() }).ToList();
            ViewBag.deptCombo = (deptCombo == null) ? new List<SelectListItem>() : deptCombo;
            
            IList<T_STORE_POSITION> positionList = Service.employeeService.GetStorePosition(new T_STORE_POSITION() {STORE_CODE = data.STORE_CODE, HIDE = false });
            if (positionList != null)
            {
                int positionSeq = positionList.Where(x => x.POSITION_TYPE == 1 && x.POSITION_CODE == SessionHelper.LoginInfo.EMPLOYEE.COMP_POSITION).First().SEQ;
                int titleSeq = positionList.Where(x => x.POSITION_TYPE == 2 && x.POSITION_CODE == SessionHelper.LoginInfo.EMPLOYEE.COMP_TITLE).First().SEQ;
                ViewBag.positionCombo1 = positionList.Where(x => x.POSITION_TYPE == 1 && x.SEQ >= positionSeq ).Select(x => new SelectListItem() { Text = x.NAME, Value = x.POSITION_CODE.ToString() }).ToList();  //직급
                ViewBag.positionCombo2 = positionList.Where(x => x.POSITION_TYPE == 2 && x.SEQ >= titleSeq).Select(x => new SelectListItem() { Text = x.NAME, Value = x.POSITION_CODE.ToString() }).ToList();  //직책
            }

            
            IList<T_STORE_WEBMENU_GROUP> menuGroupList = Service.employeeService.GetMenuGroupList(new T_STORE_WEBMENU_GROUP() { STORE_CODE = data.STORE_CODE, HIDE= false});

            var menuGroup = menuGroupList.Where(w => w.GROUP_CODE == SessionHelper.LoginInfo.EMPLOYEE.MENU_GROUP).FirstOrDefault();
            var chkGroupCode = (menuGroup == null) ? 1 : menuGroup.ORDER_SEQ;

            ViewBag.menuGroupList = (menuGroupList == null) ? new List<SelectListItem>() : menuGroupList.Where(w => w.ORDER_SEQ >= chkGroupCode).Select(x => new SelectListItem() { Text = x.GROUP_NAME, Value = x.GROUP_CODE.ToString() }).ToList();
            
            return View();
        }
        [Compress]
        public JsonResult employeeSave(T_MEMBER Param)
        {
            SessionHelper.LOG_NAME = "직원저장";

            string msg = string.Empty;
            Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            Param.BIRTH = Param.BIRTH.RemoveDateString();
            Param.INSERT_CODE = (int)(SessionHelper.LoginInfo.MEMBER == null ? 0 : SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            RTN_SAVE_DATA data = new AccountService().SaveMember(Param);
            //    data.RETURN_URL = "/advstep/step2";
            //IList<T_MEMBER> list = new AccountService().GetMemberList(new T_MEMBER { USER_ID = Param.USER_ID });
            //SessionHelper.LoginInfo.MEMBER = list.First();

            return new JsonResult { Data = data };
        }
        [Compress]
        public JsonResult SaveEmployee(EMPLOYEE_INFO param)
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

        #region >> 직원조회
        [Compress]
        public ActionResult EmployeeList(int? STORE_CODE)
        {
            var store = new BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = Convert.ToInt32(STORE_CODE.ToString(SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE.ToString(""))) }).FirstOrDefault();
            ViewBag.data = store;
            return View();
        }

        [Compress]
        public PartialViewResult PV_EmployeeList(EMPLOYEE_COND Cond)
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
        [Compress]
        public ActionResult EmployeeExcelReport(EMPLOYEE_COND Cond)
        {
            List<string> headers = new List<string>()
            { "지점명", "직책", "직급", "이름", "아이디", "전화번호", "생년월일", "상급자", "권한", "수정자", "수정시간"   };
            
            Cond.PAGE = 1; Cond.PAGE_COUNT = 100000;

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

            return Service.employeeService.GetEmployeeList(Cond).Select(s => new {
                STORE_NAME = s.STORE_NAME
               ,
                MEMBER_CODE = s.MEMBER_CODE
               ,
                COMP_POSITION_NAME = s.COMP_POSITION_NAME
               ,
                COMP_TITLE_NAME = s.COMP_TITLE_NAME
               ,
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



        #region >> 직원팝업 조회
        [Compress]
        public PartialViewResult PV_EmployeePList(SEARCH_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE_CODE == 1){}
            else if (SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4)
            {   Cond.SEARCH_DATA2 = SessionHelper.LoginInfo.STORE.COMPANY_CODE.ToString("");}
            else
            {   Cond.SEARCH_DATA3 = SessionHelper.LoginInfo.STORE_CODE.ToString("");}

            ViewBag.list = new EmployeeService().GetEmployeePopupList(Cond);
            return PartialView2();
        }

        public JsonResult getEmployeePList(SEARCH_COND Cond)
        {
            var rtn = new EmployeeService().GetEmployeePopupList(Cond);
            return new JsonResult { Data = rtn };
        }
        #endregion
    }
}