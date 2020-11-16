using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    public class STORE_STATISTICS_COND
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT { get; set; }
        public string FR_DATE { get; set; }
        public string TO_DATE { get; set; }
        public int? COMPANY_CODE { get; set; }
        public string COMPANY_NAME { get; set; }
        public int? STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public bool IS_LOCALBOX { get; set; }
        public int? LOCALBOX_REG_YN { get; set; }
        public int? BANNER_REG_YN { get; set; }
        public int? REG_YN { get; set; }
    }
    public class STORE_STATISTICS_LIST
    {
        
        public int STORE_CODE { get; set; }
        public string STORE_NAME { get; set; }
        public int? LOCALBOX_CNT { get; set; }
        public int? REAL_CNT { get; set; }
        public int? VIRTUAL_CNT { get; set; }
        public int? LOCALBOX_FRAME1_CNT { get; set; }
        public int? LOCALBOX_FRAME6_CNT { get; set; }
        public int? DEVICE_TYPE1 { get; set; }
        public int? DEVICE_TYPE2 { get; set; }
        public int? DEVICE_TYPE3 { get; set; }
        public int? AD_CNT { get; set; }
        public int? AD_TYPE1 { get; set; }
        public int? AD_TYPE2 { get; set; }
        public int? AD_TYPE3 { get; set; }
        public int? AD_FRAME_TYPE1_CNT { get; set; }
        public int? AD_FRAME_TYPE6_CNT { get; set; }
        public int? TOTAL_ROWCOUNT { get; set; }
    }
}
