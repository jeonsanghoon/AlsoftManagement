using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Semantic
{
    public static class SemanticVO
    {
        /// <summary>
        /// Semintic-UI Size 정의
        /// </summary>
        public enum Size
        {
            mini, tity, small, medium, large, big, huge, massive
        }

        public enum enColor
        {
            red, orange, yellow, olive, green, teal, blue, violet, purple, pink, brown, grey, black
        }

        public enum enButtonType
        {
            Normal, Request, New, Save, Cancel, Del, Excel, ToList, Check, Print
        }
    }
}
