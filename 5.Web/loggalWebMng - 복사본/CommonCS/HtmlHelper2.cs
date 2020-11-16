using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Web.Mvc.Html;
using ALT.Framework.Mvc;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using ALT.BizService;
using System.Web.Routing;
using System.Collections;
using loggalWebMng.Controllers;
using System.Text;
using ALT.Framework;

namespace loggalWebMng.CommonCS
{
    public static class HtmlHelper2
    {
        public static T_COMMON GetCommon(this int? val, string mainCode = "")
        {
            T_COMMON rtn = new CommonService().GetCommon(new T_COMMON_COND { MAIN_CODE = mainCode, SUB_CODE = val }).FirstOrDefault();
            return rtn;
        }
        public static string semaintic_size = "mini";
        public static MvcHtmlString Title(this HtmlHelper helper)
        {
            if (string.IsNullOrEmpty(helper.ViewBag.Title))
            {
                var chkurl = "/" + helper.ViewContext.RouteData.Values["controller"].ToString().ToLower() + "/" + helper.ViewContext.RouteData.Values["action"].ToString().ToLower();
                LOGIN_WEBMENU data = SessionHelper.LoginInfo.WebMemu.Where(w => w.MENU_URL.ToLower().Contains(chkurl)).FirstOrDefault();
                if (data == null) { data = new LOGIN_WEBMENU { NAME = "loggal Management" }; }

                helper.ViewBag.Title =data.NAME;
                SessionHelper.LoginInfo.CURRENT_MENU_NAME = data.NAME;
            }
            
            return new MvcHtmlString(helper.ViewBag.Title);

        }

        public static MvcHtmlString TitleHeader(this HtmlHelper helper, string addTitle="")
        {
            if (string.IsNullOrEmpty(helper.ViewBag.Title))
            {
                var chkurl = "/" + helper.ViewContext.RouteData.Values["controller"].ToString().ToLower() + "/" + helper.ViewContext.RouteData.Values["action"].ToString().ToLower();
                LOGIN_WEBMENU data = SessionHelper.LoginInfo.WebMemu.Where(w => w.MENU_URL.ToLower().Contains(chkurl)).FirstOrDefault();
                if (data == null) { data = new LOGIN_WEBMENU { NAME = "loggal Management" }; }

                helper.ViewBag.Title = data.NAME;

                SessionHelper.LoginInfo.CURRENT_MENU_NAME = data.NAME;
            }
          
            return new MvcHtmlString("<h2 class=\"ui header\">" + helper.ViewBag.Title + addTitle + "</h2>");

        }
        public static MvcHtmlString TitleNavigation(this HtmlHelper helper,  string addTitle)
        {
            return TitleNavigation(helper, SemanticUIHelper.Size.big, addTitle);
        }
        public static MvcHtmlString TitleNavigation(this HtmlHelper helper, SemanticUIHelper.Size size = SemanticUIHelper.Size.big, string addTitle = "")
        {
           
            List<LOGIN_WEBMENU> activeMenuList = new List<LOGIN_WEBMENU>();
           
            var chkurl = "/" + helper.ViewContext.RouteData.Values["controller"].ToString().ToLower() + "/" + helper.ViewContext.RouteData.Values["action"].ToString().ToLower();
            LOGIN_WEBMENU data = SessionHelper.LoginInfo.WebMemu.Where(w => w.MENU_URL.ToLower().Contains(chkurl)).FirstOrDefault();
            if (data == null) { data = new LOGIN_WEBMENU { NAME = "loggal Management" }; }
            else
            {
                activeMenuList.Add(data);
            }
            SessionHelper.LoginInfo.CURRENT_MENU_NAME = data.NAME;
            while (true)
            {
                data = SessionHelper.LoginInfo.WebMemu.Where(w => w.MENU_CODE == data.PARENT_CODE).FirstOrDefault();
                if (data == null) break;
                else activeMenuList.Add(data);

            }
           
            if (activeMenuList.Count() == 0) return TitleHeader(helper, addTitle);
            else
            {
                activeMenuList = activeMenuList.OrderBy(o => o.SEARCH_CODE).ToList();
                StringBuilder sbHtml = new StringBuilder();
                sbHtml.Append("<div class='ui ").Append(size.ToString()).Append(" breadcrumb'>");

                for (int i = 0; i < activeMenuList.Count(); i ++)
                {
                    if (i < activeMenuList.Count() - 1)
                    {
                        sbHtml.Append("<a class='section'>").Append(activeMenuList[i].NAME).Append("</a>");
                        sbHtml.Append("<i class='right angle icon divider'></i> ");
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(helper.ViewBag.Title))
                        {
                            string name = activeMenuList[i].NAME;
                            try
                            {
                                if (Global.Format.IsNumeric(HttpContext.Current.Request.Url.Segments[HttpContext.Current.Request.Url.Segments.Count() - 1]))
                                {
                                    name = name.Replace("등록", "정보");
                                }
                            }
                            catch (Exception) { }
                          
                            
                            sbHtml.Append("<div class='active section'>").Append(name);

                            SessionHelper.LoginInfo.CURRENT_MENU_NAME = name;
                        }
                        else
                        {
                            sbHtml.Append("<div class='active section'>").Append(helper.ViewBag.Title);
                        }

                        sbHtml.Append("</div>");
                    }
                    
                }
                sbHtml.Append((string.IsNullOrEmpty(addTitle) ? "" : addTitle)).Append("</div>");
                return new MvcHtmlString(sbHtml.ToString());
            }
        }

        /// <summary>
        /// 공통코드 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString CommonCombo(this HtmlHelper helper, string id, T_COMMON_COND Param, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null, enComboDisType enComboDisType = enComboDisType.NAME)
        {
            /***ID가 STATUS인경우 selected에 문제가 발생함(PartialView을 Ajax호출할 경우 발생함)  ****/
            optionLabel = optionLabel ?? "-선택-";
            if (optionLabel == "-1*") optionLabel = null;
            Param.HIDE = (Param.HIDE == null) ? false : Param.HIDE;
            IList<T_COMMON> list = new CommonService().GetCommon(Param);
            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);


            IEnumerable<SelectListItem> combo = new List<SelectListItem>();
            switch (enComboDisType)
            {
                case enComboDisType.REF_DATA1:
                    combo = list.Select(s => new SelectListItem { Value = s.SUB_CODE.ToString(), Text = s.REF_DATA1, Selected = (selectedValue == s.SUB_CODE.ToString()) ? true : false }).ToList();
                    break;
                case enComboDisType.REF_DATA2:
                    combo = list.Select(s => new SelectListItem { Value = s.SUB_CODE.ToString(), Text = s.REF_DATA2, Selected = (selectedValue == s.SUB_CODE.ToString()) ? true : false }).ToList();
                    break;
                case enComboDisType.REF_DATA3:
                    combo = list.Select(s => new SelectListItem { Value = s.SUB_CODE.ToString(), Text = s.REF_DATA3, Selected = (selectedValue == s.SUB_CODE.ToString()) ? true : false }).ToList();
                    break;
                default:
                    combo = list.Select(s => new SelectListItem { Value = s.SUB_CODE.ToString(), Text = s.NAME, Selected = (selectedValue == s.SUB_CODE.ToString()) ? true : false }).ToList();
                    break;
                
            }
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            //shtml += "<script >$('document').ready(function () { try{ $('#" + id + "').prev().html($('#" + id + "  option:selected').text()); } catch(e){} }); </script>";

            return new MvcHtmlString(shtml);
        }
        public enum enComboDisType
        {
            NAME, REF_DATA1, REF_DATA2, REF_DATA3
        }
        public static MvcHtmlString CommonComboNormal(this HtmlHelper helper, string id, List<SelectListItem> list, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            var combo= (List<SelectListItem>)list.Select(s => new SelectListItem { Value = s.Value, Text = s.Text, Selected = (selectedValue == s.Value) ? true : false }).ToList();

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes,semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            //shtml += "<script >$('document').ready(function () { try{ $('#" + id + "').prev().html($('#" + id + "  option:selected').text()); } catch(e){} }); </script>";

            return new MvcHtmlString(shtml);
        }

        /// <summary>
        /// 회원콤보
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString MemberCombo(this HtmlHelper helper, string id, T_MEMBER_COND Param, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            IList<T_MEMBER> list = new AccountService().GetMemberList(Param);

            var combo = list.Select(s => new SelectListItem { Value = s.MEMBER_CODE.ToString(), Text = s.USER_NAME, Selected = (selectedValue == s.MEMBER_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            return new MvcHtmlString(shtml);
        }

        /// <summary>
        /// 직원의 직책, 권한에 따른 직원조회
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString MemberCombo2(this HtmlHelper helper, string id, EMPLOYEE_COND Param, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            //IList<T_MEMBER> list = new BaseService().GetMember(Param);
            IList<EMPLOYEE_INFO> list = Service.employeeService.GetEmployeeList(Param);

            if (list == null)
            {
                list = new List<EMPLOYEE_INFO>();
            }
            var combo = list.Select(s => new SelectListItem { Value = s.MEMBER_CODE.ToString(), Text = s.USER_NAME, Selected = (selectedValue == s.MEMBER_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SetDefaultSemanticSize(htmlAttributes);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();

            return new MvcHtmlString(shtml);
        }

        /// <summary>
		/// 클래스에 사이즈가 없을 경우 mini 추가
		/// </summary>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		private static RouteValueDictionary SetDefaultSemanticSize(object htmlAttributes)
        {
            var arrHtmlAtribute = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (htmlAttributes == null)
            {
                arrHtmlAtribute["class"] = semaintic_size;
            }
            else
            {
                var classData = arrHtmlAtribute.Where(w => w.Key == "class").ToList();
                if (classData.Count() > 0)
                {
                    var array = Enum.GetNames(typeof(SemanticUIHelper.Size)).ToList();
                    if (array.Where(w => ((string)classData[0].Value).Contains(w)).Count() == 0)
                        arrHtmlAtribute["class"] = arrHtmlAtribute["class"] + " " + semaintic_size;
                }
            }
            return arrHtmlAtribute;
        }

        public static MvcHtmlString CompCombo(this HtmlHelper helper, string id, T_COMPANY_COND Param = null, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            Param = (Param == null) ? new T_COMPANY_COND { } : Param;
            Param.PAGE_COUNT = 10000;
            Param.SORT = "A.COMPANY_NAME";

            if (SessionHelper.LoginInfo.COMPANY_CODE != 1)
            {
                Param.COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE;
                optionLabel = null;
            }
            IList<T_COMPANY> list = Service.basicService.GetCompanyList(Param);



            var combo = list.Select(s => new SelectListItem { Value = s.COMPANY_CODE.ToString(), Text = s.COMPANY_NAME, Selected = (selectedValue == s.COMPANY_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            return new MvcHtmlString(shtml);
        }

        public static MvcHtmlString CompCombo(this HtmlHelper helper, string id, IList<T_COMPANY> list, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
    
            
            var combo = list.Select(s => new SelectListItem { Value = s.COMPANY_CODE.ToString(), Text = s.COMPANY_NAME, Selected = (selectedValue == s.COMPANY_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            return new MvcHtmlString(shtml);
        }
        /// <summary>
        /// 회사/매장콤보
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString CompanyStoreCombo(this HtmlHelper helper, string id, T_STORE_COND Param = null, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            Param = (Param == null) ? new T_STORE_COND { DISPLAY_MODE = "Normal" } : Param;
            if (Param.DISPLAY_MODE != "Total")
            {
                if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
                {
                    Param.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
                }

                if (!(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
                {
                    Param.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
                    selectedValue = null;
                }
            }
            if (Param.COMPANY_CODE == null)
            {
                Param.COMBO_DISPLAY = 1;
            }
            if (Param.PAGE_COUNT == null) Param.PAGE_COUNT = 999999999;
            IList<T_STORE> list = Service.basicService.GetStoreList(Param).OrderBy(o => o.COMPANY_NAME).ThenBy(o=>o.STORE_TYPE).ThenBy(o=>o.STORE_NAME).ToList();
            if (list.Count() == 1) optionLabel = null;
            var combo = list.Select(s => new SelectListItem { Value = s.COMPANY_CODE.ToString() + "|" + ((s.STORE_TYPE == 1 || s.STORE_TYPE == 4) ? "" : s.STORE_CODE.ToString()), Text = ((Param.COMBO_DISPLAY == 1 && s.COMPANY_NAME != s.STORE_NAME) ? (s.COMPANY_NAME + ">") : "") + s.STORE_NAME, Selected = (selectedValue == s.STORE_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            return new MvcHtmlString(shtml);
        }
        /// <summary>
        /// 매장콤보
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="Param"></param>
        /// <param name="selectedValue"></param>
        /// <param name="optionLabel"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString StoreCombo(this HtmlHelper helper, string id, T_STORE_COND Param =null, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            Param = (Param == null) ? new T_STORE_COND { DISPLAY_MODE ="Normal" } : Param;
            if (Param.DISPLAY_MODE != "Total")
            {
                if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
                {
                    Param.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
                }

                if (!(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
                {
                    Param.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
                    selectedValue = null;
                }
            }
            if (Param.COMPANY_CODE == null)
            {
                Param.COMBO_DISPLAY = 1;
            }
            if (Param.PAGE_COUNT == null) Param.PAGE_COUNT = 999999999;
            IList<T_STORE> list = Service.basicService.GetStoreList(new T_STORE_COND());
            if (list.Count() == 1) optionLabel = null;
            var combo = list.Select(s => new SelectListItem { Value = s.STORE_CODE.ToString(), Text = ((Param.COMBO_DISPLAY == 1 && s.COMPANY_NAME != s.STORE_NAME) ? ( s.COMPANY_NAME + ">" ) : "")+ s.STORE_NAME, Selected = (selectedValue == s.STORE_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            return new MvcHtmlString(shtml);
        }

        public static MvcHtmlString StoreCombo(this HtmlHelper helper, string id, IList<T_STORE> list,  string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
        
            if (list.Count() == 1) optionLabel = null;
            var combo = list.Select(s => new SelectListItem { Value = s.STORE_CODE.ToString(), Text = s.STORE_NAME, Selected = (selectedValue == s.STORE_CODE.ToString()) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            return new MvcHtmlString(shtml);
        }

        public static MvcHtmlString AutoCompleate(this HtmlHelper helper, string id, string url, Hashtable Params = null, object htmlAttributes = null)
        {
            System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();

            System.Text.StringBuilder sbdivAttrbuite = new System.Text.StringBuilder();
            System.Text.StringBuilder sbinputAttrbuite = new System.Text.StringBuilder();

            string splaceholder = "placeholder='검색어를 입력하세요'";
            foreach (var att in HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).ToList())
            {

                if (att.Key == "placeholder"
                    || att.Key == "placeholder"
                    )
                {
                    splaceholder = "placeholder='" + att.Value + "'";
                    //sbinputAttrbuite.Append(" ").Append(att.Key).Append("=").Append("'").Append(att.Value).Append("'");
                }
                else if (att.Key != "")
                {
                    sbdivAttrbuite.Append(" ").Append(att.Key).Append("=").Append("'").Append(att.Value).Append("'");
                }

            }

            
            sbHtml.Append("\n").Append("<div id='dv_" + id + "' class='easy-autocomplete ui input mini' ").Append(sbdivAttrbuite.ToString()).Append(">         ");
            sbHtml.Append("\n").Append("    <input type='text' id='" + id + "' name='" + id + "' ").Append(splaceholder).Append(" autocomplete ='off'").Append(sbinputAttrbuite.ToString()).Append(">  ");
            sbHtml.Append("\n").Append("    <div class='easy-autocomplete-container' id='eac-container-inputOne'>		");
            sbHtml.Append("\n").Append("        <ul style='display:block'></ul>											");
            sbHtml.Append("\n").Append("    </div>																		");
            sbHtml.Append("\n").Append("</div>	");
            


            sbHtml.Append("\n").Append("    <script type=\"text/javascript\">    ");
            sbHtml.Append("\n").Append(" $(\"document\").ready(function(){ ");
            sbHtml.Append("\n").Append(id + "_KEYUP(); ");
            sbHtml.Append("\n").Append("   }); ");
            sbHtml.Append("\n").Append("        function " + id + "_KEYUP() {                                                                                                    ");
            sbHtml.Append("\n").Append("            $(\"#" + id + "\").focusout(function () { setTimeout('$(\"#dv_" + id + ".easy-autocomplete\").find(\"ul\").hide()', 200); }); ");
            sbHtml.Append("\n").Append("            $(\"#" + id + "\").keyup(function (e) {																						");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("                if (e.keyCode == 40) //아래 방향키																					   ");
            sbHtml.Append("\n").Append("                {																														");
            sbHtml.Append("\n").Append("                    if ($(\"#dv_" + id + ".easy-autocomplete ul li.selected\").length == 0) {											");
            sbHtml.Append("\n").Append("                        $(\"#dv_" + id + ".easy-autocomplete ul li:eq(0)\").addClass(\"selected\")										");
            sbHtml.Append("\n").Append("                    } else {																											");
            sbHtml.Append("\n").Append("                        var preli = $(\"#dv_" + id + ".easy-autocomplete ul li.selected\");												");
            sbHtml.Append("\n").Append("                        $(\"#dv_" + id + ".easy-autocomplete ul li\").removeClass(\"selected\");											");
            sbHtml.Append("\n").Append("                        preli.next().addClass(\"selected\");																			");
            sbHtml.Append("\n").Append("                    }																													");
            sbHtml.Append("\n").Append("                    return;																												");
            sbHtml.Append("\n").Append("                }																														");
            sbHtml.Append("\n").Append("                else if (e.keyCode == 38) { // 위 방향키																				");
            sbHtml.Append("\n").Append("                    var preli = $(\"#dv_" + id + ".easy-autocomplete ul li.selected\");													");
            sbHtml.Append("\n").Append("                    $(\"#dv_" + id + ".easy-autocomplete ul li\").removeClass(\"selected\");												");
            sbHtml.Append("\n").Append("                    preli.prev().addClass(\"selected\");																				");
            sbHtml.Append("\n").Append("                    return;																												");
            sbHtml.Append("\n").Append("                }																														");
            sbHtml.Append("\n").Append("                else if (e.keyCode == 37 || e.keyCode == 39) {																			");
            sbHtml.Append("\n").Append("                    return;																												");
            sbHtml.Append("\n").Append("                }																														");
            sbHtml.Append("\n").Append("                else if (e.keyCode == 13) {																								");
            //sbHtml.Append("\n").Append("                    var msg = \"지역명을 (코드 : \" + $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").attr(\"keyCode\");	  ");
            //sbHtml.Append("\n").Append("                    msg += \" / 코드명 : \" + $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").text();						 ");
            //sbHtml.Append("\n").Append("                    msg += \" / 위도 : \" + $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").attr(\"LATITUDE\");				  ");
            //sbHtml.Append("\n").Append("                    msg += \" / 경도 : \" + $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").attr(\"LONGITUDE\");			  ");
            //sbHtml.Append("\n").Append("                    msg += \")로 검색합니다.\"																						  ");
            //sbHtml.Append("\n").Append("                    MessageWrite(msg, 2);																								");
            sbHtml.Append("\n").Append("			try{ var param = new Object();															");
            sbHtml.Append("\n").Append("               param.CODE = $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").attr(\"keyCode\");												  ");
            sbHtml.Append("\n").Append("               param.NAME = $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").text();					 ");
            sbHtml.Append("\n").Append("               param.LATITUDE = $(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").attr(\"LONGITUDE\");														  ");
            sbHtml.Append("\n").Append("               param.LONGITUDE = $(this).find(\"div\").attr(\"LONGITUDE\");														  ");
            sbHtml.Append("\n").Append("               " + id + "_callback(param);												  ");
            sbHtml.Append("\n").Append("			} catch(e){}   ");

            sbHtml.Append("\n").Append("                    $(\"#" + id + "\").val($(\"#dv_" + id + ".easy-autocomplete ul li.selected div\").text());							");
            sbHtml.Append("\n").Append("                    $(\"#dv_" + id + ".easy-autocomplete\").find(\"ul\").hide();															");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("                    return false;																												");
            sbHtml.Append("\n").Append("                }																														");
            sbHtml.Append("\n").Append("                $(\"#dv_" + id + ".easy-autocomplete\").find(\"ul\").show();																");
            sbHtml.Append("\n").Append("                //setTimeout(SetAuto").Append(id).Append("(), 300);																					");
            sbHtml.Append("\n").Append("                if ((e.keyCode >= 48 && e.keyCode <= 57)																				");
            sbHtml.Append("\n").Append("                    || (e.keyCode >= 65 && e.keyCode <= 90)																				");
            sbHtml.Append("\n").Append("                    || (e.keyCode >= 12592 && e.keyCode <= 12687)																		");
            sbHtml.Append("\n").Append("                    || (e.keyCode == 8)	 || (e.keyCode == 40)																									");
            sbHtml.Append("\n").Append("                    )																													");
            sbHtml.Append("\n").Append("                    BaseCommon.TimeInfo.Delay(function () {																				");
            sbHtml.Append("\n").Append("                        if (_" + id + " == $(\"#" + id + "\").val()) return;																");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("                    SetAuto").Append(id).Append("($(\"#" + id + "\").val());																			");
            sbHtml.Append("\n").Append("                }, 150);																												");
            sbHtml.Append("\n").Append("            });																															");
            sbHtml.Append("\n").Append("        }																																");
            sbHtml.Append("\n").Append("        var _" + id + " = \"\";																											");
            sbHtml.Append("\n").Append("       																																	");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("        function SetAuto").Append(id).Append("(" + id + ", Param2)																							");
            sbHtml.Append("\n").Append("        {																																");
            sbHtml.Append("\n").Append("            _" + id + " = " + id + ";																										");
            sbHtml.Append("\n").Append("            var params = new Object();																									");
            sbHtml.Append("\n").Append("            params.NAME = " + id + ";																									");
            sbHtml.Append("\n").Append("            params.CODE = 15;																											");

            sbHtml.Append("\n").Append("            if (Param2 != undefined)                    ");
            sbHtml.Append("\n").Append("            {                                           ");
            sbHtml.Append("\n").Append("                for (var key in Param2)                 ");
            sbHtml.Append("\n").Append("                {                                       ");
            sbHtml.Append("\n").Append("                    params[key] =  Param2[key];         ");
            sbHtml.Append("\n").Append("                }                                       ");
            sbHtml.Append("\n").Append("            }                                           ");

            if (Params != null)
            {
                foreach (DictionaryEntry entry in Params)
                {
                    sbHtml.Append("\n").Append("            params.").Append(entry.Key).Append(" = '").Append(entry.Value).Append("'");
                }
            }

            sbHtml.Append("\n").Append("            params = JSON.stringify(params);																							");
            sbHtml.Append("\n").Append("            var url = \"" + url + "\";																						");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("            if(" + id + " == \"\") {																										");
            sbHtml.Append("\n").Append("                $(\"#dv_" + id + ".easy-autocomplete\").find(\"ul\").hide();																");
            sbHtml.Append("\n").Append("            }																															");
            sbHtml.Append("\n").Append("            ajax.GetAjax(url, params, \"html\", function (result) {																		");

            sbHtml.Append("\n").Append("                " + id + "AutoCompleate(result)																							");
            sbHtml.Append("\n").Append("            },false);																													");
            sbHtml.Append("\n").Append("        }																																");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("        function " + id + "AutoCompleate(result) {																						");
            sbHtml.Append("\n").Append("            $(\"#dv_" + id + ".easy-autocomplete ul\").html(result);																		");
            sbHtml.Append("\n").Append("            $(\"#dv_" + id + ".easy-autocomplete\").find(\"li\").click(function () {														");
            sbHtml.Append("\n").Append("			try{ var param = new Object();															");
            sbHtml.Append("\n").Append("               param.CODE =  $(this).find(\"div\").attr(\"keyCode\");												  ");
            sbHtml.Append("\n").Append("               param.NAME =  $(this).find(\"div\").text();																	 ");
            sbHtml.Append("\n").Append("               param.LATITUDE = $(this).find(\"div\").attr(\"LATITUDE\");														  ");
            sbHtml.Append("\n").Append("               param.LONGITUDE = $(this).find(\"div\").attr(\"LONGITUDE\");														  ");
            sbHtml.Append("\n").Append("               " + id + "_callback(param);												  ");
            sbHtml.Append("\n").Append("			} catch(e){}   ");

            sbHtml.Append("\n").Append("																																		");

            sbHtml.Append("\n").Append("                $(\"#" + id + "\").val($(this).find(\"div\").text());																	");
            sbHtml.Append("\n").Append("																																		");
            sbHtml.Append("\n").Append("                $(\"#dv_" + id + ".easy-autocomplete\").find(\"ul\").hide();																");
            sbHtml.Append("\n").Append("            })																															");
            sbHtml.Append("\n").Append("        }																																");

            sbHtml.Append("\n").Append("    </script> ");
            return new MvcHtmlString(sbHtml.ToString());
        }


    }
}