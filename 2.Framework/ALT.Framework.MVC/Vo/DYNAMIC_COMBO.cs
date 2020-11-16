using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ALT.Framework.Mvc.Vo
{
    public class DROPDOWN_COND
    {
        public string name { get; set; }
        public List<SelectListItem> selectList { get; set; }
        public  string optionLabel { get; set; }
        public object htmlAttributes { get; set; }
    }
}
