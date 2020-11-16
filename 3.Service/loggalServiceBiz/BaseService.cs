using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using System.Web;

namespace loggalServiceBiz
{
    public class BaseService
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DataContext db;
        public BaseService()
        {
            //log.Debug("디비생성");
            if (db == null)
                db = new DataContext(Global.ConfigInfo.DefaultDBSource);
        }

        public BaseService(DataContext _db)
        {
            //log.Debug("디비생성");
            db = _db;
        }

        public string sqlBasePath { get { return HttpContext.Current.Server.MapPath(Global.ConfigInfo.SqlXmlPath); } }

        /// <summary>
        /// 공통코드 가져오기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_COMMON> GetCommon(T_COMMON_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Base\\Common.xml", "GetCommon"
                                                  , Param.MAIN_CODE.ToString("")
                                                  , Param.SUB_CODE.ToString("")
                                                  , Param.REF_DATA1.ToString("")
                                                  , Param.REF_DATA2.ToString("")
                                                  , Param.REF_DATA3.ToString("")
                                                  , Param.REF_DATA4.ToString("")
                                                  , Param.HIDE == null ? "" : (Param.HIDE == false ? "0" : "1")
                );
            return db.ExecuteQuery<T_COMMON>(sql).ToList();
        }
    }
}
