using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using ALT.VO.loggal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace loggalServiceBiz
{
    public class HardwareService : BaseService
    {
        /// <summary>                                                                                      
        /// T_HARDWARE 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_HARDWARE> GetHardwareList(T_HARDWARE_COND Param)
        {
            Param.SORT_ORDER = string.IsNullOrEmpty(Param.SORT_ORDER) ? "A.HARDWARE_CODE DESC" : Param.SORT_ORDER;
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\T_HARDWARE.xml", "GetHardwareList"
                                            , Param.PAGE.ToString("1")
                                            , Param.PAGE_COUNT.ToString("10")
                                            
                                            , Param.SORT_ORDER
                                            , Param.FR_DATE.RemoveDateString()
                                            , Param.TO_DATE.RemoveDateString()
                                            , Param.HARDWARE_CODE.ToString("")
                                            , Param.HARDWARE_NAME.ToString("")
                                            , Param.BRAND.ToString("")
                                            , Param.MODEL_NAME.ToString("")
                                            , Param.CONTACT_EMPLOYEE_CODE.ToString("")
                                            , Param.CONTACT_EMPLOYEE_NAME.ToString("")
                                            , (Param.HIDE == null ? "" : (Param.HIDE == true ? "1" : "0"))
                                        );
            return db.ExecuteQuery<T_HARDWARE>(sql).ToList();
        }

        /// <summary>                                                                                      
        /// T_HARDWARE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA HardwareSave(T_HARDWARE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql=string.Empty;
                    if (Param.SAVE_TYPE.ToString("") == "D")
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\T_HARDWARE.xml", "HardwareDelete"
                                                                    , Param.HARDWARE_CODE.ToString("-1")
                                                                    , Param.INSERT_CODE.ToString(""));

                    }
                    else
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\T_HARDWARE.xml", "HardwareSave"
                                                        , Param.HARDWARE_CODE.ToString("-1")
                                                        , Param.HARDWARE_NAME.ToString("")
                                                        , Param.BRAND.ToString("")
                                                        , Param.MODEL_NAME.ToString("")
                                                        , Param.HARDWARE_WIDTH.ToString("")
                                                        , Param.HARDWARE_HEIGHT.ToString("")
                                                        , Param.DISPLAY_RESOLUTION.ToString("")
                                                        , Param.HARDWARE_DESC.ToString("")
                                                        , Param.PURCHASE_DATE.RemoveDateString()
                                                        , Param.PURCHASE_COMPANY_CODE.ToString("")
                                                        , Param.PURCHASE_STORE_CODE.ToString("")
                                                        , Param.PURCHASE_EMPLOYEE_CODE.ToString("")
                                                        , Param.CONTACT_COMPANY_CODE.ToString("")
                                                        , Param.CONTACT_STORE_CODE.ToString("")
                                                        , Param.CONTACT_EMPLOYEE_CODE.ToString("")
                                                        , Param.REMARK.ToString("")
                                                        , Param.HIDE ? "1" : "0"
                                                        , Param.INSERT_CODE.ToString("")
                        );
                    }
                    rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
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
