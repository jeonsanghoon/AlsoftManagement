using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    public class STEP3_SAVE
    {
        public long? AD_CODE { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public string FR_TIME { get; set; }
        public string TO_TIME { get; set; }
        public int? STATUS { get; set; }
        public int REG_CODE { get; set; }
        /// <summary>
        /// 대표카테고리코드
        /// </summary>
        public int REP_CATEGORY_CODE { get; set; }
        public List<KEYWORD_DATA> KEYWORDS { get; set; }
    }

    public class KEYWORD_DATA
    {
        public int CODE { get; set; }
        public string NAME { get; set; }
    }
}