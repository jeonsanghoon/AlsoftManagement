using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region >> 아이템그룹테이블(T_ITEM_GROUP)


    public class T_ITEM_GROUP_COND
    {
        /// <summary>       
        /// 매장코드 T_STORE테이블의 STORE_CODE
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 그룹코드(일련번호)
        /// </summary>	   
        public Int64? GROUP_CODE { get; set; }
        /// <summary>       
        /// 그룹유형(T_COMMON 테이블의 GROUP_CODE : I001)
        /// </summary>	   
        public int? GROUP_TYPE { get; set; }
        /// <summary>       
        /// 그룹명
        /// </summary>	   
        public string GROUP_NAME { get; set; }
        /// <summary>       
        /// 레벨
        /// </summary>	   
        public int? LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 숨김여부
        /// </summary>	   
        public bool? HIDE { get; set; }
    }
        /// <summary>       
        /// 아이템그룹테이블(T_ITEM_GROUP)
        /// </summary>	   
        public class T_ITEM_GROUP
    {
        /// <summary>       
        /// 그룹코드(일련번호)
        /// </summary>	   
        public Int64 GROUP_CODE { get; set; }
        /// <summary>       
        /// 그룹유형(T_COMMON 테이블의 GROUP_CODE : I001)
        /// </summary>	   
        public int? GROUP_TYPE { get; set; }
        /// <summary>       
        /// 상위그룹코드
        /// </summary>	   
        public Int64? PARENT_GROUP_CODE { get; set; }
        /// <summary>       
        /// 레벨
        /// </summary>	   
        public int? LEVEL_DEPTH { get; set; }
        /// <summary>       
        /// 매장코드 T_STORE테이블의 STORE_CODE
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 그룹명
        /// </summary>	   
        public string GROUP_NAME { get; set; }
        /// <summary>       
        /// 그룹명표시(온라인 사이트 등 다르게 표시 할때 사용)
        /// </summary>	   
        public string GROUP_NAME_DISPLAY { get; set; }
        /// <summary>       
        /// 정렬순번
        /// </summary>	   
        public int? ORDER_SEQ { get; set; }
        /// <summary>       
        /// 이미지경로
        /// </summary>	   
        public string IMAGE_URL { get; set; }
        /// <summary>       
        /// 그룹설명
        /// </summary>	   
        public string GROUP_DESC { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부
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
    #endregion >> 아이템그룹테이블(T_ITEM_GROUP) END 


    public class T_ITEM_COND
    {
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// 아이템그룹(T_ITEM_GROUP)
        /// </summary>
        public int? GROUP_CODE { get; set; }
        /// <summary>       
        /// 그룹유형(T_COMMON 테이블의 GROUP_CODE : I001)
        /// </summary>	   
        public int? GROUP_TYPE { get; set; }
        public string GROUP_NAME { get; set; }
        public int? ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public bool? HIDE { get; set; }
    }
    #region >> 품목(T_ITEM)
    /// <summary>       
    /// 품목(T_ITEM)
    /// </summary>	   
    public class T_ITEM
    {
        /// <summary>       
        /// 품목코드
        /// </summary>	   
        public Int64 ITEM_CODE { get; set; }
        /// <summary>       
        /// 그룹코드(T_ITEM_GROUP의 GROUP_CODE)
        /// </summary>	   
        public Int64? GROUP_CODE { get; set; }
        /// <summary>       
        /// 품목명
        /// </summary>	   
        public string ITEM_NAME { get; set; }
        /// <summary>       
        /// 품목명표시(온라인 사이트 등 다르게 표시 할때 사용)
        /// </summary>	   
        public string ITEM_NAME_DISPLAY { get; set; }
        /// <summary>       
        /// T_COMMON : I002, 품목유형 1:일반, 2:세트, 3:토핑
        /// </summary>	   
        public int? ITEM_TYPE { get; set; }
        /// <summary>       
        /// 단가
        /// </summary>	   
        public decimal? COST { get; set; }
        /// <summary>       
        /// 세금
        /// </summary>	   
        public decimal? TAX { get; set; }
        /// <summary>       
        /// 세금2
        /// </summary>	   
        public decimal? TAX2 { get; set; }
        /// <summary>       
        /// 세금3
        /// </summary>	   
        public decimal? TAX3 { get; set; }
        /// <summary>       
        /// 금액
        /// </summary>	   
        public decimal? PRICE { get; set; }
        /// <summary>       
        /// 정렬순번
        /// </summary>	   
        public int? ORDER_SEQ { get; set; }
        /// <summary>       
        /// 이미지경로
        /// </summary>	   
        public string IMAGE_URL { get; set; }
        /// <summary>       
        /// 품목설명
        /// </summary>	   
        public string ITEM_DESC { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 숨김여부(1:숨김)
        /// </summary>	   
        public bool? HIDE { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록시간
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정시간
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
    #endregion >> 품목(T_ITEM) END 

}
