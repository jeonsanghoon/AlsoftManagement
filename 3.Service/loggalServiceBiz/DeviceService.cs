
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Transactions;
using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.loggal;
using ALT.VO.Common;

namespace loggalServiceBiz
{
    public class DeviceService : BaseService
    {
        public DeviceService() { }
        public DeviceService(DataContext _db) : base(_db) { }

        public List<T_DEVICE> GetDeviceList(T_DEVICE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetDeviceList"
                                         , Cond.DEVICE_CODE.ToString("")
                                         , Cond.COMPANY_CODE.ToString("")
                                         , Cond.STORE_CODE.ToString("")
                                         , Cond.GROUP_CODE.ToString("")
                                         , Cond.DEVICE_NAME.ToString("")
                                         , Cond.AUTH_NUMBER.ToString("")
                                         , Cond.DEVICE_NUMBER.ToString("")


             );

            return db.ExecuteQuery<T_DEVICE>(sql).ToList();
        }


        public RTN_SAVE_DATA DeviceSave(List<T_DEVICE> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { DATA = "" };
            string rtnMsg = string.Empty;
			
            try
            {
                
                using (TransactionScope tran = new TransactionScope())
                {
					foreach (T_DEVICE data in list)
					{
						data.CONTACT_STORE_CODE = data.CONTACT_STORE_CODE == null ? data.STORE_CODE : data.CONTACT_STORE_CODE;
						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "DeviceSave"
															   , data.DEVICE_CODE.ToString("-1")
																, data.COMPANY_CODE.ToString("")
																, data.STORE_CODE.ToString("")
																, data.MEMBER_CODE.ToString("")
																, data.DEVICE_NUMBER.ToString("")
																, data.AUTH_NUMBER.ToString("")
																, data.DEVICE_NAME.ToString("")
																, data.DEVICE_DESC.ToString("")
																, data.GROUP_CODE.ToString("")
																, data.BUSI_TYPE.ToString("")
																, data.BUSI_TYPE2.ToString("")
																, data.ADDRESS1.ToString("")
																, data.ADDRESS2.ToString("")
																, data.ZIP_CODE.ToString("")
																, data.LATITUDE.ToString("")
																, data.LONGITUDE.ToString("")
																, data.CONTACT_COMPANY_CODE.ToString("")
																, data.CONTACT_STORE_CODE.ToString("")
																, data.CONTACT_CODE.ToString("")
																, data.CONTACT_NAME.ToString("")
																, data.CONTACT_PHONE.ToString("")
																, data.CONTACT_EMAIL.ToString("")
																, data.CATEGORY_CODES.ToString("")
																, data.AD_DISTANCE.ToString("")
																, data.DATA_CYCLE_TIME.ToString("")
																, data.AD_CYCLE_TIME.ToString("")
																, data.PAGE_WAITING_TIME.ToString("")
																, data.STATUS.ToString("")
																, (data.HIDE == null | data.HIDE == false ? "0" : "1")
																, data.REMARK.ToString("")
																, data.WORKING_TIME.ToString("")
																, data.TIME_ZONE.ToString("9")
																, REG_CODE.ToString()
																, data.SAVE_TYPE.ToString("")
																, data.STATION_CODE.ToString("")
																, data.AD_FRAME_TYPE.ToString("")
																, data.HARDWARE_CODE.ToString("")
																, data.LOGO_URL.ToString("")
																, data.DEVICE_TYPE.ToString("1")
																, (data.IS_MOBILE == false | data.IS_MOBILE == null ? "0" : "1")
																);

						rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();  //db.ExecuteCommand(sql);

						bool initCheck = false;
						if ("U" == data.SAVE_TYPE)
						{
							initCheck = data.DEVICE_CODE == -1 ? true : false;
							data.IS_MOBILE = data.IS_MOBILE == null ? false : data.IS_MOBILE;
						}

						if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
						{
							throw new Exception(rtnData.ERROR_MESSAGE);
						}
						KEYWORD_SAVE_DATA keyword = new KEYWORD_SAVE_DATA
						{
							DEVICE_CODE = data.DEVICE_CODE == null || data.DEVICE_CODE < 0 ? Convert.ToInt64(rtnData.DATA) : data.DEVICE_CODE,
							REG_CODE = data.INSERT_CODE,
							KEYWORDS = data.KEYWORDS
						};
						if (data.KEYWORDS != null)
						{
							rtnMsg = new AdvertisingService().KeywordSave(ref db, keyword);
							if (!string.IsNullOrEmpty(rtnMsg))
							{
								throw new Exception(rtnMsg);
							}
						}

						RTN_SAVE_DATA rtn2 = new RTN_SAVE_DATA();

						if (data.devicePlaceList != null)
						{
							rtn2 = new AdvertisingService().DevicePlaceSave2(ref db, data.devicePlaceList, keyword.DEVICE_CODE, REG_CODE);

						}
						else if (true == initCheck && false == data.IS_MOBILE)
						{
							rtn2 = new AdvertisingService().InitDevicePlace(ref db, data, keyword.DEVICE_CODE, REG_CODE);
						}

						if (!string.IsNullOrEmpty(rtn2.ERROR_MESSAGE))
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
        /// <summary>
        /// 로컬박스별 지역등록리스트 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AD_REGION> GetDeviceRegionList(PAGE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetDeviceRegionList"
                                                                , Cond.PAGE_COUNT.ToString("10")
                                                                , Cond.PAGE.ToString("1")
                                                                , Cond.CODE.ToString("")

                                                                );
            return db.ExecuteQuery<AD_REGION>(sql).ToList();
        }

        public RTN_SAVE_DATA DeviceRegionSave(List<DEVICE_REGION> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (DEVICE_REGION data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "DeviceRegionSave"
                                                               , data.MRC_EDIT_MODE.ToString("")
                                                               , data.DEVICE_CODE.ToString("")
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

        /// <summary>
        /// 선택된 주소 지역구 키워드 자동 저장
        /// </summary>
        /// <param name="Cond"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA DeviceRegionAutoSave(CODE_DATA Cond, int? REG_CODE)
        {
            long? DEVICE_CODE = Cond.CODE;
            Cond.CODE = null;
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            if (DEVICE_CODE == null)
            {
                return rtnData;
            }
            var data = new KeywordService().GetLocalNameList(Cond).FirstOrDefault();

            if (data == null)
            {
                rtnData.ERROR_MESSAGE = "해당 지역을 선택하여 주시기 바랍니다.";
                return rtnData;
            }
            List<DEVICE_REGION> Paramlist = new List<DEVICE_REGION>();
            Paramlist.Add(new DEVICE_REGION { MRC_EDIT_MODE = "ALL_D", DEVICE_CODE = (long)DEVICE_CODE, SEARCH_CATEGORY_CODE = data.SEARCH_CODE });
            rtnData = this.DeviceRegionSave(Paramlist, REG_CODE);

            Paramlist = new List<DEVICE_REGION>();
            Paramlist.Add(new DEVICE_REGION { DEVICE_CODE = (long)DEVICE_CODE, SEARCH_CATEGORY_CODE = data.SEARCH_CODE });
            rtnData = this.DeviceRegionSave(Paramlist, REG_CODE);
            return rtnData;
        }

        public List<DEVICE_LIST> GetDeviceList(DEVICE_LIST_COND Cond)
        {
            Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "C3.REF_DATA2, A.AUTH_NUMBER, A.UPDATE_DATE DESC" : Cond.SORT;
            //string[] arrSort = Cond.SORT.Split(' ');
            //string sort = "A.UPDATE_DATE DESC";
            //switch(arrSort[0].ToUpper())
            //{

            //    case "BUSI_TYPE_NAME":
            //        sort = "C1.NAME " + arrSort[1].ToUpper();
            //        break;
            //    case "BUSI_TYPE_NAME2":
            //        sort = "C2.NAME " + arrSort[1].ToUpper();
            //        break;
            //    case "STATUS_NAME":
            //        sort = "C3.NAME " + arrSort[1].ToUpper();
            //        break;
            //    case "STORE_NAME":
            //        sort = "B.STORE_NAME " + arrSort[1].ToUpper();
            //        break;
            //    case "ADDRESS":
            //        sort = "A.ADDRESS1 + A.ADDRESS2  " + arrSort[1].ToUpper();
            //        break;
            //    case "UPDATE_NAME":
            //        sort = "M.USER_NAME  " + arrSort[1].ToUpper();
            //        break;
            //    case "GROUP_NAME":
            //        sort = "SG.GROUP_NAME  " + arrSort[1].ToUpper();
            //        break;
            //    default:
            //        sort = "A." + arrSort[0].ToUpper() + " " + arrSort[1].ToUpper();
            //        break;
            //},,16

            if(Cond.STATION_CODE != null)
            {
                Cond.SORT = "ISNULL(CASE WHEN DSP.DISTANCE - DSP.RADIUS <= 0 THEN - 1 * (DSP.RADIUS) * 100 - (DSP.RADIUS - DISTANCE)  ELSE DSP.DISTANCE - DSP.RADIUS END, 9999999999999999)";
            }

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetDeviceList2"
                                                              , Cond.PAGE_COUNT.ToString("10")
                                                              , Cond.PAGE.ToString("1")
                                                              , Cond.SORT
                                                              , Cond.LATITUDE.ToString("37.5609447")
                                                              , Cond.LONGITUDE.ToString("126.9795475")
															  , Cond.COMPANY_CODE.ToString("")
                                                              , Cond.STORE_CODE.ToString("")
                                                              , Cond.GROUP_CODE.ToString("")
                                                              , Cond.DEVICE_CODE.ToString("")
                                                              , Cond.DEVICE_NAME.ToString("")
															  , Cond.STORE_DEVICE_NAME.ToString("")
                                                              , Cond.BUSI_TYPE.ToString("")
                                                              , Cond.BUSI_TYPE2.ToString("")
                                                              , Cond.STATUS.ToString("")
                                                              , Cond.CONTACT_CODE.ToString("")
                                                              , Cond.AUTH_YN.ToString("")
                                                              , Cond.SEARCH_CATEGORY_CODE.ToString("")
                                                              , Cond.STORE_NAME.ToString("")
                                                              , Cond.CONTACT_DEPT_CODE.ToString("")
                                                              , Cond.CONTACT_DEPT_SEARCH.ToString("")
                                                              , Cond.CONTACT_PARENT_MEMBER_CODE.ToString("")
                                                              , Cond.NOT_DEVICE_CODE.ToString("")
                                                              , Cond.STATION_CODE.ToString("")
                                                              , Cond.STATION_NAME.ToString("")
                                                              , Cond.HIDE == null ? "" : (Cond.HIDE == true ? "1" : "0")
                                                              , Cond.IS_VIRTUAL_DEVICE == null ? "" : (Cond.IS_VIRTUAL_DEVICE == true ? "1" : "0")
                                                              , Cond.NOT_AD_CODE.ToString("")
															  , Cond.NOT_MY_AD_CODE.ToString("")
															  , Cond.DEVICE_CONTAINING_AD_CODE.ToString("")
                                                              , Cond.MEMBER_CODE.ToString("")
                                                              , Cond.USER_ID.ToString("")
                                                              , Cond.USER_NAME.ToString("")
															  , Cond.AD_CODE.ToString("")
															  , Cond.MY_AD_CODE.ToString("")
															  , Cond.MY_STORE_CODE.ToString("")
                                                              , Cond.AD_FRAME_TYPE.ToString("")
															  , Cond.REAL_DEVICE.ToString("")
															  , Cond.VIRTUAL_DEVICE_NAME.ToString("")
														   );
            return db.ExecuteQuery<DEVICE_LIST>(sql).ToList();
        }
		/// <summary>                                                                                      
		/// 대표/서브 로컬박스 조회하기
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public List<DEVICE_LIST> GetRelativeDeviceList(DEVICE_LIST_COND Param)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetRelativeDeviceList"
												  , Param.DEVICE_CODE.ToString("")
								);

			return db.ExecuteQuery<DEVICE_LIST>(sql).ToList();
		}
		/// <summary>                                                                                      
		/// 대표/서브 로컬박스 삭제
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>	
		public RTN_SAVE_DATA RelativeDeviceDelete(List<T_DEVICE> list, long? RELATIVE_DEVICE_CODE)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			string sql = null;

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					foreach (T_DEVICE Param in list)
					{

						sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "RelativeDeviceDelete"
												   , Param.DEVICE_CODE.ToString("")
												   , Param.PARENT_DEVICE_CODE.ToString("")
												   , RELATIVE_DEVICE_CODE.ToString("")
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
		/// 대표/서브 로컬박스 저장
		/// </summary>
		/// <param name="Param"></param>
		/// <returns></returns>
		public RTN_SAVE_DATA RelativeDeviceSave(T_DEVICE Param)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "RelativeDeviceSave"
											   , Param.SELECT_DEVICE_CODE.ToString("")
											   , Param.CUR_DEVICE_CODE.ToString("")
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
		/// <summary>                                                                                      
		/// 가상 로컬 유무 확인
		/// </summary>																					  
		/// <param name="STORE_CODE"></param>																	  
		/// <returns></returns>	
		public long GetVirtualDevice(int? STORE_CODE)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetVirtualDevice"
												, STORE_CODE												
								);
			
			return db.ExecuteQuery<long>(sql).FirstOrDefault();
		}
		/// <summary>
		/// 가상 로컬 박스 추가
		/// </summary>
		/// <param name="Param"></param>
		/// <returns></returns>
		public RTN_SAVE_DATA AddVirtulaDevice(T_DEVICE Param)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "AddVirtulaDevice"
														, Param.DEVICE_CODE
														, Param.PARENT_DEVICE_CODE
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
		/// <summary>
		/// 가상 로컬 박스 추가
		/// </summary>
		/// <param name="Param"></param>
		/// <returns></returns>
		public RTN_SAVE_DATA InitVirtualDevice(T_DEVICE Param, int? REG_CODE)
		{
			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { DATA = "" };
			string rtnMsg = string.Empty;

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "InitVirtualDevice"
																, Param.DEVICE_CODE.ToString("")
																, Param.COMPANY_CODE.ToString("")
																, Param.STORE_CODE.ToString("")
																, Param.MEMBER_CODE.ToString("")
																, Param.DEVICE_NAME.ToString("")
																, Param.DEVICE_DESC.ToString("")
																, Param.GROUP_CODE.ToString("")
																, Param.BUSI_TYPE.ToString("")
																, Param.BUSI_TYPE2.ToString("")
																, Param.CONTACT_COMPANY_CODE.ToString("")
																, Param.CONTACT_STORE_CODE.ToString("")
																, Param.CONTACT_CODE.ToString("")
																, Param.CONTACT_NAME.ToString("")
																, Param.CONTACT_PHONE.ToString("")
																, Param.CONTACT_EMAIL.ToString("")
																, Param.AD_DISTANCE.ToString("")
																, Param.STATUS.ToString("")
																, (Param.HIDE == null | Param.HIDE == false ? "0" : "1")
																, Param.REMARK.ToString("")
																, Param.TIME_ZONE.ToString("9")
																, REG_CODE.ToString()
																, Param.LOGO_URL.ToString("")
												);

					rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();

					if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
					{
						throw new Exception(rtnData.ERROR_MESSAGE);
					}

					Param.DEVICE_CODE = rtnData.DATA.ToLong();

					KEYWORD_SAVE_DATA keyword = new KEYWORD_SAVE_DATA
					{
						DEVICE_CODE = Param.DEVICE_CODE == null || Param.DEVICE_CODE < 0 ? Convert.ToInt64(rtnData.DATA) : Param.DEVICE_CODE,
						REG_CODE = Param.INSERT_CODE,
						KEYWORDS = Param.KEYWORDS
					};
					if (Param.KEYWORDS != null)
					{
						rtnMsg = new AdvertisingService().KeywordSave(ref db, keyword);
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
				rtnData.ERROR_MESSAGE = ex.Message;
			}
			return rtnData;
		}
		/// <summary>                                                                                      
		/// 대표/서브로 지정할 로컬박스 조회
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public List<DEVICE_LIST> GetRelativeDevice(DEVICE_LIST_COND Param)
		{
			Param.SORT = string.IsNullOrEmpty(Param.SORT) ? "D.DEVICE_CODE" : Param.SORT;

			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetRelativeDeviceListPop"
												, Param.PAGE.ToString("1")
												, Param.PAGE_COUNT.ToString("10")
												, Param.SORT
												, Param.DEVICE_CODE.ToString("")					
												, Param.STORE_CODE.ToString("")
												, Param.MEMBER_CODE.ToString("")
												, Param.DEVICE_NAME.ToString("")
								);

			return db.ExecuteQuery<DEVICE_LIST>(sql).ToList();
		}
		#region 로컬박스메인정보
		/// <summary>
		/// 로컬박스별 메인(Owner)광고 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public List<T_DEVICE_MAIN> GetDeviceMainList(T_DEVICE_MAIN_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "GetDeviceMainList"
                                                            , Cond.DEVICE_CODE.ToString("")
                                                            , Cond.SEQ.ToString("")
                                                            , Cond.PUBLIC_TYPE.ToString("")
                                                            , Cond.GROUP_SEQ.ToString("")
                                                            , (Cond.HIDE == null ? "" : (Cond.HIDE == true) ? "1" : "0")

                                                         );
            return db.ExecuteQuery<T_DEVICE_MAIN>(sql).ToList();
        }

        /// <summary>
        /// 로컬박스정보 메인저장
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA DeviceMainSave(T_DEVICE_MAIN Param, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                Param.FR_DATE = Param.FR_DATE.RemoveDateString();
                Param.TO_DATE = Param.TO_DATE.RemoveDateString();
                if (Param.SAVE_TYPE.ToString("") == "D")
                {
                    rtnData = this.DeviceMainDelete(Param);
                }
                else
                {
                    rtnData = this.DeviceMainSave2(Param, REG_CODE);
                }

                if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                {
                    throw new Exception(rtnData.ERROR_MESSAGE);
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }
            return rtnData;
        }



        /// <summary>
        /// 로컬박스메인 저장
        /// </summary>
        /// <param name="Param"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA DeviceMainSave2(T_DEVICE_MAIN Param, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainSave"
                                                            , Param.DEVICE_CODE.ToString()
                                                            , Param.SEQ.ToString()
                                                            , Param.TITLE.ToString("")
                                                            , Param.SUB_TITLE.ToString("")
                                                            , Param.BANNER_TYPE.ToString("3")
                                                            , Param.CONTENT_TYPE.ToString("0")
                                                            , Param.PUBLIC_TYPE.ToString("3")
                                                            , Param.CONTENT.ToString("")
                                                            , Param.CONTENT_DETAIL.ToString("")
                                                            , Param.REF_DATA1.ToString("")
                                                            , Param.REF_DATA2.ToString("")
                                                            , Param.REF_DATA3.ToString("")
                                                            , Param.REF_DATA4.ToString("")
                                                            , Param.REMARK.ToString("")
                                                            , Param.HIDE.ToBooleanString()
                                                            , REG_CODE.ToString("0")
                                                            , Param.FR_DATE.ToString("")
                                                            , Param.TO_DATE.ToString("")
                                                            , Param.FR_TIME.ToString("")
                                                            , Param.TO_TIME.ToString("")
                                                            , Param.GROUP_SEQ.ToString("1")
                                                        );
                    rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();

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
        /// 로컬박스메인정보 삭제
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>

        public RTN_SAVE_DATA DeviceMainDelete(T_DEVICE_MAIN Param)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainDelete"
                                                            , Param.DEVICE_CODE.ToString()
                                                            , Param.SEQ.ToString()
                                                            , Param.UPDATE_CODE.ToString()

                                                        );
                    db.ExecuteCommand(sql);

                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }
            return rtnData;
        }

        public RTN_SAVE_DATA DeviceMainSeqChange(T_DEVICE_MAIN_SEQ_CHANGE Param)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainSeqChange"
                                                            , Param.DEVICE_CODE.ToString()
                                                            , Param.PRE_SEQ.ToString()
                                                            , Param.SEQ.ToString()
                                                            , Param.REG_CODE.ToString()

                                                        );
                    db.ExecuteCommand(sql);

                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }
            return rtnData;
        }

        public RTN_SAVE_DATA DeviceMainGroupSeqChange(T_DEVICE_MAIN_SEQ_CHANGE Param)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMainGroup.xml", "DeviceMainGroupSeqChange"
                                                            , Param.DEVICE_CODE.ToString()
                                                            , Param.PRE_SEQ.ToString()
                                                            , Param.SEQ.ToString()
                                                            , Param.REG_CODE.ToString()

                                                        );
                    db.ExecuteCommand(sql);

                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }
            return rtnData;
        }

        #endregion >> 로컬박스메인정보

        /// <summary>
        /// 로컬박스 광고데이터 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<LOGGAL_AD_DATA> GetDailyLoggalAdList(LOGGAL_AD_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetDailyLoggalAdList"
                                            , Cond.DEVICE_NUMBER.ToString("")
                                            , Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
                                            , (Cond.UPDATE_DATE == null ? "1901-01-01 00:00:00" : Convert.ToDateTime(Cond.UPDATE_DATE).ToString("yyyy-MM-dd HH:mm:ss.fff"))
                                            , Cond.DEVICE_CODE.ToString("-1")
                                            , Cond.USER_ID.ToString("")
                                            , Cond.AD_CODE.ToString("")

                                           );
            return db.ExecuteQuery<LOGGAL_AD_DATA>(sql).ToList();
        }

        /// <summary>
        /// 로컬박스에서 광고 클릭시 업데이트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA AD_OpenPage(LOGGAL_AD_COND Cond)
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
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "AdClickSave"
                                            , Cond.DEVICE_NUMBER.ToString("")
                                            , Cond.AD_CODE.ToString("")
                                            );

                    db.ExecuteCommand(sql);
                    tran.Complete();
                    rtn.RETURN_URL = Global.ConfigInfo.MANAGEMENT_SITE.ToString("") + "/advertise/contentview/" + Cond.AD_CODE.ToString("");
                }

            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;

        }

        /// <summary>
        /// 로컬박스에서 광고 클릭시 업데이트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        


        /// <summary>
        /// 로컬박스 광고데이터 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<LOGGAL_MAIN_CONTENTLIST> GetloggalBoxMainAPIList(T_DEVICE_COND Cond, string MANAGEMENT_SITE = "")
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetloggalBoxMainAPIList"
                                            , Cond.DEVICE_NUMBER.ToString("")
                                            , (Cond.UPDATE_DATE == null ? "1901-01-01 00:00:00" : Convert.ToDateTime(Cond.UPDATE_DATE).ToString("yyyy-MM-dd HH:mm:ss.fff"))
                                            , MANAGEMENT_SITE
                                           );
            return db.ExecuteQuery<LOGGAL_MAIN_CONTENTLIST>(sql).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public long GetloggalBoxAuthNumber(T_DEVICE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetloggalBoxAuthNumber"
                                         , Cond.DEVICE_NUMBER.ToString("")
                                         , Cond.AUTH_TYPE.ToString("2")
                                        );
            return db.ExecuteQuery<long>(sql).FirstOrDefault();
        }

        /// <summary>
        /// 로컬박스에서 광고 클릭시 업데이트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA loggalBoxAuthReg(T_DEVICE_COND Cond, int? REG_CODE)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "loggalBoxAuthReg"
                                            , Cond.SAVE_MODE.ToString("")
                                            , Cond.DEVICE_CODE.ToString("")
                                            , Cond.AUTH_NUMBER.ToString("")
                                            , REG_CODE.ToString("0")
                                            , Cond.AUTH_TYPE.ToString("2")
                                            , Cond.SIGN_CODE.ToString("")
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

        public bool checkDeviceAuth(T_DEVICE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "checkDeviceAuth"
                                          , Cond.DEVICE_NUMBER.ToString("-1")
                                          , Cond.AUTH_NUMBER.ToString("")
                                          , Cond.AUTH_TYPE.ToString("2")
                                          );
            int nCnt = db.ExecuteQuery<int>(sql).FirstOrDefault();

            if (nCnt == 0) return false;
            else return true;
        }

        public List<T_DEVICE_UPDATE> GetDeviceUpdateInfo(T_DEVICE_UPDATE Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetDeviceUpdateInfo"
                                            , Cond.TABLE_NAME.ToString("")
                                            , Cond.DEVICE_NUMBER.ToString("")
                                           );
            return db.ExecuteQuery<T_DEVICE_UPDATE>(sql).ToList();
        }



        public RTN_DEVICE_INFO_DATA GetDeviceUpdateAlllist(List<DEVICE_INFO_COND> Condlist, string MANAGEMENT_SITE = "")
        {
            RTN_DEVICE_INFO_DATA rtnData = new RTN_DEVICE_INFO_DATA();
            foreach (DEVICE_INFO_COND Cond in Condlist)
            {
                Cond.UPDATE_DATE = (Cond.UPDATE_DATE == null ? Convert.ToDateTime("1901-01-01") : Cond.UPDATE_DATE);
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetDeviceUpdateInfo"
                                          , Cond.TABLE_NAME.ToString("")
                                          , Cond.DEVICE_NUMBER.ToString("")
                                          , Convert.ToDateTime(Cond.UPDATE_DATE).ToString("yyyy-MM-dd HH:mm:ss.fff")
                                         );

                List<T_DEVICE_UPDATE> updatelist = db.ExecuteQuery<T_DEVICE_UPDATE>(sql).ToList();

                foreach (var data in updatelist)
                {
                    rtnData.DEVICE_CODE = data.DEVICE_CODE;
                    rtnData.DEVICE_NAME = data.DEVICE_NAME;
                    if (data.TABLE_NAME.ToUpper() == "T_AD")
                    {
                        rtnData.AD_LIST = this.GetDailyLoggalAdList(new LOGGAL_AD_COND { DEVICE_CODE = Cond.DEVICE_CODE, DEVICE_NUMBER = data.DEVICE_NUMBER, UPDATE_DATE = data.UPDATE_DATE });
                        rtnData.AD_LIST = rtnData.AD_LIST == null ? new List<LOGGAL_AD_DATA>() : rtnData.AD_LIST;
                    }
                    else if (data.TABLE_NAME.ToUpper() == "T_DEVICE_MAIN")
                    {
                        rtnData.MAIN_LIST = this.GetloggalBoxMainAPIList(new T_DEVICE_COND { DEVICE_NUMBER = data.DEVICE_NUMBER, UPDATE_DATE = data.UPDATE_DATE }, MANAGEMENT_SITE);
                        rtnData.MAIN_LIST = rtnData.MAIN_LIST == null ? new List<LOGGAL_MAIN_CONTENTLIST>() : rtnData.MAIN_LIST;
                    }
                }

            }
            return rtnData;
        }


        public List<LOGGAL_AD_DATA> GetFavoriteNationalInfoADList(string deviceNumber)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetFavoriteNationalInfoADList"
                                            , deviceNumber
                                            , Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
                                            , "1901-01-01 00:00:00"
                                           );
            return db.ExecuteQuery<LOGGAL_AD_DATA>(sql).ToList();
        }


        /// <summary>
        /// 지역별 로컬박스정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<DEVICE_LOCATION> GetDeviceLocation(DEVICE_LOCATION_COND Cond)
        {
            if (Cond.SEARCH_CODE == "")
            {
                var data = new KeywordService().GetLocalNameList(new CODE_DATA { NAME = Cond.LOCATION_NAME }).FirstOrDefault();
                Cond.SEARCH_CODE = data.SEARCH_CODE;
            }
            string queryFunction = string.Empty;
            Cond.MODE = string.IsNullOrEmpty(Cond.MODE) ? string.Empty : Cond.MODE;
            Cond.USER_ID = string.IsNullOrEmpty(Cond.USER_ID) ? "-1" : Cond.USER_ID;
            string sql = string.Empty;
            if (Cond.STATION_CODE == null)
            {
                if (Cond.MODE.ToLower() == "map")
                {
                    sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetDeviceLocationMap"
                                               , Cond.LATITUDE.ToString("")
                                               , Cond.LONGITUDE.ToString("")
                                               , Cond.PAGE.ToString("1")
                                               , Cond.PAGE_COUNT.ToString("30")
                                               , Cond.SEARCH_CODE.ToString("")
                                               , Cond.DISTANCE.ToString("")
                                               , Cond.COMPANY_CODE.ToString("")
                                               , Cond.SEARCH_TEXT.ToString("")
                                               , Cond.STATION_CODE.ToString("")
                                               , Cond.USER_ID
                                          );
                }
                else
                {
                    sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetDeviceLocation"
                                               , Cond.LATITUDE.ToString("")
                                               , Cond.LONGITUDE.ToString("")
                                               , Cond.PAGE.ToString("1")
                                               , Cond.PAGE_COUNT.ToString("30")
                                               , Cond.SEARCH_CODE.ToString("")
                                               , Cond.DISTANCE.ToString("")
                                               , Cond.COMPANY_CODE.ToString("")
                                               , Cond.SEARCH_TEXT.ToString("")
                                               , Cond.STATION_CODE.ToString("")
                                               , Cond.USER_ID
                                          );
                }
            }
            else
            {
                if (Cond.STATION_CODE == null)
                {
                    Cond.SORT = "CASE WHEN DISTANCE < MIN_DISTANCE OR MIN_DISTANCE = 0 THEN DISTANCE ELSE MIN_DISTANCE END, DEVICE_CODE DESC";
                }
                else Cond.SORT = "STATION_DISTANCE, DEVICE_NAME";
                sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi.xml", "GetDeviceLocationDetail"
                                          , Cond.LATITUDE.ToString("")
                                          , Cond.LONGITUDE.ToString("")
                                          , Cond.PAGE.ToString("1")
                                          , Cond.PAGE_COUNT.ToString("30")
                                          , Cond.STATION_CODE.ToString("")
                                          , Cond.SORT
                                          , Cond.SEARCH_CODE.ToString("")
                                          , Cond.DISTANCE.ToString("")
                                          , Cond.COMPANY_CODE.ToString("")
                                          , Cond.SEARCH_TEXT.ToString("")
                                          , Cond.USER_ID
                                          
                                     );
            }
            return db.ExecuteQuery<DEVICE_LOCATION>(sql).ToList();
        }

        public List<DEVICE_MAIN_SHARE_LIST> GetDeviceMainShareList(DEVICE_MAIN_SHARE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml"
                                        , "GetDeviceMainShareList"
                                        , Cond.PAGE.ToString("1")
                                        , Cond.PAGE_COUNT.ToString("15")
                                        , Cond.DEVICE_CODE.ToString("-1")
                                        , Cond.TITLE.ToString("")
                                        , Cond.REP_CATEGORY_CODE.ToString("")
                                        , Cond.SHARE_STATUS.ToString("0")
                                   );
            return db.ExecuteQuery<DEVICE_MAIN_SHARE_LIST>(sql).ToList();
        }

        public RTN_SAVE_DATA DeviceMainShareReqSave(DEVICE_MAIN_SHARE_REQ_SAVE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (DEVICE_MAIN_SHARE_REQ_SAVE_DETAIL data in Param.list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainShareReqSave"
                                                , data.DEVICE_CODE
                                                , data.AD_CODE
                                                , Param.TARGET_DEVICE_CODE
                                                , Param.REG_CODE
                                                );
                        rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
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



        public List<DEVICE_MAIN_SHARE_APPROVAL_LIST> GetDeviceMainShareApprovalList(DEVICE_MAIN_SHARE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "GetDeviceMainShareApprovalList"
                                        , Cond.PAGE.ToString("1")
                                        , Cond.PAGE_COUNT.ToString("15")
                                        , Cond.SORT
                                        , Cond.DEVICE_CODE.ToString("")
                                        , Cond.AD_CODE.ToString("")
                                        , Cond.TITLE.ToString("")
                                        , Cond.REP_CATEGORY_CODE.ToString("")
                                        , Cond.SHARE_STATUS.ToString("")
                                        , Cond.COMPANY_CODE.ToString("")
                                        , Cond.MNG_COMPANY_CODE.ToString("")
                                        , Cond.COMPANY_NAME.ToString("")
                                        , Cond.DEVICE_NAME.ToString("")
                                        , Cond.SHARE_COMPANY_CODE.ToString("")
                                        , Cond.AD_STORE_CODE.ToString("")
                                        , Cond.DEVICE_STORE_CODE.ToString("")
                                   );
            return db.ExecuteQuery<DEVICE_MAIN_SHARE_APPROVAL_LIST>(sql).ToList();
        }


        public RTN_SAVE_DATA DeviceMainShareApprovalSave(DEVICE_MAIN_SHARE_APPROVAL_SAVE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (DEVICE_MAIN_SHARE_APPROVAL_SAVE_LIST data in Param.list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainShareApprovalSave"
                                                , data.DEVICE_CODE
                                                , data.AD_CODE
                                                , Param.SHARE_STATUS
                                                , Param.REG_CODE
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
        public RTN_SAVE_DATA DeviceMainApprovalCancel(DEVICE_MAIN_SHARE_APPROVAL_SAVE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (DEVICE_MAIN_SHARE_APPROVAL_SAVE_LIST data in Param.list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainApprovalCancel"
                                                , data.DEVICE_CODE
                                                , data.AD_CODE
                                                , Param.SHARE_STATUS
                                                , Param.REG_CODE
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


        public List<DEVICE_MAIN_SHARE_TOTAL_LIST> GetDeviceMainShareTotalList(DEVICE_MAIN_SHARE_TOTAL_COND Cond, List<T_COMMON> commonList)
        {
            Cond.GUBUN = Cond.GUBUN.ToString("");
            StringBuilder sbSql = new StringBuilder();
            for (int i = 0; i < commonList.Count(); i++)
            {
                sbSql.Append("    ,  SUM(CASE WHEN B.STATUS = ").Append(commonList[i].SUB_CODE).Append(" THEN 1 ELSE 0 END) AS CNT").Append((i + 1).ToString());
            }

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "GetDeviceMainShareTotalList"
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
            return db.ExecuteQuery<DEVICE_MAIN_SHARE_TOTAL_LIST>(sql).ToList();
        }

        public RTN_SAVE_DATA deviceMainShareSave(DEVICE_MAIN_SHARE_SAVE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    foreach (var deviceData in Param.DEVICE_LIST)
                    {
                        foreach (var mainData in Param.MAIN_LIST)
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainShareReqSave"
                                                    , mainData.ORI_DEVICE_CODE
                                                    , mainData.AD_CODE
                                                    , deviceData.DEVICE_CODE
                                                    , Param.REG_CODE
                                                    );
                            rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                        }
                    }

                    //foreach (DEVICE_MAIN_SHARE_REQ_SAVE_DETAIL data in Param.list)
                    //{
                    //    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "DeviceMainShareReqSave"
                    //                            , data.DEVICE_CODE
                    //                            , data.AD_CODE
                    //                            , Param.TARGET_DEVICE_CODE
                    //                            , Param.REG_CODE
                    //                            );
                    //    rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                    //}
                    tran.Complete();
                }

            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }


        public List<APPROVAL_GRAPE> GetApprovalGrape(APPROVAL_GRAPE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\adDevice.xml", "GetApprovalGrape"
                                        , Cond.COMPANY_CODE.ToString("")
                                        , Cond.MNG_COMPANY_CODE.ToString("")

                                   );
            return db.ExecuteQuery<APPROVAL_GRAPE>(sql).ToList();
        }

        public List<AD_SHARE_DEVICE_MAIN> GetAdShareDeviceList(AD_SHARE_DEVICE_MAIN_COND Cond)
        {
            
            string sql = string.Empty;
            if (Cond.GUBUN == 0)
            {
                Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "A.SHARE_STATUS, B.DEVICE_NAME" : Cond.SORT;
                sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "GetAdShareDeviceList"
                                               , Cond.PAGE
                                               , Cond.PAGE_COUNT
                                               , Cond.AD_CODE
                                               , Cond.SORT
                                               , Cond.DEVICE_NAME.ToString("")
                                   );
            }
            else
            {
                Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "B.DEVICE_NAME" : Cond.SORT;
                sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "GetAdShareDeviceList2"
                                               , Cond.PAGE
                                               , Cond.PAGE_COUNT
                                               , Cond.AD_CODE
                                               , Cond.SORT
                                               , Cond.DEVICE_NAME.ToString("")
                                   );
            }
            return db.ExecuteQuery<AD_SHARE_DEVICE_MAIN>(sql).ToList();
        }


        public List<AD_SHARE_SIGNAGE> GetAdShareSignageList(AD_SHARE_SIGNAGE_COND Cond)
        {

            string sql = string.Empty;
         
                Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "D.SIGN_NAME" : Cond.SORT;
                sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMain.xml", "GetAdShareSignageList"
                                                                , Cond.PAGE
                                                                , Cond.PAGE_COUNT
                                                                , Cond.AD_CODE
                                                                , Cond.SORT
                                                                , Cond.SIGN_NAME.ToString("")
                                                             );
            return db.ExecuteQuery<AD_SHARE_SIGNAGE>(sql).ToList();
        }


        public List<T_DEVICE_STATION> GetDeviceStationList(T_DEVICE_STATION_COND Cond)
        {
            Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "A.SHARE_STATUS, B.DEVICE_NAME" : Cond.SORT;
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStation.xml", "GetDeviceStationList"
                                               , Cond.PAGE.ToString("1")
                                               , Cond.PAGE_COUNT.ToString("15")
                                               , Cond.SORT.ToString("A.STATION_CODE")
                                               , Cond.STATION_CODE.ToString("")
                                               , Cond.STATION_NAME.ToString("")
                                               , Cond.CATEGORY_CODE.ToString("")
                                               , Cond.HIDE == null ? "" : (Cond.HIDE == true ? "1" : "0")
                                   );
            return db.ExecuteQuery<T_DEVICE_STATION>(sql).ToList();
        }


        public List<T_DEVICE_STATION> GetDeviceStationMapList(T_DEVICE_STATION_COND Cond)
        {
            Cond.SORT = string.IsNullOrEmpty(Cond.SORT) ? "A.SHARE_STATUS, B.DEVICE_NAME" : Cond.SORT;
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStation.xml", "GetDeviceStationMapList"
                                               , Cond.PAGE.ToString("1")
                                               , Cond.PAGE_COUNT.ToString("15")
                                               , Cond.SORT.ToString("A.STATION_CODE")
                                               , Cond.STATION_CODE.ToString("")
                                               , Cond.STATION_NAME.ToString("")
                                               , Cond.CATEGORY_CODE.ToString("")
                                               , Cond.HIDE == null ? "" : (Cond.HIDE == true ? "1" : "0")
                                   );
            return db.ExecuteQuery<T_DEVICE_STATION>(sql).ToList();
        }



        /// <summary>                                                                                      
        /// T_DEVICE_STATION 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA DeviceStationSave(T_DEVICE_STATION Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    if (Param.SAVE_TYPE != "D")
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStation.xml", "DeviceStationSave"
                                                        , Param.STATION_CODE.ToString("-1")
                                                        , Param.STATION_NAME.ToString("")
                                                        , Param.CATEGORY_CODE.ToString("")
                                                        , Param.ADDRESS1.ToString("")
                                                        , Param.ADDRESS2.ToString("")
                                                        , Param.ZIP_CODE.ToString("")
                                                        , Param.LATITUDE.ToString("")
                                                        , Param.LONGITUDE.ToString("")
                                                        , Param.STATION_DESC.ToString("")
                                                        , Param.REAMRK.ToString("")
                                                        , Param.HIDE == true ? "1" : "0"
                                                        , Param.INSERT_CODE.ToString("")
                                                        , Param.LOGO_URL.ToString("")

                          );
                        rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                        if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
                            throw new Exception(rtn.ERROR_MESSAGE);

                    }
                    else
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStation.xml", "DeviceStationDelete"
                                                        , Param.STATION_CODE.ToString("-1")
                                                        , Param.INSERT_CODE.ToString("")

                          );
                        rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
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

        public RTN_SAVE_DATA deviceStationCodeUpdate(DEVICE_STATION_UPDATE Param)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStation.xml", "deviceStationCodeUpdate"
                                                       , Param.DEVICE_CODES.ToString("")
                                                       , Param.STATION_CODE.ToString("")
                                                       , Param.UPDATE_CODE.ToString("")

                         );
                    rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }

        public List<DAUM_MAPLIST> GetDeviceMapList(DAUM_MAP_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "GetDeviceMapList"
                                                      , Cond.TITLE.ToString("")
                        );
            return db.ExecuteQuery<DAUM_MAPLIST>(sql).ToList();
        }

        #region >> 
        /// <summary>                                                                                      
        /// T_DEVICE_STATION_PLACE 저장하기(로컬박스장소 - T_DEVICE_STATION_PLACE 저장 -  saveparam Query)										  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA DeviceStationPlaceSave(List<T_DEVICE_STATION_PLACE> list, int? STATION_CODE, bool? IS_RANGE, int? REG_CODE)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                list = list == null ? new List<T_DEVICE_STATION_PLACE>() : list;
                using (TransactionScope tran = new TransactionScope())
                {


                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStationPlace.xml", "DeviceStationPlaceDelete", STATION_CODE.ToString("0"), IS_RANGE == true ? "1" : "0");
                    rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                    foreach (T_DEVICE_STATION_PLACE Param in list)
                    {

                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStationPlace.xml", "DeviceStationPlaceSave"
                                                       , Param.IDX.ToString("")
                                                       , STATION_CODE.ToString("0")
                                                       , IS_RANGE == true ? "1" : "0"
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
        /// T_DEVICE_STATION_PLACE 조회하기	(스테이션장소 - T_DEVICE_STATION_PLACE 저장 -  selectparam Query)					      
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_DEVICE_STATION_PLACE> GetDeviceStationPlaceList(T_DEVICE_STATION_PLACE_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceStationPlace.xml", "GetDeviceStationPlaceList"
                                                     , Param.IDX.ToString("")
                                                     , Param.STATION_CODE.ToString("")
                                                     , Param.CK_CODE.ToString("")
                                                     , Param.REGION.ToString("")
                                                     , Param.JIBUN_ADDRESS.ToString("")
                                                     , Param.ROAD_ADDRESS.ToString("")
                                                     , Param.BUILDING.ToString("")
                                                     , Param.ZIP_CODE.ToString("")
                                                     , (Param.IS_RANGE == null ? "" : (Param.IS_RANGE == true ? "1" : "0"))
                            );
            return db.ExecuteQuery<T_DEVICE_STATION_PLACE>(sql).ToList();
        }
        #endregion

        /// <summary>
        /// 로컬박스별 모바일배너 공유정보
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AD_DEVICE_SHARE_LIST> GetAdDeviceShareList(AD_DEVICE_SHARE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetAdDeviceShareList"
                                                     , Cond.DEVICE_CODE.ToString("")


                       );
            return db.ExecuteQuery<AD_DEVICE_SHARE_LIST>(sql).ToList();
        }



        /// <summary>                                                                                      
        /// T_DEVICE_MAIN_GROUP 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_DEVICE_MAIN_GROUP> GetDeviceMainGroupList(T_DEVICE_MAIN_GROUP Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMainGroup.xml", "GetDeviceMainGroupList"
                                            , Param.DEVICE_CODE.ToString()
                                            , Param.GROUP_SEQ.ToString("")
                                            , (Param.HIDE == null ? "" : (Param.HIDE == false ? "0" : "1"))
                                              );
            return db.ExecuteQuery<T_DEVICE_MAIN_GROUP>(sql).ToList();
        }

        /// <summary>                                                                                      
        /// T_DEVICE_MAIN_GROUP 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA DeviceMainGroupSave(List<T_DEVICE_MAIN_GROUP> saveList,int? REG_CODE)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                string sql = string.Empty;
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_DEVICE_MAIN_GROUP data in saveList)
                    {
                        if (data.MRC_EDIT_MODE == "D")
                        {
                            sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMainGroup.xml"
                                            , "DeviceMainGroupDelete"
                                            , data.DEVICE_CODE.ToString("-1")
                                            , data.GROUP_SEQ.ToString("-1")
                                            , REG_CODE.ToString("0")
                                            );
                        }
                        else
                        {
                            sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\DeviceMainGroup.xml"
                                            , "DeviceMainGroupSave"
                                            , data.DEVICE_CODE.ToString("-1")
                                            , data.GROUP_SEQ.ToString("-1")
                                            , data.GROUP_NAME.ToString("")
                                            , data.FRAME_TYPE.ToString("")
                                            , data.CONTENT_TYPE.ToString("1")
                                            , data.PLAY_TIME.ToString("60")
                                            , data.REMARK.ToString("")
                                            , (data.HIDE == null || data.HIDE == false ? "0" : "1")
                                            , REG_CODE.ToString("0")
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


        public RTN_SAVE_DATA DeviceCopy(long DEVICE_CODE, int? REG_CODE)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                string sql = string.Empty;
                using (TransactionScope tran = new TransactionScope())
                {
                    sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml"
                                           , "DeviceCopy"
                                           , DEVICE_CODE.ToString()
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

		/// <summary>                                                                                      
		/// T_AD_DEVICE_LOG 조회하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public List<T_AD_DEVICE_LOG> GetAdDeviceLogList(T_AD_DEVICE_LOG_COND Param)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\T_AD_DEVICE_LOG.xml", "GetAdDeviceLogList"
											, Param.PAGE_COUNT.ToString("10")
											, Param.PAGE.ToString("1")
											, Param.SORT_ORDER.ToString("A.IDX DESC")
											, Param.AD_CODE.ToString("")
											, Param.TITLE.ToString("")
											, Param.DEVICE_CODE.ToString("")
											, Param.DEVICE_NAME.ToString("")
											, Param.STATUS.ToString("")
											, Param.AD_STORE_CODE.ToString("")
											, Param.DEVICE_STORE_CODE.ToString("")
											, Param.USER_ID.ToString("")
											, Param.USER_NAME.ToString("")
											, Param.FR_DATE.ToString("")
											, Param.TO_DATE.ToString("")
											  ); 
			return db.ExecuteQuery<T_AD_DEVICE_LOG>(sql).ToList();
		}

		/// <summary>                                                                                      
		/// T_AD_DEVICE_LOG 저장하기																	  
		/// </summary>																					  
		/// <param name="Param"></param>																	  
		/// <returns></returns>																			  
		public RTN_SAVE_DATA AdDeviceLogSave(T_AD_DEVICE_LOG Param)
		{

			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\T_AD_DEVICE_LOG.xml", "AdDeviceLogSave"
													, Param.IDX.ToString()
													, Param.AD_DEVICE_CODE.ToString()
													, Param.AD_CODE.ToString("")
													, Param.DEVICE_CODE.ToString("")
													, Param.FR_DATE.ToString("")
													, Param.TO_DATE.ToString("")
													, Param.CLICK_CNT.ToString("")
													, Param.APPROVAL_COMPANY_CODE.ToString("")
													, Param.STATUS.ToString("")
													, Param.REMARK.ToString("")
													, Param.REMARK2.ToString("")

					  );
					rtn.DATA = db.ExecuteQuery<int>(sql).First().ToString();
					tran.Complete();
				}
			}
			catch (Exception ex)
			{
				rtn.ERROR_MESSAGE = ex.Message;
			}
			return rtn;
		}
		public RTN_SAVE_DATA initPlaceItem(T_MEMBER_PLACE_ITEM_COND param, int? REG_CODE)
		{
			RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();

			try
			{
				using (TransactionScope tran = new TransactionScope())
				{
					string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "initPlaceItem"
														   , param.CODE
														   , param.MEMBER_CODE
														   , param.FR_DATE.ToString("").RemoveDateString()
														   , param.TO_DATE.ToString("").RemoveDateString()
														   , param.ITEM_TYPE
														   , (param.INIT_CHECK == false | param.INIT_CHECK == null ? 0 : 1)
														   , param.GROUP_TYPE.ToString()
														   , REG_CODE
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
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "getAllPlaceItem"
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
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "getPlaceItem"
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

						string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Device.xml", "PlaceItemUseSave"
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
	}
}
