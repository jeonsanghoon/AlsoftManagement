﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 매장문의사항(T_STORE_CONTACT)
    /// <summary>       
    /// 매장문의사항(T_STORE_CONTACT)
    /// </summary>	   
    public class T_STORE_CONTACT
    {
        /// <summary>       
        /// 순번(일련번호) 기본키
        /// </summary>	   
        public Int64 IDX { get; set; }
        /// <summary>       
        /// 매장코드
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 등록일시(yyyyMMddHHmmss)
        /// </summary>	   
        public string REG_DATE { get; set; }
        /// <summary>       
        /// 이름
        /// </summary>	   
        public string NAME { get; set; }
        /// <summary>       
        /// 이메일
        /// </summary>	   
        public string EMAIL { get; set; }
        /// <summary>       
        /// 전화번호
        /// </summary>	   
        public string PHONE { get; set; }
        /// <summary>       
        /// 제목
        /// </summary>	   
        public string TITLE { get; set; }
        /// <summary>       
        /// 내용
        /// </summary>	   
        public string CONTENT { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 상태
        /// </summary>	   
        public int? STATUS { get; set; }
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
    #endregion >> 매장문의사항(T_STORE_CONTACT) END 

}
