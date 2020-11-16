using System.Drawing;
using ALT.BizService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ALT.Framework.Mvc;
using System.Web.Mvc;
using System.Text;
using ALT.Framework.Data;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using loggalWebMng.CommonCS;



namespace loggalWebMng.CommonCS
{
    public static class SemanticUIHelper2
    {
        #region >> 버튼정의(메뉴권한)

        public static MvcHtmlString Semantic_Button2(this HtmlHelper helper, string id, string text, SemanticUIHelper.enButtonType btnType = SemanticUIHelper.enButtonType.Normal, object htmlAttributes = null)
        {
            return Semantic_Button2(helper, id, text, btnType, SemanticUIHelper.Size.mini, htmlAttributes);
        }

        public static MvcHtmlString Semantic_ButtonIcon(this HtmlHelper helper, string id, string text, SemanticUIHelper.enButtonType btnType, SemanticUIHelper.Size size, object htmlAttributes = null, string iconName = null)
        {
            return Semantic_Button2(helper, id, text, btnType, SemanticUIHelper.Size.mini, htmlAttributes, null, iconName);
        }


        /// <summary>
        /// 버튼(메뉴권한추가)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <param name="btnType"></param>
        /// <param name="size"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_Button2(this HtmlHelper helper, string id, string text, SemanticUIHelper.enButtonType btnType, SemanticUIHelper.Size size, object htmlAttributes = null, string buttonpageURL = null, string iconName = null)
        {
            var chkurl = "/" + helper.ViewContext.RouteData.Values["controller"].ToString().ToLower() + "/" + helper.ViewContext.RouteData.Values["action"].ToString().ToLower();
            if (!string.IsNullOrEmpty(buttonpageURL)) chkurl = buttonpageURL;
            LOGIN_WEBMENU data = SessionHelper.LoginInfo.WebMemu.Where(w => w.MENU_URL.ToLower().Contains(chkurl)).FirstOrDefault();
            data = (data == null) ? new LOGIN_WEBMENU() { INSERT_AUTH = true, UPDATE_AUTH = true, EXCEL_AUTH = true, PRINT_AUTH = true } : data; /*자신의 메뉴에 없을 경우 버튼 비활성화*/
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append(" <button ");

            //// Type가 지정 안되었을 경우 기본 설정
            var arrType = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).Where(w => w.Key.ToLower() == "type").ToList();
            if (arrType.Count > 0) sbHtml.Append(arrType[0].Key).Append("='").Append(arrType[0].Value).Append("'");
            else sbHtml.Append("type='button'");

           
            sbHtml.Append(" class='ui ");
            var arrClass = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).Where(w => w.Key.ToLower() == "class").ToList();
            foreach (var cssData in arrClass)
            {
                sbHtml.Append(" ").Append(cssData.Value).Append(" ");
            }

            sbHtml.Append(" ").Append(btnType.ToString()).Append(" ");
            switch (btnType)
            {
                case SemanticUIHelper.enButtonType.Del:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.red.ToString()).Append(" ");
                    break;
                case SemanticUIHelper.enButtonType.Cancel:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.orange.ToString()).Append(" ");
                    break;
                case SemanticUIHelper.enButtonType.Request:
                case SemanticUIHelper.enButtonType.New:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.teal.ToString()).Append(" ");
                    break;
                case SemanticUIHelper.enButtonType.Save:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.blue.ToString()).Append(" ");
                    break;
                case SemanticUIHelper.enButtonType.Excel:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.green.ToString()).Append(" ");
                    break;
                case SemanticUIHelper.enButtonType.ToList:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.olive.ToString()).Append(" ");
                    sbHtml.Append("  ");
                    break;
                case SemanticUIHelper.enButtonType.Check:
                    sbHtml.Append(" check ");
                    break;
                case SemanticUIHelper.enButtonType.Print:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.brown.ToString()).Append(" ");
                    break;
                default:
                    break;
            }

            switch (btnType)
            {
                case SemanticUIHelper.enButtonType.New:
                    if (!data.INSERT_AUTH) sbHtml.Append(" hide ");
                    else if (SessionHelper.LoginInfo.EDIT_MODE == enEditMode.READ)
                    {
                        sbHtml.Append(" hide ");
                    }
                    break;
                case SemanticUIHelper.enButtonType.Save:
                case SemanticUIHelper.enButtonType.Del:
                case SemanticUIHelper.enButtonType.Cancel:
                    if (!data.UPDATE_AUTH) sbHtml.Append(" hide ");
                    else if (SessionHelper.LoginInfo.EDIT_MODE == enEditMode.READ)
                    {
                        sbHtml.Append(" hide ");
                    }
                    break;

                case SemanticUIHelper.enButtonType.Excel:
                    if (!data.EXCEL_AUTH) sbHtml.Append(" hide ");
                    break;
                case SemanticUIHelper.enButtonType.Print:
                    if (!data.PRINT_AUTH) sbHtml.Append(" hide ");
                    break;

                default:
                    break;
            }

          


            sbHtml.Append(size.ToString()).Append(" button' id='").Append(id).Append("' name='").Append(id).Append("'").Append(SemanticUIHelper.GetHtmlAttributeString(htmlAttributes)).Append("> ").Append("\n");
            sbHtml.Append(text).Append("\n");
            if (!string.IsNullOrEmpty(iconName))
            {
                sbHtml.Append("<i class='").Append(iconName).Append("'></i>");
            }
            sbHtml.Append(" </button>").Append("\n");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }
        #endregion

        public static MvcHtmlString Semantic_Button3(this HtmlHelper helper, string id, string text, SemanticUIHelper.enButtonType btnType = SemanticUIHelper.enButtonType.Normal, object htmlAttributes = null, string buttonPageUrl = null)
        {
            return Semantic_Button2(helper, id, text, btnType, SemanticUIHelper.Size.mini, htmlAttributes, buttonPageUrl);
        }

        public static MvcHtmlString Semantic_ButtonList(this HtmlHelper helper, string id, T_COMMON_COND Cond)
        {
            var list = new CommonService().GetCommon(Cond);

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("\n").Append(" <div class='ui buttons ").Append(Cond.color.ToString("")).Append(" list' style='width:100%; cursor:pointer'>                      ");
            
            foreach (T_COMMON data in list)
            {
                sbHtml.Append("\n").Append("     <button class='ui button ").Append(id);
                if (data.SUB_CODE.ToString("") == Cond.selectedValue.ToString(""))
                {
                    sbHtml.Append(" active");
                    Cond.selectedValue = data.SUB_CODE.ToString("");
                }
                sbHtml.Append("' type='button' value='").Append(data.SUB_CODE).Append("'>").Append(data.NAME).Append("</button>      ");
                
                
            }
            sbHtml.Append("<input type='hidden' id='").Append(id).Append("' value='").Append(Cond.selectedValue.ToString("")).Append("' />");
            sbHtml.Append("\n").Append(" </div> ");

            sbHtml.Append("\n").Append("    <script type=\"text/javascript\">    ");
            sbHtml.Append("\n").Append(" $(\"document\").ready(function(){ ");

            sbHtml.Append("\n").Append("        $(\".").Append(id).Append("\").click(function(){  ");

            sbHtml.Append("\n").Append("            $(\".").Append(id).Append("\").removeClass(\"active\"); ");
            sbHtml.Append("\n").Append("            $(this).addClass(\"active\"); ");
            sbHtml.Append("\n").Append("            $(\"#").Append(id).Append("\").val( $(\".").Append(id).Append(".active\").attr(\"value\")); ");
            sbHtml.Append("\n").Append("            try{ ").Append(id).Append("_Callback( $(\".").Append(id).Append(".active\").attr(\"value\")); }catch(e){}");
            sbHtml.Append("\n").Append("        }); ");
            sbHtml.Append("\n").Append("   }); ");
            sbHtml.Append("\n").Append(" </script> ");


            return MvcHtmlString.Create(sbHtml.ToString().Trim());
        }
        public static MvcHtmlString Semantic_ButtonList2(this HtmlHelper helper, string id, List<SelectListItem> list,  string color = "", bool bText = false)
        {
            //var list = new CommonService().GetCommon(Cond);

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("\n").Append(" <div class='ui buttons ").Append(color.ToString("")).Append(" list' style='width:100%; cursor:pointer'>                      ");
            string selectedValue = "";
            foreach (SelectListItem data in list)
            {
                sbHtml.Append("\n").Append("     <button class='ui button ").Append(id);
                if ( data.Selected)
                {
                    sbHtml.Append(" active");
                    selectedValue = data.Value.ToString("");
                }
                sbHtml.Append("' type='button' value='").Append(data.Value).Append("'>").Append(data.Text).Append("</button>      ");


            }
            if(bText) sbHtml.Append("&nbsp;<input type='text' id='").Append(id).Append("' style='width:50px;' value='").Append(selectedValue.ToString("")).Append("' /> m");
            else sbHtml.Append("&nbsp;<input type='hidden' id='").Append(id).Append("' style='width:50px;' value='").Append(selectedValue.ToString("")).Append("' />");
            sbHtml.Append("\n").Append(" </div> ");

            sbHtml.Append("\n").Append("    <script type=\"text/javascript\">    ");
            sbHtml.Append("\n").Append(" $(\"document\").ready(function(){ ");

            sbHtml.Append("\n").Append("        $(\".").Append(id).Append("\").click(function(){  ");

            sbHtml.Append("\n").Append("            $(\".").Append(id).Append("\").removeClass(\"active\"); ");
            sbHtml.Append("\n").Append("            $(this).addClass(\"active\"); ");
            sbHtml.Append("\n").Append("            $(\"#").Append(id).Append("\").val( $(\".").Append(id).Append(".active\").attr(\"value\")); ");
            sbHtml.Append("\n").Append("            try{ ").Append(id).Append("_Callback( $(\".").Append(id).Append(".active\").attr(\"value\")); }catch(e){}");
            sbHtml.Append("\n").Append("        }); ");
            sbHtml.Append("\n").Append("   }); ");
            sbHtml.Append("\n").Append(" </script> ");


            return MvcHtmlString.Create(sbHtml.ToString().Trim());
        }
    }
}