using loggalWebMng.CommonCS;
using loggalServiceBiz;
using ALT.VO.loggal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.VO.Common;
using ALT.Framework.Mvc.Helpers;
using ALT.Framework.Data;

namespace loggalWebMng.Controllers
{
    public partial class BasicController : BaseController
    {
        #region >> 로컬사이니지 관련 정보 
        /// <summary>
        /// 로컬사이니지 배너 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult GetAdSigninfoList(T_AD_SIGNINFO_COND Cond)
        {
            return new JsonResult { Data = new LoggalBoxService().GetAdSigninfoList(Cond) };
        }
        /// <summary>
        /// 로컬사이니지 배너 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult AdSigninfoSave(T_AD_SIGNINFO Param)
        {
            Param.INSERT_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new LoggalBoxService().AdSigninfoSave(Param);
            return new JsonResult { Data = rtn };
        }

        /// <summary>
        /// 로컬사이니지 기기정보
        /// </summary>
        /// <returns></returns>
        [Compress]
        public ActionResult SignageList()
        {
            return View();
        }
        /// <summary>
        /// 로컬사이니지 기기정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public PartialViewResult pv_SignageList(T_SIGNAGE_COND Cond)
        {
            ViewBag.cond = Cond;
            ViewBag.list = new LoggalBoxService().GetSignageList(Cond);
            return PartialView2();
        }


        /// <summary>
        /// 사이니지 맵 리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult GetSignageMapList(T_SIGNAGE_COND Cond)
        {
            return new JsonResult { Data = new LoggalBoxService().GetSignageList(Cond).Where(w => w.LATITUDE > 30 && w.LONGITUDE > 120).Select(s => new DAUM_MAPLIST { ACTIVE_YN = true, TITLE = s.SIGN_NAME, LATITUDE = s.LATITUDE, LONGITUDE = s.LONGITUDE, LINK_URL = "/basic/signagereg/" + s.SIGN_CODE.ToString() }).ToList() };
        }

        [Compress]
        public PartialViewResult PV_SignagePlaceList(T_SIGNAGE_PLACE_COND Cond)
        {
            ViewBag.list = new LoggalBoxService().GetSignagePlaceList(Cond);
            return PartialView2();
        }


        /// <summary>                                                                                      
        /// T_DEVICE_PLACE 저장하기(광고장소 - T_AD_PLACE 저장 -  saveparam Query)										  
        /// </summary>																					  
        /// <param name="list"></param>																	  
        /// <returns></returns>									
        [Compress]
        public JsonResult SignagePlaceSave(SIGNAGE_PLACE_SAVE param)
        {
            param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA data = new LoggalBoxService().SignagePlaceSave(param);
            return new JsonResult { Data = data };
        }
        [Compress]
        public ActionResult SignageExcelReport(T_SIGNAGE_COND Cond)
        {
            List<string> headers = new List<string>()
            { "사용여부", "코드", "사이니지명", "디스플레이", "플레이시간", "주소", "우편번호","위도","경도", "사이니지", "배너수", "수정자", "수정시간"};

            Cond.PAGE = 1;
            Cond.PAGE_COUNT = 100000;

            return new LoggalBoxService().GetSignageList(Cond).Select(s => new {
                HIDE = s.HIDE == true ? "미사용" : "사용"
               ,SIGN_CODE = s.SIGN_CODE
               ,SIGN_NAME = s.SIGN_NAME
               ,IS_VERTICAL_NAME = s.IS_VERTICAL_NAME
               ,ADDRESS = s.ADDRESS1 + " " + s.ADDRESS2
               ,ZIP_CODE = s.ZIP_CODE
               ,LATITUDE = s.LATITUDE
               ,LONGITUDE = s.LONGITUDE
               ,
                AUTH_NUMBER = s.AUTH_NUMBER == null ? "미등록" : s.AUTH_NUMBER.ToString()
               ,
                AD_SIGNINFO_CNT = s.AD_SIGNINFO_CNT
               ,
                UPDATE_NAME = s.UPDATE_NAME
               ,
                UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
            ;
        }
        /// <summary>
        /// 로컬사이니지 기기정보 저장
        /// </summary>
        /// <returns></returns>
        [Compress]
        public ActionResult SignageReg(T_SIGNAGE_COND Cond)
        {
            Cond.id = Cond.id == null ? -1 : Cond.id;
            if(Cond.id >0 ) { Cond.SIGN_CODE = Cond.id; }

            if (Cond.REPRE_SIGN_CODE != null && Cond.REPRE_SIGN_CODE > 0)
            {
                T_SIGNAGE data = new LoggalBoxService().GetSignageList(new T_SIGNAGE_COND { SIGN_CODE = Cond.REPRE_SIGN_CODE } ).FirstOrDefault();
                data.SIGN_CODE = null;
                data.SIGN_NAME = data.SIGN_NAME + "(서브)";
                data.REPRE_SIGN_CODE = Cond.REPRE_SIGN_CODE;
                data.REPRE_SIGN_NAME = data.SIGN_NAME;
                data.IS_REPRESENT = false;
                ViewBag.data = data;
            }
            else
            {
                ViewBag.data = new LoggalBoxService().GetSignageList(Cond).FirstOrDefault();
            }
            return View();
        }
        /// <summary>
        /// 로컬사이니지 기기정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult SignageSave(T_SIGNAGE Param)
        {
            Param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new LoggalBoxService().SignageSave(Param);
            return new JsonResult { Data = rtn };
        }

        /// <summary>
        /// 로컬사인배너 연결 정보  가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult GetadSigninfoSignageList(T_AD_SIGNINFO_SIGNAGE_COND Cond)
        {
            return new JsonResult { Data = new LoggalBoxService().GetadSigninfoSignageList(Cond) };
        }
        [Compress]
        public PartialViewResult pv_AdSigninfoList(T_AD_SIGNINFO_COND Cond)
        {
            ViewBag.list = new LoggalBoxService().GetAdSigninfoList(Cond);
            return PartialView2();
        }
        [Compress]
        public PartialViewResult pv_AdSigninfoSignageCard(T_AD_SIGNINFO_SIGNAGE_COND Cond)
        {
            ViewBag.list = new LoggalBoxService().GetadSigninfoSignageList(Cond);
            return PartialView2();
        }

        /// <summary>
        /// 로컬사인배너 연결 정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult adSigninfoSignageSave(T_AD_SIGNINFO_SIGNAGE_SAVE Param)
        {
            RTN_SAVE_DATA rtn = new LoggalBoxService().adSigninfoSignageSave(Param);
            return new JsonResult { Data = rtn };
        }
        #endregion
        #region >> 사이니지 제어
        /// <summary>
        /// 사이니지 제어
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public ActionResult SignageControl(T_SIGNAGE_CONTROL_COND Cond)
        {
            Cond.IDX = Cond.IDX ?? -1;
            T_SIGNAGE_CONTROL rtn = new LoggalBoxService().GetSignageControlList(Cond).FirstOrDefault();
            if (rtn == null)
            {
               
               var data = new LoggalBoxService().GetSignageList(new T_SIGNAGE_COND() {  SIGN_CODE = Cond.SIGN_CODE ?? -1 }).FirstOrDefault();
                if (data != null)
                {
                   rtn = new T_SIGNAGE_CONTROL() { SIGN_CODE = Cond.SIGN_CODE, SIGN_NAME = data.SIGN_NAME };
                }
            }
            ViewBag.data = rtn;
            if (!string.IsNullOrEmpty(Cond.USER_ID))
            {
                var rtnData =  new AccountService().GetMemberList(new T_MEMBER_COND { USER_ID = Cond.USER_ID }).FirstOrDefault();
                if(rtnData != null)
                {
                   rtnData.PASSWORD = "";
                    ViewBag.login = rtnData;

                }

            }

            return View();
        }

        /// <summary>
        /// 사이니지 제어 저장
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public JsonResult SignageControlSave(T_SIGNAGE_CONTROL Param)
        {
            RTN_SAVE_DATA rtn = new LoggalBoxService().SignageControlSave(Param);
            return new JsonResult() { Data = rtn };
        }

        public JsonResult GetSignageControlList(T_SIGNAGE_CONTROL_COND Cond)
        {
            var list = new LoggalBoxService().GetSignageControlList(Cond);
            return new JsonResult() { Data = list };
        }

        public JsonResult SignageControlPlayUpdate(T_SIGNAGE_CONTROL_UPDATE Cond)
        {
            var list = new LoggalBoxService().SignageControlPlayUpdate(Cond);
            return new JsonResult() { Data = list };
        }

        public ActionResult SignageControlList()
        {
            return View();
        }
        public ActionResult PV_SignageControlList(T_SIGNAGE_CONTROL_COND Cond)
        {
            ViewBag.list = new LoggalBoxService().GetSignageControlList(Cond); ;
            return PartialView2();
        }

        [Compress]
        public ActionResult SignageControlExcelReport(T_SIGNAGE_CONTROL_COND Cond)
        {
            List<string> headers = new List<string>()
            { "순번", "메일", "요청자", "콘텐츠", "내용", "요청시간", "완료시간", "시작시간","종료시간", "등록자","등록시간", "수정자", "수정시간"};

            Cond.PAGE = 1;
            Cond.PAGE_COUNT = 100000;

            return new LoggalBoxService().GetSignageControlList(Cond).Select(s => new {
                 IDX = s.IDX
                ,REQUEST_EMAIL = s.REQUEST_EMAIL
                ,REQUEST_NAME = s.REQUEST_NAME
                ,CONTENT_URL = s.CONTENT_URL
                ,CONTENT = s.CONTENT
                ,SIGN_NAME = s.SIGN_NAME
                ,PLAY_REQ_TIME = s.PLAY_REQ_TIME
                ,COMPLEATED_DATE = s.COMPLEATED_DATE
                ,PLAY_FR_TIME = s.PLAY_FR_TIME
                ,PLAY_TO_TIME = s.PLAY_TO_TIME
                ,UPDATE_NAME = s.UPDATE_NAME
                ,UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
            ;
        }
        #endregion

        public JsonResult SignageSubCopySave(T_SIGNAGE Param)
        {

            Param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new LoggalBoxService().SignageSubCopySave(Param);
            return new JsonResult() { Data = rtn };
        }

        
        public PartialViewResult pv_DeviceMainGroupList(T_DEVICE_MAIN_GROUP Cond)
        {
            ViewBag.list = new DeviceService().GetDeviceMainGroupList(Cond);

            return PartialView2();
        }

        public JsonResult DeviceMainGroupSave(List<T_DEVICE_MAIN_GROUP> saveList)
        {
            RTN_SAVE_DATA rtn = new DeviceService().DeviceMainGroupSave(saveList, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = rtn };
        }

        [Compress]
        public JsonResult DeviceMainGroupSeqChange(T_DEVICE_MAIN_SEQ_CHANGE Param)
        {
            Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtnData = new DeviceService().DeviceMainGroupSeqChange(Param);
            return new JsonResult { Data = rtnData };
        }

    }
}