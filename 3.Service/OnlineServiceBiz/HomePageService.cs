using ALT.Framework;
using ALT.Framework.Data;

using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineServiceBiz
{
    public class HomePageService : BaseService
    {

        public HomePageService() { }
        public HomePageService(DataContext _db) : base(_db){ }
        public List<T_MEMBER> GetMemberList(T_MEMBER Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\Account.xml", "GetMemberList"
                                           , Cond.USER_ID.ToString("")
                                           , Cond.USER_TYPE.ToString("")
                                           , Cond.PHONE.ToString("")
                                           , Cond.MOBILE.ToString("")
               );
            return db.ExecuteQuery<T_MEMBER>(sql).ToList();
        }
        public List<T_STORE_WEBMENU> GetStoreWebMenuList(T_STORE_WEBMENU Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE_WEBMENU.xml", "GetStoreWebMenuList"
                                               , Cond.STORE_CODE.ToString("")
                                               , Cond.NAME.ToString("")
                                               , (Cond.HIDE == null) ? "" : (Cond.HIDE == true) ? "1" : "0"
                   );
            return db.ExecuteQuery<T_STORE_WEBMENU>(sql).ToList();
        }

        public List<LOGIN_WEBMENU> GetLoginWebMenuList(LOGIN_WEBMENU Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE_WEBMENU.xml", "GetStoreWebMenuList"
                                               , Cond.PROJECT_SITE.ToString("")
                                               , Cond.STORE_CODE.ToString("")
                                               , Cond.NAME.ToString("")
                                               , (Cond.HIDE == null ? "0" : ((Cond.HIDE==true) ? "0" : "1"))
                        );
            return db.ExecuteQuery<LOGIN_WEBMENU>(sql).ToList();
        }

        public IList<T_COMPANY> GetCompanyList(T_COMPANY Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_COMPANY.xml", "GetCompanyList"
                                               , Cond.COMPANY_CODE.ToString("")
                                               , Cond.COMPANY_ID.ToString("")
                                               , Cond.COMPANY_NAME.ToString("")
                                               , Cond.STATUS.ToString("")
                   );
            return db.ExecuteQuery<T_COMPANY>(sql).ToList();
        }
        public IList<T_STORE> GetStoreList(T_STORE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE.xml", "GetStoreList"
                                               , Cond.COMPANY_CODE.ToString("")
                                               , Cond.COMPANY_ID.ToString("")
                                               , Cond.COMPANY_NAME.ToString("")
                                               , Cond.STORE_CODE.ToString("")
                                               , Cond.STORE_ID.ToString("")
                                               , Cond.STORE_NAME.ToString("")
                                               , Cond.STATUS.ToString("")
                                             );
            return db.ExecuteQuery<T_STORE>(sql).ToList();
        }

        public List<T_STORE_IMAGE> GetStoreImageList(T_STORE_IMAGE Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE_IMAGE.xml", "GetStoreImageList"
                                             , Cond.STORE_CODE.ToString("")
                                             , Cond.SERVICE_TYPE.ToString("")
                                             , Cond.IMAGE_TYPE.ToString("")
                                             , (Cond.HIDE == null) ? "" : (Cond.HIDE == true) ? "1" : "0"
                                           );
            return db.ExecuteQuery<T_STORE_IMAGE>(sql).ToList();
        }

        public List<T_ITEM_GROUP> GetItemGroupList(T_ITEM_GROUP_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_ITEM.xml", "GetItemGroupList"
                                           , Cond.STORE_CODE.ToString("")
                                           , Cond.GROUP_CODE.ToString("")
                                           , Cond.GROUP_TYPE.ToString("")
                                           , Cond.GROUP_NAME.ToString("")
                                           , Cond.LEVEL_DEPTH.ToString("")
                                           , (Cond.HIDE == null) ? "" : (Cond.HIDE == true) ? "1" : "0"
                                         );
            return db.ExecuteQuery<T_ITEM_GROUP>(sql).ToList();
        }

        public List<T_ITEM> GetItemList(T_ITEM_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_ITEM.xml", "GetItemList"
                                           , Cond.STORE_CODE.ToString("")
                                           , Cond.GROUP_CODE.ToString("")
                                           , Cond.GROUP_TYPE.ToString("")
                                           , Cond.ITEM_CODE.ToString("")
                                           , Cond.ITEM_NAME.ToString("")
                                           , (Cond.HIDE == null) ? "" : (Cond.HIDE == true) ? "1" : "0"
                                         );
            return db.ExecuteQuery<T_ITEM>(sql).ToList();
        }


        public List<T_STORE_BUSINESSHOURS_LIST> GetGetStoreBusinessHourList(T_STORE_BUSINESSHOURS_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE_BUSINESSHOURS.xml", "GetStoreBusinessHourList"
                                        , Cond.STORE_CODE.ToString("")
                                        , Cond.SEARCH_DATE.ToString("")
                                         );
            return db.ExecuteQuery<T_STORE_BUSINESSHOURS_LIST>(sql).ToList();
        }

        public string StoreReservationSave(T_STORE_RESERVATION Param, LOGIN_INFO login = null)
        {
            string msg = string.Empty;
         
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
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
                                              , ""
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
        public string StoreContactSave(T_STORE_CONTACT Param)
        {
            string msg = string.Empty;
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "HomePage\\T_STORE_CONTACT.xml", "StoreContactSave"
                                              , Param.IDX.ToString("0")
                                              , Param.STORE_CODE.ToString()
                                              , Param.REG_DATE.ToString(DateTime.Now.ToString("yyyyMMddHHmmdd"))

                                              , Param.NAME.ToString("")
                                              , Param.EMAIL.ToString("")
                                              , Param.PHONE.ToString("")
                                              , Param.TITLE.ToString("")
                                              , Param.CONTENT.ToString("")
                                              , Param.REMARK.ToString("")
                                              , Param.STATUS.ToString("1")
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
