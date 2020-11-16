using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    public class COMBO_COND
    {
        public string name { get; set; }
        public IEnumerable<ComboSelectListItem> selectList { get; set; }
        public string optionLabel { get; set; }
        public object htmlAttributes { get; set; }
    }
}
