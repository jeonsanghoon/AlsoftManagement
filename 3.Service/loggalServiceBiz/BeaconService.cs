
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

using System.Transactions;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.loggal;
using ALT.VO.Common;


namespace loggalServiceBiz
{
    public class BeaconService : BaseService
    {
        public BeaconService() { }
        public BeaconService(DataContext _db) : base(_db) { }
        /// <summary>                                                                                      
        /// 비콘 조회																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_BEACON> GetBeaconList(T_BEACON_COND Param)
        {
            List<T_BEACON> list = new List<T_BEACON>();
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Beacon\\T_BEACON.xml", "GetBeaconList"
                                                   , Param.PAGE.ToString("1")
                                                   , Param.PAGE_COUNT.ToString("10")
                                                   , Param.SORT.ToString("A.BEACON_CODE")
                                                   , Param.BEACON_CODE.ToString("")
                                                   , Param.BEACON_NAME.ToString("")
                                                   , Param.HIDE == null ? "" : (Param.HIDE == true ? "1" : "0")
                        );
            return db.ExecuteQuery<T_BEACON>(sql).ToList();
        }
        /// <summary>                                                                                      
        /// T_BEACON 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA BeaconSave(T_BEACON Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = string.Empty;
                    if (Param.SAVE_TYPE == "D")
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Beacon\\T_BEACON.xml", "BeaconDelete"
                                                       , Param.BEACON_CODE.ToString("-1")
                                                       );
                    }
                    else
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Beacon\\T_BEACON.xml", "BeaconSave"
                                                   , Param.BEACON_CODE.ToString("-1")
                                                   , Param.BEACON_NAME.ToString("")
                                                   , Param.DEVICE_NUMBER.ToString("")
                                                   , Param.LOGO_URL.ToString("")
                                                   , Param.ADDRESS1.ToString("")
                                                   , Param.ADDRESS2.ToString("")
                                                   , Param.ZIP_CODE.ToString("")
                                                   , Param.LATITUDE.ToString("")
                                                   , Param.LONGITUDE.ToString("")
                                                   , (Param.HIDE == false ? "0" : "1")
                                                   , Param.REMARK.ToString("")
                                                   , Param.INSERT_CODE.ToString()

                     );
                    }
                    rtn.DATA = db.ExecuteQuery<int>(sql).First().ToString();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }

        /// <summary>                                                                                      
        /// 배너 비콘 조회																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_AD_BEACON> GetAdBeaconList(T_AD_BEACON_COND Param)
        {
            //Param.DEVICE_NUBMER = Param.DEVICE_NUBMER != null ? Param.DEVICE_NUBMER : "'dsfd','sdf'";
            List <T_AD_BEACON> list = new List<T_AD_BEACON>();
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Beacon\\T_AD_BEACON.xml", "GetAdBeaconList"
                                                   , Param.PAGE.ToString("1")
                                                   , Param.PAGE_COUNT.ToString("10")
                                                   , Param.SORT.ToString("A.BEACON_CODE")
                                                   , Param.IDX.ToString("")
                                                   , Param.AD_CODE.ToString("")
                                                   , Param.DEVICE_NUBMER.ToString("")
                                                   ,(Param.DEVICE_NUMBERS  == null ? string.Empty :  Param.DEVICE_NUMBERS )
                                                   , Param.BEACON_CODE.ToString("")
                                                   , Param.BEACON_CODES.ToString("")
                                                   , Param.BEACON_NAME.ToString("")
                                                   , Param.TITLE.ToString("")
                                                   , Param.SEARCH_DATE.ToString("")
                                                   , Param.HIDE == null ? "" : (Param.HIDE == true ? "1" : "0")
                        );
            return db.ExecuteQuery<T_AD_BEACON>(sql).ToList();
        }
        /// <summary>                                                                                      
        /// T_AD_BEACON 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA AdBeaconSave(T_AD_BEACON Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = string.Empty;
                    if (Param.SAVE_TYPE == "D")
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Beacon\\T_AD_BEACON.xml", "AdBeaconDelete"
                                                       , Param.IDX.ToString("-1")
                                                       );
                    }
                    else
                    {
                        Global.DBAgent.LoadSQL(sqlBasePath + "Beacon\\T_AD_BEACON.xml", "AdBeaconSave"
                                                    , Param.IDX.ToString("-1")
                                                    , Param.AD_CODE.ToString("")
                                                    , Param.BEACON_CODE.ToString("")
                                                    , Param.FR_DATE.ToString("")
                                                    , Param.TO_DATE.ToString("")
                                                    , Param.FR_TIME.ToString("")
                                                    , Param.TO_TIME.ToString("")
                                                    , Param.REMARK.ToString("")
                                                    , (Param.HIDE == null  || Param.HIDE == false ? "0" : "1")
                                                    , Param.INSERT_CODE.ToString("")
																												  
	                      );
                    }
                    rtn.DATA = db.ExecuteQuery<int>(sql).First().ToString();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }


    }
}
