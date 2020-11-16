using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.loggal;
using ALT.Framework;
using ALT.Framework.Data;
using System.Data.Linq;
using ALT.VO.Common;
using System.Transactions;

namespace loggalServiceBiz
{
    public class ShareService : BaseService
    {
        public ShareService() { }
        public ShareService(DataContext _db) : base(_db) { }


        /// <summary>                                                                                      
        /// T_SHARE 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_SHARE> GetShareList(T_SHARE_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Base\\T_SHARE.xml", "GetShareList"
                                            , Param.PAGE.ToString("1")
                                            , Param.PAGE_COUNT.ToString("10")
                                            , Param.SORT_ORDER.ToString("A.SHARE_CODE DESC")
                                            , Param.GUBUN.ToString("") == "banner" ? "1" : "2"
                                            , Param.SHARE_CODE.ToString("")
                                            , Param.SEND_MEMBER_CODE.ToString("")
                                            , Param.AD_CODE.ToString("")
                                            , Param.DEVICE_CODE.ToString("")
                                            
                                            , Param.HIDE == null ? "" : (Param.HIDE == false ? "0" : "1")
                                            , Param.TITLE.ToString("")
                                            , Param.DEVICE_NAME.ToString("")
                                            , Param.SEND_MEMBER_NAME.ToString("")
                                            , Param.RECEIVE_MEMBER_NAME.ToString("")
                                              );
            return db.ExecuteQuery<T_SHARE>(sql).ToList();
        }
        /// <summary>                                                                                      
        /// T_SHARE_DTL 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>	

        public List<T_SHARE_DTL> GetShareDtlList(T_SHARE_DTL_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "경로\\T_SHARE_DTL.xml", "GetShareDtlList"
                                            , Param.PAGE_COUNT.ToString("10")
                                            , Param.PAGE.ToString("1")
                                            , Param.SORT_ORDER
                                              );
            return db.ExecuteQuery<T_SHARE_DTL>(sql).ToList();
        }
        /// <summary>                                                                                      
        /// T_SHARE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA ShareSave(T_SHARE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Base\\T_SHARE.xml", "ShareSave"
                                                    , Param.SHARE_CODE.ToString()
                                                    , Param.SEND_MEMBER_CODE.ToString("")
                                                   
                                                    , Param.AD_CODE.ToString("")
                                                    , Param.DEVICE_CODE.ToString("")
                                                    , Param.COMMENT.ToString("")
                                                   
                                                    , (Param.HIDE == null || Param.HIDE == false ? "0" : "1")
                                                    , Param.REMARK.ToString("")
                                                    , Param.INSERT_CODE.ToString("")

                      );
                    rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                    if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
                    {
                        throw new Exception(rtn.ERROR_MESSAGE);
                    }

                    Param.SHARE_CODE = rtn.DATA.ToLong();
                    foreach (T_SHARE_DTL data in Param.detaillist)
                    {
                        data.SHARE_CODE = Param.SHARE_CODE;
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Base\\T_SHARE.xml", "ShareDtlSave"
                                                           , data.SHARE_CODE.ToString("")
                                                           , data.RECEIVE_MEMBER_CODE.ToString("")
                                                           , (data.IS_VIEW == null || data.IS_VIEW == false ? "0" : "1")
                                                           , (data.HIDE == null || data.HIDE == false ? "0" : "1")
                                                           , data.REMARK.ToString("")
                                                           , data.INSERT_CODE.ToString("")
                        ); 													
                        db.ExecuteCommand(sql);
                    }

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
