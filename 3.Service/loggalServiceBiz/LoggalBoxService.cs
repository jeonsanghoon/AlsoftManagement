using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.loggal;
using ALT.Framework;
using ALT.Framework.Data;
using System.Data.Linq;
using ALT.VO.Common;
using System.Transactions;

namespace loggalServiceBiz
{
    public class LoggalBoxService : BaseService
    {
        public LoggalBoxService() { }
        public LoggalBoxService(DataContext _db) : base(_db) { }

        /// <summary>
        /// loggal box 광고데이터 가져오기 거리순
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<LOGGAL_BOX_DATA> GetLoggalBoxList(LOGGAL_BOX_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi2.xml"
                                        , (Cond.GUBUN == 1) ? "GetLoggalBoxList" : "GetLoggalBoxAddList"
                                        , Cond.DEVICE_CODE.ToString("0")
                                        , Cond.DEVICE_NUMBER.ToString("")
                                        , Global.ConfigInfo.HOSTING_SITE.ToString("")
                                        , Cond.HOST//Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
                                        , Cond.PAGE.ToString("1")
                                        , Cond.PAGE_COUNT.ToString("1000000")
                                        , (!Cond.bWorkingTime) ? "0" : "1"
                                        , Cond.CATEGORY_CODE.ToString("")
                                        , Cond.TITLE.ToString("")
                                        , Cond.STORE_CODE.ToString("")
                                        , Cond.AD_FRAME_TYPE.ToString("")
            );
            return db.ExecuteQuery<LOGGAL_BOX_DATA>(sql).ToList();
        }

		/// <summary>
		/// loggal box 광고데이터 가져오기 거리순(2019.11.16)
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public IList<LOGGAL_BOX_DATA2> GetLoggalBoxList2(LOGGAL_BOX_COND2 Cond)
		{
			if (3 != Cond.GUBUN)
			{
				return GetOutAdLoggalBoxList(Cond);
			}
			else
			{
				return GetUnRegAdLoggalBoxList2(Cond);
			}
		}

		public IList<LOGGAL_BOX_DATA2> GetOutAdLoggalBoxList(LOGGAL_BOX_COND2 Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi2.xml"
										, "GetLoggalBoxList2"
										, Cond.DEVICE_CODE.ToString("0")
										, Cond.DEVICE_NUMBER.ToString("")
										, Cond.PAGE.ToString("1")
										, Cond.PAGE_COUNT.ToString("1000000")
										, Cond.HOST.ToString("")//Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
										, (!Cond.bWorkingTime) ? "0" : "1"
										, Cond.STATUS.ToString("")
										, Cond.CATEGORY_CODE.ToString("")
										, Cond.TITLE.ToString("")
										, Cond.STORE_CODE.ToString("")
										, Cond.AD_FRAME_TYPE.ToString("")
										, Cond.OUT_AD_STATUS.ToString("")
			);
			return db.ExecuteQuery<LOGGAL_BOX_DATA2>(sql).ToList();
		}
		public IList<LOGGAL_BOX_DATA2> GetUnRegAdLoggalBoxList2(LOGGAL_BOX_COND2 Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi2.xml"
										, "GetLoggalBoxAddList2" 
										, Cond.DEVICE_CODE.ToString("0")
										, Cond.DEVICE_NUMBER.ToString("")
										, Cond.PAGE.ToString("1")
										, Cond.PAGE_COUNT.ToString("1000000")
										, Cond.HOST.ToString("")//Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
										, (!Cond.bWorkingTime) ? "0" : "1"
										, Cond.STATUS.ToString("")
										, Cond.CATEGORY_CODE.ToString("")
										, Cond.TITLE.ToString("")
										, Cond.STORE_CODE.ToString("")
										, Cond.AD_FRAME_TYPE.ToString("")
			);
			return db.ExecuteQuery<LOGGAL_BOX_DATA2>(sql).ToList();
		}
		/// <summary>
		/// loggal box 내배너 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public IList<LOGGAL_BOX_DATA2> GetMyLoggalBoxList(LOGGAL_BOX_COND2 Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi2.xml"
										, "GetMyLoggalBoxList"
										, Cond.DEVICE_CODE.ToString("0")
										, Cond.DEVICE_NUMBER.ToString("")
										, Cond.PAGE.ToString("1")
										, Cond.PAGE_COUNT.ToString("1000000")
										, Cond.HOST.ToString("")//Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
										, (!Cond.bWorkingTime) ? "0" : "1"
										, Cond.STATUS.ToString("")
										, Cond.CATEGORY_CODE.ToString("")
										, Cond.TITLE.ToString("")
										, Cond.STORE_CODE.ToString("")
										, Cond.AD_FRAME_TYPE.ToString("")


			);
			return db.ExecuteQuery<LOGGAL_BOX_DATA2>(sql).ToList();
		}

		/// <summary>
		/// 로컬박스 인증번호 가져오기
		/// </summary>
		/// <param name="Cond"></param>
		/// <returns></returns>
		public IList<LOGGAL_BOX_AUTH> GetLocalboxAuthNumber(LOGGAL_BOX_AUTH Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "LoggalBox\\LoggalBoxApi2.xml", "GetLocalboxAuthNumber"
                                        , Cond.AUTH_NUMBER.ToString("")
                                        , Cond.DEVICE_NUMBER.ToString("")
                                        , Cond.AUTH_TYPE.ToString("2")
            );
            return db.ExecuteQuery<LOGGAL_BOX_AUTH>(sql).ToList();
        }
        #region >> T_SIGNAGE 테이블 조회/ 저장
        /// <summary>                                                                                      
        /// T_SIGNAGE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_SIGNAGE> GetSignageList(T_SIGNAGE_COND Param)
        {
            List<T_SIGNAGE> list = new List<T_SIGNAGE>();
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE.xml", "GetSignageList"
                                                   , Param.PAGE.ToString("1")
                                                   , Param.PAGE_COUNT.ToString("10")
                                                   , Param.SORT.ToString("A.SIGN_CODE")
                                                   , Param.SIGN_CODE.ToString("")
                                                   , Param.SIGN_NAME.ToString("")
                                                   , (Param.IS_REPRESENT == null ? "" : (Param.IS_REPRESENT == true ? "1" : "0"))
                                                   , Param.REPRE_SIGN_CODE.ToString("")
                                                   , Param.AUTH_NUMBER.ToString("")
                                                   
                                                   
                                                   , Param.AUTH_YN.ToString("")
                                                   , Param.COMPANY_CODE.ToString("")
                                                   , Param.COMPANY_NAME.ToString("")
                                                   , Param.STORE_CODE.ToString("")
                                                   , Param.STORE_NAME.ToString("")
                                                   , Param.MEMBER_CODE.ToString("")
                                                   , Param.MEMBER_NAME.ToString("")
                                                   , Param.CONTACT_COMPANY_CODE.ToString("")
                                                   , Param.CONTACT_COMPANY_NAME.ToString("")
                                                   , Param.CONTACT_STORE_CODE.ToString("")
                                                   , Param.CONTACT_STORE_NAME.ToString("")
                                                   , Param.CONTACT_CODE.ToString("")
                                                   , Param.CONTACT_NAME.ToString("")
                                                   , Param.HIDE == null ? "" : (Param.HIDE == true ? "1" : "0")
                        );
            return db.ExecuteQuery<T_SIGNAGE>(sql).ToList();
        }
        /// <summary>                                                                                      
        /// T_SIGNAGE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA SignageSave(T_SIGNAGE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = string.Empty;
                    if (Param.SAVE_TYPE == "D")
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE.xml", "SignageDelete"
                                                       , Param.SIGN_CODE.ToString("-1")
                                                       );
                    }
                    else
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE.xml", "SignageSave"
                                                        , Param.SIGN_CODE.ToString("-1")
                                                        , Param.SIGN_NAME.ToString("")
                                                        , Param.AUTH_NUMBER.ToString("")
                                                        , Param.DEVICE_NUMBER.ToString("")
                                                        , Param.IS_VERTICAL == false ? "0" : "1"
                                                        , Param.PLAY_TIME.ToString("")
                                                        , Param.ADDRESS1.ToString("")
                                                        , Param.ADDRESS2.ToString("")
                                                        , Param.ZIP_CODE.ToString("")
                                                        , Param.LATITUDE.ToString("")
                                                        , Param.LONGITUDE.ToString("")
                                                        , Param.RADIUS.ToString("")
                                                        , (Param.HIDE == null || Param.HIDE == false) ? "0" :"1"
                                                        , Param.REMARK.ToString("")
                                                        , Param.INSERT_CODE.ToString("")

                          );
                    }
                    rtn.DATA = db.ExecuteQuery<long>(sql).First().ToString();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }
        #endregion

        #region >> T_AD_SIGNINFO 테이블 조회/저장
        /// <summary>                                                                                      
        /// T_AD_SIGNINFO 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_AD_SIGNINFO> GetAdSigninfoList(T_AD_SIGNINFO_COND Param)
        {

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_AD_SIGNINFO.xml", "GetAdSigninfoList"
                                                   , Param.PAGE.ToString("1")
                                                   , Param.PAGE_SIZE.ToString("10")
                                                   , Param.SORT.ToString("A.IDX DESC")
                                                   , Param.IDX.ToString("")
                                                   , Param.AD_CODE.ToString("")
                                                   , Param.EXT_SIGN_CODE.ToString("")
                                                   , Param.SIGN_TYPE.ToString("")
                                                   , Param.IS_VERTICAL.ToBooleanString("")
                                                   , Param.PUBLIC_TYPE.ToString("")
                                                   , Param.PLAY_TIME.ToString("")
                                                   , Param.TITLE.ToString("")
                                                   , Param.HIDE.ToBooleanString("")
                                                   
                     );
            List<T_AD_SIGNINFO> list = db.ExecuteQuery<T_AD_SIGNINFO>(sql).ToList();
            return list;
        }

        /// <summary>                                                                                      
        /// T_AD_SIGNINFO 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA AdSigninfoSave(T_AD_SIGNINFO Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_AD_SIGNINFO.xml", "AdSigninfoSave"
                                                        , Param.IDX.ToString("-1")
                                                        , Param.TITLE.ToString("")
                                                        , Param.AD_CODE.ToString()
                                                        , Param.SEQ.ToString()
                                                        , Param.HIDE.ToBooleanString()
                                                        , Param.IS_VERTICAL.ToBooleanString()
                                                        , Param.PUBLIC_TYPE.ToString("3")
                                                        , Param.PLAY_TIME.ToString()
                                                        , Param.SIGN_TYPE.ToString()
                                                        , Param.CONTENT_URL.ToString("")
                                                        , Param.FR_DATE.ToString("")
                                                        , Param.TO_DATE.ToString("")
                                                        , Param.FR_TIME.ToString("")
                                                        , Param.TO_TIME.ToString("")
                                                        , Param.REMARK.ToString("")
                                                        , Param.INSERT_CODE.ToString()

                      );
                    rtn.DATA = db.ExecuteQuery<string>(sql).First();
                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }

        #endregion

        #region >> T_AD_SIGNINFO_SIGNAGE 테이블 조회/저장
        /// <summary>                                                                                      
        /// T_AD_SIGNINFO_SIGNAGE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_AD_SIGNINFO_SIGNAGE> GetadSigninfoSignageList(T_AD_SIGNINFO_SIGNAGE_COND Param)
        {

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_AD_SIGNINFO_SIGNAGE.xml", "GetadSigninfoSignageList"
                                                   , Param.PAGE.ToString("1")
                                                   , Param.PAGE_COUNT.ToString("10")
                                                   , Param.SORT_ORDER.ToString("A.IDX")
                                                   , Param.IDX.ToString("")
                                                   , Param.AD_CODE.ToString("")
                                                   , Param.SIGN_CODE.ToString("")
                                                   , Param.TITLE.ToString("")
                                                   , (Param.IS_VERTICAL == null ? ""  : (Param.IS_VERTICAL == true ? "1" : "0"))
                                                   , Param.PLAY_TIME.ToString("")
                                                   , Param.SIGN_TYPE.ToString("")
                                                   

                     );
            return db.ExecuteQuery<T_AD_SIGNINFO_SIGNAGE>(sql).ToList();
        }

        /// <summary>                                                                                      
        /// T_AD_SIGNINFO_SIGNAGE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA adSigninfoSignageSave(T_AD_SIGNINFO_SIGNAGE_SAVE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_AD_SIGNINFO_SIGNAGE data in Param.list)
                    {
                        data.SIGN_CODE = Param.SIGN_CODE;
                        data.INSERT_CODE = Param.REG_CODE;

                        if (data.SAVE_TYPE.ToString("") == "D")
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_AD_SIGNINFO_SIGNAGE.xml", "adSigninfoSignageDel"
                                                          , data.IDX.ToString("-1")
                            );
                            rtn.DATA = db.ExecuteQuery<string>(sql).First();
                        }
                        else if (data.SAVE_TYPE.ToString("") == "HIDE")
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_AD_SIGNINFO_SIGNAGE.xml", "adSigninfoSignageHide"
                                                          , data.IDX.ToString("-1")
                            );
                            rtn.DATA = db.ExecuteQuery<string>(sql).First();
                        }
                        else
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_AD_SIGNINFO_SIGNAGE.xml", "adSigninfoSignageSave"
                                                            , data.IDX.ToString("-1")
                                                            , data.AD_CODE.ToString("")
                                                            , data.SIGN_CODE.ToString("")
                                                            , data.FR_DATE.ToString("")
                                                            , data.TO_DATE.ToString("")
                                                            , data.FR_TIME.ToString("")
                                                            , data.TO_DATE.ToString("")
                                                            , data.REMARK.ToString("")
                                                            , (data.HIDE == null || data.HIDE == false ? "0" : "1")
                                                            , data.INSERT_CODE.ToString("")

                              );
                            rtn.DATA = db.ExecuteQuery<string>(sql).First();
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
        #endregion


        #region >> 로컬사이니지장소
        /// <summary>                                                                                      
        /// T_SIGNAGE_PLACE 저장하기(로컬사이니지장소 - T_SIGNAGE_PLACE 저장 -  saveparam Query)										  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA SignagePlaceSave(SIGNAGE_PLACE_SAVE param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                param.list = param.list == null ? new List<T_SIGNAGE_PLACE>() : param.list;
                using (TransactionScope tran = new TransactionScope())
                {


                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE_PLACE.xml", "SignagePlaceDelete", param.SIGN_CODE.ToString("0"), param.PLACE_TYPE.ToString("1"));
                    rtn = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                    foreach (T_SIGNAGE_PLACE data in param.list)
                    {

                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE_PLACE.xml", "SignagePlaceSave"
                                                       , data.IDX.ToString("")
                                                       , param.SIGN_CODE.ToString("0")
                                                       , param.PLACE_TYPE.ToString("1")
                                                       , data.CK_CODE.ToString("")
                                                       , data.REGION.ToString("")
                                                       , data.JIBUN_ADDRESS.ToString("")
                                                       , data.ROAD_ADDRESS.ToString("")
                                                       , data.BUILDING.ToString("")
                                                       , data.ZIP_CODE.ToString("")
                                                       , data.LATITUDE.ToString("")
                                                       , data.LONGITUDE.ToString("")
                                                       , data.RADIUS.ToString("500")
                                                       , data.REMARK.ToString("")
                                                       , param.REG_CODE.ToString()

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
        /// T_SIGNAGE_PLACE 조회하기	(로컬사인장소 - T_SIGNAGE_PLACE 저장 -  selectparam Query)					      
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_SIGNAGE_PLACE> GetSignagePlaceList(T_SIGNAGE_PLACE_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE_PLACE.xml", "GetSignagePlaceList"
                                                     , Param.IDX.ToString("")
                                                     , Param.SIGN_CODE.ToString("")
                                                     , Param.PLACE_TYPE.ToString("1")
                                                     , Param.CK_CODE.ToString("")
                                                     , Param.REGION.ToString("")
                                                     , Param.JIBUN_ADDRESS.ToString("")
                                                     , Param.ROAD_ADDRESS.ToString("")
                                                     , Param.BUILDING.ToString("")
                                                     , Param.ZIP_CODE.ToString("")
                            );
            return db.ExecuteQuery<T_SIGNAGE_PLACE>(sql).ToList();
        }
        #endregion

        /// <summary>                                                                                      
        /// T_SIGNAGE_CONTROL 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_SIGNAGE_CONTROL> GetSignageControlList(T_SIGNAGE_CONTROL_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE_CONTROL.xml", "GetSignageControlList"
                                            , Param.PAGE.ToString("1")
                                            , Param.PAGE_COUNT.ToString("10")
                                            , Param.SORT_ORDER.ToString("CASE WHEN A.COMPLEATED_DATE IS NULL THEN 0 ELSE 1 END, A.IDX DESC")
                                            , Param.IDX.ToString("")
                                            , Param.SIGN_CODE.ToString("")
                                            , Param.SIGN_NAME.ToString("")
                                            , Param.PLAY_TYPE.ToString("1")
                                            , Param.FR_PLAY_REQ_TIME.ToString("")
                                            , Param.TO_PLAY_REQ_TIME.ToString("")
                                            , Param.IS_COMPLEATED == null ? "" : ((Param.IS_COMPLEATED==true) ? "A.COMPLEATED_DATE IS NOT NULL" : "A.COMPLEATED_DATE IS NULL")
                                            , Param.HIDE.ToBooleanString()
                                              );
            return db.ExecuteQuery<T_SIGNAGE_CONTROL>(sql).ToList();
        }

        /// <summary>                                                                                      
        /// T_SIGNAGE_CONTROL 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA SignageControlSave(T_SIGNAGE_CONTROL Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE_CONTROL.xml", "SignageControlSave"
                                                    , Param.IDX.ToString()
                                                    , Param.SIGN_CODE.ToString("")
                                                    , Param.PLAY_TYPE.ToString("")
                                                    , Param.PLAY_REQ_TIME.ToDefaultDateString()
                                                    , Param.PLAY_FR_TIME.ToDefaultDateString()
                                                    , Param.PLAY_TO_TIME.ToDefaultDateString()
                                                    , Param.CONTENT_URL.ToString("")
                                                    , Param.CONTENT.ToString("")
                                                    , Param.REQUEST_NAME.ToString("")
                                                    , Param.REQUEST_EMAIL.ToString("")
                                                    , Param.COMPLEATED_DATE.ToDefaultDateString()
                                                    , Param.HIDE.ToBooleanString("0")
                                                    , Param.INSERT_CODE.ToString("0")

                      );
                    rtn.DATA = db.ExecuteQuery<string>(sql).First();
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
        /// T_SIGNAGE_CONTROL 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA SignageControlPlayUpdate(T_SIGNAGE_CONTROL_UPDATE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE_CONTROL.xml", "SignageControlPlayUpdate"
                                                    , Param.IDX
                                                    , Param.PLAY_FR_TIME.ToString("")
                                                    , Param.PLAY_TO_TIME.ToString("")
                                                    , Param.UPDATE_CODE.ToString("0")

                      );
                    rtn.DATA = db.ExecuteQuery<string>(sql).First();
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
        /// T_SIGNAGE_CONTROL 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA SignageSubCopySave(T_SIGNAGE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE.xml", "SignageSubCopySave"
                                                    , Param.REPRE_SIGN_CODE.ToString("")
                                                    , Param.INSERT_CODE.ToString("0")

                      );
                    rtn.DATA = db.ExecuteQuery<string>(sql).First();
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
        /// 모바일에서 사이니지 조회																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<MOBILE_SIGNAGE_LIST> GetMobileSignageList(MOBILE_SIGNAGE_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Signage\\T_SIGNAGE.xml", "GetMobileSignageList"
                                            , Param.LATITUDE.ToString("")
                                            , Param.LONGITUDE.ToString("")
                                            , Param.PAGE.ToString("1")
                                            , Param.PAGE_COUNT.ToString("10")
                                            , Param.SIGN_NAME
                                            
                                              );
            return db.ExecuteQuery<MOBILE_SIGNAGE_LIST>(sql).ToList();
        }

    }
}
