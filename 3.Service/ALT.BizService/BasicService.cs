using ALT.Framework;
using ALT.Framework.Data;
using ALT.Framework.Mvc;
using ALT.VO.Common;
using ALT.VO.loggal;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ALT.BizService
{
    public class BasicService : BaseService
    {
        public BasicService() { }
        public BasicService(System.Data.Linq.DataContext _db) : base(_db) { }

        #region >> 회사정보
        public IList<T_COMPANY> GetCompanyList(T_COMPANY_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_COMPANY.xml", "GetCompanyList"
                                               , Cond.PAGE_COUNT.ToString("15")
                                               , Cond.PAGE.ToString("1")
                                               , Cond.SORT.ToString("A.COMPANY_CODE DESC")
                                               , Cond.COMPANY_CODE.ToString("")
                                               , Cond.COMPANY_ID.ToString("")
                                               , Cond.COMPANY_TYPE.ToString("")
                                               , Cond.COMPANY_NAME.ToString("")
                                               , Cond.STATUS.ToString("")
                                               , Cond.INSERT_NAME.ToString("")
                   );
            return db.ExecuteQuery<T_COMPANY>(sql).ToList();
        }

        public RTN_SAVE_DATA CompanySave(T_COMPANY Param)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
            try
            {
                
               
                using (TransactionScope tran = new TransactionScope())
                {
                    rtnData = this.CompanySave_Exec(Param);
                    if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE)) throw new Exception(rtnData.ERROR_MESSAGE);
                    tran.Complete();
                }
            }catch(Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }
            return rtnData;
        }

        public RTN_SAVE_DATA CompanySave_Exec(T_COMPANY Param, DataContext db1 = null)
        {
            if (db1 == null) db1 = db;
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
            if (!(string.IsNullOrEmpty(Param.PASSWORD) || Param.PASSWORD == "******"))
            {
                Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
            }

            if (Param.SAVE_TYPE != "D")
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_COMPANY.xml", "CompanySave"
                                                , Param.COMPANY_CODE.ToString("-1")
                                                , Param.COMPANY_ID.ToString("")
                                                , Param.PASSWORD.ToString("")
                                                , Param.COMPANY_NAME.ToString("")
                                                , Param.COMPANY_TYPE.ToString("")
                                                , Param.COMPANY_TYPE2.ToString("")
                                                , Param.PHONE.ToString("")
                                                , Param.MOBILE.ToString("")
                                                , Param.EMAIL.ToString("")
                                                , Param.ADDRESS1.ToString("")
                                                , Param.ADDRESS2.ToString("")
                                                , Param.ZIP_CODE.ToString("")
                                                , Param.LATITUDE.ToString("")
                                                , Param.LONGITUDE.ToString("")
                                                , Param.OWNER_NAME.ToString("")
                                                , Param.OWNER_PHONE.ToString("")
                                                , Param.OWNER_MOBILE.ToString("")
                                                , Param.OWNER_EMAIL.ToString("")
                                                , (Param.STORE_SYNC == null || Param.STORE_SYNC == false) ? "0" : "1"
                                                , Param.STATUS.ToString("9")
                                                , Param.CULTURE_NAME.ToString("ko-KR")
                                                , Param.THEME_NAME.ToString("SPICYX")
                                                , Param.REMARK.ToString("")
                                                , Param.INSERT_CODE.ToString("0")

                   );
                rtnData = db1.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
            }
            else
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_COMPANY.xml", "CompanyDelete"
                                               , Param.COMPANY_CODE.ToString("-1")
                                               , Param.INSERT_CODE
                  );
                rtnData = db1.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
            }

            return rtnData;
        }
        #endregion

        #region >> 사업장(매장) 정보
        public IList<T_STORE> GetStoreList(T_STORE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE.xml", "GetStoreList"
                                               , Cond.PAGE_COUNT.ToString("15")
                                               , Cond.PAGE.ToString("1")
                                               , Cond.SORT.ToString("B.COMPANY_NAME, A.STORE_NAME")
                                               , Cond.COMPANY_CODE.ToString("")
                                               , Cond.COMPANY_ID.ToString("")
                                               , Cond.COMPANY_NAME.ToString("")
                                               , Cond.STORE_CODE.ToString("")
                                               , Cond.STORE_ID.ToString("")
                                               , Cond.STORE_NAME.ToString("")
                                               , Cond.STATUS.ToString("")
                                               , Cond.INSERT_NAME.ToString("")

                                             );
            return db.ExecuteQuery<T_STORE>(sql).ToList();
        }

        public RTN_SAVE_DATA StoreSave(T_STORE Param)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
            try
            {
                if (!(string.IsNullOrEmpty(Param.PASSWORD) || Param.PASSWORD == "******"))
                {
                    Param.PASSWORD = GlobalMvc.Util.Encrypt_PW(Param.PASSWORD);
                }

                using (TransactionScope tran = new TransactionScope())
                {
                    if (Param.SAVE_TYPE != "D")
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE.xml", "StoreSave"
                                                        , Param.STORE_CODE.ToString("-1")
                                                        , Param.COMPANY_CODE.ToString("")
                                                        , Param.STORE_ID.ToString("")
                                                        , Param.PASSWORD.ToString("")
                                                        , Param.STORE_NAME.ToString("")
                                                        , Param.STORE_TYPE.ToString("")
                                                        , Param.PHONE.ToString("")
                                                        , Param.MOBILE.ToString("")
                                                        , Param.EMAIL.ToString("")
                                                        , Param.ADDRESS1.ToString("")
                                                        , Param.ADDRESS2.ToString("")
                                                        , Param.ZIP_CODE.ToString("")
                                                        , Param.LATITUDE.ToString("")
                                                        , Param.LONGITUDE.ToString("")
                                                        , Param.OWNER_NAME.ToString("")
                                                        , Param.OWNER_PHONE.ToString("")
                                                        , Param.OWNER_MOBILE.ToString("")
                                                        , Param.OWNER_EMAIL.ToString("")
                                                        , Param.STATUS.ToString("9")
                                                        , Param.CULTURE_NAME.ToString("")
                                                        , Param.THEME_NAME.ToString("")
                                                        , Param.TIME_ZONE.ToString("9")
                                                        , Param.BUSI_REG_NUMBER.ToString("")
                                                        , Param.REMARK.ToString("")
                                                        , Param.INSERT_CODE
                           );
                        rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                    }
                    else
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE.xml", "StoreDelete"
                                                       , Param.STORE_CODE.ToString("-1")
                                                       , Param.INSERT_CODE
                          );
                        rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                    }

                    if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                    {
                        throw new Exception(rtnData.ERROR_MESSAGE);
                    }

                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }
            return rtnData;
        }
        #endregion >> 사업장(매장) 정보

       


    }
}
