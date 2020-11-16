using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ALT.VO.loggal;
using loggalServiceBiz;
using Newtonsoft.Json;
using ALT.VO.Common;
using ALT.Framework;
using ALT.Framework.Mvc.Helpers;
namespace loggalApi.Controllers
{
    public class SignageController : BaseController
    {
        [HttpPost]
        [DeflateCompression]
        public IList<T_SIGNAGE> GetSignageList(T_SIGNAGE_COND Cond)
        {
            IList<T_SIGNAGE> list = new LoggalBoxService().GetSignageList(Cond);
            return list;
        }
        /// <summary>
        /// 사이니지 제어 정보 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<T_SIGNAGE_CONTROL> GetSignageControlList([FromBody]T_SIGNAGE_CONTROL_COND Cond)
        {
            IList<T_SIGNAGE_CONTROL> list = new LoggalBoxService().GetSignageControlList(Cond);
            return list;
        }
        /// <summary>
        /// 사이니지 제어 정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public RTN_SAVE_DATA SignageControlSave(T_SIGNAGE_CONTROL Param)
        {
            return new LoggalBoxService().SignageControlSave(Param);
        }

        /// <summary>                                                                                      
        /// 모바일에서 사이니지 조회																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        [HttpPost]
        [DeflateCompression]
        public List<MOBILE_SIGNAGE_LIST> GetMobileSignageList(MOBILE_SIGNAGE_COND Cond)
        {
            Cond.LATITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LAT) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LAT)) : Cond.LATITUDE;
            Cond.LONGITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LONG) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LONG)) : Cond.LONGITUDE;
            return new LoggalBoxService().GetMobileSignageList(Cond);
        }
    }
}