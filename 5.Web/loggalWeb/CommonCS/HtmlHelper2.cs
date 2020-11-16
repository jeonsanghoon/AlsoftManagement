using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALT.VO.Common;

using System.Web.Mvc;
using System.Web.Mvc.Html;
using loggalServiceBiz;
using ALT.Framework.Mvc.Helpers;
using System.Collections;

namespace loggalWeb.CommonCS
{
    public static class SemanticUI_Helper2
    {
        public static string semaintic_size = "mini";
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
        public static MvcHtmlString CommonCombo(this HtmlHelper helper, string id, T_COMMON_COND Param, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            Param.HIDE = (Param.HIDE == null) ? false : Param.HIDE;
            IList<T_COMMON> list = new BaseService().GetCommon(Param);

            var combo = list.Select(s => new SelectListItem { Value = s.SUB_CODE.ToString(), Text = s.NAME, Selected = (selectedValue == s.SUB_CODE.ToString()) ? true : false });
            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);

            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            //shtml += "<script >$('document').ready(function () { try{ $('#" + id + "').prev().html($('#" + id + "  option:selected').text()); } catch(e){} }); </script>";

            return new MvcHtmlString(shtml);
        }

        public static MvcHtmlString CommonComboNormal(this HtmlHelper helper, string id, List<SelectListItem> list, string selectedValue = null, string optionLabel = "-선택-", object htmlAttributes = null)
        {
            var combo = list.Select(s => new SelectListItem { Value = s.Value, Text = s.Text, Selected = (selectedValue == s.Value) ? true : false });

            var arrHtmlAtribute = SemanticUIHelper.SetDefaultSemanticSize(htmlAttributes, semaintic_size);
            string shtml = helper.DropDownList(id, combo, optionLabel, arrHtmlAtribute).ToHtmlString();
            //shtml += "<script >$('document').ready(function () { try{ $('#" + id + "').prev().html($('#" + id + "  option:selected').text()); } catch(e){} }); </script>";

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


            sbHtml.Append("\n").Append("<div id='dv_" + id + "' class='easy-autocomplete' ").Append(sbdivAttrbuite.ToString()).Append(">         ");
            sbHtml.Append("\n").Append("    <input id='" + id + "' name='" + id + "' ").Append(splaceholder).Append(" autocomplete ='off'").Append(sbinputAttrbuite.ToString()).Append(">  ");
            sbHtml.Append("\n").Append("    <div class='easy-autocomplete-container' id='eac-container-inputOne'>		");
            sbHtml.Append("\n").Append("        <ul style='display:block'></ul>											");
            sbHtml.Append("\n").Append("    </div>																		");
            sbHtml.Append("\n").Append("</div>																			");


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
            sbHtml.Append("\n").Append("                    return;																												");
            sbHtml.Append("\n").Append("                }																														");
            sbHtml.Append("\n").Append("                $(\"#dv_" + id + ".easy-autocomplete\").find(\"ul\").show();																");
            sbHtml.Append("\n").Append("                //setTimeout(SetAuto").Append(id).Append("(), 300);																					");
            sbHtml.Append("\n").Append("                if ((e.keyCode >= 48 && e.keyCode <= 57)																				");
            sbHtml.Append("\n").Append("                    || (e.keyCode >= 65 && e.keyCode <= 90)																				");
            sbHtml.Append("\n").Append("                    || (e.keyCode >= 12592 && e.keyCode <= 12687)																		");
            sbHtml.Append("\n").Append("                    || (e.keyCode == 8)																									");
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
            sbHtml.Append("\n").Append("            var url = \"" + url +"\";																						");
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
            //public static MvcHtmlString TinyMCE_UploadTargetForm(this HtmlHelper helper, string id)
            //{
            //    System.Text.StringBuilder sbHtml = new System.Text.StringBuilder();
            //    sbHtml.Append("\n").Append("<iframe id=\"form_target\" name=\"form_target\" style =\"display: none\" ></iframe> ");
            //    sbHtml.Append("\n").Append("<form id=\"").Append(id).Append("_form\" action =\"/base/UploadImage/\" target =\"form_target\" method =\"post\" enctype =\"multipart/form-data\" style =\"width: 0px; height: 0; overflow: hidden\" > ");
            //    sbHtml.Append("\n").Append("    <input id=\"").Append(id).Append("_file1\" name =\"").Append(id).Append("_file1\" type =\"file\" onchange =\"").Append(id).Append("_image_Onchange(); \"> ");
            //    sbHtml.Append("\n").Append("</form> ");
            //    return new MvcHtmlString(sbHtml.ToString());
            //}
        }

}