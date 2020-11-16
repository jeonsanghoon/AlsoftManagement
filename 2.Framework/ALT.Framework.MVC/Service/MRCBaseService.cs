using System.IO;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ALT.Framework.Mvc.Service
{
    public class MRCBaseService
    {
        protected readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public DataContext db;
        public MRCBaseService()
        {
            //log.Debug("디비생성");
            if (db == null)
                db = new DataContext(Global.ConfigInfo.DefaultDBSource);
        }

        public MRCBaseService(DataContext _db)
        {
            //log.Debug("디비생성");
            db = _db;
        }

        public string sqlBasePath
        {
            get
            {
                try
                {
                    return HttpContext.Current.Server.MapPath(Global.ConfigInfo.SqlXmlPath);
                }
                catch (Exception)
                {
                    string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                    UriBuilder uri = new UriBuilder(codeBase);
                    string path = Uri.UnescapeDataString(uri.Path);
                    return Path.GetDirectoryName(path) + Global.ConfigInfo.SqlXmlPath;
                }
            }
        }

    }
}
