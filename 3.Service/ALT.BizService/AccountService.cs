using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace ALT.BizService
{
    public class AccountService : BaseService
    {
        public AccountService() { }
        public AccountService(System.Data.Linq.DataContext _db) : base(_db) { }


        /// <summary>
        /// 사용자정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<T_MEMBER> GetMemberList(T_MEMBER_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER.xml", "GetMemberList"
                                           , Cond.USER_ID.ToString("")
                                           , Cond.USER_TYPE.ToString("")
                                           , Cond.PHONE.ToString("")
                                           , Cond.MOBILE.ToString("")
                                           , Cond.PASSWORD_CHANGE_URL.ToString("")
                                           , (Cond.EMPLOYEE_YN == null || Cond.EMPLOYEE_YN == true) ? "" : Cond.USER_ID.ToString()
                                           , Cond.MEMBER_CODE.ToString("")
                                        
               );
            return db.ExecuteQuery<T_MEMBER>(sql).ToList();
        }


        /// <summary>
        /// 직원정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<EMPLOYEE_INFO> GetEmployeeInfoList(EMPLOYEE_INFO_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "GetEmployeeInfoList"
                                           , Cond.MEMBER_CODE.ToString("")
                                           , Cond.STORE_CODE.ToString("")
                                           , Cond.DEPT_CODE.ToString("")
                                           , Cond.COMP_TITLE.ToString("")

               );
            return db.ExecuteQuery<EMPLOYEE_INFO>(sql).ToList();
        }

        /// <summary>
        /// 로그인한 직원 메뉴정보 가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<LOGIN_WEBMENU> GetLoginWebMenuList(LOGIN_WEBMENU Cond)
        {

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER.xml", "GetLoginWebMenuList"
                                           , Cond.PROJECT_SITE.ToString("")
                                           , Cond.STORE_CODE.ToString("1")
                                           , Cond.MEMBER_CODE.ToString("")

               );
            return db.ExecuteQuery<LOGIN_WEBMENU>(sql).ToList();
        }

        public RTN_SAVE_DATA MemberModify(T_MEMBER Param, T_STORE Param2)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    if (Param.SHARE_AUTH_NUMBER.ToString("").Length != 2)
                    {
                        Param.SHARE_AUTH_NUMBER = "00";
                    }


                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER.xml", "MemberSave"
                                                            , Param.MEMBER_CODE.ToString("-1")
                                                            , Param.USER_TYPE.ToString("5")
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
                                                            , Param.REMARK.ToString("")
                                                            , Param.PASSWORD_CHANGE_URL.ToString("")
                                                            , Param.SHARE_AUTH_NUMBER.ToString("")
                                                            , Convert.ToDateTime(Param.PASSWORD_AUTH_TIME.ToString("1901-01-01")).ToString("yyyy-MM-dd HH:mm:ss.fff")
                                                            , Param.HIDE == true ? "1" : "0"
                                                            , Param.INSERT_CODE.ToString() /*Admin*/
                                                    );
                    var rtn2 = db.ExecuteQuery<int>(sql);
                    rtn.DATA = Convert.ToString(rtn2.First());


                     sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "MemberModifySave"

                                                         , Param2.COMPANY_CODE.ToString("")
                                                         , Param2.STORE_CODE.ToString("")
                                                         , Param2.COMPANY_TYPE2.ToString("7")
                                                         , Param2.BUSI_REG_NUMBER.ToString("")
                                                         , Param.INSERT_CODE.ToString()
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
        /// 사용자정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA SaveMember(T_MEMBER Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER.xml", "MemberSave"
                                                    , Param.MEMBER_CODE.ToString("-1")
                                                    , Param.USER_TYPE.ToString("5")
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
                                                    , Param.REMARK.ToString("")
                                                    , Param.PASSWORD_CHANGE_URL.ToString("")
                                                    , Param.SHARE_AUTH_NUMBER.ToString("")
                                                    , Convert.ToDateTime(Param.PASSWORD_AUTH_TIME.ToString("1901-01-01")).ToString("yyyy-MM-dd HH:mm:ss.fff")
                                                    , Param.HIDE == true ? "1" : "0"
                                                    , Param.INSERT_CODE.ToString() /*Admin*/

                      );

                    var rtn2 = db.ExecuteQuery<int>(sql);
                    rtn.DATA = Convert.ToString(rtn2.First());
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
        /// 사용자정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA ReqPasswordChange(T_MEMBER Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                
                using (TransactionScope tran = new TransactionScope())
                {
                
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER.xml", "ReqPasswordChange"
                                                    , Param.USER_ID.ToString("-1")
                                                    , Param.PASSWORD_CHANGE_URL.ToString("")

                      );

                    rtn.DATA = db.ExecuteQuery<DateTime>(sql).First().ToString("yyyy-MM-dd HH:mm:ss");

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
        /// 사용자정보 저장하기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA PasswordChangeSave(T_MEMBER Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER.xml", "PasswordChangeSave"
                                                    , Param.USER_ID.ToString("")
                                                    , Param.PASSWORD.ToString("")


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

    }
}
