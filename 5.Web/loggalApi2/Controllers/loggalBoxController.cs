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

namespace loggalApi2.Controllers
{
    public class loggalBoxController : BaseController
    {
        /// <summary>
        /// 광고리스트 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<LOGGAL_AD_DATA> GetDailyLoggalAdList([FromBody]LOGGAL_AD_COND Cond)
        {
            IList<LOGGAL_AD_DATA> list = new DeviceService().GetDailyLoggalAdList(Cond);
            return list;
        }

        /// <summary>
        /// 광고리스트 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpGet]
        [DeflateCompression]
        public IList<LOGGAL_AD_DATA> GetDailyLoggalAdList2([FromUri]LOGGAL_AD_COND Cond)
        {
            IList<LOGGAL_AD_DATA> list = new DeviceService().GetDailyLoggalAdList(Cond);
            return list;
        }


        /// <summary>
        /// 광고클릭시 체크 및 클릭수 저장(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public RTN_SAVE_DATA AD_OpenPage([FromBody]LOGGAL_AD_COND Cond)
        {
            return new DeviceService().AD_OpenPage(Cond);
        }
        /// <summary>
        /// 메인광고 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public List<LOGGAL_MAIN_CONTENTLIST> GetloggalBoxMainAPIList([FromBody]T_DEVICE_COND Cond)
        {
            return new DeviceService().GetloggalBoxMainAPIList(Cond, Global.ConfigInfo.MANAGEMENT_SITE);
        }

        /// <summary>
        /// 기기인증번호 생성하기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public long GetloggalBoxAuthNumber([FromBody]T_DEVICE_COND Cond)
        {
            logger.Debug("/loggalbox/GetloggalBoxAuthNumber >>" + JsonConvert.SerializeObject(Cond));
            return new DeviceService().GetloggalBoxAuthNumber(Cond);
        }
        [HttpGet]
        [DeflateCompression]
        public List<LOGGAL_MAIN_CONTENTLIST> GetloggalBoxMainAPIList2([FromUri]T_DEVICE_COND Cond)
        {
            return new DeviceService().GetloggalBoxMainAPIList(Cond);
        }

        /// <summary>
        /// 기기리스트 가져오기(Post방식)
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<T_DEVICE> GetDeviceList([FromBody]T_DEVICE_COND Cond)
        {
            return new DeviceService().GetDeviceList(Cond);
        }


        /// <summary>
        /// 기기인증여부 체크하기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public bool CheckDeviceAuth([FromBody]T_DEVICE_COND Cond)
        {
            logger.Debug("loggalbox>CheckDeviceAuth" + JsonConvert.SerializeObject(Cond));
            Cond.AUTH_NUMBER = Cond.AUTH_NUMBER == null ? -1 : Cond.AUTH_NUMBER;
            return new DeviceService().checkDeviceAuth(Cond);
        }

        /// <summary>
        /// 기기인증여부 체크하기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public List<T_DEVICE_UPDATE> GetDeviceUpdateInfo([FromBody]T_DEVICE_UPDATE Cond)
        {
            return new DeviceService().GetDeviceUpdateInfo(Cond);
        }

        /// <summary>
        /// 기기인증여부 체크하기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public RTN_DEVICE_INFO_DATA GetDeviceUpdateAlllist([FromBody]List<DEVICE_INFO_COND> Condlist)
        {
            return new DeviceService().GetDeviceUpdateAlllist(Condlist, Global.ConfigInfo.MANAGEMENT_SITE);
        }

        /// <summary>
        /// 즐겨찾기 배너리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<LOGGAL_AD_DATA> FavoriteNationalInfoADList([FromBody]string deviceNumber)
        {
            IList<LOGGAL_AD_DATA> list = new DeviceService().GetFavoriteNationalInfoADList(deviceNumber);
            return list;
        }

        /// <summary>
        /// 지역별 기기정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public List<DEVICE_LOCATION> GetDeviceLocation([FromBody] DEVICE_LOCATION_COND Cond)
        {
            Cond.LATITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LAT) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LAT)) : Cond.LATITUDE;
            Cond.LONGITUDE = !string.IsNullOrEmpty(Cond.SEARCH_LONG) ? Convert.ToDecimal(Global.SecurityInfo.Decrypt_data(Cond.SEARCH_LONG)) : Cond.LONGITUDE;
            return new DeviceService().GetDeviceLocation(Cond);
        }

        /// <summary>
        /// 기기별 메인(Owner)광고 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public List<T_DEVICE_MAIN> GetDeviceMainList([FromBody] T_DEVICE_MAIN_COND Cond)
        {
            Cond.HIDE = false;
            return new DeviceService().GetDeviceMainList(Cond);
        }
        /// <summary>
        /// loggal box 광고데이터 가져오기 거리순
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<LOGGAL_BOX_DATA> GetLoggalBoxList(LOGGAL_BOX_COND Cond)
        {
            return new LoggalBoxService().GetLoggalBoxList(Cond);
        }
        /// <summary>
        /// 로컬박스 인증번호 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        [DeflateCompression]
        public IList<LOGGAL_BOX_AUTH> GetLocalboxAuthNumber(LOGGAL_BOX_AUTH Cond)
        {
            return new LoggalBoxService().GetLocalboxAuthNumber(Cond);
        }



        /// <summary>
        /// 로컬박스정보 맵정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [HttpPost]
        public List<T_DEVICE_STATION> GetDeviceStationMapList(T_DEVICE_STATION_COND Cond)
        {
            List<T_DEVICE_STATION> list = new DeviceService().GetDeviceStationMapList(Cond).Where(w => w.LATITUDE > 30 && w.LONGITUDE > 120).ToList();
            return list;
        }

    }
}