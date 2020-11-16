using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Transactions;
namespace loggalServiceBiz
{
    public class AccountService : BaseService
    {

        public AccountService() { }
        public AccountService(DataContext _db) : base(_db){}
        /// <summary>
        /// 사용자정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<T_MEMBER> GetMemberList(T_MEMBER_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\Member.xml", "GetMemberList"
                                           , Cond.USER_ID.ToString("")
                                           , Cond.PASSWORD.ToString("")
                                           , Cond.USER_TYPE.ToString("")
                                           , Cond.PHONE.ToString("")
                                           , Cond.MOBILE.ToString("")
                                           , Cond.KAKAO_ID.ToString("")
                                           , Cond.GOOGLE_ID.ToString("")
                                           , Cond.NAVER_ID.ToString("")
                                           , Cond.FACEBOOK_ID.ToString("")
                                           , (Cond.HIDE == null ? "" : Cond.HIDE == true ? "1" : "0")
               );
            return db.ExecuteQuery<T_MEMBER>(sql).ToList();
        }
        /// <summary>
        /// 사용자정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA SaveMember(T_MEMBER Param)

        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                Param.SAVE_MODE = Param.SAVE_MODE.ToString("");
                if (Param.SAVE_MODE =="U" || Param.SAVE_MODE == "D")/*모바일에서 저장 할경우에만 값이 있음 N일경우에는 신규이기때문에 해당로직에서 제외*/
                {
                    T_MEMBER data =  this.GetMemberList(new T_MEMBER_COND { USER_ID = Param.USER_ID }).FirstOrDefault();
                    if (data == null) data = new T_MEMBER() { USER_ID = Param.USER_ID };
                  
                    data.USER_NAME = Param.USER_NAME;
                    data.BIRTH = Param.BIRTH;
                    data.GENDER = Param.GENDER;
                    data.PHONE = Param.PHONE;
                    data.SAVE_MODE = Param.SAVE_MODE;
                    Param = data;
                    if (Param.SAVE_MODE == "D") Param.HIDE = true;
                }

                if(string.IsNullOrEmpty(Param.USER_ID))
                {
                    throw new Exception("아이디를 입력하여 주시기 바랍니다.");
                }

                if (string.IsNullOrEmpty(Param.PASSWORD))
                {
                    throw new Exception("암호를 입력하여 주시기 바랍니다.");
                }

                using (TransactionScope tran = new TransactionScope()) {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\Member.xml", "MemberSave"
                                                    , Param.MEMBER_CODE.ToString("-1")
                                                    , Param.USER_TYPE.ToString("2")
                                                    , Param.USER_ID.ToString("")
                                                    , Param.PASSWORD.ToString("")
                                                    , Param.USER_NAME.ToString("")
                                                    , Param.EMAIL.ToString("")
                                                    , Param.PHONE.ToString("")
                                                    , Param.MOBILE.ToString("")
                                                    , Param.ADDRESS1.ToString("")
                                                    , Param.ADDRESS2.ToString("")
                                                    , Param.ZIP_CODE.ToString("")
                                                    , Param.BIRTH.ToString("")
                                                    , Param.GENDER.ToString("1")
                                                    , Param.PASSWORD_CHANGE_URL.ToString("")
                                                    , Param.PASSWORD_AUTH_TIME.ToString("1901-01-01 00:00:00")
                                                    , Param.KAKAO_ID.ToString("")
                                                    , Param.GOOGLE_ID.ToString("")
                                                    , Param.NAVER_ID.ToString("")
                                                    , Param.FACEBOOK_ID.ToString("")
                                                    , Param.HIDE == true ? "1" : "0"
                                                    , Param.REMARK.ToString("")
                                                    , Param.INSERT_CODE.ToString() /*Admin*/
                                                    , Param.SAVE_MODE.ToString("")
                                                    , Param.thumnailPath.ToString("")
                                                  

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

        #region >> 회원별 북마크 정보
        /// <summary>                                                                                      
        /// T_MEMBER_BOOKMARK 조회하기	(회원별 북마크정보 - T_MEMBER_BOOKMARK 저장 -  selectparam Query)					      
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_MEMBER_BOOKMARK> GetMemberbookmarkList(T_MEMBER_BOOKMARK_COND Param)
        {

            List<T_MEMBER_BOOKMARK> list = new List<T_MEMBER_BOOKMARK>();
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\T_MEMBER_BOOKMARK.xml", "GetMemberbookmarkList"
                                                     , Param.PAGE.ToString("1")
                                                     , Param.PAGE_COUNT.ToString("15")
                                                     , Param.SORT_ORDER.ToString("A.BOOKMARK_CODE DESC")
                                                     , Param.BOOKMARK_CODE.ToString("")
                                                     , Param.MEMBER_CODE.ToString("")
                                                     , Param.USER_ID.ToString("")
                                                     , Param.BOOKMARK_TYPE.ToString("")
                                                     , Param.BOOKMARK_KIND.ToString("1")
                                                     , Param.BOOKMARK_URL.ToString("")
                                                     , Param.TITLE.ToString("")
                                                     , Param.DEVICE_NAME.ToString("")
                                                     , Param.USER_NAME.ToString("")
                                                     
                                                     
                        );
            list = db.ExecuteQuery<T_MEMBER_BOOKMARK>(sql).ToList();
            return list;
        }

        /// <summary>                                                                                      
        /// T_MEMBER_BOOKMARK 북마크 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA MemberbookmarkSave(List<T_MEMBER_BOOKMARK> list, string saveMode = "")
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_MEMBER_BOOKMARK Param in list)
                    {
                        Param.SAVE_MODE = !string.IsNullOrEmpty(saveMode) ? saveMode : Param.SAVE_MODE;
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\T_MEMBER_BOOKMARK.xml", "MemberbookmarkSave"
                                                        , Param.BOOKMARK_CODE.ToString()
                                                        , Param.MEMBER_CODE.ToString()
                                                        , Param.BOOKMARK_NAME.ToString("")
                                                        , Param.BOOKMARK_KIND.ToString("1")
                                                        , Param.BOOKMARK_TYPE.ToString("1")
                                                        , Param.DEVICE_CODE.ToString("")
                                                        , Param.AD_CODE.ToString("")
                                                        , Param.BOOKMARK_URL.ToString("")
                                                        , Param.MEMO.ToString("")
                                                        , Param.REMARK.ToString("")
                                                        , Param.USER_ID.ToString("")
                                                        , Param.SAVE_MODE.ToString("")
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

        public List<BOOKMARK_AD_LIST> GetBookmarkAdList(BOOKMARK_AD_COND Cond)
        {
            Cond.SORT_ORDER = string.IsNullOrEmpty(Cond.SORT_ORDER) ? " A.BOOKMARK_CODE DESC " : Cond.SORT_ORDER;
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\T_MEMBER_BOOKMARK.xml", "GetBookmarkAdList"
                                                   , Cond.PAGE.ToString("1")
                                                   , Cond.PAGE_COUNT.ToString("15")
                                                   , Global.ConfigInfo.MANAGEMENT_SITE.ToString("")
                                                   , Cond.SORT_ORDER.ToString("")
                                                   , Cond.USER_ID.ToString("")
                                                   , Cond.TITLE.ToString("")
                                                   , Cond.USER_NAME.ToString("")
                                                   , Cond.BOOKMARK_KIND.ToString("")
                                                   );

           return db.ExecuteQuery<BOOKMARK_AD_LIST>(sql).ToList();
        }
        #endregion >> 회원별 북마크정보

        #region >> 모바일 로그인
        public MOBILE_LOGIN_DATA GetMobileLogin(MOBILE_MEMBER_LOGIN_COND Cond)
        {

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\MEMBER.xml", "GetMobileLogin"
                                                  , Cond.USER_ID.ToString("")
                                                  , Cond.PASSWORD.ToString("")
                                                  , Cond.KAKAO_ID.ToString("")
                                                  , Cond.GOOGLE_ID.ToString("")
                                                  , Cond.NAVER_ID.ToString("")
                                                  , Cond.FACEBOOK_ID.ToString("")
                                                  );

            return db.ExecuteQuery<MOBILE_LOGIN_DATA>(sql).FirstOrDefault();
        }


        public MOBILE_LOGIN_DATA MobilePasswordChange(MOBILE_MEMBER_LOGIN_COND Cond)
        {

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\MEMBER.xml", "MobilePasswordChange"
                                                  , Cond.USER_ID.ToString("")
                                                  , Cond.PASSWORD.ToString("")
                                                  , Cond.NEW_PASSWORD.ToString("")
                                                  );

            return db.ExecuteQuery<MOBILE_LOGIN_DATA>(sql).FirstOrDefault();
        }
        
        #endregion

        public RTN_SAVE_DATA MemberSnsIDUpdate(T_MEMBER_SNS_UPDATE Param)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\MEMBER.xml", "MemberSnsIDUpdate"
                                                    , Param.USER_ID
                                                    , Param.PASSWORD
                                                    , Param.SNS_TYPE.ToString("1")
                                                    , Param.KAKAO_ID.ToString("1")
                                                    , Param.GOOGLE_ID.ToString("")
                                                    , Param.NAVER_ID.ToString("")
                                                    , Param.FACEBOOK_ID.ToString("")
                                                    , Param.UPDATE_CODE.ToString("0")
                                                    
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
        /// T_MEMBER_BOOKMARK 북마크 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA MemberPasswordChange(T_MEMBER_PASSWROD_CHANGE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Account\\Member.xml", "MemberPasswordChange"
                                                    , Param.USER_ID.ToString("")
                                                    , Param.PASSWORD.ToString("")
                                                    , Param.UPDATE_CODE.ToString("0")
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
    }
}
