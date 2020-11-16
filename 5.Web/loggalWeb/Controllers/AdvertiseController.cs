using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalServiceBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace loggalWeb.Controllers
{
    public class AdvertiseController : Controller
    {
        // GET: Advertise
        [Compress]
        public ActionResult Content(long id, long? deviceCode = null, int bannerKind=2)
        {
        
            T_AD dataSel = new AdvertisingService().GetT_AD_List(id).FirstOrDefault();
           // Task.Run(() => { new AdvertisingService().AdContentClickSave(new LOGGAL_AD_COND { AD_CODE = id, DEVICE_CODE = deviceCode }); });
            //new AdvertisingService().AdContentClickSave(new LOGGAL_AD_COND { AD_CODE = id, DEVICE_CODE = deviceCode });

            /* T_AD_PLAY_LOG param = new T_AD_PLAY_LOG()
             {
                 DEVICE_CODE = deviceCode, AD_CODE = id
                 , DEVICE_KIND = (deviceCode == null ? 1 : 2)
                 , FRAME_TYPE = 1
                 , PLAY_TYPE = 2
                 , REMARK = ""
                 , INSERT_CODE = 0

             };
             RTN_SAVE_DATA data = new AdvertisingService().AdPlayLogSave(param);*/
            string deviceName = "";

            if (deviceCode != null )
            {
                var device = new LoggalBoxService().GetLoggalBoxList(new LOGGAL_BOX_COND { DEVICE_CODE = deviceCode }).FirstOrDefault();
                deviceName = device == null? "" : device.DEVICE_NAME;
            }

            T_AD_PLAY_LOG_MONGO param = new T_AD_PLAY_LOG_MONGO()
            {
                DEVICE_CODE = deviceCode,
                DEVICE_NAME = deviceName,
                AD_CODE = id,
                TITLE = dataSel.TITLE,
                DEVICE_KIND = (deviceCode == null ? 1 : 2),
                DEVICE_KIND_NAME = (deviceCode == null ? "모바일" : "로컬박스"),
                FRAME_TYPE = 1,
                FRAME_TYPE_NAME = "1 Frame",
                BANNER_TYPE2    = 1,
                BANNER_TYPE2_NAME = "이미지",
                BANNER_KIND = bannerKind,
                BANNER_KIND_NAME = bannerKind == 1 ? "내배너" :"일반배너",
                PLAY_TYPE = 2,
                PLAY_TYPE_NAME = "클릭",
                PLAY_TIME = 30,
                REMARK = ""
            };
           
            Task.Run(async () =>
            {
                //new CommonService().GetCommon(new T_COMMON_COND() { ADD_COND = "AND MAIN_CODE IN ('A010','B008','H002','L003','P004')" }).Select(s => new { s.MAIN_CODE, s.SUB_CODE, s.NAME });
                new MongoDBService().AdPlayLogSave(param);
                await Task.FromResult(false);
            });
            ViewBag.data = dataSel;
            ViewBag.deviceCode = deviceCode;
            return View();
        }

        [Compress]
        public JsonResult AdContentClickSave(LOGGAL_AD_COND Param)
        {

            RTN_SAVE_DATA data = new AdvertisingService().AdContentClickSave(Param);
            return new JsonResult { Data = data };
        }

        [Compress]
        public JsonResult AdFavoriteSave(T_AD_FAVORITE Param)
        {

            Param.USER_IP = Request.UserHostAddress;
            RTN_SAVE_DATA data = new AdvertisingService().AdFavoriteSave(Param);

            return new JsonResult { Data = data };
        }
    }
}