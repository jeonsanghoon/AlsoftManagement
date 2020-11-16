using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.VO.Common
{
    /// <summary>
    /// 쿠폰 조회 조건
    /// </summary>
    public class T_SALE_COUPON_COND
    {
        public Int64? COUPON_CODE { get; set; }
        public string COUPON_NO { get; set; }
        public int? COUPON_TYPE { get; set; }
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
        /// 업체별 할인코드 구분, 값이 없으면 전체 적용대상
        /// </summary>
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 매출매장코드(T_STORE의 STORE_CODE)
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>
        /// T_ITEM_GROUP테이블의 GROUP_CODE이며 값이 있을 경우 해당 그룹만 할인 대상이 됨
        /// </summary>
        public Int64? ITEM_GROUP_CODE { get; set; }

        /// <summary>       
        /// 사용일조회조건(From)
        /// </summary>	   
        public string FR_USE_DATE { get; set; }
        /// <summary>       
        /// 사용일조회조건(To)
        /// </summary>	   
        public string TO_USE_DATE { get; set; }
        /// <summary>
        /// 사용가능일 조회조건
        /// </summary>
        public string AVAILABLE_DATE { get; set; }

    }

    #region >> 쿠폰테이블(T_SALE_COUPON)
    /// <summary>       
    /// 쿠폰테이블(T_SALE_COUPON)
    /// </summary>	   
    public class T_SALE_COUPON
    {
        /// <summary>       
        /// 쿠폰코드(자동순번)
        /// </summary>	   
        public Int64 COUPON_CODE { get; set; }
        /// <summary>       
        /// 쿠폰번호
        /// </summary>	   
        public string COUPON_NO { get; set; }
        /// <summary>       
        /// 쿠폰유형
        /// </summary>	   
        public int? COUPON_TYPE { get; set; }
        /// <summary>       
        /// 쿠폰사용일
        /// </summary>	   
        public string USE_DATE { get; set; }
        /// <summary>       
        /// 사용가능시작일
        /// </summary>	   
        public string FR_DATE { get; set; }
        /// <summary>       
        /// 사용가능종료일
        /// </summary>	   
        public string TO_DATE { get; set; }
        /// <summary>       
        /// 쿠폰사용회원, 값이 null 일경우 회원 상관없이 모두 할인
        /// </summary>	   
        public int? MEMBER_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용가능업체
        /// </summary>	   
        public int? COMPANY_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용가능업체명
        /// </summary>	   
        public string COMPANY_NAME { get; set; }
        /// <summary>       
        /// 쿠폰사용매장
        /// </summary>	   
        public int? STORE_CODE { get; set; }
        /// <summary>       
        /// 쿠폰사용매장
        /// </summary>	   
        public string STORE_NAME { get; set; }
        /// <summary>       
        /// 할인대상 아이템그룹코드 : 아이템그룹할인일경우 등록됨
        /// </summary>	  
        public Int64? ITEM_GROUP_CODE { get; set; }
        /// <summary>       
        /// 할인대상 아이템그룹명 : 아이템그룹할인일경우 등록됨
        /// </summary>	  
        public string ITEM_GROUP_NAME { get; set; }
        /// <summary>       
        /// 할인대상 아이템코드 : 아이템할인일경우 등록됨
        /// </summary>	   
        public Int64? ITEM_CODE { get; set; }
        /// <summary>       
        /// 할인대상 아이템명 : 아이템할인일경우 등록됨
        /// </summary>	
        public string ITEM_NAME { get; set; }
        /// <summary>       
        /// 할인율
        /// </summary>	   
        public decimal? DISCOUNT_RATE { get; set; }
        /// <summary>       
        /// 할인가능금액
        /// </summary>	   
        public decimal? DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 사용가능최소결재금액
        /// </summary>	   
        public decimal? MIN_PAY_AMT { get; set; }
        /// <summary>       
        /// 최종할인금액
        /// </summary>	   
        public decimal? USE_DISCOUNT_AMT { get; set; }
        /// <summary>       
        /// 사용유무(1:사용 0:미사용)
        /// </summary>	   
        public bool USE_YN { get; set; }
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
        /// <summary>       
        /// 등록자
        /// </summary>	   
        public int? INSERT_CODE { get; set; }
        /// <summary>       
        /// 등록일
        /// </summary>	   
        public DateTime? INSERT_DATE { get; set; }
        /// <summary>       
        /// 쿠폰테이블
        /// </summary>	   
        public int? UPDATE_CODE { get; set; }
        /// <summary>       
        /// 쿠폰테이블
        /// </summary>	   
        public DateTime? UPDATE_DATE { get; set; }
    }
    #endregion >> 쿠폰테이블(T_SALE_COUPON) END 

}
