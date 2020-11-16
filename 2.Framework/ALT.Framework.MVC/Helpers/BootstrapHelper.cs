using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web.Mvc.Html;
namespace ALT.Framework.Mvc.Helpers
{
    public class TouchSpan
    {
     
        public int? width { get; set; }
        public int? min { get; set; }
        public int? max { get; set; }
        public decimal? step { get; set; }
        public int? decimals { get; set; }
        public int? boostat { get; set; }
        public int? maxboostedstep { get; set;}
        public string postfix { get; set; }
    
    }
    public static class BootstrapHelper
    {
        /// <summary>
        /// Bootstrap TouchSpin
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="Param"></param>
        /// <returns>http://www.virtuosoft.eu/code/bootstrap-touchspin/</returns>
        public static MvcHtmlString TouchSpan(this HtmlHelper helper, string id, decimal? value = null, TouchSpan Param =null)
        {
         
            StringBuilder sbHtml = new StringBuilder();
            string strvalue = value == null ? "" : value.ToString();
            List<TouchSpan> list = new List<TouchSpan>();
            list.Add(Param);
            sbHtml.Append("<span>");

            sbHtml.Append("<input id = '").Append(id).Append("' type='text' value='").Append(value).Append("' name='").Append(id ).Append("'> ").Append("\n");
            sbHtml.Append("</span>");
            sbHtml.Append("<script>                                                     ").Append("\n");
            sbHtml.Append("$(document).ready(function(){ ").Append("\n");
            sbHtml.Append("            $('#").Append(id).Append("').TouchSpin({          ").Append("\n");


            int nCnt = 0;
            if (Param != null)
            {
                Type entityType = typeof(TouchSpan);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
                foreach (TouchSpan item in list)
                {
                    foreach (PropertyDescriptor prop in properties)
                    {
                        if (prop.GetValue(item) != null)
                        {
                            if(nCnt>0)
                            {
                                sbHtml.Append(",");
                            }
                            sbHtml.Append(prop.Name).Append(": '").Append(prop.GetValue(item)).Append("'");
                            nCnt++;
                        }
                    }
                }
            }

     
            sbHtml.Append("            });                                              ").Append("\n");
            sbHtml.Append("    $('#").Append(id).Append("').parent().css('width','").Append(Param.width).Append("px' );").Append("\n");
            sbHtml.Append("    $('#").Append(id).Append("').css('text-align','right' );").Append("\n");
            sbHtml.Append(" });").Append("\n");
            sbHtml.Append(" $('#").Append(id).Append("').on('change', function() { try{ touchSpinChangeEvent('").Append(id).Append("') }catch(e){} }); ").Append("\n");

           // sbHtml.Append(" $('#").Append(id).Append("').change(function(){try{ touchChangeEvent('").Append(id).Append("') }catch(e){}});").Append("\n");

           sbHtml.Append("</script>                                                    ").Append("\n");


            return new MvcHtmlString(sbHtml.ToString().Trim());
        }

        public static MvcHtmlString DateTimePicker(this HtmlHelper helper, string id, Boot_DateTimePicker Option = null)
        {
            StringBuilder sbHtml = new StringBuilder();
            if (Option == null) Option = new Boot_DateTimePicker();
            Option.language = Option.language == null ? "ko-KR" : Option.language;
            Option.format = Option.format == null ? "yyyy-MM-dd HH:mm:ss" : Option.format;
            Option.maskInput = Option.maskInput == null ? true : Option.maskInput;
            Option.pickDate = Option.pickDate == null ? true : Option.pickDate;
            Option.pickTime = Option.pickTime == null ? true : Option.pickTime;
            Option.pick12HourFormat = Option.pick12HourFormat == null ? true : Option.pick12HourFormat;
            Option.pickSeconds = Option.pickSeconds == null ? true : Option.pickSeconds;
            Option.startDate = Option.startDate == null ? "-Infinity" : Option.startDate;
            Option.endDate = Option.endDate == null ? "Infinity" : Option.endDate;

            sbHtml.Append("<div id =\"").Append(id).Append("\" class=\"input-append\">             ").Append("\n");
            sbHtml.Append("<input type=\"text\"></input>                ").Append("\n");
            sbHtml.Append("<span class=\"add-on\">                                                 ").Append("\n");
            sbHtml.Append("    <i data-time-icon=\"icon-time\" data-date-icon=\"icon-calendar\">   ").Append("\n");
            sbHtml.Append("    </i>                                                                ").Append("\n");
            sbHtml.Append("</span>                                                                 ").Append("\n");
            sbHtml.Append("</div>                                                                  ").Append("\n");
            sbHtml.Append("<script type='text/javascript'>                                         ").Append("\n");
   
            sbHtml.Append("$(document).ready(function(){ ").Append("\n");
            sbHtml.Append("            $('#").Append(id).Append("').datetimepicker({          ").Append("\n");

            List<Boot_DateTimePicker> list = new List<Boot_DateTimePicker>();
            list.Add(Option);
            int nCnt = 0;
            if (Option != null)
            {
                Type entityType = typeof(Boot_DateTimePicker);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);
                foreach (Boot_DateTimePicker item in list)
                {
                    foreach (PropertyDescriptor prop in properties)
                    {
                        if (prop.GetValue(item) != null)
                        {
                            if (nCnt > 0)
                            {
                                sbHtml.Append(",");
                            }
                            if(prop.PropertyType == typeof(string))
                            {
                                if (prop.GetValue(item).ToString().Contains("Infinity"))
                                { sbHtml.Append(prop.Name).Append(": ").Append(prop.GetValue(item)).Append(""); }
                                else
                                    sbHtml.Append(prop.Name).Append(": '").Append(prop.GetValue(item)).Append("'");
                            }else // if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(int) || prop.PropertyType == typeof(long) || prop.PropertyType == typeof(decimal))
                            {
                                sbHtml.Append(prop.Name).Append(": ").Append(prop.GetValue(item).ToString().ToLower()).Append("");
                            }
                                nCnt++;
                        }
                    }
                }
            }


            sbHtml.Append("            });       })                                       ").Append("\n");
          

            sbHtml.Append("</script>                                                    ").Append("\n");
            return new MvcHtmlString(sbHtml.ToString());
        }
    }

    public class Boot_DateTimePicker
    {
        public string language { get; set; }
        public string format { get; set; }
        /// <summary>
        /// disables the text input mask
        /// </summary>
        public bool? maskInput { get; set; }
        /// <summary>
        ///  // disables the date picker
        /// </summary>
        public bool? pickDate { get; set; }
        /// <summary>
        /// disables the time picker
        /// </summary>
        public bool? pickTime { get; set; }
        /// <summary>
        /// enables the 12-hour format time picker
        /// </summary>
        public bool? pick12HourFormat { get; set; }
        /// <summary>
        /// disables seconds in the time picker
        /// </summary>
        public bool? pickSeconds { get; set; }
        /// <summary>
        ///  set a minimum date
        /// </summary>
        public string startDate { get; set; }
        /// <summary>
        ///  set a maximum date
        /// </summary>
        public string endDate { get; set; }
    }
}



