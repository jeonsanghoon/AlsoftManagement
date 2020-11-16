using ALT.BizService;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalWebMng.CommonCS;
using OnlineServiceBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using ALT.Framework.Mvc.Vo;
using loggalServiceBiz;
using ALT.Framework.Mvc.Helpers;
using ALT.Framework.Mvc;

namespace loggalWebMng.Controllers
{
    public class CommonController : BaseController
    {

        #region >> 메뉴 등록
        // GET: Common
        [AltloggalAuthorization]
        public ActionResult WebMenuRegList()
        {
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_WebMenuRegList(T_WEBMENU_COND Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            if (!(SessionHelper.LoginInfo.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1)) Cond.MENU_CODE = -1;
            ViewBag.list = Service.commoneService.GetWebMenuList(Cond);
            ViewBag.combolist = new CommonService().GetCommon(new T_COMMON_COND { ADD_COND = " AND MAIN_CODE IN('M001','U002','U003')", HIDE = false });

            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult WebMenuSave(List<T_WEBMENU> list)
        {
            RTN_SAVE_DATA data = new CommonService().WebMenuSave(list, Global.ConfigInfo.PROJECT_SITE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = data };
        }
        [AltloggalAuthorization]
        public ActionResult WebMenuExcelReport(T_WEBMENU_COND Cond)
        {
            if (!(SessionHelper.LoginInfo.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1)) Cond.MENU_CODE = -1;
            List<string> headers = new List<string>()
            { "코드", "메뉴명", "페이지주소", "메뉴클래스", "레벨", "메뉴권한", "업체", "지점", "등록자", "등록시간", "수정자", "수정시간"   };

            

           return  Service.commoneService.GetWebMenuList(Cond).Select(s => new {
                SEARCH_CODE = s.SEARCH_CODE
               ,
                NAME = s.NAME
               ,
                MENU_URL = s.MENU_URL
               ,
                MENU_CLASS = s.MENU_CLASS
               ,
                LEVEL = s.LEVEL
               ,MENU_AUTH = s.MENU_AUTH_NAME
               ,
                MENU_COMPANY_NAME = s.MENU_COMPANY_NAME
               ,
                MENU_STORE_NAME = s.MENU_STORE_NAME
               ,
                INSERT_NAME = s.INSERT_NAME
               ,
                INSERT_DATE = s.INSERT_DATE
                 ,
                UPDATE_NAME = s.UPDATE_NAME
               ,
                UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
          
        }

        #endregion

        #region >> 메뉴 등록
        /// <summary>
        /// GET: Common
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult StoreWebmenuRegList()
        {
            return View();
        }
        /// <summary>
        /// 매장별 웹메뉴 리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult pv_StoreWebMenuRegList(T_STORE_WEBMENU Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.MENU_CODE = -1; 
            ViewBag.list = Service.commoneService.GetStoreWebMenuList(Cond);

            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult StoreWebMenuSave(List<T_STORE_WEBMENU> list)
        {
            RTN_SAVE_DATA data = new CommonService().StoreWebMenuSave(list, Global.ConfigInfo.PROJECT_SITE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = data };
        }
        #endregion

        #region >> 메뉴그룹등록
        [AltloggalAuthorization]
        public ActionResult menuGroupRegList(int? id)
        {

            ViewBag.STORE_CODE = id == null ? SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE : id;
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_menuGroupRegList(T_STORE_WEBMENU_GROUP Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            Cond.STORE_CODE = Cond.STORE_CODE == null ? SessionHelper.LoginInfo.STORE_CODE : Cond.STORE_CODE;

            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.STORE_CODE = -1;
            ViewBag.list = new CommonService().GetWebGroupList(Cond);
            ViewBag.STORE_CODE = Cond.STORE_CODE;
            return PartialView2();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_menuGroupRegList2(T_STORE_WEBMENU_GROUP_MENU Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            Cond.STORE_CODE = Cond.STORE_CODE == null ? SessionHelper.LoginInfo.STORE_CODE : Cond.STORE_CODE;
            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.STORE_CODE = -1;
            ViewBag.list = new CommonService().GetGroupMenuList(Cond);

            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult menuGroupSave(List<T_STORE_WEBMENU_GROUP> list)
        {
            RTN_SAVE_DATA data = new CommonService().menuGroupSave(list
                                                            , Global.ConfigInfo.PROJECT_SITE
                                                             , SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = data };
        }
        [AltloggalAuthorization]
        public JsonResult GroupMenuSave(List<T_STORE_WEBMENU_GROUP_MENU> list)
        {
            RTN_SAVE_DATA data = new CommonService().GroupMenuSave(list
                                                             , SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = data };
        }

        #endregion
        [AltloggalAuthorization]
        public ActionResult GroupMemberReg(int? id)
        {
            ViewBag.STORE_CODE = id == null ? SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE : id;
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult MemberMenuReg(int? id)
        {
            ViewBag.STORE_CODE = id == null ? SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE : id;
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_GroupMemberRegList(T_STORE_WEBMENU_GROUP Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            Cond.STORE_CODE = Cond.STORE_CODE == null ? -1 : Cond.STORE_CODE;
            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.STORE_CODE = -1;
            ViewBag.list = new CommonService().GetWebGroupList(Cond);
            return PartialView2();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_GroupMemberRegList2(EMPLOYEE_INFO_COND Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            Cond.STORE_CODE = Cond.STORE_CODE == null ? -1 : Cond.STORE_CODE;
            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.STORE_CODE = -1;
            ViewBag.list = new ALT.BizService.AccountService().GetEmployeeInfoList(Cond);
            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult groupMemberSave(GROUP_MEMBER_SAVE Param)
        {
            Param.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            
            Param.REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            
            RTN_SAVE_DATA data = new CommonService().groupMemberSave(Param);

            return new JsonResult { Data = data };
        }
        [AltloggalAuthorization]
        public PartialViewResult PV_MemberMenuRegList(EMPLOYEE_INFO_COND Cond)
        {

            Cond.STORE_CODE = Cond.STORE_CODE == null ? -1 : Cond.STORE_CODE;
            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.STORE_CODE = -1;
            ViewBag.list = new ALT.BizService.AccountService().GetEmployeeInfoList(Cond);
            return PartialView2();
        }
        [AltloggalAuthorization]
        public PartialViewResult PV_MemberMenuRegList2(T_STORE_WEBMENU_EMPLOYEE_MENU Cond)
        {
            Cond.PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            Cond.STORE_CODE = Cond.STORE_CODE == null ? -1 : Cond.STORE_CODE;
            if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != 1) Cond.STORE_CODE = -1;
            ViewBag.list = new CommonService().GetMemberMenuList(Cond);
            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult MemberMenuSave(List<T_STORE_WEBMENU_EMPLOYEE_MENU> list)
        {

            int PROJECT_SITE = Global.ConfigInfo.PROJECT_SITE;
            
            int? REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = Service.commoneService.MemberMenuSave(list, REG_CODE);

            return new JsonResult { Data = data };
        }


        #region >> Combo
        /// <summary>
        /// 직원콤보
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult EmployeeCombo(string name, EMPLOYEE_COND Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            List<SelectListItem> combolist = new List<SelectListItem>();
            IList<EMPLOYEE_INFO> list = new ALT.BizService.EmployeeService().GetEmployeeList(Param);
            if (list == null)
            {
                list = new List<EMPLOYEE_INFO>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.MEMBER_CODE.ToString(), Text = s.USER_NAME, Selected = (selectedValue == s.MEMBER_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }

        /// <summary>
        /// 공통콤보
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult CommonCombo(string name, T_COMMON_COND Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            List<SelectListItem> combolist = new List<SelectListItem>();
            Param.NAME = "";
            IList<T_COMMON> list = new ALT.BizService.CommonService().GetCommon(Param);
            if (list == null)
            {
                list = new List<T_COMMON>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.SUB_CODE.ToString(), Text = s.NAME, Selected = (selectedValue == s.SUB_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }
        /// <summary>
        /// 업체콤보
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult CompanyCombo(string name, T_COMPANY_COND Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            List<SelectListItem> combolist = new List<SelectListItem>();
            IList<T_COMPANY> list = new ALT.BizService.BasicService().GetCompanyList(Param);
            if (list == null)
            {
                list = new List<T_COMPANY>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.COMPANY_CODE.ToString(), Text = s.COMPANY_NAME, Selected = (selectedValue == s.COMPANY_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }
        /// <summary>
        /// 지점콤보
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult StoreCombo(string name, T_STORE_COND Param, string selectedValue, string optionLabel, string htmlAttributes)
        {

            
            List<SelectListItem> combolist = new List<SelectListItem>();
            Param.PAGE_COUNT = 3000;
            IList<T_STORE> list = new ALT.BizService.BasicService().GetStoreList(Param);
            if (list == null)
            {
                list = new List<T_STORE>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.STORE_CODE.ToString(), Text = s.STORE_NAME, Selected = (selectedValue == s.STORE_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }

        /// <summary>
        /// 지점별 부서
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult StoreDeptCombo(string name, T_STORE_DEPT Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            Param.STORE_CODE = Param.STORE_CODE == null ? -1 : Param.STORE_CODE;
            List<SelectListItem> combolist = new List<SelectListItem>();
            IList<T_STORE_DEPT> list = new ALT.BizService.CommonService().GetDept(Param);


            if (list == null)
            {
                list = new List<T_STORE_DEPT>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.DEPT_CODE.ToString(), Text = s.DEPT_NAME, Selected = (selectedValue == s.DEPT_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }

        /// <summary>
        /// 지점별 직급 직책콤보
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult StorePositionCombo(string name, T_STORE_POSITION Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            Param.STORE_CODE = Param.STORE_CODE == null ? -1 : Param.STORE_CODE;
            List<SelectListItem> combolist = new List<SelectListItem>();
            IList<T_STORE_POSITION> list = new ALT.BizService.CommonService().GetPosition(Param);
            if (list == null)
            {
                list = new List<T_STORE_POSITION>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.POSITION_CODE.ToString(), Text = s.NAME, Selected = (selectedValue == s.POSITION_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }

        /// <summary>
        /// 메뉴그룹콤보
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult StoreMenuGroupCombo(string name, T_STORE_WEBMENU_GROUP Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            Param.STORE_CODE = Param.STORE_CODE == null ? -1 : Param.STORE_CODE;

            List<SelectListItem> combolist = new List<SelectListItem>();
            IList<T_STORE_WEBMENU_GROUP> list = new ALT.BizService.CommonService().GetWebGroupList(Param);
            if (list == null)
            {
                list = new List<T_STORE_WEBMENU_GROUP>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.GROUP_CODE.ToString(), Text = s.GROUP_NAME, Selected = (selectedValue == s.GROUP_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }

        /// <summary>
        /// 지점별그룹
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult StoreGroupCombo(string name, T_STORE_GROUP Param, string selectedValue, string optionLabel, string htmlAttributes)
        {
            Param.STORE_CODE = Param.STORE_CODE == null ? -1 : Param.STORE_CODE;

            List<SelectListItem> combolist = new List<SelectListItem>();
            IList<T_STORE_GROUP> list = new loggalServiceBiz.StoreService().GetStoreGroup(Param);
            if (list == null)
            {
                list = new List<T_STORE_GROUP>();
            }
            DROPDOWN_COND data = new DROPDOWN_COND
            {
                name = name
                ,
                selectList = list.Select(s => new SelectListItem { Value = s.GROUP_CODE.ToString(), Text = s.GROUP_NAME, Selected = (selectedValue == s.GROUP_CODE.ToString()) ? true : false }).ToList()
                ,
                optionLabel = optionLabel
                ,
                htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "" })
            };

            return PartialCombo(data);
        }
        #endregion
        [AltloggalAuthorization]
        public ActionResult LogList()
        {
            SessionHelper.LOG_NAME = "로그조회";
            return View();
        }
        public PartialViewResult PV_LogList(T_LOG_COND Cond)
        {
            if (SessionHelper.LoginInfo.COMPANY_CODE != 1) Cond.COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE;
            if (!(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4)) Cond.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;

            switch (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH)
            {
                case 2: /*상급부서*/
                    Cond.DEPT_SEARCH = SessionHelper.LoginInfo.EMPLOYEE.DEPT_SEARCH + "%";
                    break;
                case 3:/*상급부서*/
                    Cond.DEPT_SEARCH = SessionHelper.LoginInfo.EMPLOYEE.DEPT_SEARCH;
                    break;
                case 8:/*상급자*/
                    Cond.PARENT_MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
                    break;
                case 9:/*본인*/
                    Cond.LOGIN_MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
                    break;
            }

            ViewBag.list = Service.commoneService.GetLogList(Cond);
            return PartialView2();
        }
        [AltloggalAuthorization]
        public ActionResult Adfavorite()
        {
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_AdfavoriteList(T_AD_FAVORITE_COND Cond)
        {
            if (!(SessionHelper.LoginInfo.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1)) Cond.AD_CODE = -1;

            ViewBag.list = new AdvertisingService().GetAdFavoriteList(Cond);
            return PartialView2();
        }


        #region >> 지역 자동 검색
        /// <summary>
        /// 지역자동검색
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult PV_LocalNameList(CODE_DATA Cond)
        {
            ViewBag.Cond = Cond;
            ViewBag.list = new KeywordService().GetLocalNameList(Cond);
            return PartialView2();
        }
        #endregion
        [AltloggalAuthorization]
        public JsonResult KeywordList(string q, string type)
        {
           IList<CODE_DATA> list = new KeywordService().GetKeywordKoreanList(new ALT.VO.loggal.KEYWORD_COND { KEYWORD_TYPE = type, KEYWORD_NAME = q });
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #region >> 공통코드관리
        [AltloggalAuthorization]
        public ActionResult CommonRegList()
        {
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult PV_CommonRegList(T_COMMON_COND Cond)
        {
            if (!(SessionHelper.LoginInfo.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1)) Cond.MAIN_CODE = "-1";
            ViewBag.list = new ALT.BizService.CommonService().GetCommonPageList(Cond);
            return PartialView2();
        }

        [AltloggalAuthorization]
        public JsonResult CommonSave(List<T_COMMON> list)
        {
            int? REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = Service.commoneService.CommonSave(list, REG_CODE);

            return new JsonResult { Data = data };
        }
        [AltloggalAuthorization]
        public ActionResult CommonExcelReport(T_COMMON_COND Cond)
        {

            List<string> headers = new List<string>()
            { "그룹코드", "상세코드", "순번", "참조코드1", "참조코드2", "참조코드3", "참조코드4", "사용여부", "등록자", "등록시간" ,"수정자", "수정시간"   };
            return new ALT.BizService.CommonService().GetCommon(Cond).Select(s => new {
                MAIN_CODE = s.MAIN_CODE
               ,
                SUB_CODE = s.SUB_CODE
               ,
                ORDER_SEQ = s.ORDER_SEQ
               ,
                REF_DATA1 = s.REF_DATA1
               ,REF_DATA2 = s.REF_DATA2
               ,REF_DATA3 = s.REF_DATA3
               ,REF_DATA4 = s.REF_DATA4
               ,HIDE       = (s.HIDE == null || s.HIDE == false) ? "미사용" : "사용"
               ,INSERT_NAME = s.INSERT_NAME
               ,UPDATE_NAME = s.UPDATE_NAME
               ,UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }
        #endregion

        #region >> 지점별 그룹정보
        [AltloggalAuthorization]
        public PartialViewResult PV_StoreGroupList(T_STORE_GROUP Cond)
        {

            ViewBag.list = new loggalServiceBiz.StoreService().GetStoreGroup(Cond);
            return PartialView2();
        }

        [AltloggalAuthorization]
        public JsonResult StoreGroupSave(List<T_STORE_GROUP> list)
        {
            RTN_SAVE_DATA data = new StoreService().StoreGroupSave(list,  SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = data };
        }

        #endregion

        [AltloggalAuthorization]
        public JsonResult GetDaumMapApiAddressTolnglat(string ADDRESS)
        {
            dynamic data = GlobalMvc.WebService.GetAPIServer<dynamic>("https://dapi.kakao.com/v2/local/search/address.json?query=" + Server.UrlEncode(ADDRESS) + "&output=json", true);
            return new JsonResult { Data = JsonConvert.SerializeObject(data.documents), JsonRequestBehavior= JsonRequestBehavior.AllowGet };
        }
		
		[AltloggalAuthorization]
		public JsonResult GetCoord2address(string lat, string longi)
		{
			dynamic data = GlobalMvc.WebService.GetAPIServer<dynamic>("https://dapi.kakao.com/v2/local/geo/coord2address.json?x=" + Server.UrlEncode(longi.Replace("_", ".")) + "&y=" + Server.UrlEncode(lat.Replace("_", ".")) + "&input_coord=WGS84", true);
			return new JsonResult { Data = JsonConvert.SerializeObject(data.documents), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
		}
		[AltloggalAuthorization]
        public JsonResult FileSave(T_FILE param)
        {
            param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = new CommonService().FileSave(param);
            return new JsonResult { Data = data };
        }
        [AltloggalAuthorization]
        public ActionResult Policy(int? id) {
            ViewBag.nPage = id ?? 0; ;
            return View();
        }
      
    }
}