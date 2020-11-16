using loggalServiceBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ALT.VO.loggal;
using ALT.Framework.Mvc;
using loggalWebMng.CommonCS;
using ALT.Framework.Data;
using ALT.VO.Common;
using ALT.Framework.Mvc.Helpers;
using ALT.BizService;

namespace loggalWebMng.Controllers
{
    public class DeviceController : BaseController
    {
	/// <summary>
	/// 이전 로컬박스뷰(2019.11.26일이전)
	/// </summary>
	/// <param name="id"></param>
	/// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult localboxView2(long id)
        {
            var list = new LoggalBoxService().GetLoggalBoxList(new LOGGAL_BOX_COND { DEVICE_CODE = id, bWorkingTime = true, HOST = GlobalMvc.Host });

            List<LOGGAL_BOX_INFO> groupList = list.Where(w => w.GROUP_SEQ != null).GroupBy(g => new { g.GROUP_SEQ, g.GROUP_NAME, g.CONTENT_TYPE, g.AD_FRAME_TYPE, g.PLAY_TIME }).Select(s => new LOGGAL_BOX_INFO { GROUP_SEQ = s.Key.GROUP_SEQ, CATEGORY_NAME = s.Key.GROUP_NAME, CONTENT_TYPE = s.Key.CONTENT_TYPE, FRAME_TYPE = s.Key.AD_FRAME_TYPE, PLAY_TIME = s.Key.PLAY_TIME }).ToList();


            for (int i = 0; i < groupList.Count(); i++)
            {
                groupList[i].list = list.Where(w => groupList[i].GROUP_SEQ != null && w.GROUP_SEQ == groupList[i].GROUP_SEQ).Select(s => new LOGGAL_BOX_INFO_DETAIL { AD_CODE = s.AD_CODE, LOGO_URL = s.LOGO_URL, BANNER_IMAGE = s.BANNER_IMAGE, CONTENT_URL = s.CONTENT_URL, BANNER_TYPE = s.BANNER_TYPE, TITLE = s.TITLE, SUB_TITLE = s.SUB_TITLE, COMPANY_NAME = s.COMPANY_NAME, BOOKMARK_CNT = s.BOOKMARK_CNT, CLICK_CNT = s.CLICK_CNT }).ToList();
            }


            IList<LOGGAL_BOX_DATA> mobileBannerlist = list.Where(w => w.GROUP_SEQ == null).ToList();

            List<LOGGAL_BOX_INFO> BannerGroupList = new List<LOGGAL_BOX_INFO>();

            ViewBag.commonlist = new CommonService().GetCommon(new T_COMMON_COND() { ADD_COND = "AND MAIN_CODE IN ('A010','B008','H002','L003','P004')" }).Select(s => new { s.MAIN_CODE, s.SUB_CODE, s.NAME });

            if (mobileBannerlist.Count() > 0)
            {

                int nFrameCnt = (int)(mobileBannerlist.First().AD_FRAME_TYPE ?? 6);
                nFrameCnt = nFrameCnt > 2 ? nFrameCnt + 2 : nFrameCnt;

                var bannergroupList = mobileBannerlist.Select(s => new {
                    s.CATEGORY_CODE
                                , s.CATEGORY_NAME
                                , s.CONTENT_TYPE, s.AD_FRAME_TYPE, s.PLAY_TIME }).Distinct().ToList();
                int nGroupSeq = 1;
                foreach (var data in bannergroupList)
                {
                    int nCnt = 0;


                    var rowList = mobileBannerlist.Where(w => w.CATEGORY_NAME == data.CATEGORY_NAME && w.CONTENT_TYPE == data.CONTENT_TYPE).ToList();
                    int nTot = rowList.Count();

                    while (true)
                    {

                        LOGGAL_BOX_INFO rowData = new LOGGAL_BOX_INFO { GROUP_SEQ = nGroupSeq
                                                                        , CATEGORY_NAME = data.CATEGORY_NAME
                                                                        , CONTENT_TYPE = data.CONTENT_TYPE
                                                                        , FRAME_TYPE = data.AD_FRAME_TYPE
                                                                        , PLAY_TIME = data.PLAY_TIME
                        };

                        //rowData.list = rowList.Skip(nCnt * FRAME_TYPE).Take(FRAME_TYPE).Select(s => new LOGGAL_BOX_INFO_DETAIL { BANNER_TYPE = s.BANNER_TYPE, TITLE = s.TITLE, SUB_TITLE = s.SUB_TITLE, AD_CODE = s.AD_CODE, LOGO_URL = s.LOGO_URL, COMPANY_NAME = s.COMPANY_NAME + " " + (s.DISTANCE == 0 ? "" : Convert.ToDouble((double)s.DISTANCE / (double)1000.00).ToString("#,##0.00") + "km"), CONTENT_URL = s.CONTENT_URL }).ToList();
                        rowData.list = rowList.Skip(nCnt * nFrameCnt).Take(nFrameCnt).Select(s => new LOGGAL_BOX_INFO_DETAIL { BANNER_TYPE = s.BANNER_TYPE, TITLE = s.TITLE, SUB_TITLE = s.SUB_TITLE, AD_CODE = s.AD_CODE, LOGO_URL = s.LOGO_URL, BANNER_IMAGE = s.BANNER_IMAGE,  COMPANY_NAME = s.COMPANY_NAME, CONTENT_URL = s.CONTENT_URL, CLICK_CNT = s.CLICK_CNT, BOOKMARK_CNT = s.BOOKMARK_CNT, FAVORITE_CNT = s.FAVORITE_CNT }).ToList();

                        BannerGroupList.Add(rowData);
                        nCnt++;
                        rowData.SEQ = nCnt;
                        nGroupSeq++;
                        if (nTot <= nCnt * nFrameCnt)
                        {
                            break;
                        }
                    }
                }
            }

            T_DEVICE device = new DeviceService().GetDeviceList(new T_DEVICE_COND { DEVICE_CODE = id }).FirstOrDefault();
            ViewBag.device = device;
            ViewBag.GroupList = groupList;
            ViewBag.BannerGroupList = BannerGroupList;

            string viewName = "/Views/device/partial/localboxleftMenu.cshtml";
            ViewBag.leftMenu = GlobalMvc.Common.RenderPartialViewToString(this, viewName, new { });

            SessionHelper.PAGE_WAITING_TIME = device.PAGE_WAITING_TIME.ToString("60");

            return View();
        }
		/// <summary>
		/// 이전 로컬박스뷰(2019.11.26일이후)
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[AltloggalAuthorization]
		public ActionResult localboxView(long id)
		{


			var list = new LoggalBoxService().GetLoggalBoxList2(new LOGGAL_BOX_COND2 { GUBUN = 1, DEVICE_CODE = id, bWorkingTime = true, HOST = GlobalMvc.Host, STATUS=9 });

			List<LOGGAL_BOX_INFO> groupList = list.Where(w => w.CATEGORY_CODE == 6336).GroupBy(g => new {g.GROUP_SEQ, g.CATEGORY_CODE, g.CATEGORY_NAME, g.BANNER_TYPE2, g.AD_FRAME_TYPE, g.AD_CYCLE_TIME }).Select(s => new LOGGAL_BOX_INFO { GROUP_SEQ = s.Key.GROUP_SEQ, CATEGORY_CODE = s.Key.CATEGORY_CODE, CATEGORY_NAME = (s.Key.AD_FRAME_TYPE == 1 ? s.Max(s1=>s1.TITLE) : s.Key.CATEGORY_NAME), BANNER_TYPE2 = s.Key.BANNER_TYPE2, FRAME_TYPE = s.Key.AD_FRAME_TYPE, PLAY_TIME = s.Key.AD_CYCLE_TIME }).ToList();

			if (groupList.Count() > 0)
			{
				int nMyBannerGroup = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(groupList.Count()) / Convert.ToDecimal(groupList.First().FRAME_TYPE)));

				for (int i = 0; i < groupList.Count(); i++)
				{
					groupList[i].list = list.Where(w => groupList[i].GROUP_SEQ == w.GROUP_SEQ && w.CATEGORY_CODE == groupList[i].CATEGORY_CODE).Select(s => new LOGGAL_BOX_INFO_DETAIL { AD_CODE = s.AD_CODE, LOGO_URL = s.LOGO_URL, BANNER_IMAGE = s.BANNER_IMAGE, CONTENT_URL = s.CONTENT_URL, BANNER_TYPE = s.BANNER_TYPE, TITLE = s.TITLE, SUB_TITLE = s.SUB_TITLE, COMPANY_NAME = s.STORE_NAME, BOOKMARK_CNT = s.BOOKMARK_CNT, CLICK_CNT = s.CLICK_CNT }).ToList();
				}
			}


			IList<LOGGAL_BOX_DATA2> mobileBannerlist = list.Where(w => w.CATEGORY_CODE != 6336).ToList();

			List<LOGGAL_BOX_INFO> BannerGroupList = new List<LOGGAL_BOX_INFO>();

			ViewBag.commonlist = new CommonService().GetCommon(new T_COMMON_COND() { ADD_COND = "AND MAIN_CODE IN ('A010','B008','H002','L003','P004')" }).Select(s => new { s.MAIN_CODE, s.SUB_CODE, s.NAME });

			if (mobileBannerlist.Count() > 0)
			{

				int nFrameCnt = mobileBannerlist.First().AD_FRAME_TYPE;
				nFrameCnt = nFrameCnt > 2 ? nFrameCnt + 2 : nFrameCnt;

				var bannergroupList = mobileBannerlist.Select(s => new {
					CATEGORY_CODE = s.CATEGORY_CODE
				   ,CATEGORY_NAME = s.CATEGORY_NAME
				   ,BANNER_TYPE2 = s.BANNER_TYPE2
				   ,AD_FRAME_TYPE = s.AD_FRAME_TYPE
				   ,PLAY_TIME = s.AD_CYCLE_TIME
				}).Distinct().ToList();
				int nGroupSeq = 1;
				foreach (var data in bannergroupList)
				{
					int nCnt = 0;


					var rowList = mobileBannerlist.Where(w => w.CATEGORY_NAME == data.CATEGORY_NAME && w.BANNER_TYPE2 == data.BANNER_TYPE2).ToList();
					int nTot = rowList.Count();

					while (true)
					{

						LOGGAL_BOX_INFO rowData = new LOGGAL_BOX_INFO
						{
							GROUP_SEQ = nGroupSeq,
							CATEGORY_NAME = data.CATEGORY_NAME,
							BANNER_TYPE2 = data.BANNER_TYPE2,
							FRAME_TYPE = data.AD_FRAME_TYPE,
							PLAY_TIME = data.PLAY_TIME
						};

						//rowData.list = rowList.Skip(nCnt * FRAME_TYPE).Take(FRAME_TYPE).Select(s => new LOGGAL_BOX_INFO_DETAIL { BANNER_TYPE = s.BANNER_TYPE, TITLE = s.TITLE, SUB_TITLE = s.SUB_TITLE, AD_CODE = s.AD_CODE, LOGO_URL = s.LOGO_URL, COMPANY_NAME = s.COMPANY_NAME + " " + (s.DISTANCE == 0 ? "" : Convert.ToDouble((double)s.DISTANCE / (double)1000.00).ToString("#,##0.00") + "km"), CONTENT_URL = s.CONTENT_URL }).ToList();
						rowData.list = rowList.Skip(nCnt * nFrameCnt).Take(nFrameCnt).Select(s => new LOGGAL_BOX_INFO_DETAIL { BANNER_TYPE = s.BANNER_TYPE, TITLE = s.TITLE, SUB_TITLE = s.SUB_TITLE, AD_CODE = s.AD_CODE, LOGO_URL = s.LOGO_URL, BANNER_IMAGE = s.BANNER_IMAGE, COMPANY_NAME = s.STORE_NAME, CONTENT_URL = s.CONTENT_URL, CLICK_CNT = s.CLICK_CNT, BOOKMARK_CNT = s.BOOKMARK_CNT, FAVORITE_CNT = s.FAVORITE_CNT }).ToList();

						BannerGroupList.Add(rowData);
						nCnt++;
						rowData.SEQ = nCnt;
						nGroupSeq++;
						if (nTot <= nCnt * nFrameCnt)
						{
							break;
						}
					}
				}
			}


			var devicedata = list.First();
			ViewBag.device = list.Count()> 0 ? new T_DEVICE() {
				DEVICE_CODE = devicedata.DEVICE_CODE
			  , DEVICE_NAME = devicedata.DEVICE_NAME
			  , AD_CYCLE_TIME =	devicedata.AD_CYCLE_TIME
			  , DATA_CYCLE_TIME = devicedata.DATA_CYCLE_TIME
			  , AD_FRAME_TYPE = devicedata.AD_FRAME_TYPE
			} : new T_DEVICE();
			ViewBag.GroupList = groupList;
			ViewBag.BannerGroupList = BannerGroupList;

			string viewName = "/Views/device/partial/localboxleftMenu.cshtml";
			ViewBag.leftMenu = GlobalMvc.Common.RenderPartialViewToString(this, viewName, new { });

			SessionHelper.PAGE_WAITING_TIME = devicedata.PAGE_WAITING_TIME.ToString("60");

			return View();
		}

		#region >> 하드웨어 정보
		[AltloggalAuthorization]
        public ActionResult HardwareList()
        {
            return View();
        }
        [AltloggalAuthorization]
        public ActionResult HardwareReg(int? id)
        {
            id = id == null ? -1 : id;
            ViewBag.data = new HardwareService().GetHardwareList(new T_HARDWARE_COND { HARDWARE_CODE = id }).FirstOrDefault();

            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult PV_HardwareList(T_HARDWARE_COND Cond)
        {
            List<T_HARDWARE> rtn = new HardwareService().GetHardwareList(Cond);
            ViewBag.list = rtn;
            ViewBag.Cond = Cond;
            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult HardwareSave(T_HARDWARE Param)
        {

            Param.INSERT_CODE = SessionHelper.LoginInfo.MEMBER.MEMBER_CODE;
            RTN_SAVE_DATA rtn = new HardwareService().HardwareSave(Param);
            return new JsonResult() { Data = rtn };
        }

        [AltloggalAuthorization]
        public ActionResult HardwareExcelReport(T_HARDWARE_COND Cond)
        {
            List<string> headers = new List<string>()
            { "번호", "이름", "브랜드", "해상도", "구매일자", "모델명", "설명", "사용여부", "비고", "등록자", "등록시간", "수정자", "수정시간", "로컬박스명"   };


            Cond.PAGE = 1; Cond.PAGE_COUNT = 100000;


            return new HardwareService().GetHardwareList(Cond).Select(s => new {
                HARDWARE_CODE = s.HARDWARE_CODE
                     , HARDWARE_NAME = s.HARDWARE_NAME
                     , BRAND_NAME = s.BRAND_NAME
                     , DISPLAY_RESOLUTION_NAME = s.DISPLAY_RESOLUTION_NAME
                     , PURCHASE_DATE = s.PURCHASE_DATE.ToFormatDate()
                     , MODEL_NAME = s.MODEL_NAME
                     , HARDWARE_DESC = s.HARDWARE_DESC
                     , HIDE_NAME = s.HIDE_NAME
                     , REMARK = s.REMARK
                     , INSERT_NAME = s.INSERT_NAME
                     , INSERT_DATE = s.INSERT_DATE
                     , UPDATE_NAME = s.UPDATE_NAME
                     , UPDATE_DATE = s.UPDATE_DATE
                     , DEVICE_NAME = s.DEVICE_NAME
            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);

        }
       
        #endregion

        #region >> 재생로그 관련
        [AltloggalAuthorization]
        public ActionResult adplayloglist()
        {
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_AdplaylogList(T_AD_PLAY_LOG_COND Cond)
        {
            ViewBag.list = new loggalServiceBiz.AdvertisingService().GetAdPlayLogList(Cond);
            return PartialView2();
        }
        [AltloggalAuthorization]
        public JsonResult adPlaylogSave(T_AD_PLAY_LOG_MONGO param, string AD_CODES, string AD_TITLES)
        {
            //logger.Debug("adPlaylogSave after =>" + base.GetParameter());


            //RTN_SAVE_DATA data = new AdvertisingService().AdPlayLogSave(param);
            RTN_SAVE_DATA data = new MongoDBService().AdPlayLogSave(param, AD_CODES, AD_TITLES);
            //logger.Debug("adPlaylogSave after =>" + base.GetParameter());
            return new JsonResult { Data = data };
        }



        [AltloggalAuthorization]
        public ActionResult adplaylogExcelReport(T_AD_PLAY_LOG_COND Cond)
        {
            List<string> headers = new List<string>()
            { "구분", "일자", "제목", "로컬박스명", "재생유형", "재생시간", "등록자", "비고"};


            Cond.PAGE = 1; Cond.PAGE_COUNT = 100000;


            return new AdvertisingService().GetAdPlayLogList(Cond).Select(s => new {
                DEVICE_KIND_NAME = s.DEVICE_KIND_NAME
                     , REG_DATE = s.REG_DATE.ToDefaultDateString()
                     , TITLE = s.TITLE
                     , DEVICE_NAME = s.DEVICE_NAME
                     , BANNER_TYPE2_NAME = s.BANNER_TYPE2_NAME
                     , PLAY_TIME = s.PLAY_TIME
                     , INSERT_NAME = s.INSERT_NAME
                     , REMARK = s.REMARK

            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);

        }

        #region 몽고디비용 광고재생로그
        [AltloggalAuthorization]
        public ActionResult adplaylogMongolist(T_AD_PLAY_LOG_COND Cond)
        {
            ViewBag.Cond = Cond;
            return View();
        }
        [AltloggalAuthorization]
        public PartialViewResult pv_AdplaylogMongoList(T_AD_PLAY_LOG_COND Cond)
        {
            ViewBag.data = new MongoDBService().getAdPlayLogList(Cond);
            ViewBag.cond = Cond;
            return PartialView2();
        }

        [AltloggalAuthorization]
        public ActionResult adplaylogMongoExcelReport(T_AD_PLAY_LOG_COND Cond)
        {
            List<string> headers = new List<string>()
            { "구분", "일자","광고코드", "제목","로컬박스코드", "로컬박스명","배너유형","프레임", "재생유형", "재생시간", "비고"};


            Cond.PAGE = 1; Cond.PAGE_COUNT = 1000000000;


            return new MongoDBService().getAdPlayLogList(Cond).list.Select(s => new {
                DEVICE_KIND_NAME = s.DEVICE_KIND_NAME
               , REG_DATE = s.REG_DATE
               , AD_CODE = s.AD_CODE
               , TITLE = s.TITLE
               , DEVICE_CODE = s.DEVICE_CODE
               , DEVICE_NAME = s.DEVICE_NAME
               , BANNER_TYPE2_NAME = s.BANNER_TYPE2_NAME
               , AD_FRAME_TYPE_NAME = s.FRAME_TYPE_NAME
               , PLAY_TIME = s.PLAY_TIME
               , REMARK = s.REMARK

            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
            
        }
        #endregion
        #endregion
        
        #region >> 일자별 배너재생 집계리스트
        /// <summary>
        /// 일자별 배너재생 집계리스트
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayDailyTotalList(AdPlayTotalCond Cond)
        {
            ViewBag.Cond = Cond;
            return View();
        }
        /// <summary>
        /// 일자별 배너재생 집계리스트
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult pv_AdPlayDailyTotalList(AdPlayTotalCond Cond)
        {
        
            ViewBag.list = new MongoDBService().GetAdPlayDailyTotalList(Cond);
            return PartialView2();
        }
        /// <summary>
        /// 일자별 배너재생 집계리스트 엑셀
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult adPlayDailyTotalExcelReport(AdPlayTotalCond Cond)
        {
            List<string> headers = new List<string>()
            { "순번", "일자","구분", "제목","배너유형","재생유형","프레임", "재생시간(초)", "건수", "광고주" };


            Cond.PAGE = 1; Cond.PAGE_COUNT = 1000000000;


            return new MongoDBService().GetAdPlayDailyTotalList(Cond).Select(s => new {
                ROW_IDX = s.ROW_IDX,
                REG_DAY = s.REG_DAY,
                DEVICE_KIND = s.DEVICE_KIND_NAME,
                TITLE = s.TITLE,
                BANNER_TYPE2_NAME = s.BANNER_TYPE2_NAME,
                FRAME_TYPE_NAME = s.FRAME_TYPE_NAME,
                SUB_PLAY_TIME = s.SUB_PLAY_TIME
               ,SUB_QTY = s.SUB_QTY
               ,STORE_NAME = s.STORE_NAME

            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);

        }
        #endregion

        #region >> 일자별로컬박스재생집계
        /// <summary>
        /// 일자별로컬박스재생집계
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayDailyLocalboxTotalList(AdPlayLocalboxTotalCond Cond)
        {
            ViewBag.Cond = Cond;
            return View();
        }
        /// <summary>
        /// 일자별로컬박스재너재생집계
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult pv_AdPlayDailyLocalboxTotalList(AdPlayLocalboxTotalCond Cond)
        {
            ViewBag.list = new MongoDBService().GetAdPlayDailyLocalboxTotalList(Cond);
            return PartialView2();
        }
        /// <summary>
        /// 일자별로컬박스재생집계 엑셀
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayDailyLocalboxTotalExcelReport(AdPlayLocalboxTotalCond Cond)
        {
            List<string> headers = new List<string>()
            { "순번", "일자","로컬박스", "구분", "이미지-1(초)","이미지-6(초)","이미지-12(초)","클릭(초)", "비디오(초)", "유튜브(초)", "건수", "총시간","로컬사업자"};


            Cond.PAGE = 1; Cond.PAGE_COUNT = 1000000000;

            

            return new MongoDBService().GetAdPlayDailyLocalboxTotalList(Cond).Select(s => new {
                ROW_IDX = s.ROW_IDX,
                REG_DAY = s.REG_DAY,
                DEVICE_NAME = s.DEVICE_NAME,
                BANNER_KIND_NAME = s.BANNER_KIND_NAME,
                IMAGE1_PLAY_TIME = s.IMAGE1_PLAY_TIME,
                IMAGE6_PLAY_TIME = s.IMAGE6_PLAY_TIME,
                IMAGE12_PLAY_TIME = s.IMAGE12_PLAY_TIME,
                CLICK_PLAY_TIME = s.CLICK_PLAY_TIME,
                VIDEO_PLAY_TIME = s.VIDEO_PLAY_TIME,
                YOUTUBE_PLAY_TIME = s.YOUTUBE_PLAY_TIME,
                TOT_QTY = s.TOT_QTY,
                TOT_PLAY_TIME = s.TOT_PLAY_TIME,
                STORE_NAME = s.STORE_NAME

            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }
        #endregion 

        #region >> 광고주별 배너 재생 집계리스트
        /// <summary>
        /// 광고주별 배너 재생 집계리스트
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayBusiTotalList()
        {
            return View();
        }
        /// <summary>
        /// 광고주별 배너 재생 집계리스트
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult pv_AdPlayBusiTotalList(AdPlayTotalCond Cond)
        {
            ViewBag.list = new MongoDBService().GetAdPlayBusiTotalList(Cond);
            return PartialView2();
        }
        /// <summary>
        /// 광고주별 배너 재생 집계리스트 엑셀
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayBusiTotalExcelReport(AdPlayTotalCond Cond)
        {
            List<string> headers = new List<string>()
            { "순번", "광고주","광고수", "이미지-1(건)", "이미지-1(초)","이미지-6(건)","이미지-6(초)","이미지-12(건)","이미지-12(초)","클릭(건)","클릭(초)"
             , "비디오(건)", "비디오(초)", "유튜브(건)", "유튜브(초)", "총건수","총시간(초)"};


            Cond.PAGE = 1; Cond.PAGE_COUNT = 1000000000;



            return new MongoDBService().GetAdPlayBusiTotalList(Cond).Select(s => new {
                ROW_IDX = s.ROW_IDX,
                STRENAME = s.STORE_NAME,
                AD_CNT = s.AD_CNT,
                IMAGE1_PLAY_QTY = s.IMAGE1_PLAY_QTY,
                IMAGE1_PLAY_TIME = s.IMAGE1_PLAY_TIME,
                IMAGE6_PLAY_QTY = s.IMAGE6_PLAY_QTY,
                IMAGE6_PLAY_TIME = s.IMAGE6_PLAY_TIME,
                IMAGE12_PLAY_QTY = s.IMAGE12_PLAY_QTY,
                IMAGE12_PLAY_TIME = s.IMAGE12_PLAY_TIME,
                CLICK_PLAY_QTY = s.CLICK_PLAY_QTY,
                CLICK_PLAY_TIME = s.CLICK_PLAY_TIME,
                VIDEO_PLAY_QTY = s.VIDEO_PLAY_QTY,
                VIDEO_PLAY_TIME = s.VIDEO_PLAY_TIME,
                YOUTUBE_PLAY_QTY = s.YOUTUBE_PLAY_QTY,
                YOUTUBE_PLAY_TIME = s.YOUTUBE_PLAY_TIME,
                TOT_QTY = s.TOT_QTY,
                TOT_PLAY_TIME = s.TOT_PLAY_TIME

            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }
        #endregion

        /// <summary>
        /// 사업주별 광고 플레이 집계리스트
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayBusiLocalTotalList()
        {
            return View();
        }

        /// <summary>
        /// 사업주별 광고 플레이 집계리스트
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult pv_AdPlayBusiLocalTotalList(AdPlayTotalCond Cond)
        {
            ViewBag.list = new MongoDBService().GetAdPlayBusiLocalTotalList(Cond);
            return PartialView2();
        }

        /// <summary>
        /// 사업주별 광고 플레이 집계리스트 엑셀
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayBusiLocalTotalExcelReport(AdPlayTotalCond Cond)
        {
            List<string> headers = new List<string>()
            { "순번", "사업주","로컬박스수", "이미지-1(건)", "이미지-1(초)", "이미지-6(건)", "이미지-6(초)","이미지-12(건)","이미지-12(초)","클릭(건)","클릭(초)"
             , "비디오(건)", "비디오(초)", "유튜브(건)", "유튜브(초)", "총건수","총시간(초)"};


            Cond.PAGE = 1; Cond.PAGE_COUNT = 1000000000;

           

            return new MongoDBService().GetAdPlayBusiLocalTotalList(Cond).Select(s => new {
                ROW_IDX = s.ROW_IDX,
                STRENAME = s.STORE_NAME,
                AD_CNT = s.DEVICE_CNT,
                IMAGE1_PLAY_QTY = s.IMAGE1_PLAY_QTY,
                IMAGE1_PLAY_TIME = s.IMAGE1_PLAY_TIME,
                IMAGE6_PLAY_QTY = s.IMAGE6_PLAY_QTY,
                IMAGE6_PLAY_TIME = s.IMAGE6_PLAY_TIME,
                IMAGE12_PLAY_QTY = s.IMAGE12_PLAY_QTY,
                IMAGE12_PLAY_TIME = s.IMAGE12_PLAY_TIME,
                CLICK_PLAY_QTY = s.CLICK_PLAY_QTY,
                CLICK_PLAY_TIME = s.CLICK_PLAY_TIME,
                VIDEO_PLAY_QTY = s.VIDEO_PLAY_QTY,
                VIDEO_PLAY_TIME = s.VIDEO_PLAY_TIME,
                YOUTUBE_PLAY_QTY = s.YOUTUBE_PLAY_QTY,
                YOUTUBE_PLAY_TIME = s.YOUTUBE_PLAY_TIME,
                TOT_QTY = s.TOT_QTY,
                TOT_PLAY_TIME = s.TOT_PLAY_TIME

            }).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
        }

        /// <summary>
        /// 월별 광고 플레이 집계리스트 
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public ActionResult AdPlayMonthTotalList()
        {
            return View();
        }
        /// <summary>
        /// 월별 광고 플레이 집계리스트 
        /// </summary>
        /// <returns></returns>
        [AltloggalAuthorization]
        public PartialViewResult pv_AdPlayMonthTotalList(AdPlayTotalCond Cond)
        {
            ViewBag.list = new MongoDBService().GetAdPlayMonthTotalList(Cond);
            return PartialView2();
        }

        public JsonResult AdPlayScheduler()
        {
            var rtn = new MongoDBService().MongoDBToSqlServer(DateTime.Now.AddDays(-1).ToString("yyyyMMdd"), DateTime.Now.ToString("yyyyMMdd"));

            if(string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
            {
                SessionHelper.ScheduleInfo = new MongoDBService().GetAdPlayShedulerInfo();
                rtn.DATA = SessionHelper.ScheduleInfo.MONGODB_BASE_DATE.ToFormatDate();
                rtn.DATA2 = SessionHelper.ScheduleInfo.MONGODB_APPLY_TIME.ToString();
            }
            return new JsonResult { Data = rtn };
        }


        /// <summary>
        /// 로컬박스복사
        /// </summary>
        /// <param name="DEVICE_CODE"></param>
        /// <returns></returns>
        [AltloggalAuthorization]
        public JsonResult DeviceCopy(long DEVICE_CODE)
        {
            RTN_SAVE_DATA rtn = new DeviceService().DeviceCopy(DEVICE_CODE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
            return new JsonResult { Data = rtn };
        }

		[AltloggalAuthorization]
		public ActionResult AdDeviceLogList()
		{
			return View();
		}

		[AltloggalAuthorization]
		public PartialViewResult PV_AdDeviceLogList(T_AD_DEVICE_LOG_COND Cond)
		{
			ViewBag.list = Service.deviceService.GetAdDeviceLogList(Cond);
			
			return PartialView2();
		}

		[AltloggalAuthorization]
		public ActionResult AdDeviceLogExcelReport(T_AD_DEVICE_LOG_COND Cond)
		{
			List<string> headers = new List<string>()
			{ "일자", "배너코드", "제목", "시작일자", "종료일자", "프레임", "로컬박스명", "상태", "배너업체", "로컬박스업체", "클릭수", "비고", "추가글" };

			Cond.PAGE = 1; Cond.PAGE_COUNT = 100000;

			return Service.deviceService.GetAdDeviceLogList(Cond).Select(s => new {
			   UPDATE_DATE = s.UPDATE_DATE
			   ,
				AD_CODE = s.AD_CODE
			   ,
				TITLE = s.TITLE
			   ,
				FR_DATE = s.FR_DATE
			   ,
				TO_DATE = s.TO_DATE
			   ,
				AD_FRAME_TYPE = s.AD_FRAME_TYPE
			   ,
				DEVICE_CODE = s.DEVICE_CODE
			   ,
				STATUS = s.STATUS
			   ,
				AD_STORE_NAME = s.AD_STORE_NAME
			   ,
				DEVICE_STORE_NAME = s.DEVICE_STORE_NAME
			   ,
				CLICK_CNT = s.CLICK_CNT
			   ,
				REMARK = s.REMARK
			   ,
				REMARK2 = s.REMARK2
			}).ToList().ToExcel(SessionHelper.LoginInfo.CURRENT_MENU_NAME + DateTime.Now.ToString("_yyyyMMddHHmmss"), headers);
		}
		[AltloggalAuthorization]
		public PartialViewResult pv_RelativeDevice(DEVICE_LIST_COND Cond)
		{
			ViewBag.list = new DeviceService().GetRelativeDevice(Cond);
			return PartialView2();
		}
		[AltloggalAuthorization]
		public JsonResult RelativeDeviceSave(T_DEVICE Param)
		{
			RTN_SAVE_DATA data = Service.deviceService.RelativeDeviceSave(Param);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult GetVirtualDevice(int? STORE_CODE)
		{
			long data = Service.deviceService.GetVirtualDevice(STORE_CODE);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult AddVirtulaDevice(T_DEVICE Param)
		{
			RTN_SAVE_DATA data = Service.deviceService.AddVirtulaDevice(Param);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult InitVirtualDevice(T_DEVICE Param)
		{
			RTN_SAVE_DATA data = Service.deviceService.InitVirtualDevice(Param, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);
			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult InitPlaceItem(T_MEMBER_PLACE_ITEM_COND param)
		{
			RTN_SAVE_DATA data = new DeviceService().initPlaceItem(param, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult GetAllPlaceItem(T_MEMBER_PLACE_ITEM_COND param)
		{
			List<T_MEMBER_PLACE_ITEM> data = new DeviceService().getAllPlaceItem(param);

			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult GetPlaceItem(T_MEMBER_PLACE_ITEM_COND param)
		{
			List<T_MEMBER_PLACE_ITEM> data = new DeviceService().getPlaceItem(param);

			foreach(T_MEMBER_PLACE_ITEM item in data)
			{
				SessionHelper.LoginInfo.EMPLOYEE.MAKER_MAX_COUNT += item.ITEM_PURCHASE_CNT;
			}

			return new JsonResult { Data = data };
		}
		[AltloggalAuthorization]
		public JsonResult PlaceItemUseSave(List<T_MEMBER_PLACE_ITEM> list, long? CODE)
		{
			RTN_SAVE_DATA data = new DeviceService().PlaceItemUseSave(list, CODE, SessionHelper.LoginInfo.MEMBER.MEMBER_CODE);

			return new JsonResult { Data = data };
		}
	}
}