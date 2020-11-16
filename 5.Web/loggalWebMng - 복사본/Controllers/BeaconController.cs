using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalServiceBiz;
using loggalWebMng.CommonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class BeaconController : BaseController
    {
        #region >> 로컬사이니지 관련 정보 
        /// <summary>
        /// 로컬사이니지 배너 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public JsonResult GetAdBeaconList(T_AD_BEACON_COND Cond)
        {
            return new JsonResult { Data = new BeaconService().GetAdBeaconList(Cond) };
        }
        /// <summary>
        /// 로컬사이니지 배너 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public JsonResult AdBeaconSave(T_AD_BEACON Param)
        {
            Param.INSERT_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new BeaconService().AdBeaconSave(Param);
            return new JsonResult { Data = rtn };
        }

        /// <summary>
        /// 로컬사이니지 기기정보
        /// </summary>
        /// <returns></returns>
        public ActionResult BeaconList()
        {
            return View();
        }
        /// <summary>
        /// 로컬사이니지 기기정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public PartialViewResult pv_BeaconList(T_BEACON_COND Cond)
        {
            ViewBag.list = new BeaconService().GetBeaconList(Cond);
            return PartialView2();
        }

        [Compress]
        public ActionResult BeaconExcelReport(T_BEACON_COND Cond)
        {
            List<string> headers = new List<string>()
            { "사용여부", "코드", "사이니지명", "디스플레이", "플레이시간", "주소", "우편번호","위도","경도", "사이니지", "배너수", "수정자", "수정시간"};

            Cond.PAGE = 1;
            Cond.PAGE_COUNT = 100000;

            return new BeaconService().GetBeaconList(Cond).Select(s => new {
                HIDE = s.HIDE == true ? "미사용" : "사용"
               ,BEACON_CODE = s.BEACON_CODE
               ,BEACON_NAME = s.BEACON_NAME
               ,ADDRESS = s.ADDRESS1 + " " + s.ADDRESS2
               ,ZIP_CODE = s.ZIP_CODE
               ,LATITUDE = s.LATITUDE
               ,LONGITUDE = s.LONGITUDE
               ,UPDATE_NAME = s.UPDATE_NAME
               ,UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
            ;
        }
        /// <summary>
        /// 로컬사이니지 기기정보 저장
        /// </summary>
        /// <returns></returns>
        public ActionResult BeaconReg(long? id)
        {
            id = id == null ? -1 : id;
            ViewBag.data = new BeaconService().GetBeaconList(new T_BEACON_COND { BEACON_CODE = id }).FirstOrDefault();
            return View();
        }
        /// <summary>
        /// 로컬사이니지 기기정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public JsonResult BeaconSave(T_BEACON Param)
        {
            Param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new BeaconService().BeaconSave(Param);
            return new JsonResult { Data = rtn };
        }

        public PartialViewResult pv_AdBeaconList(T_AD_BEACON_COND Cond)
        {
            ViewBag.list = new BeaconService().GetAdBeaconList(Cond);
            return PartialView2();
        }
        public PartialViewResult pv_AdBeaconCard(T_AD_BEACON_COND Cond)
        {
            ViewBag.list = new BeaconService().GetAdBeaconList(Cond);
            return PartialView2();
        }

        /// <summary>
        /// 로컬사인배너 연결 정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public JsonResult adBeaconSave(T_AD_BEACON Param)
        {
            RTN_SAVE_DATA rtn = new BeaconService().AdBeaconSave(Param);
            return new JsonResult { Data = rtn };
        }
        #endregion 
    }
}