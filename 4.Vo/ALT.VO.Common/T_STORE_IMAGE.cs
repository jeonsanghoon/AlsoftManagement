using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 매장관련이미지(T_STORE_IMAGE)
    /// <summary>       
    /// 매장관련이미지(T_STORE_IMAGE)
    /// </summary>	   
    public class T_STORE_IMAGE
    {
        /// <summary>       
        /// 매장코드
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 서비스코드(T_COMMON 테이블의 MAIN_CODE : S004)
        /// </summary>	   
        public int? SERVICE_TYPE { get; set; }
        /// <summary>       
        /// 이미지유형(T_COMMON 테이블의 MAIN_CODE : S005)
        /// </summary>	   
        public int? IMAGE_TYPE { get; set; }
        /// <summary>       
        /// 순번
        /// </summary>	   
        public int? SEQ { get; set; }
        /// <summary>       
        /// 이미지URL
        /// </summary>	   
        public string IMAGE_URL { get; set; }
        /// <summary>       
        /// 데이터1
        /// </summary>	   
        public string DATA1 { get; set; }
        /// <summary>       
        /// 데이터2
        /// </summary>	   
        public string DATA2 { get; set; }
        /// <summary>       
        /// 데이터3
        /// </summary>	   
        public string DATA3 { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
    #endregion >> 매장관련이미지(T_STORE_IMAGE) END 

}
