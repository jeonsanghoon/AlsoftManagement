using ALT.BizService;
using ALT.Framework;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using System.Collections.Generic;
using System.Web.Http;

namespace loggalApi.Controllers
{
    public class CommonController : BaseController
    {
        /// <summary>                                                                                      
        /// T_FILE 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>		
        [HttpPost]
        [DeflateCompression]
        public RTN_SAVE_DATA FileSave([FromBody]T_FILE Param)
        {
            Param.TABLE_NAME = Global.SecurityInfo.Decrypt_data(Param.TABLE_NAME);
            Param.TABLE_KEY = Global.SecurityInfo.Decrypt_data(Param.TABLE_KEY);
            Param.FILE_URL = Global.SecurityInfo.Decrypt_data(Param.FILE_URL);
            RTN_SAVE_DATA rtn = new CommonService().FileSave(Param);
            return rtn;
        }

        [HttpPost]
        [DeflateCompression]
        public List<T_FILE> GetFileList(T_FILE Cond)
        {
            Cond.TABLE_NAME = Global.SecurityInfo.Decrypt_data(Cond.TABLE_NAME);
            Cond.TABLE_KEY = Global.SecurityInfo.Decrypt_data(Cond.TABLE_KEY);

            List<T_FILE> list = new CommonService().GetFileList(Cond);
            return list;
        }
    }
}