using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.loggal;
using ALT.VO.Common;
using System.Transactions;
using ALT.Framework;
using ALT.Framework.Data;

namespace loggalServiceBiz
{
	public class AdvertisingService : BaseService
	{
		public AdvertisingService() { }
		public AdvertisingService(DataContext _db) : base(_db) { }

		/**********************************************/
		/* 광고테이블 - T_AD 저장 -  saveparam Query */
		/**********************************************/
		/// <summary>                                                                                      
		/// T_AD 저장하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA T_AD_Save(T_AD Param, T_DEVICE_MAIN device = null)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					if (device == null) device = new T_DEVICE_MAIN();

					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "T_AD_Save"
													, Param.AD_CODE.ToString("-1")
													, (string.IsNullOrEmpty(Param.REG_DATE) ? DateTime.Now.ToString("yyyyMMdd") : Param.REG_DATE.RemoveDateString())
													, Param.LOGO_URL.ToString("")
													, Param.TITLE.ToString("")
													, Param.SUB_TITLE.ToString("")
													, Param.BANNER_TYPE.ToString("1")
													, Param.CONTENT.ToString("")
													, Param.FR_DATE.ToString("").RemoveDateString()
													, Param.TO_DATE.ToString("").RemoveDateString()
													, Param.FR_TIME.ToString("")
													, Param.TO_TIME.ToString("")
													, Param.CLICK_CNT.ToString()
													, Param.GRADE_POINT.ToString()
													, Param.COMPANY_CODE.ToString("")
													, Param.STORE_CODE.ToString("")
													, Param.MEMBER_CODE.ToString("")
													, Param.REP_CATEGORY_CODE.ToString("")
													, Param.REMARK.ToString("")
													, Param.STATUS.ToString("")
													, Param.HIDE ? "1" : "0"
													, Param.INSERT_CODE.ToString()
													, Param.CONTACT_STORE_CODE.ToString("")
													, device.DEVICE_CODE.ToString("")
													, device.SEQ.ToString("")
													, Param.GROUP_CODE.ToString("")
													, Param.MEMBER_NAME.ToString("")
													, Param.MOBILE.ToString("")
													, Param.CONTACT_COMPANY_CODE.ToString("")
													, Param.CONTACT_CODE.ToString("")
													, Param.CONTACT_NAME.ToString("")
													, Param.CONTACT_MOBILE.ToString("")
													, Param.AD_TYPE.ToString("1")
													, Param.CONTENT_TYPE.ToString()
													, Param.AD_TYPE2.ToString("1")
													, Param.BANNER_TYPE2.ToString("1")
													, Param.FILE_URL.ToString("")
													, Param.AD_FRAME_TYPE.ToString("6")
					);
					rtn.DATA = db.ExecuteQuery<long>(sql).First().ToString();
					Param.AD_CODE = Convert.ToInt64(rtn.DATA);
					KEYWORD_SAVE_DATA keyword = new KEYWORD_SAVE_DATA
					{
						AD_CODE = Param.AD_CODE,
						REG_CODE = Param.INSERT_CODE,
						KEYWORDS = Param.KEYWORDS,
					};
					if (Param.KEYWORDS != null)
					{
						string rtnMsg = this.KeywordSave(ref db, keyword);
						if (!string.IsNullOrEmpty(rtnMsg))
						{
							throw new Exception(rtnMsg);
						}
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		public string KeywordSave(ref DataContext _db, KEYWORD_SAVE_DATA Param)
		{
			string msg = string.Empty;
			try
			{
				new KeywordService().SearchKeywordDelete(new CATEGORY_KEYWORD_COND { AD_CODE = Param.AD_CODE, DEVICE_CODE = Param.DEVICE_CODE }, ref _db);


				foreach (KEYWORD_DATA data in Param.KEYWORDS.Distinct())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Keyword.xml", "adDeviceKeywordSave"
															 , Param.AD_CODE.ToString("")
															 , Param.DEVICE_CODE.ToString("")
															 , data.CODE.ToString()
															 , data.NAME.ToString("")
															 , Param.REG_CODE.ToString("")
															 );
					_db.ExecuteCommand(sql);
				}
			}
			catch (Exception ex)
			{
				msg = ex.Message;
			}
			return msg;

		}


		public IList<AD_STATUS> GetAdStatusList(AD_STATUS_COND Param)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetAdStatusList", Param.USER_ID, Param.TOP_CNT.ToString(""), Param.AD_COND_STRING.ToString(""));
			return db.ExecuteQuery<AD_STATUS>(sql).ToList();
		}

		/// <summary>
		/// 광고테이블정보가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public IList<T_AD> GetT_AD_List(long AD_CODE)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "Get_T_AD_List"
											  , AD_CODE.ToString(""));

			return db.ExecuteQuery<T_AD>(sql).ToList();
		}


		/**********************************************/
		/* 광고테이블 - T_AD 저장 -  saveparam Query */
		/**********************************************/
		/// <summary>                                                                                      
		/// T_AD 저장하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA Step3_Save(STEP3_SAVE Param)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				T_AD adData = GetT_AD_List((int)Param.AD_CODE).FirstOrDefault();
				if (adData == null) adData = new T_AD();
				adData.FR_DATE = Param.FR_DATE;
				adData.TO_DATE = Param.TO_DATE;
				adData.FR_TIME = Param.FR_TIME;
				adData.TO_TIME = Param.TO_TIME;
				adData.STATUS = Param.STATUS;
				adData.REP_CATEGORY_CODE = Param.REP_CATEGORY_CODE;
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "T_AD_Save"
													, adData.AD_CODE.ToString("-1")
													, adData.LOGO_URL.ToString("")
													, adData.TITLE.ToString("")
													, adData.SUB_TITLE.ToString("")
													, adData.CONTENT.ToString("")
													, adData.FR_DATE.ToString("")
													, adData.TO_DATE.ToString("")
													, adData.FR_TIME.ToString("")
													, adData.TO_TIME.ToString("")
													, adData.CLICK_CNT.ToString()
													, adData.GRADE_POINT.ToString()
													, adData.COMPANY_CODE.ToString("")
													, adData.STORE_CODE.ToString("")
													, adData.MEMBER_CODE.ToString("")
													, adData.REP_CATEGORY_CODE.ToString("")
													, adData.REMARK.ToString("")
													, adData.STATUS.ToString("")
													, adData.HIDE ? "1" : "0"
													, Param.REG_CODE.ToString()
													, adData.STORE_NAME.ToString("")
													, adData.MOBILE.ToString("")

					);
					rtn.DATA = db.ExecuteQuery<long>(sql).First().ToString();


					sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\SearchKeyword.xml", "SearchKeywordDelete", rtn.DATA, "", "", "", "", "2");
					db.ExecuteCommand(sql);

					foreach (KEYWORD_DATA data in Param.KEYWORDS)
					{
						sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Keyword.xml", "adDeviceKeywordSave"
																 , Param.AD_CODE.ToString("")
																 , data.CODE.ToString()
																 , data.NAME.ToString("")
																 , Param.REG_CODE.ToString("")
																 );
						db.ExecuteCommand(sql);
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		/**********************************************/
		/* 광고테이블 - T_AD 저장 -  saveparam Query */
		/**********************************************/
		/// <summary>                                                                                      
		/// T_AD 저장하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA AdDelete(long[] ADCode, int? REG_CODE)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (long adCode in ADCode)
					{
						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "AdDelete"
														, adCode.ToString()
														, REG_CODE.ToString()
						   );
						rtn.ERROR_MESSAGE = db.ExecuteQuery<string>(sql).First().ToString();
						if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
							throw new Exception(rtn.ERROR_MESSAGE);
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		/// <summary>
		/// 광고별 지역등록리스트 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public List<AD_REGION> GetAdRegionList(PAGE_COND Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Region.xml", "GetAdRegionList"
																, Cond.PAGE_COUNT.ToString("10")
																, Cond.PAGE.ToString("1")
																, Cond.CODE.ToString("")

																);
			return db.ExecuteQuery<AD_REGION>(sql).ToList();
		}
		/// <summary>
		/// 선택된 주소 지역구 키워드 자동 저장
		/// </summary>
		/// <param name="Cond"></param>
		/// <param name="REG_CODE"></param>
		/// <returns></returns>
		public RTN_SAVE_DATA AdRegionAutoSave(CODE_DATA Cond, int? REG_CODE)
		{
			long? AD_CODE = Cond.CODE;
			Cond.CODE = null;
			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

			if (AD_CODE == null)
			{
				return rtnData;
			}
			var data = new KeywordService().GetLocalNameList(Cond).FirstOrDefault();

			if (data == null)
			{
				rtnData.ERROR_MESSAGE = "해당 지역을 선택하여 주시기 바랍니다.";
				return rtnData;
			}
			List<AD_REGION> Paramlist = new List<AD_REGION>();
			Paramlist.Add(new AD_REGION { MRC_EDIT_MODE = "ALL_D", AD_CODE = (long)AD_CODE, SEARCH_CATEGORY_CODE = data.SEARCH_CODE });
			rtnData = this.AdRegionSave(Paramlist, REG_CODE);

			Paramlist = new List<AD_REGION>();
			Paramlist.Add(new AD_REGION { AD_CODE = (long)AD_CODE, SEARCH_CATEGORY_CODE = data.SEARCH_CODE });
			rtnData = this.AdRegionSave(Paramlist, REG_CODE);
			return rtnData;
		}
		public RTN_SAVE_DATA AdRegionSave(List<AD_REGION> list, int? REG_CODE)
		{
			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (AD_REGION data in list)
					{
						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Region.xml", "AdRegionSave"
															   , data.MRC_EDIT_MODE.ToString("")
															   , data.AD_CODE.ToString("")
															   , data.SEARCH_CATEGORY_CODE.ToString("")
															   , REG_CODE.ToString("0")
															);
						db.ExecuteCommand(sql);
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtnData.ERROR_MESSAGE = ex.Message;
			}

			return rtnData;
		}
		public RTN_SAVE_DATA initPlaceItem(T_MEMBER_PLACE_ITEM_COND param, int? REG_CODE)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "initPlaceItem"
														   , param.CODE.ToString()
														   , param.MEMBER_CODE.ToString()
														   , param.FR_DATE.ToString("").RemoveDateString()
														   , param.TO_DATE.ToString("").RemoveDateString()
														   , param.ITEM_TYPE.ToString()
														   , param.GROUP_TYPE.ToString()
														   , REG_CODE.ToString()
														);
					rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();

					if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
					{
						throw new Exception(rtn.ERROR_MESSAGE);
					}

					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}

			return rtn;
		}
		public List<T_MEMBER_PLACE_ITEM> getAllPlaceItem(T_MEMBER_PLACE_ITEM_COND param)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "getAllPlaceItem"
														, param.MEMBER_CODE.ToString()
														, param.CODE.ToString()
														, param.ITEM_TYPE.ToString()
														, param.ITEM_TYPE_LIMIT.ToString()
														);

			return db.ExecuteQuery<T_MEMBER_PLACE_ITEM>(sql).ToList();
		}
		public List<T_MEMBER_PLACE_ITEM> getPlaceItem(T_MEMBER_PLACE_ITEM_COND param)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "getPlaceItem"
														, param.MEMBER_CODE.ToString()
														, param.CODE.ToString()
														, param.ITEM_TYPE.ToString()
														, param.ITEM_TYPE_LIMIT.ToString()																												
														, param.GROUP_TYPE.ToString()
														);

			return db.ExecuteQuery<T_MEMBER_PLACE_ITEM>(sql).ToList();
		}
		public RTN_SAVE_DATA PlaceItemUseSave(List<T_MEMBER_PLACE_ITEM> list, long? CODE, int? REG_CODE)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				list = list == null ? new List<T_MEMBER_PLACE_ITEM>() : list;
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (T_MEMBER_PLACE_ITEM Param in list)
					{

						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "PlaceItemUseSave"
														  , Param.MEMBER_ITEM_IDX.ToString("")
														  , CODE.ToString("")
														  , Param.ITEM_USE_CNT.ToString("")
														  , Param.FR_DATE.ToString("").RemoveDateString()
														  , Param.TO_DATE.ToString("").RemoveDateString()
														  , REG_CODE.ToString()

						 );
						rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();

						if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
						{
							throw new Exception(rtn.ERROR_MESSAGE);
						}
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		/// <summary>
		/// 로컬박스 광고데이터 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public List<AD_LIST> GetActivedAd(long AD_CODE)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetActivedAd"
											, AD_CODE.ToString("")
										   );
			return db.ExecuteQuery<AD_LIST>(sql).ToList();
		}

		/// <summary>
		/// loggal mobile 광고 클릭시 업데이트
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public RTN_SAVE_DATA AD_OpenPageForMobile(long adCode)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				/**/
				//if (this.GetActivedAd(adCode).Count() == 0)
				//{
				//    throw new Exception("현재의 광고는 유효하지 않습니다.");
				//}
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "AdClickSave"
											, adCode.ToString("")
											);

					db.ExecuteCommand(sql);
					tran.Complete();
					rtn.RETURN_URL = Global.ConfigInfo.MANAGEMENT_SITE.ToString("") + "/advertise/contentview/" + adCode;
				}

			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		#region >> 광고 즐겨찾기

		/// <summary>
		/// 광고별 지역등록리스트 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public List<T_AD_FAVORITE> GetAdFavoriteList(T_AD_FAVORITE_COND Cond)
		{
			Cond.FR_DATE = Cond.FR_DATE.ToString("19010101").RemoveDateString();
			Cond.TO_DATE = Cond.TO_DATE.ToString(DateTime.Now.ToString("yyyyMMdd")).RemoveDateString();
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Favorite.xml", "GetAdFavoriteList"
																, Cond.PAGE.ToString("1")
																, Cond.PAGE_COUNT.ToString("10")
																, Cond.FR_DATE
																, Cond.TO_DATE
																, Cond.IDX.ToString("")
																, Cond.FAV_TYPE.ToString("")
																, Cond.AD_CODE.ToString("")
																, Cond.DEVICE_CODE.ToString("")
																, Cond.DEVICE_SEQ.ToString("")
																, Cond.MOBILE.ToString("")
																, Cond.SNS_TYPE.ToString("")
																, Cond.EMAIL.ToString("")
																, Cond.USER_IP.ToString("")
																, Cond.AD_TITLE.ToString("")
																, Cond.DEVICE_NAME.ToString("")
															 );
			return db.ExecuteQuery<T_AD_FAVORITE>(sql).ToList();
		}

		public RTN_SAVE_DATA AdFavoriteSave(T_AD_FAVORITE data)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{

				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Favorite.xml", "AdFavoriteSave"
											, data.IDX.ToString()
											, data.FAV_TYPE.ToString()
											, data.AD_CODE.ToString()
											, data.DEVICE_CODE.ToString("")
											, data.DEVICE_SEQ.ToString("")
											, data.MOBILE.ToString("")
											, data.SNS_TYPE.ToString("")
											, data.EMAIL.ToString("")
											, data.USER_IP.ToString("")
											, data.REMARK.ToString("")
											);

					db.ExecuteCommand(sql);
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		#endregion >> 광고 즐겨찾기

		public List<AD_DEVICE_LIST> GetAdDeviceList(SEARCH_COND Cond)
		{
			Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "AD_CODE" : Cond.SORT;
			string[] arrSort = Cond.SORT.Split(' ');
			string sort = "AD_CODE DESC";
			switch (arrSort[0].ToUpper())
			{
				case "FR_DATE":
					sort = "CASE WHEN B.AD_DEVICE_CODE IS NULL THEN A.FR_DATE ELSE B.FR_DATE END " + arrSort[1].ToUpper();
					break;
				case "TO_DATE":
					sort = "CASE WHEN B.AD_DEVICE_CODE IS NULL THEN A.TO_DATE ELSE B.TO_DATE END " + arrSort[1].ToUpper();
					break;
				default:
					sort = "A." + arrSort[0].ToUpper() + " " + arrSort[1].ToUpper();
					break;
			}
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "GetAdDeviceList"
															   , Cond.PAGE.ToString("1")
															   , Cond.PAGE_COUNT.ToString("10")

															   , Cond.SEARCH_DATA  /*DEVICE_CODE*/
															   , sort
															   , Cond.SEARCH_DATA1 /*TITLE*/
															   , Cond.SEARCH_DATA2 /*REP_CATEGORY_CODE*/
															   , Cond.SEARCH_DATA9 /*1일 경우 로컬박스에 등록된 광고만*/
															);
			return db.ExecuteQuery<AD_DEVICE_LIST>(sql).ToList();
		}

		public RTN_SAVE_DATA DevcieAdInfoSave(AdDeviceSaveData saveData, int? REG_CODE)
		{

			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (T_AD_DEVICE data in saveData.list)
					{
						string sql = string.Empty;

						sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "T_AD_DEVICE_MOBILE_Save"
																 , data.AD_CODE
																 , data.DEVICE_CODE
																 , REG_CODE
																 , data.IS_MOBILE == true ? "1" : "0"
													);
						db.ExecuteCommand(sql);
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtnData.ERROR_MESSAGE = ex.Message;
			}
			return rtnData;
		}
		public RTN_SAVE_DATA AdDeviceSeqSave(List<LOGGAL_BOX_DATA2> list, int? REG_CODE)
		{
			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
			try
			{
				using (TransactionScope tran = new TransactionScope())
				{

					string sql = string.Empty;
					foreach (LOGGAL_BOX_DATA2 Param in list)
					{
						sql = Global.DBAgent.LoadCondSQL(sqlBasePath + "Advertising\\AdDevice.xml", "AdDeviceSeqSave"
													  , Param.AD_CODE.ToString("")
													  , Param.DEVICE_CODE.ToString("")
													  , Param.DISPLAY_SEQ.ToString("")
													  , REG_CODE

						 );

						rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
						if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
						{
							throw new Exception(rtnData.ERROR_MESSAGE);
						}
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtnData.ERROR_MESSAGE = ex.Message;
			}
			return rtnData;
		}

		public List<T_AD_DEVICE> Get_T_AD_DEVICE_LIST(T_AD_DEVICE Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "Get_T_AD_DEVICE_LIST"
															   , Cond.AD_DEVICE_CODE
															   , Cond.AD_CODE.ToString("")
															   , Cond.DEVICE_CODE
															   , (Cond.HIDE == null || Cond.HIDE == true ? "1" : "0")
															);
			return db.ExecuteQuery<T_AD_DEVICE>(sql).ToList();
		}

		public RTN_SAVE_DATA T_AD_DEVICE_Save(AdDeviceSaveData saveData, int? loginCode = 0)
		{

			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (T_AD_DEVICE data in saveData.list)
					{
						string sql = string.Empty;
						if (saveData.SAVE_TYPE != "D")
						{
							data.FR_DATE = data.FR_DATE.RemoveDateString();
							data.TO_DATE = data.TO_DATE.RemoveDateString();
							sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "T_AD_DEVICE_Save"
																										, data.AD_DEVICE_CODE
																										, data.AD_CODE
																										, data.DEVICE_CODE
																										, data.FR_DATE
																										, data.TO_DATE
																										, data.FR_TIME.ToString("")
																										, data.TO_TIME.ToString("")
																										//, data.FR_UTC_DATE
																										//, data.TO_UTC_DATE
																										//, data.FR_UTC_TIME
																										//, data.TO_UTC_TIME
																										, data.CLICK_CNT.ToString("0")
																										, data.STATUS.ToString("1")
																										, data.HIDE == null || data.HIDE == true ? "1" : "0"
																										, data.REMARK.ToString("")
																										, loginCode.ToString("0")
																										, data.APPROVAL_COMPANY_CODE
																									);
						}
						else
						{
							sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "T_AD_DEVICE_Delete"
																										, data.AD_DEVICE_CODE
																										, data.AD_CODE
																										, data.DEVICE_CODE
																										, loginCode.ToString("0")
																								  );
						}
						db.ExecuteCommand(sql);
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtnData.ERROR_MESSAGE = ex.Message;
			}
			return rtnData;
		}

		/// <summary>
		/// 광고리스트 페이지조회
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public List<T_AD> GetT_Ad_PageList(AD_PAGE_COND Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetT_Ad_PageList"
															  , Cond.PAGE_COUNT.ToString("10")
															  , Cond.PAGE.ToString("1")
															  , Convert.ToInt32(Cond.PAGE_COUNT.ToString("10")) * Convert.ToInt32(Cond.PAGE.ToString("1"))
															  , Cond.SORT
															  , Cond.LATITUDE.ToString("37")
															  , Cond.LONGITUDE.ToString("127")
															  , Cond.AD_CODE.ToString("")  /*AD_CODE */
															  , Cond.AD_CODE.ToString("") /*AD_CODE*/
															  , Cond.TITLE.ToString("") /*TITLE*/
															  , Cond.COMPANY_CODE.ToString("") /*COMPANY_CODE 업체*/
															  , Cond.STORE_CODE.ToString("") /*STORE_CODE  지점*/
															  , Cond.DEPT_SEARCH.ToString("") /*DEPT_SEARCH 상위부서*/
															  , Cond.DEPT_CODE.ToString("") /*DEPT_CODE   부서*/
															  , Cond.PARENT_MEMBER_CODE.ToString("") /*PARENT_MEMBER_CODE  상급자*/
															  , Cond.MEMBER_CODE.ToString("") /*MEMBER_CODE  본인권한*/
															  , Cond.GROUP_CODE.ToString("") /*GROUP_CODE*/
															  , Cond.BANNER_TYPE.ToString("")/*BANNER_TYPE 배너유형 */
															  , Cond.STATUS.ToString("")/*STATUS 승인상태 */
															  , Cond.BASE_DATE.ToString("") /*기준일자*/
															  , (Cond.HIDE == null ? "" : (Cond.HIDE == true ? "1" : "0"))
															  , Cond.USER_ID.ToString("")
															  , Cond.USER_NAME.ToString("")
															  , Cond.AD_FRAME_TYPE.ToString("")

														   );

			return db.ExecuteQuery<T_AD>(sql).ToList();
		}

		public List<AdOnDeviceList> GetAdOnDeviceList(SEARCH_COND Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "GetAdOnDeviceList"
															, Cond.PAGE_COUNT.ToString("10")
															, Cond.PAGE.ToString("1")
															, Cond.SORT
															, Cond.SEARCH_DATA.ToString("") /*REP_CATEGORY_CODE 대표카테고리*/
															, Cond.SEARCH_DATA1.ToString("") /*TITLE 제목*/
															, Cond.SEARCH_DATA3.ToString("")/*COMPANY_CODE*/
															, Cond.SEARCH_DATA4.ToString("")/*STORE_CODE*/
															, Cond.SEARCH_DATA5.ToString("")/*DEPT_SEARCH 상위부서*/
															, Cond.SEARCH_DATA6.ToString("")/*DEPT_CODE   부서*/
															, Cond.SEARCH_DATA7.ToString("")/*PARENT_MEMBER_CODE  상급자*/
															, Cond.SEARCH_DATA8.ToString("")/*MEMBER_CODE  본인권한*/
															, Cond.SEARCH_DATA9.ToString("") /*AD_FRAME_TYPE 프레임유형*/
															, Cond.SEARCH_DATA10.ToString("") /*IS_BOOKMARK 북마크여부*/
														   );

			return db.ExecuteQuery<AdOnDeviceList>(sql).ToList();
		}

		/// <summary>
		/// 로컬박스에 배너 공유 요청
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>
		public RTN_SAVE_DATA AdToDeviceShare(List<AD_DEVICE_SHARE> list, int? REG_CODE = 0)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				foreach (AD_DEVICE_SHARE Cond in list)
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "adDeviceShareSave"
																	, Cond.DEVICE_CODE.ToString() /*로컬박스코드 DECVIE_CODE*/
																	, Cond.AD_CODE.ToString() /*광고코드 AD_CODE*/
																	, Cond.TIMEZONE_OFFSET.ToString("9")
																	, Cond.COMMENT.ToString("")
																	, REG_CODE.ToString("0")
																);
					db.ExecuteCommand(sql);
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		public List<AdOnDeviceList2> GetAdOnDeviceList2(SEARCH_COND Cond)
		{
			string adFrameType = string.IsNullOrEmpty(Cond.SEARCH_DATA11) ? "-1" : Cond.SEARCH_DATA11;
			Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? " CASE WHEN A.AD_FRAME_TYPE != " + adFrameType + " THEN 99999 ELSE AEM.ORDER_SEQ END, AEM.ORDER_SEQ, A.DEVICE_NAME" : Cond.SORT;
			string[] arrSort = Cond.SORT.Split(' ');
			string sort = Cond.SORT;
			switch (arrSort[0].ToUpper())
			{
				case "FR_DATE":
					sort = "CASE WHEN AE.AD_DEVICE_CODE IS NULL THEN AD.FR_DATE ELSE AE.FR_DATE END " + arrSort[1].ToUpper();
					break;
				case "TO_DATE":
					sort = "CASE WHEN AE.AD_DEVICE_CODE IS NULL THEN AD.TO_DATE ELSE AE.TO_DATE END " + arrSort[1].ToUpper();
					break;

			}
			Cond.SEARCH_DATA11 = "";

			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "GetAdOnDeviceList2"
															, Cond.PAGE_COUNT.ToString("10")
															, Cond.PAGE.ToString("1")
															, Cond.SEARCH_DATA.ToString("")  /*AD_CODE 광고코드*/
															, sort
															, Cond.SEARCH_DATA1.ToString("") /*TITLE 제목*/
															, Cond.SEARCH_DATA2.ToString("") /*매장명 또는 로컬박스명*/
															, Cond.SEARCH_DATA3.ToString("")/*COMPANY_CODE*/
															, Cond.SEARCH_DATA4.ToString("")/*STORE_CODE*/
															, Cond.SEARCH_DATA5.ToString("")/*DEPT_SEARCH 상위부서*/
															, Cond.SEARCH_DATA6.ToString("")/*DEPT_CODE   부서*/
															, Cond.SEARCH_DATA7.ToString("")/*PARENT_MEMBER_CODE  상급자*/
															, Cond.SEARCH_DATA8.ToString("")/*MEMBER_CODE  본인권한*/
															, Cond.SEARCH_DATA9.ToString("")/*1 일경우 등록된 로컬박스만 보기*/
															, Cond.SEARCH_DATA10.ToString("")/*KEYWORD_NAME*/
															, Cond.SEARCH_DATA11.ToString("")/*AD_FRAME_TYPE 프레임유형*/
															, Cond.SEARCH_DATA12.ToString("")/*IS_BOOKMARK의 사용자코드*/
															);
			return db.ExecuteQuery<AdOnDeviceList2>(sql).ToList();
		}
		#region >> 광고장소
		/// <summary>                                                                                      
		/// T_AD_PLACE 저장하기(광고장소 - T_AD_PLACE 저장 -  saveparam Query)										  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA AdplaceSave(List<T_AD_PLACE> list, int? AD_CODE, int? REG_CODE)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				list = list == null ? new List<T_AD_PLACE>() : list;
				using (TransactionScope tran = new TransactionScope())
				{


					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Place.xml", "AdPlaceDelete", AD_CODE.ToString("0"));
					rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
					foreach (T_AD_PLACE Param in list)
					{

						sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Place.xml", "AdPlaceSave"
													   , Param.IDX.ToString("")
													   , AD_CODE.ToString("0")
													   , Param.CK_CODE.ToString("")
													   , Param.REGION.ToString("")
													   , Param.JIBUN_ADDRESS.ToString("")
													   , Param.ROAD_ADDRESS.ToString("")
													   , Param.BUILDING.ToString("")
													   , Param.ZIP_CODE.ToString("")
													   , Param.LATITUDE.ToString("")
													   , Param.LONGITUDE.ToString("")
													   , Param.RADIUS.ToString("500")
													   , REG_CODE.ToString()

						 );
						rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
						if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
						{
							throw new Exception(rtn.ERROR_MESSAGE);
						}
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}
		/// <summary>                                                                                      
		/// T_AD_PLACE 조회하기	(광고장소 - T_AD_PLACE)					      
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public List<T_AD_PLACE> GetAdplaceList(T_AD_PLACE_COND Param)
		{

			List<T_AD_PLACE> list = new List<T_AD_PLACE>();
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Place.xml", "GetAdPlaceList"
													 , Param.IDX.ToString("")
													 , Param.AD_CODE.ToString("")
													 , Param.CK_CODE.ToString("")
													 , Param.REGION.ToString("")
													 , Param.JIBUN_ADDRESS.ToString("")
													 , Param.ROAD_ADDRESS.ToString("")
													 , Param.BUILDING.ToString("")
													 , Param.ZIP_CODE.ToString("")
							);
			list = db.ExecuteQuery<T_AD_PLACE>(sql).ToList();
			return list;
		}

		#endregion >> 광고장소

		public MOBILE_AD_DETAIL_DATA GetMobileAdDetail(MOBILE_AD_DETAIL_COND Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Mobile.xml", "GetMobileAdDetail"
												   , Cond.AD_CODE.ToString("")
												   , Cond.USER_ID.ToString("")
												   , Global.ConfigInfo.MANAGEMENT_SITE
						  );
			return db.ExecuteQuery<MOBILE_AD_DETAIL_DATA>(sql).FirstOrDefault();
		}


		public List<MOBILE_AD_SEARCH_DATA> GetMobileAdSearchList(MOBILE_AD_SEARCH_COND Cond)
		{
			StringBuilder sbSqlKeywordName = new StringBuilder();
			//string keywordSearch = string.Empty;
			var keywordNamelist = Cond.KEYWORD_NAME.ToString("").Trim().Split(' ');
			foreach (var data in keywordNamelist)
			{
				if (!string.IsNullOrEmpty(data))
				{
					sbSqlKeywordName.Append("\n").AppendFormat(" AND (A.TITLE LIKE '%{0}%'", data);
					sbSqlKeywordName.Append("\n").AppendFormat("      OR A.CONTENT like '%{0}%'", data);
					sbSqlKeywordName.Append("\n").AppendFormat("      OR A.AD_CODE IN(SELECT AD_CODE FROM T_SEARCH_KEYWORD ", data);
					sbSqlKeywordName.Append("\n").Append("                             WHERE KEYWORD_CODE IN(SELECT BASE_CODE FROM T_KEYWORD ");
					sbSqlKeywordName.Append("\n").AppendFormat("                                             WHERE KEYWORD_NAME LIKE '%{0}%') ", data);
					sbSqlKeywordName.Append("\n").AppendFormat("                      ) ");
					sbSqlKeywordName.Append("\n").AppendFormat("     ) ");
				}
			}


			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_Mobile.xml", "GetMobileAdSearchList"
												   , Cond.PAGE_COUNT.ToString("15")
												   , Cond.PAGE.ToString("1")
												   , Cond.LATITUDE.ToString("")
												   , Cond.LONGITUDE.ToString("")
												   , Cond.TIME_ZONE.ToString("540")
												   , Cond.USER_ID.ToString("")
												   , Global.ConfigInfo.MANAGEMENT_SITE
												   , Cond.CATEGORY_CODE.ToString("")
												   , Cond.CATEGORY_CODES.ToString("")
												   , Cond.KEYWORD_CODE.ToString("")
												   , sbSqlKeywordName.ToString()
												   , Cond.DISTANCE.ToString("3000")

						  );
			return db.ExecuteQuery<MOBILE_AD_SEARCH_DATA>(sql).ToList();
		}

		/// <summary>                                                                                      
		/// 서브배너(T_AD_SUB) 조회																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public IList<T_AD_SUB> GetAdSubList(T_AD_SUB Param)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AD_SUB.xml", "GetAdSubList"
											 , Param.AD_CODE.ToString("")
						 );
			return db.ExecuteQuery<T_AD_SUB>(sql).ToList();
		}

		/// <summary>                                                                                      
		/// 서브배너(T_AD_SUB) 저장하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA AdSubSave(IList<T_AD_SUB> list)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			string sql = string.Empty;
			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (T_AD_SUB Param in list)
					{
						if (Param.SAVE_MODE == "D")
						{
							sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AD_SUB.xml", "AdSubDelete"
													   , Param.IDX.ToString("-1")
													   );
						}
						else
						{
							sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AD_SUB.xml", "AdSubSave"
													   , Param.IDX.ToString("-1")
													   , Param.AD_CODE.ToString("")
													   , Param.SEQ.ToString("")
													   , Param.IMG_URL.ToString("")
													   , Param.TITLE.ToString("")
													   , Param.RELATION.ToString("")
													   , Param.CONTENT.ToString("")
													   , Param.REG_NAME.ToString("")
													   , Param.REMARK.ToString("")
													   , Param.INSERT_CODE.ToString("")

								);
						}
						db.ExecuteCommand(sql);
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}


		#region >> 로컬박스장소
		/// <summary>                                                                                      
		/// T_DEVICE_PLACE 저장하기(로컬박스장소 - T_DEVICE_PLACE 저장 -  saveparam Query)										  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA DevicePlaceSave(List<T_DEVICE_PLACE> list, long? DEVICE_CODE, int? REG_CODE)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				list = list == null ? new List<T_DEVICE_PLACE>() : list;
				using (TransactionScope tran = new TransactionScope())
				{


					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "DevicePlaceDelete", DEVICE_CODE.ToString("0"));
					rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
					foreach (T_DEVICE_PLACE Param in list)
					{

						sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "DevicePlaceSave"
													   , Param.IDX.ToString("")
													   , DEVICE_CODE.ToString("0")
													   , Param.CK_CODE.ToString("")
													   , Param.REGION.ToString("")
													   , Param.JIBUN_ADDRESS.ToString("")
													   , Param.ROAD_ADDRESS.ToString("")
													   , Param.BUILDING.ToString("")
													   , Param.ZIP_CODE.ToString("")
													   , Param.LATITUDE.ToString("")
													   , Param.LONGITUDE.ToString("")
													   , Param.RADIUS.ToString("500")
													   , REG_CODE.ToString()

						 );
						rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
						if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
						{
							throw new Exception(rtn.ERROR_MESSAGE);
						}
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		/// <summary>                                                                                      
		/// T_DEVICE_PLACE 저장하기(로컬박스장소 - T_DEVICE_PLACE 저장 -  saveparam Query)										  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA DevicePlaceSave2(ref DataContext _db, List<T_DEVICE_PLACE> list, long? DEVICE_CODE, int? REG_CODE)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				list = list == null ? new List<T_DEVICE_PLACE>() : list;


				string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "DevicePlaceDelete", DEVICE_CODE.ToString("0"));
				rtn = _db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
				foreach (T_DEVICE_PLACE Param in list)
				{

					sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "DevicePlaceSave"
												   , Param.IDX.ToString("")
												   , DEVICE_CODE.ToString("0")
												   , Param.CK_CODE.ToString("")
												   , Param.REGION.ToString("")
												   , Param.JIBUN_ADDRESS.ToString("")
												   , Param.ROAD_ADDRESS.ToString("")
												   , Param.BUILDING.ToString("")
												   , Param.ZIP_CODE.ToString("")
												   , Param.LATITUDE.ToString("")
												   , Param.LONGITUDE.ToString("")
												   , Param.RADIUS.ToString("1000")
												   , REG_CODE.ToString()

					 );
					rtn = _db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
					if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
					{
						throw new Exception(rtn.ERROR_MESSAGE);
					}

				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}
		/// <summary>                                                                                      
		/// T_DEVICE_PLACE 최초 초기화										  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA InitDevicePlace(ref DataContext _db, T_DEVICE param, long? DEVICE_CODE, int? REG_CODE)
		{
			T_DEVICE_PLACE data  = new T_DEVICE_PLACE();

			data.JIBUN_ADDRESS	= param.ADDRESS1;
			data.ZIP_CODE		= param.ZIP_CODE;
			data.LATITUDE		= param.LATITUDE;
			data.LONGITUDE		= param.LONGITUDE;

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "DevicePlaceDelete", DEVICE_CODE.ToString("0"));
				rtn = _db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();

				sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "DevicePlaceSave"
											   , data.IDX.ToString("")
											   , DEVICE_CODE.ToString("0")
											   , data.CK_CODE.ToString("")
											   , data.REGION.ToString("")
											   , data.JIBUN_ADDRESS.ToString("")
											   , data.ROAD_ADDRESS.ToString("")
											   , data.BUILDING.ToString("")
											   , data.ZIP_CODE.ToString("")
											   , data.LATITUDE.ToString("")
											   , data.LONGITUDE.ToString("")
											   , data.RADIUS.ToString("1000")
											   , REG_CODE.ToString()

				 );
				rtn = _db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
				if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
				{
					throw new Exception(rtn.ERROR_MESSAGE);
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}
		/// <summary>                                                                                      
		/// T_DEVICE_PLACE 조회하기	(로컬박스장소 - T_DEVICE_PLACE 저장 -  selectparam Query)					      
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public List<T_DEVICE_PLACE> GetDevicePlaceList(T_DEVICE_PLACE_COND Param)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DevicePlace.xml", "GetDevicePlaceList"
													 , Param.IDX.ToString("")
													 , Param.DEVICE_CODE.ToString("")
													 , Param.CK_CODE.ToString("")
													 , Param.REGION.ToString("")
													 , Param.JIBUN_ADDRESS.ToString("")
													 , Param.ROAD_ADDRESS.ToString("")
													 , Param.BUILDING.ToString("")
													 , Param.ZIP_CODE.ToString("")
							);
			return db.ExecuteQuery<T_DEVICE_PLACE>(sql).ToList();
		}

		#endregion >> 로컬박스장소


		public RTN_SAVE_DATA AdContentClickSave(LOGGAL_AD_COND Cond)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				//if (this.GetDailyLoggalAdList(Cond).Count() == 0)
				//{
				//    throw new Exception("현재의 광고는 유효하지 않습니다.");
				//}
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "advertising\\ad.xml", "AdContentClickSave"
												, Cond.AD_CODE.ToString("")
												, Cond.DEVICE_CODE.ToString("")
											);

					db.ExecuteCommand(sql);
					tran.Complete();
				}

			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;

		}


		public RTN_SAVE_DATA AdStatusApprovalSave(T_AD_STATUS param)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				/**/
				//if (this.GetActivedAd(adCode).Count() == 0)
				//{
				//    throw new Exception("현재의 광고는 유효하지 않습니다.");
				//}
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "AdStatusSave"
											, param.AD_CODE.ToString()
											, param.STATUS.ToString()
											, param.REG_CODE.ToString()
											);

					db.ExecuteCommand(sql);
					tran.Complete();

				}

			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}


		/// <summary>                                                                                      
		/// T_AD_PLAY_LOG 조회하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public List<T_AD_PLAY_LOG> GetAdPlayLogList(T_AD_PLAY_LOG_COND Param)
		{
			Param.SORT_ORDER = string.IsNullOrEmpty(Param.SORT_ORDER) ? "A.IDX DESC" : Param.SORT_ORDER;
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG.xml", "GetAdPlayLogList"
											, Param.PAGE.ToString("1")
											, Param.PAGE_COUNT.ToString("10")
											, Param.SORT_ORDER
											, Param.FR_DATE.RemoveDateString().ToString("")
											, Param.TO_DATE.RemoveDateString().ToString("")
											, Param.DEVICE_KIND.ToString("")
											, Param.AD_CODE.ToString("")
											, Param.DEVICE_CODE.ToString("")
											, Param.TITLE.ToString("")
											, Param.DEVICE_NAME.ToString("")
											, Param.BANNER_KIND.ToString("")
											  );
			return db.ExecuteQuery<T_AD_PLAY_LOG>(sql).ToList();
		}

		/// <summary>                                                                                      
		/// T_AD_PLAY_LOG 저장하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA AdPlayLogSave(T_AD_PLAY_LOG Param)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				DateTime regDate = Param.REG_DATE ?? DateTime.Now;
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = string.Empty;
					if (string.IsNullOrEmpty(Param.AD_CODES))
					{
						sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG.xml", "AdPlayLogSave"
													   , Param.IDX.ToString("-1")
													   , regDate.ToString("yyyy-MM-dd HH:mm:ss.fff")
													   , Param.DEVICE_KIND.ToString("")
													   , Param.DEVICE_CODE.ToString("")
													   , Param.AD_CODE.ToString("")
													   , Param.FRAME_TYPE.ToString("1")
													   , Param.PLAY_TYPE.ToString("1")
													   , Param.PLAY_TIME.ToString("")
													   , Param.REMARK.ToString("")
													   , Param.INSERT_CODE.ToString("")

						 );
						rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
						if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
						{
							throw new Exception(rtn.ERROR_MESSAGE);
						}
					}
					else
					{
						string[] arrAdCode = Param.AD_CODES.Split(',');
						foreach (string adCode in arrAdCode)
						{
							sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG.xml", "AdPlayLogSave"
													, Param.IDX.ToString("-1")
													, regDate.ToString("yyyy-MM-dd HH:mm:ss")
													, Param.DEVICE_KIND.ToString("")
													, Param.DEVICE_CODE.ToString("")
													, adCode
													, Param.FRAME_TYPE.ToString("1")
													, Param.PLAY_TYPE.ToString("1")
													, Param.PLAY_TIME.ToString("")
													, Param.REMARK.ToString("")
													, Param.INSERT_CODE.ToString("")

							);
							rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
							if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
							{
								throw new Exception(rtn.ERROR_MESSAGE);
							}
						}
					}
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}

		public RTN_SAVE_DATA myBannerToDeviceSave(MY_BANNER_SAVE param)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{

				using (TransactionScope tran = new TransactionScope())
				{
					if (param.SAVE_MODE == "N")
					{
						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "myBannerToDeviceSave"

												, param.AD_CODE.ToString("-1")
												, param.DEVICE_CODES.ToString("-1")
												, param.REG_CODE.ToString("0")
												);

						db.ExecuteCommand(sql);
					}
					else if (param.SAVE_MODE == "D")
					{
						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "myBannerToDeviceDelete"

												, param.AD_CODE.ToString("-1")
												, param.DEVICE_CODES.ToString("-1")
												, param.REG_CODE.ToString("0")
												);

						db.ExecuteCommand(sql);
					}
					tran.Complete();

				}

			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}


		public List<AD_DEVICE_SHARE_TOTAL_LIST> GetAdDeviceShareTotalList(AD_DEVICE_SHARE_TOTAL_COND Cond, List<T_COMMON> commonList)
		{
			Cond.GUBUN = Cond.GUBUN.ToString("");
			StringBuilder sbSql = new StringBuilder();
			for (int i = 0; i < commonList.Count(); i++)
			{
				sbSql.Append("    ,  SUM(CASE WHEN B.STATUS = ").Append(commonList[i].SUB_CODE).Append(" THEN 1 ELSE 0 END) AS CNT").Append((i + 1).ToString());
			}

			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\AdDevice.xml", "GetAdDeviceShareTotalList"
									  , Cond.PAGE.ToString("1")
									  , Cond.PAGE_COUNT.ToString("15")
									  , Cond.SORT
									  , sbSql.ToString()
									  , Cond.SEARCH_TEXT.ToString("")
									  , Cond.FR_DATE.ToString("").RemoveDateString()
									  , Cond.TO_DATE.ToString("").RemoveDateString()
									  , Cond.GUBUN == "1" ? Cond.COMPANY_CODE.ToString("") : "" /*요청한 정보*/
									  , Cond.GUBUN == "2" ? Cond.COMPANY_CODE.ToString("") : "" /*요청받은 정보*/
									  , Cond.GUBUN == "" ? Cond.COMPANY_CODE.ToString("") : ""  /*전체내역*/
								 );
			return db.ExecuteQuery<AD_DEVICE_SHARE_TOTAL_LIST>(sql).ToList();
		}

		public RTN_SAVE_DATA AdCopy(long AD_CODE, int? REG_CODE)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				string sql = string.Empty;
				using (TransactionScope tran = new TransactionScope())
				{
					sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml"
										   , "AdCopy"
										   , AD_CODE.ToString()
										   , REG_CODE.ToString("0")
										   );
					rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}
	}
}