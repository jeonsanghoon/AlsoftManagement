using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using ALT.VO.loggal;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Transactions;

namespace loggalServiceBiz
{
    public class StoreService :BaseService
    {
        public StoreService() { }
        public StoreService(DataContext _db) : base(_db) { }

        public IList<T_COMPANY> GetCompanyList(T_COMPANY_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Store\\T_COMPANY.xml", "GetCompanyList"
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
        public List<T_STORE_GROUP> GetStoreGroup(T_STORE_GROUP Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Store\\T_STORE_GROUP.xml", "GetStoreGroup"
                                       , Cond.GROUP_CODE.ToString("")
                                       , Cond.STORE_CODE.ToString("")
                                       , Cond.GROUP_TYPE.ToString("")
            );
            return db.ExecuteQuery<T_STORE_GROUP>(sql).ToList();
        }
        public RTN_SAVE_DATA StoreGroupSave(List<T_STORE_GROUP> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            try
            {

                using (TransactionScope tran = new TransactionScope())
                {
                    StringBuilder sbSql = new StringBuilder();
                    string sql = string.Empty;
                    foreach (T_STORE_GROUP Param in list)
                    {
                        if (Param.MRC_EDIT_MODE != "D")
                        {
                            sql = Global.DBAgent.LoadCondSQL(sqlBasePath + "Store\\T_STORE_GROUP.xml", "StoreGroupSave"
                                                          , Param.GROUP_CODE.ToString()
                                                          , Param.STORE_CODE.ToString()
                                                          , Param.GROUP_TYPE.ToString()
                                                          , Param.GROUP_NAME.ToString("")
                                                          , Param.SEQ.ToString()
                                                          , Param.REMARK.ToString("")
                                                          , REG_CODE

                             );
                            db.ExecuteCommand(sql);
                        }
                        else
                        {
                            sql = Global.DBAgent.LoadSQL(sqlBasePath + "Store\\T_STORE_GROUP.xml", "StoreGroupDelete"
                                                           , Param.GROUP_CODE.ToString()
                                                           , REG_CODE
                              );
                            rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                            if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                                throw new Exception(rtnData.ERROR_MESSAGE);
                        }
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

        public List<STORE_STATISTICS_LIST> GetStoreStatisticsList(STORE_STATISTICS_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Store\\storeStatistics.xml", "GetStoreStatisticsList"
                                       , Cond.PAGE_COUNT.ToString("10")
                                       , Cond.PAGE.ToString("")
                                       , Cond.SORT.ToString("A.STORE_NAME")
                                       , Cond.FR_DATE.ToString("").RemoveDateString()
                                       , Cond.TO_DATE.ToString("").RemoveDateString()
                                       , Cond.COMPANY_CODE.ToString("")
                                       , Cond.COMPANY_NAME.ToString("")
                                       , Cond.STORE_CODE.ToString("")
                                       , Cond.STORE_NAME.ToString("")
                                       , Cond.LOCALBOX_REG_YN.ToString("")
                                       , Cond.BANNER_REG_YN.ToString("")
                                       , Cond.REG_YN.ToString("")
            );
            return db.ExecuteQuery<STORE_STATISTICS_LIST>(sql).ToList();
        }
    }
}
