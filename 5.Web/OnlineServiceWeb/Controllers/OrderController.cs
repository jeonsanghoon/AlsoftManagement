using System;
using System.Linq;
using System.Web.Mvc;
using ALT.Framework.Mvc.Contoller;
using ALT.Framework.Mvc.Helpers;
using System.Collections.Generic;
using ALT.Framework.Mvc;
using OnlineServiceBiz;
using ALT.VO.Common;
using OnlineServiceWeb.CommonCS;

namespace OnlineServiceWeb.Controllers
{
    public class OrderController : BaseController
    {


        public ActionResult Cart()
        {

            return ReturnView();
        }
        // GET: Order
        /// <summary>
        /// 장바구니 추가
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public JsonResult ShoppingCartAdd(SHOPPING_ITEM Param)
        {
            string msg = string.Empty;
            string cartListHtml = string.Empty;
            string totAmt = String.Format(SessionHelper.LoginInfo.CultureInfo, "{0:C}", 0);
            try
            {
                SHOPPING_CART cart = new SHOPPING_CART();
                if (SessionHelper.LoginInfo.SHOPPING_CART == null)
                {
                    cart = new SHOPPING_CART();
                }
                else
                    cart = SessionHelper.LoginInfo.SHOPPING_CART;

                if (Param != null)
                {
                    List<SHOPPING_ITEM> Itemlist = new List<SHOPPING_ITEM>();
                    if (cart.ITEM_LIST == null)
                    {
                        Itemlist = new List<SHOPPING_ITEM>();
                    }
                    else
                        Itemlist = cart.ITEM_LIST;
                    bool chkAdd = false;
                    foreach (var item in Itemlist.Where(w => w.ITEM_CODE == Param.ITEM_CODE && w.ITEM_GROUP_NAME == Param.ITEM_GROUP_NAME))
                    {
                        item.CNT = item.CNT + 1;
                        item.SALES_AMT = item.CNT * item.PRICE;
                        chkAdd = true;
                    }
                    if (!chkAdd)
                    {
                        Param.SALES_AMT = Param.PRICE; Itemlist.Add(Param);
                    }
                    cart.ITEM_LIST = Itemlist;
                    this.SetCartMasterData(ref cart);

                    totAmt = String.Format(SessionHelper.LoginInfo.CultureInfo, "{0:C}", cart.TOTAL_AMT);
                   

                    SessionHelper.LoginInfo.SHOPPING_CART = cart;
                   
                }
            }catch(Exception ex)
            {
               msg = ex.Message;
            }
            return new JsonResult { Data = new {  TOT_AMT = totAmt, error_message = msg } };
        }

        /// <summary>
        /// 카트리스트 데이터를 기준으로 Cart 업데이트
        /// </summary>
        /// <param name="cart"></param>
        public void SetCartMasterData(ref SHOPPING_CART cart)
        {
            var Itemlist = cart.ITEM_LIST;
            var query = Itemlist.GroupBy(g => true)
                        .Select(s => new
                        {
                            ITEM_DISCOUNT_AMT = s.Sum(s1 => s1.DISCOUNT_AMT)
                            ,
                            TOTAL_AMT = s.Sum(s1 => s1.SALES_AMT)
                            ,
                            BEFORE_AMT = s.Sum(s1 => s1.COST)
                            ,
                            ITEM_CNT = s.Sum(s1=>s1.CNT)
                        }).FirstOrDefault();



            if (query != null)
            {
                cart.ITEM_DISCOUNT_AMT = query.ITEM_DISCOUNT_AMT;
                cart.BEFORE_AMT = query.BEFORE_AMT;
                cart.TOTAL_AMT = query.TOTAL_AMT;
                cart.ITEM_CNT = query.ITEM_CNT;

            }
            cart.ITEM_LIST = Itemlist;
        }

        /// <summary>
        /// 카트 수량 변경 또는 삭제
        /// </summary>
        /// <returns></returns>
        public JsonResult CartUpdateOrDel(SHOPPING_ITEM Param, string saveType = "U")
        {
            string cartListHtml = string.Empty, msg = string.Empty, totAmt = string.Empty;
            try
            {
                SHOPPING_CART cart = SessionHelper.LoginInfo.SHOPPING_CART;
                List<SHOPPING_ITEM> itemlist = cart.ITEM_LIST;

                if (itemlist != null && saveType.ToUpper() == "D") {
                    itemlist.RemoveAll(x => x.ITEM_GROUP_NAME == Param.ITEM_GROUP_NAME && x.ITEM_CODE == Param.ITEM_CODE);
                }
                if (itemlist != null && saveType.ToUpper() == "U")
                {
                    foreach (var item in itemlist.Where(w => w.ITEM_CODE == Param.ITEM_CODE && w.ITEM_GROUP_NAME == Param.ITEM_GROUP_NAME))
                    {
                        item.CNT = Param.CNT;
                        item.SALES_AMT = item.CNT * item.PRICE;
                    }
                }
                this.SetCartMasterData(ref cart);
                totAmt = String.Format(SessionHelper.LoginInfo.CultureInfo, "{0:C}", cart.TOTAL_AMT);
                string viewName = "/Views/Theme/" + SessionHelper.LoginInfo.STORE.THEME_NAME + "/Order/PatialView/pv_CartList.cshtml";
                cartListHtml = GlobalMvc.Common.RenderPartialViewToString(this, viewName, new { });
            }
            catch (Exception ex) { msg = ex.Message; }

            return new JsonResult { Data = new { CartListHtml = cartListHtml, TOT_AMT = totAmt, error_message = msg } };
        }
        /// <summary>
        /// 온라인 메뉴 + 자리 예약
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public JsonResult CartReservation(T_STORE_RESERVATION Param)
        {
            string msg = string.Empty, redirect_Url = string.Empty;

          
            msg = new OrderService().StoreOnlineCartReservationSave(Param, SessionHelper.LoginInfo);

            /// 에러가 없을 경우 메일 발송
            if (string.IsNullOrEmpty(msg))
            {
                SessionHelper.LoginInfo.SHOPPING_CART = new SHOPPING_CART();
                new MailHelper().SendMail(new MAILINFO()
                {
                    ACCEPT_ID = Param.EMAIL + ";" + SessionHelper.LoginInfo.STORE.EMAIL
                  //  ,SENDER_ID="jsh0147@naver.com"
                  ,
                    SENDER_NAME = SessionHelper.LoginInfo.STORE.STORE_NAME
                  ,
                    SUBJECT = "[예약메일] - (" + Param.REQUEST_DATE + ")" + Param.NAME + "님이 예약을 요청하였습니다."
                  ,
                    CONTENT = Param.CONTENT

                });
            }
            return new JsonResult { Data = new { redirect_url = redirect_Url, error_message = msg } };
        }

        public ActionResult purchaselist()
        {
            return ReturnView();
        }


    }
}