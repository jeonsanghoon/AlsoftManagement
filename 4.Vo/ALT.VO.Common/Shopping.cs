using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    /// <summary>
    /// 장바구니 정보
    /// </summary>
    public class SHOPPING_CART
    {
        public decimal BEFORE_AMT { get; set; }
        public int ITEM_CNT { get; set; }
        public decimal TOTAL_AMT { get; set; }
        public int ORDER_DISCOUNT_TYPE { get; set; }
        public decimal ORDER_DISCOUNT_AMT { get; set; }
        public decimal ITEM_DISCOUNT_AMT { get; set; }
        public decimal DELIVERY_FEE { get; set; }
        public decimal ETC_ADD_AMT { get; set; }
        public decimal TOT_TAX { get; set; }
        public decimal TAX1 { get; set; }
        public decimal TAX2 { get; set; }
        public decimal TAX3 { get; set; }
        public string TAX1_NAME { get; set; }
        public string TAX2_NAME { get; set; }
        public string TAX3_NAME { get; set; }

        public List<SHOPPING_ITEM> ITEM_LIST { get; set; }
        public List<CART_COUPON_USE> COUPON_LIST { get; set; }
        public List<T_SALE_DISCOUNT> DISCOUNT_LIST { get; set; }
        public List<T_SALE_TIP> TIP_LIST { get; set; }

    }


    /// <summary>
    /// 주문시 쿠폰사용
    /// </summary>
    public class CART_COUPON_USE
    {
        /// <summary>       
        /// 쿠폰코드(자동순번)
        /// </summary>	   
        public Int64 COUPON_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용일
        /// </summary>	   
        public string USE_DATE { get; set; }
        /// <summary>       
        /// 쿠폰사용 유무
        /// </summary>	   
        public bool USE_YN { get; set; }
        /// <summary>       
        /// 쿠폰사용회원, 값이 null 일경우 회원 상관없이 모두 할인
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용업체
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 사용가능품목그룹, 값이 null 일경우 폼목그룹에 상관없이 모두 할인
        /// </summary>	
        public Int64? ITEM_GROUP_CODE { get; set; }
        /// <summary>       
        /// 사용가능품목그룹명, 값이 null 일경우 폼목그룹에 상관없이 모두 할인
        /// </summary>	
        public string ITEM_GROUP_NAME { get; set; }
        /// <summary>       
        /// 사용가능품목, 값이 null 일경우 폼목에 상관없이 모두 할인
        /// </summary>	
        public Int64? ITEM_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용매장
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// T_SALE테이블 참조
        /// </summary>	   
        public Int64? SALE_CODE { get; set; }
        /// <summary>       
        /// T_SALE_ITEM테이블 참조, 값이 없을 경우 주문할인 있을 경우 해당 아이템 할인
        /// </summary>	   
        public int? SALE_ITEM_SEQ { get; set; }
        /// <summary>
        /// 비고
        /// </summary>
        public string REMARK { get; set; }
    }


    /// <summary>
    /// 장바구니 아이템 정보
    /// </summary>
    public class SHOPPING_ITEM
    {
        public string ITEM_GROUP_NAME { get; set; }
        public string ITEM_IMAGEURL { get; set; }
        public int ITEM_SEQ { get; set; }
        public Int64 ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        /// <summary>       
        /// 아이템유형T_ITEM 테이블의 ITEM_TYPE그대로저장 T_COMMON : I002
        /// </summary>	   
        public int ITEM_TYPE { get; set; }
        public decimal COST { get; set; }
        public int CNT { get; set; }
        /// <summary>       
        /// 총세금(T_ITEM TOT_TAX * CNT)
        /// </summary>	   
        public decimal TOT_TAX { get; set; }
        /// <summary>       
        /// 세금1(T_ITEM TAX1 * CNT)
        /// </summary>	   
        public decimal TAX1 { get; set; }
        /// <summary>       
        /// 세금2(T_ITEM TAX2 * CNT)
        /// </summary>	   
        public decimal TAX2 { get; set; }
        /// <summary>       
        /// 세금3(T_ITEM TAX3 * CNT)
        /// </summary>	   
        public decimal TAX3 { get; set; }
        /// <summary>       
        /// 할인유형
        /// </summary>	   
        public int DISCOUNT_TYPE { get; set; }
        public decimal DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 토핑템플릿코드(토핑테이브은아직만들어지지않음)
        /// </summary>	   
        public int? TOPPING_CODE { get; set; }
        public decimal PRICE { get; set; }
        public decimal SALES_AMT { get; set; }

        public string MEMO { get; set; }

        public List<T_SALE_ITEM_ADD> ADD_ITEM_LIST { get; set; }

    }

}
