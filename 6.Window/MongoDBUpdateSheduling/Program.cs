using ALT.VO.Common;
using loggalServiceBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDBUpdateScheduling
{
    class Program
    {
        static void Main(string[] args)
        {
            RTN_SAVE_DATA rtn;
            if (Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")) <= 20190516)  rtn = new MongoDBService().MongoDBToSqlServer(DateTime.Now.AddDays(-3).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));
            else rtn = new MongoDBService().MongoDBToSqlServer(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));

        
            //rtn = new MongoDBService().MongoDBToSqlServer(DateTime.Now.ToString("yyyyMMdd"));
            if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE)) Console.Write(rtn.ERROR_MESSAGE);
        }
    }
}
