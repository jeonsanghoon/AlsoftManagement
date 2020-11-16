using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;

namespace ALT.Framework.Data
{
    public class CULTURE_INFO : System.Globalization.CultureInfo
    {
        
        public CULTURE_INFO(string name = "ko-KR") : base(name){}
        public CULTURE_INFO(int culture) : base(culture){ }

        public CULTURE_INFO(string name, bool useUserOverride):base(name,useUserOverride){ }
        public CULTURE_INFO(int culture, bool useUserOverride) : base(culture, useUserOverride) { }

        
        public string CurrencyString(decimal amount)
        {
            var numberFormat = NumberFormat;
          
            String pattern = null;
            switch (numberFormat.CurrencyPositivePattern)
            {
                case 1:
                    pattern = "{1:N" + numberFormat.CurrencyDecimalDigits + "}{0}";
                    break;
                case 2:
                    pattern = "{0} {1:N" + numberFormat.CurrencyDecimalDigits + "}";
                    break;
                case 3:                                                                                          
                    pattern = "{1:N" + numberFormat.CurrencyDecimalDigits + "} {0}";
                    break;
                default:
                    pattern = "{0}{1:N" + numberFormat.CurrencyDecimalDigits + "}";
                    break;
            }
            return String.Format(this, pattern, numberFormat.CurrencySymbol, amount);
            
        }

      


    }
}
