using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.Common;
using ALT.Framework;
using ALT.Framework.Data;
using System.Transactions;

namespace ALT.BizService
{
    public class GeoService : BaseService
    {
        public GeoService() { }
        public GeoService(System.Data.Linq.DataContext _db) : base(_db) { }
        /// <summary>                                                                                      
        /// T_GEO 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public async Task<RTN_SAVE_DATA> GeoSave(List<T_GEO> list, string SAVE_TYPE = "D")
        {
            string sql = string.Empty;
           RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    if(SAVE_TYPE == "D")
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "GeoService\\Geo\\T_GEO.xml", "GeoDelete"
                                        , list[0].GEO_TYPE
                                        , ""
                                       );
                        db.ExecuteCommand(sql);
                    }
                    foreach (T_GEO Param in list)
                    {
                          sql = Global.DBAgent.LoadSQL(sqlBasePath + "GeoService\\Geo\\T_GEO.xml", "GeoSave"
                                                        
                                                        , Param.GEO_TYPE
                                                        , Param.CODE
                                                        , Param.NAME.ToString("")
                                                        , Param.COORDINATES.ToString("")
                                                        , Param.INSERT_CODE.ToString("0")
                                    );
                        rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                    }
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
               rtn.ERROR_MESSAGE = ex.Message;
            }
            //await this._task;
            await Task.Delay(0);
            return rtn;
        }

        public List<T_GEO> GetGeoList(T_GEO Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "GeoService\\Geo\\T_GEO.xml", "GetGeoList"
                                                      , Cond.GEO_TYPE
                                                      , Cond.CODE
                                                      , Cond.NAME.ToString("")
                                                    
                                  );
            return db.ExecuteQuery<T_GEO>(sql).ToList();
        }
   }
}
