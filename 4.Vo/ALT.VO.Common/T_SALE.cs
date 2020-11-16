using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    #region>> 매출데이터 조회 조건
  
    /// <summary>
    /// 매출데이터 조회 조건
    /// </summary>
    public class T_SLAE_COND
    {
        /// <summary>       
        /// 매출번호(자동순번)
        /// </summary>	   
        public Int64? SALE_CODE { get; set; }
        /// <summary>       
        /// 매출번호별순번
        /// </summary>	   
        public int? ITEM_SEQ { get; set; }
        /// <summary>       
        /// 아이템코드(T_ITEM 테이블의 ITEM_CODE)
        /// </summary>	   
        public Int64? ITEM_CODE { get; set; }
        /// <summary>       
        /// 매출매장코드(T_STORE의 STORE_CODE)
        /// </summary>	   
        public int STORE_CODE { get; set; }
    }
    #endregion

    #region >> 매출테이블(T_SALE)
    /// <summary>       
    /// 매출테이블(T_SALE)
    /// </summary>	   
    public class T_SALE
    {
        /// <summary>       
        /// 매출번호(자동순번)
        /// </summary>	   
        public Int64 SALE_CODE { get; set; }
        /// <summary>       
        /// 매출일(yyyyMMddHHmmss)
        /// </summary>	   
        public string SALE_DATE { get; set; }
        /// <summary>       
        /// 계산서번호(기본:SALE_CODE와같음)
        /// </summary>	   
        public string BILL_NO { get; set; }
        /// <summary>       
        /// 매출매장코드(T_STORE의 STORE_CODE)
        /// </summary>	   
        public int STORE_CODE { get; set; }
        /// <summary>       
        /// 회원코드(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 총세금
        /// </summary>	   
        public decimal TOT_TAX { get; set; }
        /// <summary>       
        /// 세금1
        /// </summary>	   
        public decimal TAX1 { get; set; }
        /// <summary>       
        /// 세금2
        /// </summary>	   
        public decimal TAX2 { get; set; }
        /// <summary>       
        /// 세금3
        /// </summary>	   
        public decimal TAX3 { get; set; }
        /// <summary>       
        /// 배달비
        /// </summary>	   
        public decimal DELIVERY_FEE { get; set; }
        /// <summary>       
        /// 팁
        /// </summary>	   
        public decimal TIP_AMT { get; set; }
        /// <summary>       
        /// 기타추가금액
        /// </summary>	   
        public decimal ADD_AMT { get; set; }
        /// <summary>       
        /// 품목할인금액
        /// </summary>	   
        public decimal ITEM_DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 주문할인유형(T_COMMON:I002)
        /// </summary>	   
        public int ORDER_DISCOUNT_TYPE { get; set; }
        /// <summary>       
        /// 주문할인금액
        /// </summary>	   
        public decimal ORDER_DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 매출금액
        /// </summary>	   
        public decimal SALE_AMT { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일시
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 매출테이블(T_SALE) END 

    #region >> 매출아이템테이블(T_SALE_ITEM)
    /// <summary>       
    /// 매출아이템테이블(T_SALE_ITEM)
    /// </summary>	   
    public class T_SALE_ITEM
    {
        /// <summary>       
        /// 매출번호(T_SALE 테이블의 SALE_CODE)
        /// </summary>	   
        public Int64 SALE_CODE { get; set; }
        /// <summary>       
        /// 매출번호별순번
        /// </summary>	   
        public int ITEM_SEQ { get; set; }
        /// <summary>       
        /// 아이템코드(T_ITEM 테이블의 ITEM_CODE)
        /// </summary>	   
        public Int64 ITEM_CODE { get; set; }
        /// <summary>       
        /// 아이템명
        /// </summary>	   
        public string ITEM_NAME { get; set; }
        /// <summary>       
        /// 아이템유형T_ITEM 테이블의 ITEM_TYPE그대로저장
        /// </summary>	   
        public int ITEM_TYPE { get; set; }
        /// <summary>       
        /// 원가(T_ITEM COST * CNT)
        /// </summary>	   
        public decimal COST { get; set; }
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
        /// 판매갯수
        /// </summary>	   
        public int CNT { get; set; }
        /// <summary>       
        /// 가격(T_ITEM PRICE * CNT)
        /// </summary>	   
        public decimal PRICE { get; set; }
        /// <summary>       
        /// 할인유형
        /// </summary>	   
        public int DISCOUNT_TYPE { get; set; }
        /// <summary>       
        /// 할인가격
        /// </summary>	   
        public decimal DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 토핑템플릿코드(토핑테이브은아직만들어지지않음)
        /// </summary>	   
        public int? TOPPING_CODE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일시
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 매출아이템테이블(T_SALE_ITEM) END 

    #region >> 품목추가정보(세트,토핑등)(T_SALE_ITEM_ADD)
    /// <summary>       
    /// 품목추가정보(세트,토핑등)(T_SALE_ITEM_ADD)
    /// </summary>	   
    public class T_SALE_ITEM_ADD
    {
        /// <summary>       
        /// 매출번호(T_SALE 테이블의 SALE_CODE)
        /// </summary>	   
        public Int64 SALE_CODE { get; set; }
        /// <summary>       
        /// 품목추가정보(세트,토핑등)
        /// </summary>	   
        public int ITEM_SEQ { get; set; }
        /// <summary>       
        /// 매출번호별 순번
        /// </summary>	   
        public int SEQ { get; set; }
        /// <summary>       
        /// T_SALE_ITEM테이블의 ITEM_TYPE와 같음
        /// </summary>	   
        public int ITEM_TYPE { get; set; }
        /// <summary>       
        /// 대상품목 T_ITEM의 ITEM_CODE
        /// </summary>	   
        public Int64 ITEM_CODE { get; set; }
        /// <summary>       
        /// 원가(T_ITEM COST * CNT)
        /// </summary>	   
        public decimal COST { get; set; }
        /// <summary>       
        /// 판매갯수
        /// </summary>	   
        public decimal CNT { get; set; }
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
        /// 추가가격
        /// </summary>	   
        public decimal PRICE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일시
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 품목추가정보(세트,토핑등)(T_SALE_ITEM_ADD) END 

    #region >> 매출할인테이블(T_SALE_DISCOUNT)
    /// <summary>       
    /// 매출할인테이블(T_SALE_DISCOUNT)
    /// </summary>	   
    public class T_SALE_DISCOUNT
    {
        /// <summary>       
        /// 매출번호(T_SALE 테이블의 SALE_CODE)
        /// </summary>	   
        public Int64 SALE_CODE { get; set; }
        /// <summary>       
        /// 매출번호별 순번
        /// </summary>	   
        public int SEQ { get; set; }
        /// <summary>       
        /// T_SALE_ITEM 테이블의 ITEM_SEQ, 주문할인일경우에는 NULL
        /// </summary>	   
        public int? ITEM_SEQ { get; set; }
        /// <summary>       
        /// 매출할인유형(T_SALE테이블 DISCOUNT_AMT 할인정보, T_COMMON : C002)
        /// </summary>	   
        public int? DISCOUNT_TYPE { get; set; }
        /// <summary>       
        /// 매출아이템할인유형(T_SALE_ITEM 테이블 DISCOUNT_AMT 할인정보,T_T_COMMON : C002)
        /// </summary>	   
        public int? ITEM_DISCOUNT_TYPE { get; set; }
        /// <summary>       
        /// 할인대상금액
        /// </summary>	   
        public decimal BASE_AMT { get; set; }
        /// <summary>       
        /// 할인율
        /// </summary>	   
        public decimal DISCOUNT_RATE { get; set; }
        /// <summary>       
        /// 실제할인금액
        /// </summary>	   
        public decimal DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일시
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 매출할인테이블(T_SALE_DISCOUNT) END 

    #region >> 매출팁정보(T_SALE_TIP)
    /// <summary>       
    /// 매출팁정보(T_SALE_TIP)
    /// </summary>	   
    public class T_SALE_TIP
    {
        /// <summary>       
        /// 매출번호(T_SALE 테이블의 SALE_CODE)
        /// </summary>	   
        public Int64 SALE_CODE { get; set; }
        /// <summary>       
        /// 매출번호별 순번
        /// </summary>	   
        public int SEQ { get; set; }
        /// <summary>       
        /// 직원코드(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public Int64 EMPLOYEE_MEMBER_CODE { get; set; }
        /// <summary>       
        /// 직원이름
        /// </summary>	   
        public string EMPLOYEE_NAME { get; set; }
        /// <summary>       
        /// 고객코드(T_MEMBER의 MEMBER_CODE)
        /// </summary>	   
        public int CUSTOMER_MEMBER_CODE { get; set; }
        /// <summary>       
        /// 고객이름
        /// </summary>	   
        public string CUSTOMER_MEMBER_NAME { get; set; }
        /// <summary>       
        /// 고객이실제준 금액
        /// </summary>	   
        public decimal COST { get; set; }
        /// <summary>       
        /// 팁의 총세금
        /// </summary>	   
        public decimal TOT_TIP_TAX { get; set; }
        /// <summary>       
        /// 팁세금1
        /// </summary>	   
        public decimal TIP_TAX1 { get; set; }
        /// <summary>       
        /// 팁세금2
        /// </summary>	   
        public decimal TIP_TAX2 { get; set; }
        /// <summary>       
        /// 팁세금3
        /// </summary>	   
        public decimal TIP_TAX3 { get; set; }
        /// <summary>       
        /// 세금이포함된가격(ORI_PRICE + TOT_TIP_TAX)
        /// </summary>	   
        public decimal PRICE { get; set; }
        /// <summary>       
        /// 비고
        /// </summary>	   
        public string REMARK { get; set; }
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일시
        /// </summary>	   
        public DateTime INSERT_DATE { get; set; }
        /// <summary>       
        /// 수정자
        /// </summary>	   
        public int UPDATE_CODE { get; set; }
        /// <summary>       
        /// 수정일시
        /// </summary>	   
        public DateTime UPDATE_DATE { get; set; }
    }
    #endregion >> 매출팁정보(T_SALE_TIP) END 
}
