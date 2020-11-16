using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace OnlineServiceBiz
{
    public class OrderService : BaseService
    {
        public OrderService() { }
        public OrderService(System.Data.Linq.DataContext _db) :base(_db){}
        #region >> T_SALE 테이블 조회/저장
        /// <summary>
        /// 매출데이터 조회(T_SALE)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<T_SALE> GetSaleList(T_SLAE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE.xml", "GetSaleList"
                                           , Cond.SALE_CODE.ToString("")
                                           , Cond.STORE_CODE.ToString("")
               );
            return db.ExecuteQuery<T_SALE>(sql).ToList();
        }

        /// <summary>
        /// 매출데이터 저장(T_SALE)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public string SaleSave(T_SALE Cond)
        {
            string msg = string.Empty;

            try
            {

                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE.xml", "SaleSave"
                                               , Cond.SALE_CODE
                                               , Cond.SALE_DATE.ToString("")
                                               , Cond.BILL_NO.ToString("")
                                               , Cond.STORE_CODE
                                               , Cond.MEMBER_CODE.ToString("0")
                                               , Cond.TOT_TAX
                                               , Cond.TAX1
                                               , Cond.TAX2
                                               , Cond.TAX3
                                               , Cond.DELIVERY_FEE
                                               , Cond.TIP_AMT
                                               , Cond.ADD_AMT
                                               , Cond.ITEM_DISCOUNT_AMT
                                               , Cond.ORDER_DISCOUNT_TYPE
                                               , Cond.ORDER_DISCOUNT_AMT
                                               , Cond.SALE_AMT
                                               , Cond.REMARK.ToString("")
                                               , Cond.INSERT_CODE

                    );
                Cond.SALE_CODE = db.ExecuteQuery<Int64>(sql).First();

            }
            catch (Exception ex)
            {
                msg = "Error => " + ex.Message;
            }
            if (!msg.Contains("Error"))
            {
                msg = "SALE_CODE:" + Cond.SALE_CODE.ToString();
            }
            return msg;
        }
        #endregion

        #region >> T_SALE_ITEM 테이블 조회/저장
        /// <summary>
        /// 매출아이템데이터 조회(T_SALE_ITEM)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<T_SALE_ITEM> GetSaleItemList(T_SLAE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_ITEM.xml", "GetSaleItemList"
                                           , Cond.SALE_CODE.ToString("")
                                           , Cond.ITEM_SEQ.ToString("")
                                           , Cond.ITEM_CODE.ToString("")
               );
            return db.ExecuteQuery<T_SALE_ITEM>(sql).ToList();
        }

        /// <summary>
        /// 매출아이템데이터 저장(T_SALE_ITEM)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public string SaleItemSave(T_SALE_ITEM Cond)
        {
            string msg = string.Empty;
            try
            {

                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_ITEM.xml", "SaleItemSave"
                                               , Cond.SALE_CODE
                                               , Cond.ITEM_SEQ
                                               , Cond.ITEM_CODE
                                               , Cond.ITEM_NAME.ToString("")
                                               , Cond.ITEM_TYPE
                                               , Cond.COST
                                               , Cond.TOT_TAX
                                               , Cond.TAX1
                                               , Cond.TAX2
                                               , Cond.TAX3
                                               , Cond.CNT
                                               , Cond.PRICE
                                               , Cond.DISCOUNT_TYPE
                                               , Cond.DISCOUNT_AMT
                                               , Cond.TOPPING_CODE
                                               , Cond.REMARK.ToString("")
                                               , Cond.INSERT_CODE

                                );
                db.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion


        #region >> T_SALE_ITEM_ADD 테이블 조회/저장
        /// <summary>
        /// 품목추가정보(세트,토핑등) 조회(T_SALE_ITEM_ADD)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<T_SALE_ITEM_ADD> GetSaleItemAddList(T_SLAE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_ITEM_ADD.xml", "GetSaleItemAddList"
                                           , Cond.SALE_CODE.ToString("")
                                           , Cond.ITEM_SEQ.ToString("")
                                           , Cond.ITEM_CODE.ToString("")
               );
            return db.ExecuteQuery<T_SALE_ITEM_ADD>(sql).ToList();
        }

        /// <summary>
        /// 품목추가정보(세트,토핑등) 저장(T_SALE_ITEM_ADD)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public string SaleItemAddSave(T_SALE_ITEM_ADD Cond)
        {
            string msg = string.Empty;
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_ITEM_ADD.xml", "SaleItemAddSave"
                                                   , Cond.SALE_CODE
                                                   , Cond.ITEM_SEQ
                                                   , Cond.SEQ
                                                   , Cond.ITEM_TYPE
                                                   , Cond.ITEM_CODE
                                                   , Cond.PRICE
                                                   , Cond.REMARK.ToString("")
                                                   , Cond.INSERT_CODE

                        );
                db.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion


        #region >> T_SALE_DISCOUNT 테이블 조회/저장
        /// <summary>
        /// 매출할인테이블 조회(T_SALE_ITEM_DISCOUNT)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<T_SALE_DISCOUNT> GetSaleDiscountList(T_SLAE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_DISCOUNT.xml", "GetSaleDiscountList"
                                           , Cond.SALE_CODE.ToString("")
               );
            return db.ExecuteQuery<T_SALE_DISCOUNT>(sql).ToList();
        }

        /// <summary>
        /// 품목추가정보(세트,토핑등) 저장(T_SALE_ITEM_DISCOUNT)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public string SaleDiscountSave(T_SALE_DISCOUNT Cond)
        {
            string msg = string.Empty;
            try
            {

                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_DISCOUNT.xml", "SaleDiscountSave"
                                               , Cond.SALE_CODE
                                               , Cond.SEQ
                                               , Cond.ITEM_SEQ.ToString("")
                                               , Cond.DISCOUNT_TYPE.ToString("")
                                               , Cond.ITEM_DISCOUNT_TYPE.ToString("")
                                               , Cond.BASE_AMT
                                               , Cond.DISCOUNT_RATE
                                               , Cond.DISCOUNT_AMT
                                               , Cond.REMARK.ToString("")
                                               , Cond.INSERT_CODE

                    );
                db.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion


        #region >> T_SALE_TIP 테이블 조회/저장
        /// <summary>
        /// 매출할인테이블 조회(T_SALE_ITEM_DISCOUNT)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<T_SALE_TIP> GetSaleTipList(T_SLAE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_TIP.xml", "GetSaleTipSave"
                                           , Cond.SALE_CODE.ToString("")
               );
            return db.ExecuteQuery<T_SALE_TIP>(sql).ToList();
        }

        /// <summary>
        /// 품목추가정보(세트,토핑등) 저장(T_SALE_ITEM_DISCOUNT)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public string SaleTipSave(T_SALE_TIP Cond)
        {
            string msg = string.Empty;
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_TIP.xml", "SaleTipSave"
                                                  , Cond.SALE_CODE
                                                  , Cond.SEQ
                                                  , Cond.EMPLOYEE_MEMBER_CODE
                                                  , Cond.EMPLOYEE_NAME.ToString("")
                                                  , Cond.CUSTOMER_MEMBER_CODE
                                                  , Cond.CUSTOMER_MEMBER_NAME.ToString("")
                                                  , Cond.COST
                                                  , Cond.TIP_TAX1
                                                  , Cond.TIP_TAX2
                                                  , Cond.TIP_TAX3
                                                  , Cond.PRICE
                                                  , Cond.REMARK.ToString("")
                                                  , Cond.INSERT_CODE

                       );
                db.ExecuteCommand(sql);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion


        #region >> T_SALE_COUPON 테이블 조회/저장
        /// <summary>
        /// 매출할인테이블 조회(T_SALE_ITEM_DISCOUNT)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<T_SALE_COUPON> GetCouponList(T_SALE_COUPON_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_COUPON.xml", "GetCouponList"
                                           , Cond.COUPON_CODE.ToString("")
                                           , Cond.COUPON_NO.ToString("")
                                           , Cond.COUPON_TYPE.ToString("")
                                           , Cond.AVAILABLE_DATE.ToString("")
                                           , Cond.STORE_CODE.ToString("")
                                           , Cond.FR_USE_DATE.ToString("")
                                           , Cond.TO_USE_DATE.ToString("")
               );
            return db.ExecuteQuery<T_SALE_COUPON>(sql).ToList();
        }

        /// <summary>
        /// 품목추가정보(세트,토핑등) 저장(T_SALE_ITEM_DISCOUNT)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public string SaleCouponSave(T_SALE_COUPON Cond)
        {
            string msg = string.Empty;
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Sale\\T_SALE_TIP.xml", "SaleTipSave"
                                                    , Cond.COUPON_CODE
                                                    , Cond.COUPON_NO
                                                    , Cond.USE_DATE
                                                    , Cond.FR_DATE
                                                    , Cond.TO_DATE
                                                    , Cond.MEMBER_CODE
                                                    , Cond.STORE_CODE
                                                    , Cond.ITEM_CODE
                                                    , Cond.DISCOUNT_RATE
                                                    , Cond.DISCOUNT_AMT
                                                    , Cond.MIN_PAY_AMT
                                                    , Cond.USE_DISCOUNT_AMT
                                                    , Cond.USE_YN
                                                    , Cond.SALE_CODE
                                                    , Cond.SALE_ITEM_SEQ
                                                    , Cond.REMARK
                                                    , Cond.INSERT_CODE
                                                    , Cond.INSERT_DATE
                                                    , Cond.UPDATE_CODE
                                                    , Cond.UPDATE_DATE

                        );
                db.ExecuteCommand(sql);

            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
        #endregion


        public string StoreOnlineCartReservationSave(T_STORE_RESERVATION Param, LOGIN_INFO login = null)
        {
            string msg = string.Empty;
            Int64 nSALE_CODE = 0;
            OrderService service = new OrderService(db);
            try
            {
             
                using (TransactionScope tran = new TransactionScope())
                {
                    if (login != null && login.SHOPPING_CART != null && (login.SHOPPING_CART.ITEM_LIST != null && login.SHOPPING_CART.ITEM_LIST.Count() > 0))
                    {
                        Param.STORE_CODE = login.STORE_CODE;
                        SHOPPING_CART cart = login.SHOPPING_CART;
                        string sSaleData = service.SaleSave(new T_SALE
                        {
                            SALE_CODE = 0,
                            BILL_NO = "",
                            STORE_CODE = (int)Param.STORE_CODE,
                            MEMBER_CODE = login.MEMBER.MEMBER_CODE,
                            TOT_TAX = cart.TOT_TAX,
                            TAX1 = cart.TAX1,
                            TAX2 = cart.TAX2,
                            TAX3 = cart.TAX3,
                            DELIVERY_FEE = login.SHOPPING_CART.DELIVERY_FEE,
                            TIP_AMT = (login.SHOPPING_CART.TIP_LIST == null) ? 0 : login.SHOPPING_CART.TIP_LIST.Sum(s => s.TOT_TIP_TAX),
                            ADD_AMT = login.SHOPPING_CART.ITEM_LIST.Sum(s => s.ADD_ITEM_LIST == null ? 0 : s.ADD_ITEM_LIST.Sum(ss => ss.PRICE)),
                            ITEM_DISCOUNT_AMT = (login.SHOPPING_CART.DISCOUNT_LIST ==null ? 0 : login.SHOPPING_CART.DISCOUNT_LIST.Where(w => w.ITEM_SEQ != null && w.ITEM_SEQ > 0).Sum(s => s.DISCOUNT_AMT)),
                            ORDER_DISCOUNT_TYPE = cart.ORDER_DISCOUNT_TYPE,
                            ORDER_DISCOUNT_AMT = cart.ORDER_DISCOUNT_AMT,
                            SALE_AMT = cart.TOTAL_AMT,
                            REMARK = "",
                            INSERT_CODE = (int)login.MEMBER.MEMBER_CODE
                        });

                        if (sSaleData.Contains("Error =>")) { return sSaleData; }
                        else
                        {
                            if(sSaleData.Split(':').Count() == 2)
                                nSALE_CODE = Convert.ToInt64(sSaleData.Split(':')[1]);
                        }

                        #region >> 매출 아이템 정보 등록
                        if (cart.ITEM_LIST != null)
                        {
                            foreach (SHOPPING_ITEM itemData in cart.ITEM_LIST)
                            {
                                T_SALE_ITEM itemSaveData = new T_SALE_ITEM { };

                                itemSaveData.SALE_CODE = nSALE_CODE;
                                itemSaveData.ITEM_SEQ = itemData.ITEM_SEQ;
                                itemSaveData.ITEM_CODE = itemData.ITEM_CODE;
                                itemSaveData.ITEM_NAME = itemData.ITEM_NAME;
                                itemSaveData.ITEM_TYPE = itemData.ITEM_TYPE;
                                itemSaveData.COST = itemData.COST;
                                itemSaveData.TOT_TAX = itemData.TOT_TAX;
                                itemSaveData.TAX1 = itemData.TAX1;
                                itemSaveData.TAX2 = itemData.TAX2;
                                itemSaveData.TAX3 = itemData.TAX3;
                                itemSaveData.CNT = itemData.CNT;
                                itemSaveData.PRICE = itemData.PRICE;
                                itemSaveData.DISCOUNT_TYPE = itemData.DISCOUNT_TYPE;
                                itemSaveData.DISCOUNT_AMT = itemData.DISCOUNT_AMT;
                                itemSaveData.TOPPING_CODE = itemData.TOPPING_CODE;
                                itemSaveData.REMARK = itemData.MEMO;
                                itemSaveData.INSERT_CODE = (int)login.MEMBER.MEMBER_CODE;
                                #region >> 세트메뉴정보 및 토핑 정보 등록
                                if (itemData.ADD_ITEM_LIST != null && itemData.ADD_ITEM_LIST.Count() > 0)
                                {
                                    foreach (T_SALE_ITEM_ADD addData in itemData.ADD_ITEM_LIST)
                                    {
                                        addData.SALE_CODE = nSALE_CODE;
                                        addData.ITEM_SEQ = itemData.ITEM_SEQ;
                                        addData.INSERT_CODE = (int)login.MEMBER.MEMBER_CODE;
                                        msg = service.SaleItemAddSave(addData);
                                        if (!string.IsNullOrEmpty(msg)) return msg;
                                    }
                                }
                                #endregion

                                msg = service.SaleItemSave(itemSaveData);
                                if (!string.IsNullOrEmpty(msg)) return msg;
                            }
                        }
                        #endregion

                        #region >>  쿠폰 사용정보 등록
                        if (login.SHOPPING_CART.COUPON_LIST != null && login.SHOPPING_CART.COUPON_LIST.Count() > 0)
                        {
                            //SALE_CODE
                            foreach (CART_COUPON_USE couponData in login.SHOPPING_CART.COUPON_LIST)
                            {
                                T_SALE_COUPON coupon = service.GetCouponList(new T_SALE_COUPON_COND { COUPON_CODE = couponData.COUPON_CODE }).FirstOrDefault();
                                if (coupon == null)
                                {
                                    return "해당쿠폰은 유효하지 않은 쿠폰입니다.";
                                }

                                if (Convert.ToInt32(couponData.USE_DATE) < Convert.ToInt32(coupon.FR_DATE))
                                {
                                    return "해당쿠폰은 유효하지 않은 쿠폰입니다. 사용가능일 : " + coupon.FR_DATE.ToFormatDate() + "~" + coupon.TO_DATE.ToFormatDate();
                                }

                                if (coupon.USE_YN)
                                {
                                    return "이미 사용된 쿠폰입니다.(사용가능매장 : " + coupon.STORE_NAME + " 사용일:" + coupon.USE_DATE + ")";
                                }

                                if (coupon.COMPANY_CODE != null && coupon.COMPANY_CODE != couponData.COMPANY_CODE)
                                {
                                    return "해당업체에서는 사용이 불가능합니다..(사용가능업체 : " + coupon.COMPANY_NAME + " 사용가능일:" + Global.Format.ConvertFromToFormatDate(coupon.FR_DATE, coupon.TO_DATE) + ")";
                                }

                                if (coupon.STORE_CODE != null && coupon.STORE_CODE != couponData.STORE_CODE)
                                {
                                    return "해당업체에서는 사용이 불가능합니다..(사용가능매장 : " + coupon.STORE_CODE + " 사용가능일:" + Global.Format.ConvertFromToFormatDate(coupon.FR_DATE, coupon.TO_DATE) + ")";
                                }

                                if (coupon.ITEM_GROUP_CODE != null && coupon.ITEM_GROUP_CODE != couponData.ITEM_GROUP_CODE)
                                {
                                    return "해당품목그룹은 사용이 불가능합니다..(사용가능품목그룹 : " + coupon.ITEM_GROUP_NAME + " 사용가능일:" + Global.Format.ConvertFromToFormatDate(coupon.FR_DATE, coupon.TO_DATE) + ")";
                                }
                                if (coupon.ITEM_CODE != null && coupon.ITEM_CODE != couponData.ITEM_CODE)
                                {
                                    return "해당품목은 사용이 불가능합니다..(사용가능품목 : " + coupon.ITEM_NAME + " 사용가능일:" + Global.Format.ConvertFromToFormatDate(coupon.FR_DATE, coupon.TO_DATE) + ")";
                                }

                                couponData.SALE_CODE = nSALE_CODE;
                                coupon.SALE_CODE = couponData.SALE_CODE;
                                coupon.SALE_ITEM_SEQ = couponData.SALE_ITEM_SEQ;
                                coupon.ITEM_CODE = couponData.ITEM_CODE;
                                coupon.USE_DATE = couponData.USE_DATE;
                                coupon.USE_YN = couponData.USE_YN;
                                coupon.DISCOUNT_RATE = coupon.DISCOUNT_RATE;
                                coupon.USE_DISCOUNT_AMT = coupon.DISCOUNT_AMT;
                                coupon.INSERT_CODE = login.MEMBER.MEMBER_CODE;

                                msg = service.SaleCouponSave(coupon);

                                if (string.IsNullOrEmpty(msg)) return msg;
                            }
                        }
                        #endregion

                        #region >> 할인정보 등록
                        if (cart.DISCOUNT_LIST != null && cart.DISCOUNT_LIST.Count() > 0)
                        {
                            foreach (T_SALE_DISCOUNT disData in cart.DISCOUNT_LIST)
                            {
                                disData.SALE_CODE = nSALE_CODE;
                                disData.INSERT_CODE = (int)login.MEMBER.MEMBER_CODE;
                                msg = service.SaleDiscountSave(disData);
                                if (!string.IsNullOrEmpty(msg)) return msg;
                            }
                        }
                        #endregion

                        #region >> 팁 정보 등록
                        if (cart.TIP_LIST != null && cart.TIP_LIST.Count() > 0)
                        {
                            foreach (T_SALE_TIP disData in cart.TIP_LIST)
                            {
                                disData.SALE_CODE = nSALE_CODE;
                                disData.INSERT_CODE = (int)login.MEMBER.MEMBER_CODE;
                                msg = service.SaleTipSave(disData);
                                if (!string.IsNullOrEmpty(msg)) return msg;
                            }
                        }
                        #endregion
                    }

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE_RESERVATION.xml", "StoreReservationSave"
                                              , Param.IDX.ToString("0")
                                              , Param.STORE_CODE.ToString()
                                              , Param.REG_DATE.ToString(DateTime.Now.ToString("yyyyMMddHHmmdd"))
                                              , Param.REQUEST_DATE.ToString("")
                                              , Param.NAME.ToString("")
                                              , Param.EMAIL.ToString("")
                                              , Param.PHONE.ToString("")
                                              , Param.PEOPLE_NUMBER.ToString("")
                                              , Param.CONTENT.ToString("")
                                              , Param.REMARK.ToString("")
                                              , Param.STATUS.ToString("1")
                                              , nSALE_CODE.ToString()
                                              , Param.INSERT_CODE.ToString("0")
                                               );

                    db.ExecuteCommand(sql);
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return msg;
        }
    }
}
