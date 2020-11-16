using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.loggal
{
    public class PAGE_COND_VO
    {
        public int? PAGE { get; set; }
        public int? PAGE_COUNT { get; set; }
        public string SORT_ORDER { get; set; }
    }

    public class BASE_VO
    {
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>
        /// 숨김여부
        /// </summary>
        public bool HIDE { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        public string INSERT_NAME { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>
        /// 수정자
        /// </summary>
        public string UPDATE_NAME { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
}
