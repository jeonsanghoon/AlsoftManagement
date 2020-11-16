
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.loggal;
using ALT.VO.Common;

namespace loggalServiceBiz
{
    public class RequestADService : BaseService
    {
        public RequestADService() { }
        public RequestADService(DataContext _db) : base(_db) { }


        public List<STEP_LOCAL_LIST> GetStep4localAddList(STEP_LOCAL_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\RequestADStep.xml", "GetStep4localAddList"
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.PAGE.ToString("1")
                                         , Cond.AD_CODE.ToString("-1")
                                         , Cond.SEARCH_CODE.ToString("")
                                         , Cond.STORE_NAME.ToString("")

             );
            return db.ExecuteQuery<STEP_LOCAL_LIST>(sql).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<STEP_LOCAL_LIST> GetStep4localAddList2(STEP_LOCAL_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\RequestADStep.xml", "GetStep4localAddList2"
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.PAGE.ToString("1")
                                         , Cond.SEARCH_CODE.ToString("")


             );
            return db.ExecuteQuery<STEP_LOCAL_LIST>(sql).ToList();
        }
        public RTN_SAVE_DATA Step4Save(STEP4_SAVE Param)
        {
            object obj = Param;
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string deviceCodes = Param.DEVICE_CODE.FromArrayToString();
                    deviceCodes = string.IsNullOrEmpty(deviceCodes) ? "0" : deviceCodes;
                    string adDeviceCodes = Param.AD_DEVICE_CODE.FromArrayToString();
                    adDeviceCodes = string.IsNullOrEmpty(adDeviceCodes) ? "0" : adDeviceCodes;

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\RequestADStep.xml", "Step4Save"
                                             , Param.SAVE_TYPE.ToString("")
                                             , Param.AD_CODE.ToString("0")
                                             , deviceCodes
                                             , adDeviceCodes
                                             , Param.TIMEZONE_OFFSET.ToString("9")
                                             , Param.REG_CODE.ToString("0")

                                        );
                    db.ExecuteCommand(sql);
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }

            return rtnData;
        }

        public RTN_SAVE_DATA AdShareRequest(List<STEP4_SAVE> list)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (STEP4_SAVE Param in list)
                    {
                        string deviceCodes = Param.DEVICE_CODE.FromArrayToString();
                        deviceCodes = string.IsNullOrEmpty(deviceCodes) ? "0" : deviceCodes;
                        string adDeviceCodes = Param.AD_DEVICE_CODE.FromArrayToString();
                        adDeviceCodes = string.IsNullOrEmpty(adDeviceCodes) ? "0" : adDeviceCodes;

                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\RequestADStep.xml", "AdShareRequest"
                                                 , Param.SAVE_TYPE.ToString("")
                                                 , Param.AD_CODE.ToString("0")
                                                 , deviceCodes
                                                 , adDeviceCodes
                                                 , Param.TIMEZONE_OFFSET.ToString("9")
                                                 , Param.STATUS.ToString("1")
                                                 , Param.REG_CODE.ToString("0")
                                            );
                        db.ExecuteCommand(sql);
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


        public RTN_SAVE_DATA Step5Save(STEP5_SAVE Param)
        {
            object obj = Param;
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\RequestADStep.xml", "Step5Save"

                                             , Param.AD_CODE.ToString("0")
                                             , Param.REG_CODE.ToString("0")

                                        );
                    db.ExecuteCommand(sql);
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }

            return rtnData;
        }

        public List<STEPLIST> GetStepList(STEPLIST_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\RequestADStep.xml", "GetStepList"
                                             , Cond.MEMBER_CODE.ToString("0")
                                             , Cond.PAGE_COUNT.ToString("10")
                                             , Cond.PAGE.ToString("1")
                                             , Cond.TITLE.ToString("")
                                            );
            return db.ExecuteQuery<STEPLIST>(sql).ToList();
        }
    }
}