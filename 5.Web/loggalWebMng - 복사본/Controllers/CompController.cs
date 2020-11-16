using ALT.BizService;
using ALT.Framework.Data;
using ALT.Framework.Mvc.Helpers;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalWebMng.CommonCS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loggalWebMng.Controllers
{
    public class CompController : BaseController
    {
        [Compress]
        public ActionResult CompList()
        {
            return View();
        }
        #region >> 회사정보
        [Compress]
        public ActionResult CompReg(int? id = -1)
        {
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                id = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }

            T_COMPANY data = new BasicService().GetCompanyList(new T_COMPANY_COND { COMPANY_CODE = id }).FirstOrDefault();

            if (data != null && !string.IsNullOrEmpty(data.PASSWORD)) data.PASSWORD = "******";
            ViewBag.data = data;
            return View();
        }
        [Compress]
        public JsonResult GetCompList(T_COMPANY_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }
            return new JsonResult { Data = new BasicService().GetCompanyList(Cond) };
        }
        [Compress]
        public JsonResult CompanySave(T_COMPANY Param)
        {
            Param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new BasicService().CompanySave(Param);
            return new JsonResult() { Data = rtn };
        }
        [Compress]
        public PartialViewResult PV_CompList(T_COMPANY_COND Cond)
        {
            /*알트소프트에 전체권한이 아닐경우 자기 업체만 조회*/
            if (!(SessionHelper.LoginInfo.STORE.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1))
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }
            ViewBag.list = new BasicService().GetCompanyList(Cond);
            return PartialView2();
        }

        /// <summary>
        /// 회사정보 맵 리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult GetCompanyMapList(T_COMPANY_COND Cond)
        {
            if (!(SessionHelper.LoginInfo.STORE.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1))
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }
            return new JsonResult { Data = new BasicService().GetCompanyList(Cond).Where(w => w.LATITUDE > 30 && w.LONGITUDE > 120).Select(s => new DAUM_MAPLIST { ACTIVE_YN = true, TITLE = s.COMPANY_NAME, LATITUDE = s.LATITUDE, LONGITUDE = s.LONGITUDE, LINK_URL = "/comp/compReg/" + s.COMPANY_CODE.ToString() }).ToList() };
        }

        [Compress]
        public ActionResult CompanyExcelReport(T_COMPANY_COND Cond)
        {
            /*알트소프트에 전체권한이 아닐경우 자기 업체만 조회*/
            if (!(SessionHelper.LoginInfo.STORE.STORE_CODE == 1 && SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH == 1))
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }
            List<string> headers = new List<string>()
            { "코드", "아이디", "회사명", "회사유형","회사유형2", "대표전화", "이메일", "주소", "상태", "비고", "수정자", "수정시간"   };

            Cond.PAGE = 1;
            Cond.PAGE_COUNT = 100000;
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }
            return new BasicService().GetCompanyList(Cond).Select(s => new {
                                           COMPANY_CODE = s.COMPANY_CODE
                                          ,COMPANY_ID = s.COMPANY_ID
                                          ,COMPANY_NAME = s.COMPANY_NAME
                                          ,COMPANY_TYPE_NAME = s.COMPANY_TYPE_NAME
                                          ,COMPANY_TYPE_NAME2 = s.COMPANY_TYPE_NAME2
                                          ,PHONE = s.PHONE
                                          ,EMAIL = s.EMAIL
                                          ,ADDRESS = s.ADDRESS1 + " " + s.ADDRESS2 + "(우)" + s.ZIP_CODE
                                          ,STATUS_NAME = s.STATUS_NAME
                                          ,REMARK = s.REMARK
                                          ,UPDATE_NAME = s.UPDATE_NAME
                                          ,UPDATE_DATE = s.UPDATE_DATE
                                       }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }
        #endregion >> 회사정보

        #region >> 지점(사업장)정보
        [Compress]
        public ActionResult storeList(T_STORE_COND Cond)
        {
            ViewBag.data = Cond;
            return View();
        }
        [Compress]
        public ActionResult storereg(int? id = -1, int? COMPANY_CODE = -1)
        {
            if (SessionHelper.LoginInfo.STORE_CODE != 1 || !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE;
            }

            if (SessionHelper.LoginInfo.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                id = SessionHelper.LoginInfo.STORE_CODE;
            }

            T_STORE data = new BasicService().GetStoreList(new T_STORE_COND { STORE_CODE = id }).FirstOrDefault();
            if (data != null && !string.IsNullOrEmpty(data.PASSWORD)) data.PASSWORD = "******";
            data = (data == null) ? new T_STORE() { COMPANY_CODE = COMPANY_CODE, STORE_CODE = -1 } : data;


            ViewBag.data = data;
            
            return View();
        }
        [Compress]
        public JsonResult GetStoreList(T_STORE_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
            }

            return new JsonResult { Data = new BasicService().GetStoreList(Cond) };
        }
        /// <summary>
        /// 매장정보 맵 리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [Compress]
        public JsonResult GetStoreMapList(T_STORE_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
            }
            return new JsonResult { Data = 
                                new BasicService().GetStoreList(Cond).Where(w => w.LATITUDE > 30 && w.LONGITUDE > 120)
                                                        .Select(s => new DAUM_MAPLIST { ACTIVE_YN = true
                                                                , CONTENT = "<b>배너 :</b> " + s.AD_CNT0.ToString() + "/" + s.AD_CNT1.ToString() + "/" + s.AD_CNT2.ToString()
                                                                           + " <b>로컬박스 :</b> " + s.DEVICE_CNT0.ToString() + "/" + s.DEVICE_CNT1.ToString() + "/" + s.DEVICE_CNT2.ToString()
                                                                ,  TITLE = s.COMPANY_NAME, LATITUDE = s.LATITUDE, LONGITUDE = s.LONGITUDE, LINK_URL = "/comp/storereg/" + s.STORE_CODE.ToString() }).ToList() };
        }
        [Compress]
        public JsonResult StoreSave(T_STORE Param)
        {
            Param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new BasicService().StoreSave(Param);
            return new JsonResult() { Data = rtn };
        }
        [Compress]
        public PartialViewResult PV_StoreList(T_STORE_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
            }

            ViewBag.list = new BasicService().GetStoreList(Cond);
            return PartialView2();
        }
        [Compress]
        public ActionResult StoreExcelReport(T_STORE_COND Cond)
        {
            if (SessionHelper.LoginInfo.STORE.STORE_CODE != 1)
            {
                Cond.COMPANY_CODE = SessionHelper.LoginInfo.STORE.COMPANY_CODE;
            }

            if (SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
            {
                Cond.STORE_CODE = SessionHelper.LoginInfo.STORE.STORE_CODE;
            }

            List<string> headers = new List<string>()
            { "회사명","코드", "아이디", "사업장명", "유형", "대표전화", "이메일", "주소", "상태", "시간대(UTC+)", "비고", "수정자", "수정시간"   };

            Cond.PAGE = 1;
            Cond.PAGE_COUNT = 100000;

            return new BasicService().GetStoreList(Cond).Select(s => new {
                COMPANY_NAME    = s.COMPANY_NAME
               ,STORE_CODE      = s.STORE_CODE
               ,STORE_ID        = s.STORE_ID
               ,STORE_NAME      = s.STORE_NAME
               ,STORE_TYPE_NAME = s.STORE_TYPE_NAME
               ,PHONE           = s.PHONE
               ,EMAIL           = s.EMAIL
               ,ADDRESS = s.ADDRESS1 + " " + s.ADDRESS2 + "(우)" + s.ZIP_CODE
               ,STATUS_NAME     = s.STATUS_NAME
               ,TIME_ZONE       = s.TIME_ZONE
               ,REMARK          = s.REMARK
               ,UPDATE_NAME     = s.UPDATE_NAME
               ,UPDATE_DATE     = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }
        #endregion >> 회사정보

        #region >> 지점별 부서정보
        [Compress]
        public ActionResult StoreDeptRegList(int? id)
        {

            id = id == null ? SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE : id;
            ViewBag.STORE_CODE = id;
            return View();
        }
        [Compress]
        public PartialViewResult pv_StoreDeptRegList(T_STORE_DEPT Cond)
        {

            Cond.HIDE = null;
            ViewBag.list = new CommonService().GetDept(Cond);
            return PartialView2();
        }
        [Compress]
        public JsonResult storeDeptSave(List<T_STORE_DEPT> list)
        {
            RTN_SAVE_DATA data = new CommonService().DeptSave(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

            return new JsonResult { Data = data };
        }
        [Compress]
        public ActionResult StoreDeptExcelReport(T_STORE_DEPT Cond)
        {

            List<string> headers = new List<string>()
            { "코드", "부서명", "상위부서", "미사용","레벨", "수정자", "수정시간" };


            return new CommonService().GetDept(Cond).Select(s => new {
                DEPT_SEARCH = s.DEPT_SEARCH
               ,DEPT_NAME = s.DEPT_NAME
               ,PARENT_DEPT_NAME = s.PARENT_DEPT_NAME
               ,HIDE = s.HIDE_NAME
               ,LEVEL = s.LEVEL
               ,UPDATE_NAME = s.UPDATE_NAME
               ,UPDATE_DATE = s.UPDATE_DATE
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }

        #endregion >> 지점별 부서정보

        #region >> 지점별 직급/직책 저장
        [Compress]
        public ActionResult StorePositionRegList(int? id)
		{
			id = id == null ? SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE : id;
			ViewBag.STORE_CODE = id;
			return View();
		}
        [Compress]
        public PartialViewResult pv_StorePositionRegList(T_STORE_POSITION Cond)
		{
			ViewBag.list = new CommonService().GetPosition(Cond);
			return PartialView2();
		}
        [Compress]
        public JsonResult storePositionuSave(List<T_STORE_POSITION> list)
		{
			RTN_SAVE_DATA data = new CommonService().PositionSave(list, (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

			return new JsonResult { Data = data };
		}
        #endregion >> 지점별 직급/직책 저장
	}


}