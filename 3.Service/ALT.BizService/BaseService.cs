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
using ALT.Framework.Mvc.Service;
using ALT.Framework.Mvc;
using System.Transactions;

namespace ALT.BizService
{

    public class BaseService : MRCBaseService
    {
        public BaseService() { }
        public BaseService(System.Data.Linq.DataContext _db) : base(_db) { }
       
       
    }
}
