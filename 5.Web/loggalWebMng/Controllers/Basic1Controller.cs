using ALT.Framework.Mvc.Helpers;
using ALT.Framework.Mvc.Vo;
using ALT.VO.Common;
using ALT.VO.loggal;
using loggalServiceBiz;
using loggalWebMng.CommonCS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.Framework.Data;
using ALT.Framework.Mvc;
using System.Data;

namespace loggalWebMng.Controllers
{
	public partial class BasicController : BaseController
	{
		/// <summary>
		/// 로컬박스조회
		/// </summary>
		/// <returns></returns>
		[AltloggalAuthorization]
		public ActionResult deviceList(DEVICE_LIST_COND Cond)
		{
			SessionHelper.LOG_NAME = "로컬박스조회";
			ViewBag.Cond = Cond;
			return View();
		}

		[AltloggalAuthorization]
		public PartialViewResult PV_devicelist(DEVICE_LIST_COND Cond)
		{
			if (Cond.DISPLAY_MODE != "Total") this.SetDeviceListAuth(ref Cond);

			ViewBag.DISPLAY_MODE = Cond.DISPLAY_MODE;
			ViewBag.list = Service.deviceService.GetDeviceList(Cond);
			if (!string.IsNullOrEmpty(Cond.DISPLAY_MODE) && Cond.DISPLAY_MODE.ToLower() == "tab")
			{
				return PartialView2("~/views/basic/partial/pv_devicelist_tab.cshtml");
			}
			else
			{
				return PartialView2();
			}
		}


		/// <summary>
		/// 로컬박스전체조회
		/// </summary>
		/// <returns></returns>
		[AltloggalAuthorization]
		public ActionResult deviceTotList()
		{
			SessionHelper.LOG_NAME = "로컬박스전체조회";
			return View();
		}
		/// <summary>
		/// 로컬박스등록
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public ActionResult deviceReg(long? id)
		{
			SessionHelper.LOG_NAME = "로컬박스등록";
			long DEVICE_CODE = (long)(id == null ? -1 : id);


			T_DEVICE data = Service.deviceService.GetDeviceList(new T_DEVICE_COND { DEVICE_CODE = DEVICE_CODE }).FirstOrDefault();
			ViewBag.data = data = data == null ? new T_DEVICE() : data;
			if (SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE == 1 || SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE == data.COMPANY_CODE || id == null)
				SessionHelper.LoginInfo.EDIT_MODE = enEditMode.WRITE;
			else
				SessionHelper.LoginInfo.EDIT_MODE = enEditMode.READ;

			#region >> 초기화
			if (data.DEVICE_CODE == null || data.DEVICE_CODE <= 0)
			{
				data.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				data.COMPANY_NAME = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_NAME;
				data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
				data.STORE_NAME = SessionHelper.LoginInfo.EMPLOYEE.STORE_NAME;
				data.MOBILE = SessionHelper.LoginInfo.EMPLOYEE.MOBILE;
				data.MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
				data.MEMBER_NAME = SessionHelper.LoginInfo.MEMBER.USER_NAME;
				data.CONTACT_COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				data.CONTACT_COMPANY_NAME = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_NAME;
				data.CONTACT_STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
				data.CONTACT_STORE_NAME = SessionHelper.LoginInfo.EMPLOYEE.STORE_NAME;
				data.CONTACT_MOBILE = SessionHelper.LoginInfo.EMPLOYEE.MOBILE;
				data.CONTACT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
				data.CONTACT_NAME = SessionHelper.LoginInfo.MEMBER.USER_NAME;
			}
			#endregion >> 초기화

			ViewBag.KeywordList = new KeywordService().GetAdDeviceSearchKeyword(new CATEGORY_KEYWORD_COND { DEVICE_CODE = DEVICE_CODE, KEYWORD_TYPE = 2 });
			return View();
		}

		/// <summary>
		/// 로컬박스등록
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public ActionResult deviceReg2(long? id)
		{
			SessionHelper.LOG_NAME = "로컬박스등록";
			long DEVICE_CODE = (long)(id == null ? -1 : id);


			T_DEVICE data = Service.deviceService.GetDeviceList(new T_DEVICE_COND { DEVICE_CODE = DEVICE_CODE }).FirstOrDefault();
			ViewBag.data = data = data == null ? new T_DEVICE() : data;
			if (SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE == 1 || SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE == data.COMPANY_CODE || id == null)
				SessionHelper.LoginInfo.EDIT_MODE = enEditMode.WRITE;
			else
				SessionHelper.LoginInfo.EDIT_MODE = enEditMode.READ;

			#region >> 초기화
			if (data.DEVICE_CODE == null || data.DEVICE_CODE <= 0)
			{
				data.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				data.COMPANY_NAME = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_NAME;
				data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
				data.STORE_NAME = SessionHelper.LoginInfo.EMPLOYEE.STORE_NAME;
				data.MOBILE = SessionHelper.LoginInfo.EMPLOYEE.MOBILE;
				data.MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
				data.MEMBER_NAME = SessionHelper.LoginInfo.MEMBER.USER_NAME;
				data.CONTACT_COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				data.CONTACT_COMPANY_NAME = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_NAME;
				data.CONTACT_STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
				data.CONTACT_STORE_NAME = SessionHelper.LoginInfo.EMPLOYEE.STORE_NAME;
				data.CONTACT_MOBILE = SessionHelper.LoginInfo.EMPLOYEE.MOBILE;
				data.CONTACT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
				data.CONTACT_NAME = SessionHelper.LoginInfo.MEMBER.USER_NAME;
			}
			#endregion >> 초기화

			ViewBag.KeywordList = new KeywordService().GetAdDeviceSearchKeyword(new CATEGORY_KEYWORD_COND { DEVICE_CODE = DEVICE_CODE, KEYWORD_TYPE = 2 });
			return View();
		}
		public ActionResult DeviceVirtual(long? id)
		{ 
			this.setDeviceReg(id);
			return View("/views/basic/deviceVirtualReg.cshtml");
		}
		public ActionResult deviceVirtualReg(long? id)
		{
			if (null == id)
			{
				JsonResult data = new DeviceController().GetVirtualDevice(SessionHelper.LoginInfo.STORE.STORE_CODE);
				if(0 != (long)data.Data)
				{
					id = (long)data.Data;
				}
			}

			return RedirectToAction("DeviceVirtual", "basic", new
			{
				id = id
			});
		}
		/// <summary>
		/// 로컬박스등록
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// 
		public ActionResult deviceReg_1frame(long? id)
		{
			this.setDeviceReg(id, 1);
			return View("/views/basic/devicereg3.cshtml");
		}


		public ActionResult deviceReg_6frame(long? id)
		{
			this.setDeviceReg(id, 6);
			return View("/views/basic/devicereg3.cshtml");
		}

		[AltloggalAuthorization]
		public ActionResult deviceReg3(long? id)
		{
			this.setDeviceReg(id);
			T_DEVICE data = ViewBag.data;
	
			return RedirectToAction("deviceReg_" + (data.AD_FRAME_TYPE ?? 1).ToString() + "Frame", "basic", new
			{
				id = id
			});
		
			//return View();
		}
		private void setDeviceReg(long? id, int? adFrameType = 1)
		{
			SessionHelper.LOG_NAME = "로컬박스등록";
			long DEVICE_CODE = (long)(id == null ? -1 : id);
			adFrameType = adFrameType ?? 1;

			T_DEVICE data = Service.deviceService.GetDeviceList(new T_DEVICE_COND { DEVICE_CODE = DEVICE_CODE }).FirstOrDefault();


			#region >> 초기화
			if (data == null)
			{
				data = new T_DEVICE();
				data.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				data.COMPANY_NAME = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_NAME;
				data.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
				data.STORE_NAME = SessionHelper.LoginInfo.EMPLOYEE.STORE_NAME;
				data.MOBILE = SessionHelper.LoginInfo.EMPLOYEE.MOBILE;
				data.MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
				data.MEMBER_NAME = SessionHelper.LoginInfo.MEMBER.USER_NAME;
				data.CONTACT_COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				data.CONTACT_COMPANY_NAME = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_NAME;
				data.CONTACT_STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
				data.CONTACT_STORE_NAME = SessionHelper.LoginInfo.EMPLOYEE.STORE_NAME;
				data.CONTACT_MOBILE = SessionHelper.LoginInfo.EMPLOYEE.MOBILE;
				data.CONTACT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
				data.CONTACT_NAME = SessionHelper.LoginInfo.MEMBER.USER_NAME;
				data.AD_FRAME_TYPE = adFrameType;
			}
			if (SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE == 1 || SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE == data.COMPANY_CODE || id == null)
				SessionHelper.LoginInfo.EDIT_MODE = enEditMode.WRITE;
			else
				SessionHelper.LoginInfo.EDIT_MODE = enEditMode.READ;

			#endregion >> 초기화
			ViewBag.data = data;
			ViewBag.KeywordList = new KeywordService().GetAdDeviceSearchKeyword(new CATEGORY_KEYWORD_COND { DEVICE_CODE = DEVICE_CODE, KEYWORD_TYPE = 2 });

		}

		/// <summary>
		/// 로컬박스정보 맵정보 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public JsonResult GetDeviceMapList(DAUM_MAP_COND Cond)
		{
			List<DAUM_MAPLIST> list = new DeviceService().GetDeviceMapList(Cond);
			return new JsonResult { Data = list };
			//return new JsonResult { Data = Service.deviceService.GetDeviceList(Cond).Where(w=>w.LATITUDE > 30 && w.LONGITUDE > 120).Select(s => new DAUM_MAPLIST { ACTIVE_YN = (s.AUTH_NUMBER == null ? false : true)  , TITLE = s.DEVICE_NAME, LATITUDE = s.LATITUDE, LONGITUDE = s.LONGITUDE, LINK_URL = "/basic/deviceReg/" + s.DEVICE_CODE.ToString() }).OrderBy(o=>o.ACTIVE_YN).ToList() };
		}
		[AltloggalAuthorization]
		public PartialViewResult PV_LocalNameList(CODE_DATA Cond)
		{
			ViewBag.Cond = Cond;
			ViewBag.list = new KeywordService().GetLocalNameList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public PartialViewResult PV_RegionList(PAGE_COND Cond)
		{
			ViewBag.list = Service.deviceService.GetDeviceRegionList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult DeviceSave(List<T_DEVICE> list)
		{
			SessionHelper.LOG_NAME = "로컬박스저장";


			RTN_SAVE_DATA data = Service.deviceService.DeviceSave(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult DeviceRegionSave(List<DEVICE_REGION> list)
		{
			RTN_SAVE_DATA data = Service.deviceService.DeviceRegionSave(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}

		/// <summary>
		/// 선택된 주소 지역구 검색어 자동 저장
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public JsonResult DeviceRegionAutoSave(CODE_DATA Cond)
		{
			RTN_SAVE_DATA data = Service.deviceService.DeviceRegionAutoSave(Cond, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}

		[AltloggalAuthorization]
		private void SetDeviceListAuth(ref DEVICE_LIST_COND Cond)
		{
			Cond.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH.COMPANY_CODE;
			Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH.STORE_CODE;
			Cond.CONTACT_DEPT_SEARCH = SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH.DEPT_SEARCH;
			Cond.CONTACT_PARENT_MEMBER_CODE = SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH.PARENT_MEMBER_CODE;
			Cond.CONTACT_CODE = SessionHelper.LoginInfo.EMPLOYEE_SEARCH_AUTH.MEMBER_CODE;
		}

		[AltloggalAuthorization]
		public ActionResult DeviceExcelReport(DEVICE_LIST_COND Cond)
		{
			if (Cond.DISPLAY_MODE != "Total") this.SetDeviceListAuth(ref Cond);

			List<string> headers = new List<string>()
				{ "코드", "로컬박스명", "지점명", "영리구분", "사업구분", "담당자명", "담당자전화", "인증번호", "주소", "수정자", "수정시간"   };

			Cond.PAGE = 1;
			Cond.PAGE_COUNT = 100000;

			return Service.deviceService.GetDeviceList(Cond).Select(s => new
			{
				DEVICE_CODE = s.DEVICE_CODE
			   ,
				DEVICE_NAME = s.DEVICE_NAME
			   ,
				STORE_NAME = s.STORE_NAME
			   ,
				BUSI_TYPE_NAME = s.BUSI_TYPE_NAME
			   ,
				BUSI_TYPE_NAME2 = s.BUSI_TYPE_NAME2
			   ,
				CONTACT_NAME = s.CONTACT_NAME
			   ,
				CONTACT_PHONE = s.CONTACT_PHONE
			   ,
				AUTH_NUMBER = s.AUTH_NUMBER
			   ,
				ADDRESS = s.ADDRESS
			   ,
				UPDATE_NAME = s.UPDATE_NAME
			   ,
				UPDATE_DATE = s.UPDATE_DATE
			}).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
			;
		}





		[AltloggalAuthorization]
		public PartialViewResult PV_DeviceMainList(T_DEVICE_MAIN_COND Cond)
		{
			ViewBag.page = "/basic/devicereg3";
			ViewBag.list = Service.deviceService.GetDeviceMainList(Cond);
			return PartialView2();
		}

		[AltloggalAuthorization]
		public PartialViewResult PV_DeviceMainList2(T_DEVICE_MAIN_COND Cond)
		{
			ViewBag.page = "/basic/devicereg3";
			ViewBag.list = Service.deviceService.GetDeviceMainList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult DeviceMainSave(T_DEVICE_MAIN param)
		{
			RTN_SAVE_DATA data = Service.deviceService.DeviceMainSave(param, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult DeviceMainDelete(T_DEVICE_MAIN param)
		{
			param.UPDATE_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA data = Service.deviceService.DeviceMainDelete(param);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult DeviceMainList(T_DEVICE_MAIN_COND Cond)
		{
			var list = Service.deviceService.GetDeviceMainList(Cond);
			return new JsonResult { Data = list };
		}

		[AltloggalAuthorization]
		public JsonResult loggalBoxAuthReg(T_DEVICE_COND param)
		{
			RTN_SAVE_DATA data = Service.deviceService.loggalBoxAuthReg(param, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public ActionResult deviceMainDetail(T_DEVICE_MAIN_COND Cond)
		{
			ViewBag.data = Service.deviceService.GetDeviceMainList(Cond).First();
			return View();
		}
		[AltloggalAuthorization]
		#region >> 광고 및 로컬박스메인 상세보기 공유 

		/// <summary>
		/// 공유된리스트
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public PartialViewResult PV_AD_Favorite(T_AD_FAVORITE_COND Cond)
		{
			ViewBag.list = new AdvertisingService().GetAdFavoriteList(Cond);
			return PartialView2();
		}

		/// <summary>
		/// SNS 전화번호 클릭시 저장
		/// </summary>
		/// <param name="Param"></param>
		/// <returns></returns>
		public JsonResult AdFavoriteSave(T_AD_FAVORITE Param)
		{
			Param.USER_IP = Request.UserHostAddress;
			RTN_SAVE_DATA data = new AdvertisingService().AdFavoriteSave(Param);

			return new JsonResult { Data = data };
		}
		#endregion
		[AltloggalAuthorization]
		public ActionResult AdDevice()
		{
			return View();
		}
		[AltloggalAuthorization]

		public PartialViewResult pv_AdDevicelist(DEVICE_LIST_COND Cond)
		{
			if (SessionHelper.LoginInfo.STORE_CODE != 1 || !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
			{
				Cond.COMPANY_CODE = SessionHelper.LoginInfo.COMPANY_CODE;
			}
			if (SessionHelper.LoginInfo.STORE_CODE != 1 && !(SessionHelper.LoginInfo.STORE.STORE_TYPE == 1 || SessionHelper.LoginInfo.STORE.STORE_TYPE == 4))
			{
				Cond.STORE_CODE = SessionHelper.LoginInfo.STORE_CODE;
			}



			if (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH != null)
			{
				switch (SessionHelper.LoginInfo.EMPLOYEE.EMP_AUTH)
				{
					case 2: /*상위부서권한*/
						Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
						Cond.CONTACT_DEPT_SEARCH = SessionHelper.LoginInfo.EMPLOYEE.DEPT_SEARCH;
						break;
					case 3:/*부서권한*/
						Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
						Cond.CONTACT_DEPT_CODE = SessionHelper.LoginInfo.EMPLOYEE.DEPT_CODE;
						break;
					case 8:/*상급자권한*/
						Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
						Cond.CONTACT_PARENT_MEMBER_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
						break;
					case 9:/*본인권한*/
						Cond.STORE_CODE = SessionHelper.LoginInfo.EMPLOYEE.STORE_CODE;
						Cond.CONTACT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
						break;
				}
			}

			ViewBag.list = Service.deviceService.GetDeviceList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_AdDevicelist2(LOGGAL_BOX_COND2 Cond, int? SELECT_TYPE)
		{
			ViewBag.cond = Cond;
			Cond.HOST = GlobalMvc.Host;
			Cond.STATUS = 9;

			if(1 == SELECT_TYPE){
				ViewBag.list = new loggalServiceBiz.LoggalBoxService().GetMyLoggalBoxList(Cond);
			}
			else
			{
				ViewBag.list = new loggalServiceBiz.LoggalBoxService().GetLoggalBoxList2(Cond);
			}

			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult AdDeviceSave(AdDeviceSaveData saveData)
		{
			RTN_SAVE_DATA rtn = new AdvertisingService().T_AD_DEVICE_Save(saveData, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = rtn };
		}
		[AltloggalAuthorization]
		public JsonResult DevcieAdInfoSave(AdDeviceSaveData saveData)
		{
			RTN_SAVE_DATA rtn = new AdvertisingService().DevcieAdInfoSave(saveData, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = rtn };
		}
		[AltloggalAuthorization]
		public JsonResult AdDeviceSeqSave(List<LOGGAL_BOX_DATA2> list)
		{
			RTN_SAVE_DATA data = new AdvertisingService().AdDeviceSeqSave(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}

		[AltloggalAuthorization]
		public JsonResult AdToDeviceShare(List<AD_DEVICE_SHARE> list)
		{
			RTN_SAVE_DATA rtn = new AdvertisingService().AdToDeviceShare(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = rtn };
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_RelativeDevice(DEVICE_LIST_COND Cond)
		{
			ViewBag.list = new DeviceService().GetRelativeDeviceList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult RelativeDeviceDelete(List<T_DEVICE> list, long? RELATIVE_DEVICE_CODE)
		{
			RTN_SAVE_DATA data = Service.deviceService.RelativeDeviceDelete(list, RELATIVE_DEVICE_CODE);
			return new JsonResult { Data = data };
		}

		#region >> 지역 카테고리 & 검색어 저장
		[AltloggalAuthorization]
		public ActionResult CategoryRegList()
		{
			return View();
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_CategoryRegList(CATEGORY_PAGE_COND Cond)
		{

			ViewBag.list = new CategoryService().GetCategoryPageList(Cond);
			ViewBag.TABLE_NAME = Cond.TABLE_NAME;
			return PartialView2();
		}

		/// <summary>
		/// 카테고리별 검색어
		/// </summary>
		/// <param name="name"></param>
		/// <param name="Param"></param>
		/// <param name="selectedValue"></param>
		/// <param name="optionLabel"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public PartialViewResult CategoryKeywordCombo(string name, CATEGORY_KEYWORD_COND Cond, string selectedValue, string optionLabel, string htmlAttributes)
		{
			List<SelectListItem> combolist = new List<SelectListItem>();
			IList<CODE_DATA> list = new KeywordService().GetCategoryKeywordList2(Cond);
			if (list == null)
			{
				list = new List<CODE_DATA>();
			}
			DROPDOWN_COND data = new DROPDOWN_COND
			{
				name = name
				,
				selectList = list.Select(s => new SelectListItem { Value = s.CODE.ToString(), Text = s.NAME, Selected = true }).ToList()
				,
				optionLabel = optionLabel
				,
				htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "", @multiple = "" })
			};

			return PartialCombo(data);
		}

		/// <summary>
		/// 검색어 동의어
		/// </summary>
		/// <param name="name"></param>
		/// <param name="Param"></param>
		/// <param name="selectedValue"></param>
		/// <param name="optionLabel"></param>
		/// <param name="htmlAttributes"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public PartialViewResult KeywordSynonymCombo(string name, T_KEYWORD_COND Cond, string selectedValue, string optionLabel, string htmlAttributes)
		{
			List<SelectListItem> combolist = new List<SelectListItem>();
			Cond.PAGE = 1;
			Cond.PAGE_COUNT = 100;
			Cond.IS_SYNONYM = true;
			IList<T_KEYWORD> list = new KeywordService().GetKeywordPageList(Cond);
			if (list == null)
			{
				list = new List<T_KEYWORD>();
			}
			DROPDOWN_COND data = new DROPDOWN_COND
			{
				name = name
				,
				selectList = list.Select(s => new SelectListItem { Value = s.KEYWORD_CODE.ToString(), Text = s.KEYWORD_NAME, Selected = true }).ToList()
				,
				optionLabel = optionLabel
				,
				htmlAttributes = JsonConvert.DeserializeAnonymousType(htmlAttributes, new { @class = "", @style = "", @placeholder = "", @readonly = "", @multiple = "" })
			};

			return PartialCombo(data);
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_CategoryRegList2(T_KEYWORD_COND Cond)
		{
			ViewBag.TABLE_NAME = Cond.TABLE_NAME;
			ViewBag.list = new KeywordService().GetKeywordPageList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult CategorySave(List<T_CATEGORY> list)
		{
			RTN_SAVE_DATA rtnData = new CategoryService().CategorySave(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = rtnData };
		}

		/// <summary>
		/// 동의어 저장
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public JsonResult KEYWORD_Synonym_Save(KEYWORD_SYNONYM_SAVE saveData)
		{
			RTN_SAVE_DATA rtnData = new KeywordService().KEYWORD_Synonym_Save(saveData);
			return new JsonResult { Data = rtnData };
		}
		[AltloggalAuthorization]
		public JsonResult KeywordSave(List<T_KEYWORD> list)
		{
			RTN_SAVE_DATA rtnData = new KeywordService().T_KEYWORD_Save(list, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = rtnData };
		}
		[AltloggalAuthorization]
		public JsonResult CategoryKeywordSave(CATEGORY_KEYWORD_SAVE Param)
		{
			Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtnData = new KeywordService().CategoryKeywordSave(Param);
			return new JsonResult { Data = rtnData };
		}

		#endregion
		[AltloggalAuthorization]
		public JsonResult DeviceMainSeqChange(T_DEVICE_MAIN_SEQ_CHANGE Param)
		{
			Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtnData = new DeviceService().DeviceMainSeqChange(Param);
			return new JsonResult { Data = rtnData };
		}
		[AltloggalAuthorization]
		public ViewResult CategoryRegionRegList()
		{
			return View();
		}
		[AltloggalAuthorization]
		public ViewResult CategoryHotPlaceRegList()
		{
			return View();
		}


		#region >> 로컬박스 장소
		[AltloggalAuthorization]
		public PartialViewResult PV_DevicePlaceList(T_DEVICE_PLACE_COND Cond)
		{
			ViewBag.list = new AdvertisingService().GetDevicePlaceList(Cond);
			return PartialView2();
		}


		/// <summary>                                                                                      
		/// T_DEVICE_PLACE 저장하기(광고장소 - T_AD_PLACE 저장 -  saveparam Query)										  
		/// </summary>																					  
		/// <param name="list"></param>																	  
		/// <returns></returns>									
		[AltloggalAuthorization]
		public JsonResult DeviceplaceSave(List<T_DEVICE_PLACE> list, int? DEVICE_CODE = 0)
		{
			RTN_SAVE_DATA data = new AdvertisingService().DevicePlaceSave(list, DEVICE_CODE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}
		#endregion
		[AltloggalAuthorization]
		public ActionResult DeviceLocation()
		{
			return View();
		}
		[AltloggalAuthorization]
		public ActionResult deviceMainShareReq()
		{
			return View();
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_DeviceMainShareList(DEVICE_MAIN_SHARE_COND Cond)
		{
			ViewBag.list = Service.deviceService.GetDeviceMainShareList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_DeviceMainDeviceList(DEVICE_LIST_COND Cond)
		{
			ViewBag.list = Service.deviceService.GetDeviceList(Cond);
			return PartialView2("~/Views/basic/partial/pv_AdDevicelist.cshtml");
		}
		/// <summary>
		/// 로컬박스메인공유 저장
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public JsonResult DeviceMainShareReqSave(DEVICE_MAIN_SHARE_REQ_SAVE Param)
		{
			Param.REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtnData = new DeviceService().DeviceMainShareReqSave(Param);
			return new JsonResult { Data = rtnData };
		}

		#region >> 로컬박스메인 공유 승인
		[AltloggalAuthorization]
		public ActionResult DeviceMainShareApprovalList(DEVICE_MAIN_SHARE_COND Cond)
		{
			ViewBag.Cond = Cond;


			return View();
		}
		[AltloggalAuthorization]
		public PartialViewResult PV_DeviceMainShareApprovalList(DEVICE_MAIN_SHARE_COND Cond)
		{
			if (SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE != 1)
			{
				if (Cond.GUBUN == "2") Cond.MNG_COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				else Cond.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
			}
			else
			{
				Cond.GUBUN = "";
			}
			ViewBag.list = new DeviceService().GetDeviceMainShareApprovalList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public ActionResult DeviceMainShareRequestList(DEVICE_MAIN_SHARE_COND Cond)
		{
			ViewBag.Cond = Cond;
			return View();
		}

		[AltloggalAuthorization]
		public PartialViewResult PV_DeviceMainShareRequestList(DEVICE_MAIN_SHARE_COND Cond)
		{
			if (SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE != 1)
			{
				if (Cond.GUBUN == "")
					Cond.SHARE_COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				else if (Cond.GUBUN == "1")
					Cond.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
				else
					Cond.MNG_COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
			}

			ViewBag.list = new DeviceService().GetDeviceMainShareApprovalList(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult DeviceMainApprovalSave(DEVICE_MAIN_SHARE_APPROVAL_SAVE Param)
		{
			Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtnData = new DeviceService().DeviceMainShareApprovalSave(Param);
			return new JsonResult { Data = rtnData };
		}

		[AltloggalAuthorization]
		public JsonResult DeviceMainApprovalCancel(DEVICE_MAIN_SHARE_APPROVAL_SAVE Param)
		{
			Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtnData = new DeviceService().DeviceMainApprovalCancel(Param);
			return new JsonResult { Data = rtnData };
		}
		#endregion

		#region >> 로컬박스 내배너 공유 리스트
		[AltloggalAuthorization]
		public ActionResult DeviceMainShareTotalList()
		{
			ViewBag.addColumns = new BaseService().GetCommon(new T_COMMON_COND { MAIN_CODE = "A009", REF_DATA1 = "Y", HIDE = false }).ToList();
			return View();
		}
		[AltloggalAuthorization]
		public PartialViewResult PV_DeviceMainShareTotalList(DEVICE_MAIN_SHARE_TOTAL_COND Cond)
		{
			var commonList = new BaseService().GetCommon(new T_COMMON_COND { MAIN_CODE = "A009", REF_DATA1 = "Y", HIDE = false }).ToList();
			ViewBag.data = Cond;
			ViewBag.commonList = commonList;

			if (SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE != 1)
			{
				Cond.COMPANY_CODE = SessionHelper.LoginInfo.EMPLOYEE.COMPANY_CODE;
			}
			else
			{
				Cond.GUBUN = "2";
			}

			ViewBag.list = new DeviceService().GetDeviceMainShareTotalList(Cond, commonList);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult deviceMainShareSave(DEVICE_MAIN_SHARE_SAVE Param)
		{
			Param.REG_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtnData = new DeviceService().deviceMainShareSave(Param);
			return new JsonResult { Data = rtnData };
		}
		#endregion

		#region >> 로컬박스 스테이션리스트
		[AltloggalAuthorization]
		public ActionResult deviceStationList()
		{
			return View();
		}
		[AltloggalAuthorization]
		public PartialViewResult PV_deviceStationList(T_DEVICE_STATION_COND Cond)
		{
			ViewBag.list = new DeviceService().GetDeviceStationList(Cond);
			ViewBag.Cond = Cond;
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult GetDeviceStationList(T_DEVICE_STATION_COND Cond)
		{
			var list = new DeviceService().GetDeviceStationList(Cond);
			return new JsonResult { Data = list };
		}

		[AltloggalAuthorization]
		public ActionResult deviceStationReg(int? id = 0)
		{
			ViewBag.data = new DeviceService().GetDeviceStationList(new T_DEVICE_STATION_COND { STATION_CODE = id }).FirstOrDefault();
			return View();
		}
		[AltloggalAuthorization]
		public JsonResult deviceStationSave(T_DEVICE_STATION data)
		{
			RTN_SAVE_DATA rtn = new DeviceService().DeviceStationSave(data);

			return new JsonResult { Data = rtn };
		}
		[AltloggalAuthorization]
		public JsonResult deviceStationCodeUpdate(DEVICE_STATION_UPDATE param)
		{
			param.UPDATE_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA rtn = new DeviceService().deviceStationCodeUpdate(param);
			return new JsonResult { Data = rtn };
		}

		/// <summary>
		/// 로컬박스정보 맵정보 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public JsonResult GetDeviceStationMapList(T_DEVICE_STATION_COND Cond)
		{

			return new JsonResult { Data = Service.deviceService.GetDeviceStationList(Cond).Where(w => w.LATITUDE > 30 && w.LONGITUDE > 120).Select(s => new DAUM_MAPLIST { EnMapType = enMapType.Station, ACTIVE_YN = true, TITLE = s.STATION_NAME, SUB_TITLE = s.DEVICE_CNT.ToString() + "(" + s.NEW_DEVICE_CNT.ToString() + ")", CONTENT = s.STATION_DESC, LATITUDE = s.LATITUDE, LONGITUDE = s.LONGITUDE, LINK_URL = "/basic/devicestationreg/" + s.STATION_CODE.ToString() }).OrderBy(o => o.ACTIVE_YN).ToList() };
		}
		[AltloggalAuthorization]
		public PartialViewResult PV_DeviceStationPlaceList(T_DEVICE_STATION_PLACE_COND Cond)
		{
			ViewBag.list = new DeviceService().GetDeviceStationPlaceList(Cond);
			return PartialView2();
		}
		/// <summary>                                                                                      
		/// T_DEVICE_STATION_PLACE 저장하기(광고장소 - T_DEVICE_STATION_PLACE 저장 -  saveparam Query)										  
		/// </summary>																					  
		/// <param name="list"></param>																	  
		/// <returns></returns>									
		[AltloggalAuthorization]
		public JsonResult DeviceStationPlaceSave(List<T_DEVICE_STATION_PLACE> list, int? STATION_CODE = 0, bool? IS_RANGE = true)
		{
			RTN_SAVE_DATA data = new DeviceService().DeviceStationPlaceSave(list, STATION_CODE, IS_RANGE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public ActionResult DeviceStationExcelReport(T_DEVICE_STATION_COND Cond)
		{
			List<string> headers = new List<string>()
			{ "사용여부", "스테이션명", "주소", "우편번호", "위도", "경도", "설명", "박수갯수", "수정자", "수정시간"};

			Cond.PAGE = 1;
			Cond.PAGE_COUNT = 100000;

			return Service.deviceService.GetDeviceStationList(Cond).Select(s => new
			{
				HIDE = s.HIDE == true ? "미사용" : "사용"
			   ,
				STATION_NAME = s.STATION_NAME
			   ,
				ADDRESS = s.ADDRESS1.ToString("") + " " + s.ADDRESS2.ToString("")
			   ,
				ZIP_CODE = s.ZIP_CODE
			   ,
				LATITUDE = s.LATITUDE
			   ,
				LONGITUDE = s.LONGITUDE
			   ,
				STATION_DESC = s.STATION_DESC
			   ,
				DEVICE_CNT = s.DEVICE_CNT
			   ,
				UPDATE_NAME = s.UPDATE_NAME
			   ,
				UPDATE_DATE = s.UPDATE_DATE
			}).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
			;
		}
		#endregion
		[AltloggalAuthorization]
		public PartialViewResult pv_AdDeviceShareList(AD_DEVICE_SHARE_COND Cond)
		{
			ViewBag.list = new DeviceService().GetAdDeviceShareList(Cond);
			ViewBag.Cond = Cond;
			return PartialView2();
		}

		#region >> 장소상품 그룹 등록
		[AltloggalAuthorization]
		public ActionResult PlaceItemGroupRegList()
		{
			return View();
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_PlaceItemGroupRegList(T_PLACE_ITEM_GROUP_COND Cond)
		{
			ViewBag.list = new CategoryService().GetPlaceItemGroupPageList(Cond);
			return PartialView2();
		}
		#endregion
		[AltloggalAuthorization]
		public JsonResult PlaceItemGroupSave(List<T_PLACE_ITEM_GROUP> list)
		{
			int? REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA data = new CategoryService().PlaceItemGroupSave(list, REG_CODE);

			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult PlaceItemGroupSeqSave(List<T_PLACE_ITEM_GROUP> list)
		{
			int? REG_CODE = (int)SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
			RTN_SAVE_DATA data = new CategoryService().PlaceItemGroupSeqSave(list, REG_CODE);

			return new JsonResult { Data = data };
		}
	}

}