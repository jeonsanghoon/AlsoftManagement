using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using ALT.Framework.Data;
namespace ALT.Framework.Mvc.Helpers
{
    public static class SemanticUIHelper
    {
        
        /// <summary>
        /// 클래스에 사이즈가 없을 경우 mini 추가
        /// </summary>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static RouteValueDictionary SetDefaultSemanticSize(object htmlAttributes, string semaintic_size = "mini")
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

        /// <summary>
        /// Html Attribute string로 값 가져오기
        /// </summary>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string GetHtmlAttributeString(object htmlAttributes)
        {
            StringBuilder sbHtml = new StringBuilder();

            RouteValueDictionary customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            
            foreach (KeyValuePair<string, object> customAttribute in customAttributes)
            {
                sbHtml.Append(" ").Append(customAttribute.Key.ToString()).Append("='").Append(customAttribute.Value.ToString()).Append("'");
            }

            return sbHtml.ToString();
        }

        /// <summary>
        /// Html Attribute string로 값 가져오기
        /// </summary>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static string GetHtmlAttributeString(RouteValueDictionary htmlAttributes)
        {
            StringBuilder sbHtml = new StringBuilder();

            RouteValueDictionary customAttributes = htmlAttributes;

            foreach (KeyValuePair<string, object> customAttribute in customAttributes)
            {
                sbHtml.Append(" ").Append(customAttribute.Key.ToString()).Append("='").Append(customAttribute.Value.ToString()).Append("'");
            }

            return sbHtml.ToString();
        }

        /// <summary>
        /// Semintic-UI Size 정의
        /// </summary>
        public enum Size
        {
            mini, tity, small, medium, large, big, huge, massive
        }

        /// <summary>
        /// 제목
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE(this HtmlHelper helper, string TITLE)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<h1 class='ui header'>").Append(TITLE).Append("</h1> ");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }


        /// <summary>
        /// 제목(Title)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE2(this HtmlHelper helper, string TITLE, bool bRequired = false, int nSize=4)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("     <h").Append(nSize.ToString()).Append(" class='ui header'>  ").Append("\n");
            ///필수일 경우 * 표시
            if (bRequired) sbHtml.Append("         <span style = 'color:red;'> * </span > ");
            sbHtml.Append(TITLE).Append("\n");
            sbHtml.Append("     </h").Append(nSize.ToString()).Append(">  ").Append("\n");


            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        /// <summary>
        /// 제목
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE_INTABLE(this HtmlHelper helper, string TITLE, string iconNames = "chevron circle right icon")
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<div class='inline icon field titleintable'>");
            if (!string.IsNullOrEmpty(iconNames))
            {
                sbHtml.Append("<i class='chevron circle right icon'></i>");
            }
            sbHtml.Append(TITLE).Append("</div> ");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }


        /// <summary>
        /// 제목(Title) 내용확장 메서드, 다음 타이틀 margin-left: -100
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE2_Expand(this HtmlHelper helper, string TITLE, bool bRequired = false)
        {
            StringBuilder sbHtml = new StringBuilder();
            string randomId = DateTime.Now.ToString("ID_yyyyMMddHHmmss");
            sbHtml.Append("<div class='ui grid'>");
            sbHtml.Append("  <div class='four wide column' style='padding-top:0.5em;'><i style='font-size:1.5em;cursor:pointer;' name='btnExpend' class='fa fa-plus-circle expand ").Append(randomId).Append("' title='정보보기' aria-hidden='true'></i></div> ");
            sbHtml.Append("  <div class='twelve wide column' style='padding-right:1em;'>");
            /// 제목 만드는 함수 호출
            sbHtml.Append(Semantic_TITLE2(helper, TITLE, bRequired).ToString());
            sbHtml.Append("  </div>");
            sbHtml.Append("</div>");
            sbHtml.Append("\n").Append("<script type='text/javascript'>");
            sbHtml.Append("\n").Append("     $('document').ready(function() {                                                                 ");

            sbHtml.Append("\n").Append("         var idx = $('.fa.expand.").Append(randomId).Append("').parents('table').find('tr').index($('.fa.expand.").Append(randomId).Append("').parents('tr'))");
            //sbHtml.Append("\n").Append("         $('.fa.expand.").Append(randomId).Append("').parents('table').find('tr:nth-child( n + ' + String(idx  + 2) + ') td.title').css('background-color','#fdfdfd');");
            sbHtml.Append("\n").Append("         $('.fa.expand.").Append(randomId).Append("').parents('table').children('tr:eq(0) td.title').addClass('tdExpand')");
            sbHtml.Append("\n").Append("         $('.fa.expand.").Append(randomId).Append("').parents('table').children('tr:gt(' + idx +')').addClass('expandhide')");
			//sbHtml.Append("\n").Append("         $($('.fa.expand').parents('td').nextAll('.title').eq(0)).css('margin-left',-100);                            ");
			//sbHtml.Append("\n").Append("         $($('.fa.expand').parents('td').nextAll('.title').eq(0)).css('margin-top',5);                            ");
			sbHtml.Append("\n").Append("          $.each($('.fa.expand.").Append(randomId).Append("').parents('.ui.table').find('tr'), function (index, htmlEdit) {											  ");
			sbHtml.Append("\n").Append("              if (index > idx) {																			  ");
			sbHtml.Append("\n").Append("                 $(this).addClass('expandhide');	  ");
			sbHtml.Append("\n").Append("              }	                                                                                      ");
			sbHtml.Append("\n").Append("          })		");		

			sbHtml.Append("\n").Append("          $('.fa.expand.").Append(randomId).Append("').click(function() {							  ");
            sbHtml.Append("\n").Append("              if ($(this).hasClass('fa-minus-circle')) {											  ");
            sbHtml.Append("\n").Append("                  $(this).removeClass('fa-minus-circle');											  ");
            sbHtml.Append("\n").Append("                  $(this).addClass('fa-plus-circle');												  ");
			sbHtml.Append("\n").Append("                  $(this).parents('table').find('.expandshow').addClass('expandhide').removeClass('expandshow');	  ");
			sbHtml.Append("\n").Append("                  $(this).attr('title', '정보보기');                                                  ");

			sbHtml.Append("\n").Append("              }																						  ");
            sbHtml.Append("\n").Append("              else {																				  ");
            sbHtml.Append("\n").Append("                  $(this).removeClass('fa-plus-circle');											  ");
            sbHtml.Append("\n").Append("                  $(this).addClass('fa-minus-circle');												  ");
			sbHtml.Append("\n").Append("                  $(this).parents('table').find('.expandhide').addClass('expandshow').removeClass('expandhide');	  ");
			sbHtml.Append("\n").Append("                  $(this).attr('title', '정보보기');             ");
			sbHtml.Append("\n").Append("              }																						  ");
            sbHtml.Append("\n").Append("          																							  ");
            sbHtml.Append("\n").Append("             // var idx =  $('.fa.expand.').Append(randomId)').parents('table').find('tr').index($(this).parents('tr'));								  ");
            sbHtml.Append("\n").Append("          																									  ");
            sbHtml.Append("\n").Append("          })																									  ");

            
            sbHtml.Append("\n").Append("     })																										  ");
            sbHtml.Append("\n").Append("</script>");

            return new MvcHtmlString(sbHtml.ToString().Trim());
        }
        public static MvcHtmlString Semantic_TITLE_INLINE(this HtmlHelper helper,  string TITLE, bool bRequired = false)
        {
            return Semantic_TITLE_INLINE(helper, null, TITLE, bRequired);
        }
        /// <summary>
        /// 제목(Title)
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="TITLE"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TITLE_INLINE(this HtmlHelper helper, string id, string TITLE, bool bRequired = false)
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("     <lebel class='ui inlineheader'>").Append("\n");
            ///필수일 경우 * 표시
            if (bRequired) sbHtml.Append("         <span style = 'color:red;'> * </span > ");
            if (!string.IsNullOrEmpty(id)) sbHtml.Append("<span id='").Append(id).Append("'>");
            sbHtml.Append(TITLE);
            if (!string.IsNullOrEmpty(id)) sbHtml.Append("</span>");
            sbHtml.Append("     </lebel>  ").Append("\n");


            return new MvcHtmlString(sbHtml.ToString().Trim());
        }
        public static MvcHtmlString Discription(this HtmlHelper helper, string comment)
        {
            return new MvcHtmlString("<span class='discription'>" + comment + "</span>");
        }
        public static MvcHtmlString Semantic_TextBox(this HtmlHelper helper, string id, string value = "", object htmlAttributes = null, string iconName = null)
        {
            return Semantic_TextBox(helper, id, value,Size.mini, htmlAttributes, iconName);
        }
        public static MvcHtmlString Semantic_TextBox(this HtmlHelper helper, string id, string value, string width, object htmlAttributes = null, string iconName = null)
        {
            return Semantic_TextBox(helper, id, value, Size.mini, width, htmlAttributes, iconName, null);
        }
        public static MvcHtmlString Semantic_TextBox(this HtmlHelper helper, string id, string value, Size size, object htmlAttributes = null, string iconName = null, string firstIcon = null)
        {
            return Semantic_TextBox(helper, id, value, size, null, htmlAttributes, iconName, firstIcon);
        }
        /// <summary>
        /// Semintic UI TextBox 정의
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <param name="size"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        public static MvcHtmlString Semantic_TextBox(this HtmlHelper helper, string id, string value, Size size , string width , object htmlAttributes = null, string iconName = null, string firstIcon = null)
        {
            StringBuilder sbHtml = new StringBuilder();

            
            var sHtmlAttr = SemanticUIHelper.GetHtmlAttributeString(htmlAttributes);

            RouteValueDictionary arrHtmlAtribute = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
           
          
             if (htmlAttributes != null)
            {
                var classData = arrHtmlAtribute.Where(w => w.Key == "class").ToList();
                if (classData.Count() > 0)
                {
                    arrHtmlAtribute["class"] = arrHtmlAtribute["class"] + " ui input field ";
                    var array = Enum.GetNames(typeof(SemanticUIHelper.Size)).ToList();
                    if (array.Where(w => ((string)classData[0].Value).Contains(w)).Count() == 0)
                        arrHtmlAtribute["class"] = arrHtmlAtribute["class"] + size.ToString();
                }
             
            }

            arrHtmlAtribute["class"] =  string.IsNullOrEmpty((string)arrHtmlAtribute["class"]) ? "ui input field " + size.ToString() : arrHtmlAtribute["class"];
            arrHtmlAtribute["class"] = arrHtmlAtribute["class"] + (!string.IsNullOrEmpty(iconName) ? " icon" : "");
            arrHtmlAtribute["class"] = arrHtmlAtribute["class"] + (!string.IsNullOrEmpty(firstIcon) ? " left icon" : "");

            sbHtml.Append("<div ").Append(SemanticUIHelper.GetHtmlAttributeString(arrHtmlAtribute));
            if (width != null)
                sbHtml.Append(" style='width:").Append(width).Append("'");
            sbHtml.Append("> ");
          
            if (((string)arrHtmlAtribute.Where(w => w.Key == "class").First().Value).Contains("dataclear"))
            {
                /*  <!-- 크롬 비번 자동완성 방지를 위한 input -->
                                    <input type="password" style="display: block; width:0px; height:0px; border: 0;" @@focus="$refs.pwdInput.focus()">
                                    <input type="password" class="form-control m-b-5" placeholder="패스워드" v-model="user.password" autocomplete="off" ref="pwdInput" />*/

                string addType = arrHtmlAtribute.Where(w => w.Key == "type").Count() > 0 ? " type='" + (string)arrHtmlAtribute.Where(w => w.Key == "type").FirstOrDefault().Value + "' " : "";
                sbHtml.Append("<input  id ='").Append(id).Append("_fake' name = '").Append(id).Append("_fake'").Append(addType).Append(" autocomplete = 'new-password' class='fake-autofill-fields' style = 'display: none;' @focus='$refs.").Append(id).Append("_ref.focus()'>");
            }
            if (!string.IsNullOrEmpty(firstIcon))
            {
                sbHtml.Append("<i class='").Append(firstIcon).Append("'></i>");
            }
            sbHtml.Append("<input  id='").Append(id).Append("' name='").Append(id).Append("' value='").Append(value).Append("'").Append(SemanticUIHelper.GetHtmlAttributeString(htmlAttributes) ).Append(" ref='").Append(id).Append("_ref'");

            if (!SemanticUIHelper.GetHtmlAttributeString(htmlAttributes).ToUpper().Contains("TYPE"))
            {
                sbHtml.Append(" type='text' ");
            }

            sbHtml.Append("> ");
            if (!string.IsNullOrEmpty(iconName))
            {
                sbHtml.Append("<i class='").Append(iconName).Append("'></i>");
            }
            sbHtml.Append("</div>                      ");
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        /// <summary>
        /// Semantic-UI YesOrNo 버튼정의
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// 
        public static MvcHtmlString Semantic_YesOrNo(this HtmlHelper helper, string id, string selectedValue = "1", bool? bReadonly = false)
        {

            selectedValue = selectedValue.ToString("").ToLower();
            selectedValue = selectedValue == "false" ? "0" : "1";
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value ="1", Text="예", Selected=(selectedValue == "1") ? true : false});
            list.Add(new SelectListItem { Value = "0", Text = "아니오", Selected = (selectedValue == "0") ? true : false });
            return Semantic_YesOrNo(helper, id, list, Size.mini, bReadonly);
        }

        /// <summary>
        /// Semantic-UI YesOrNo 버튼정의
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// 
        public static MvcHtmlString Semantic_YesOrNo_Boolean(this HtmlHelper helper, string id, string selectedValue = "true", bool? bReadonly = false)
        {

            selectedValue = selectedValue.ToString("").ToLower();
            selectedValue = selectedValue == "false" ? "0" : "1";
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem { Value = "true", Text = "예", Selected = (selectedValue == "1") ? true : false });
            list.Add(new SelectListItem { Value = "false", Text = "아니오", Selected = (selectedValue == "0") ? true : false });
            return Semantic_YesOrNo(helper, id, list, Size.mini, bReadonly);
        }


        public static MvcHtmlString Semantic_YesOrNo(this HtmlHelper helper, string id, List<SelectListItem> list, Size size = Size.mini, bool? bReadonly = false)
        {
            StringBuilder sbHtml = new StringBuilder();
            bReadonly = bReadonly ?? false;
            if (list.Count() == 2)
            {
                string sSize = size == Size.mini ? "" : size.ToString();
                string selectValue = list.Where(w => w.Selected).FirstOrDefault() == null ? list[0].Value : list.Where(w => w.Selected).FirstOrDefault().Value;
                sbHtml.Append("<div id='dv_").Append(id).Append("' class='ui ").Append(sSize).Append("yesorno buttons'>").Append("\n");
                sbHtml.Append("    <button type='button' id='btn_").Append(id).Append("_").Append(list[0].Value).Append("' class='ui ").Append((list[0].Value == selectValue) ? "active" : "").Append(" button' onclick='dv_").Append(id).Append("_Click(\"").Append(list[0].Value).Append("\");'> ").Append(list[0].Text).Append("</button>").Append("\n");
                sbHtml.Append("    <div class='or'></div>                              ").Append("\n");
                sbHtml.Append("    <button type='button' id='btn_").Append(id).Append("_").Append(list[1].Value).Append("' class='ui ").Append((list[1].Value == selectValue) ? "active" : "").Append(" button' onclick='dv_").Append(id).Append("_Click(\"").Append(list[1].Value).Append("\");'>").Append(list[1].Text).Append("</button>").Append("\n");
                sbHtml.Append("    <input id='").Append(id).Append("' name='").Append(id).Append("' type='hidden' value='").Append(selectValue).Append("'>").Append("\n");
                sbHtml.Append("</div>                                                  ").Append("\n");
                sbHtml.Append("<script type='text/javascript'>  ").Append("\n");
                sbHtml.Append(" function dv_").Append(id).Append("_Click(val){  ").Append("\n");

                if (bReadonly == false)
                {
                    sbHtml.Append("   $('#").Append(id).Append("').val(val); ").Append("\n");
                    sbHtml.Append("   $('#dv_").Append(id).Append(" button').removeClass('active');  ").Append("\n");
                    sbHtml.Append("   $('#btn_").Append(id).Append("_' + val).addClass('active');  ").Append("\n");
                    sbHtml.Append("   try{ ").Append(id).Append("_Change(val); }catch(e){}").Append("\n");
                }
                sbHtml.Append(" }  ").Append("\n");
                sbHtml.Append("</script>                        ").Append("\n");
            }
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        /// <summary>
        /// Semantic-UI YesOrNo 버튼정의
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        /// 
      
        public static MvcHtmlString Semantic_YesOrNo(this HtmlHelper helper, string id, List<SelectListItem> list, Size size, string value)
        {
            StringBuilder sbHtml = new StringBuilder();
            
            if (list.Count() == 2)
            {
                string selectValue = list.Where(w => w.Selected).FirstOrDefault() == null ? list[0].Value : list.Where(w => w.Selected).FirstOrDefault().Value;
                if (value != null && list.Select(s => s.Value.ToLower()).Contains(value.ToLower()))
                {
                    selectValue = value;
                }
                string sSize = size == Size.mini ? "" : size.ToString();

                selectValue = (string.IsNullOrEmpty(selectValue) ? "" : selectValue).ToLower();
                list[0].Value = (string.IsNullOrEmpty(list[0].Value) ? "" : list[0].Value).ToLower();
                list[1].Value = (string.IsNullOrEmpty(list[1].Value) ? "" : list[1].Value).ToLower();
                sbHtml.Append("<div id='dv_").Append(id).Append("' class='ui ").Append(sSize).Append("yesorno buttons'>").Append("\n");
                sbHtml.Append("    <button type='button' id='btn_").Append(id).Append("_").Append(list[0].Value).Append("' class='ui ").Append((list[0].Value == selectValue) ? "active" : "").Append(" button' onclick='dv_").Append(id).Append("_Click(\"").Append(list[0].Value).Append("\");'> ").Append(list[0].Text).Append("</button>").Append("\n");
                sbHtml.Append("    <div class='or'></div>                              ").Append("\n");
                sbHtml.Append("    <button type='button' id='btn_").Append(id).Append("_").Append(list[1].Value).Append("' class='ui ").Append((list[1].Value == selectValue) ? "active" : "").Append(" button' onclick='dv_").Append(id).Append("_Click(\"").Append(list[1].Value).Append("\");'>").Append(list[1].Text).Append("</button>").Append("\n");
                sbHtml.Append("    <input id='").Append(id).Append("'name=").Append(id).Append(" type='hidden' value='").Append(selectValue).Append("'>").Append("\n");
                sbHtml.Append("</div>                                                  ").Append("\n");
                sbHtml.Append("<script type='text/javascript'>  ").Append("\n");
                sbHtml.Append(" function dv_").Append(id).Append("_Click(val, bEvt){  ").Append("\n");
                sbHtml.Append("   $('#").Append(id).Append("').val(val);  ").Append("\n");
                sbHtml.Append("   $('#dv_").Append(id).Append(" button').removeClass('active');  ").Append("\n");
                sbHtml.Append("   $('#btn_").Append(id).Append("_' + val).addClass('active');  ").Append("\n");
                sbHtml.Append("    try{ bEvt = (bEvt == undefined ? true : bEvt); if(bEvt){ ").Append(id).Append("_Callback(val); } } catch(e){}  ").Append("\n");
                sbHtml.Append(" }  ").Append("\n");
                sbHtml.Append("</script>                        ").Append("\n");
            }
            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        public static MvcHtmlString Semantic_CheckBox(this HtmlHelper helper, string id, string label="")
        {
            return Semantic_CheckBox(helper, id, label, "", false, Size.mini, "");
        }
        public static MvcHtmlString Semantic_CheckBox(this HtmlHelper helper, string id, string label, string val)
        {
         
           return Semantic_CheckBox(helper, id, label, val, false, Size.mini, "");

        }
        public static MvcHtmlString Semantic_CheckBox(this HtmlHelper helper, string id, string label, string val,  Size size = Size.mini, string addClass = "")
        {
            return Semantic_CheckBox(helper, id, label, val, false, Size.mini, "");
        }
        public static MvcHtmlString Semantic_CheckBox(this HtmlHelper helper, string id, string label, string val , bool bChecked , Size size = Size.mini, string addClass = "")
        {
            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append(" <div class='ui checkbox ").Append(addClass).Append("' id='dv_" + id + "'>                    ").Append("\n");
            sbHtml.Append("   <input type='checkbox' id='").Append(id).Append("' name ='").Append(id).Append("' value='").Append((val == null ? "1" : val)).Append("'").Append((bChecked) ? "checked" : "").Append(">   ").Append("\n");
            sbHtml.Append("   <label>").Append(label).Append("</label>     ").Append("\n");
            sbHtml.Append(" </div>                                       ").Append("\n");



            return new MvcHtmlString(sbHtml.ToString().Trim());

        }

        public static MvcHtmlString Semantic_CheckBoxList(this HtmlHelper helper, string id, List<SelectListItem> list, Size size = Size.mini, string addClass = "")
        {
            StringBuilder sbHtml = new StringBuilder();
            
            sbHtml.Append(" <div class='ui checkbox ").Append(addClass).Append("' id='dv_" + id + "'>                    ").Append("\n");

            foreach (SelectListItem data in list)
            {
                sbHtml.Append("   <input type='checkbox' id='").Append(id).Append("' name ='").Append(id).Append("' value='").Append(data.Value).Append("'").Append((data.Selected) ? "checked" : "").Append(">   ").Append("\n");
                sbHtml.Append("   <label>").Append(data.Text).Append("</label>     ").Append("\n");
            }
            sbHtml.Append(" </div>                                       ").Append("\n");



            return new MvcHtmlString(sbHtml.ToString().Trim());

        }


        #region >> 버튼정의
        public enum enColor
        {
            red, orange, yellow, olive, green, teal, blue, violet, purple, pink, brown, grey, black
        }

        public enum enButtonType
        {
            Normal, Request, New, Save, Cancel, Del, Excel, ToList, Check, Print,Copy
        }

        public static MvcHtmlString Semantic_Button(this HtmlHelper helper, string id, string text, enButtonType btnType = enButtonType.Normal, object htmlAttributes = null)
        {
            return Semantic_Button(helper, id, text, btnType, Size.mini, htmlAttributes);
        }
        public static MvcHtmlString Semantic_Button(this HtmlHelper helper, string id, string text, enButtonType btnType , Size size , object htmlAttributes = null)
        {
            StringBuilder sbHtml = new StringBuilder();
            
            sbHtml.Append(" <button ");

            var arrType = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).Where(w => w.Key.ToLower() == "type").ToList();
            var arrClass = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).Where(w => w.Key.ToLower() == "class").Select(s=>s.Value).ToList();;
            string addClass = string.Empty;
            if(arrClass.Count() > 0) addClass = string.Join(" ", arrClass);
            if (arrType.Count > 0) sbHtml.Append(arrType[0].Key).Append("='").Append(arrType[0].Value).Append("'");
            else sbHtml.Append("type='button'");
            sbHtml.Append(" class='ui ");
            switch (btnType)
            {
                case enButtonType.Del:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.red.ToString()).Append(" ");
                    break;
                case enButtonType.Cancel:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.orange.ToString()).Append(" ");
                    break;
                case enButtonType.New:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.teal.ToString()).Append(" ");
                    break;
                case enButtonType.Request:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.olive.ToString()).Append(" ");
                    break;
                    
                case enButtonType.Save:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.blue.ToString()).Append(" ");
                    break;
                case enButtonType.Excel:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.green.ToString()).Append(" ");
                    break;
                case enButtonType.ToList:
                    sbHtml.Append(" ").Append(SemanticUIHelper.enColor.grey.ToString()).Append(" ");
                    break;
                default:
                    break;
            }
            sbHtml.Append(size.ToString()).Append(" button ").Append( addClass).Append("' id='").Append(id).Append("' name='").Append(id).Append("'").Append(SemanticUIHelper.GetHtmlAttributeString(htmlAttributes)).Append("> ").Append("\n");
            sbHtml.Append(text).Append("\n");
            sbHtml.Append(" </button>").Append("\n");
            
            return MvcHtmlString.Create(sbHtml.ToString().Trim());
        }
        public static MvcHtmlString Semantic_ButtonList(this HtmlHelper helper, string id, List<SelectListItem> list, string color = "")
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("\n").Append(" <div class='ui buttons ").Append(color.ToString("")).Append(" list' style='width:100%; cursor:pointer'>                      ");
            string selectedValue = string.Empty;
            foreach (SelectListItem data in list)
            {
                sbHtml.Append("\n").Append("     <button class='ui button ").Append(id);
                if (data.Selected)
                {
                    sbHtml.Append(" active");
                    selectedValue = data.Value;
                }
                sbHtml.Append("' type='button' value='").Append(data.Value).Append("'>").Append(data.Text).Append("</button>      ");
                
             
            }
            sbHtml.Append("<input type='hidden' id='").Append(id).Append("' value='").Append(selectedValue).Append("' />");
            sbHtml.Append("\n").Append(" </div> ");

            sbHtml.Append("\n").Append("    <script type=\"text/javascript\">    ");
            sbHtml.Append("\n").Append(" $(\"document\").ready(function(){ ");

            sbHtml.Append("\n").Append("        $(\".").Append(id).Append("\").click(function(){ ");

            sbHtml.Append("\n").Append("            $(\".").Append(id).Append("\").removeClass(\"active\"); ");
            sbHtml.Append("\n").Append("            $(this).addClass(\"active\"); ");
            sbHtml.Append("\n").Append("            $(\"#").Append(id).Append("\").val( $(\".").Append(id).Append(".active\").attr(\"value\")); ");
            sbHtml.Append("\n").Append("            try{ ").Append(id).Append("_Callback( $(\".").Append(id).Append(".active\").attr(\"value\")); }catch(e){}");
            sbHtml.Append("\n").Append("        }); ");
            sbHtml.Append("\n").Append("   }); ");
            sbHtml.Append("\n").Append(" </script> ");

            
            return MvcHtmlString.Create(sbHtml.ToString().Trim());
        }


        #endregion
        

    }
}

