using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.Common;
using ALT.Framework;
using ALT.Framework.Data;
using System.Transactions;
using ALT.VO.loggal;

namespace ALT.BizService
{
    public class EmployeeService : BaseService
    {
        public EmployeeService() { }
        public EmployeeService(System.Data.Linq.DataContext _db) : base(_db) { }
        public IList<T_STORE_POSITION> GetStorePosition(T_STORE_POSITION param)
        {
           
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "GetStorePosition"
                                                    , param.STORE_CODE.ToString("")
                                                    , param.HIDE == null ? "" : (param.HIDE == false ? "0" : "1")
                                                    , param.POSITION_TYPE.ToString("")
                );
                return db.ExecuteQuery<T_STORE_POSITION>(sql).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetStorePosition : " + ex.Message);
                return new List<T_STORE_POSITION>();
            }

        }

        public IList<T_STORE_DEPT> GetStoreDept(T_STORE_DEPT param)
        {
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "GetStoreDept"
                                            , param.STORE_CODE.ToString("")
                                            , param.PARENT_DEPT_CODE.ToString("")
                            );
                return db.ExecuteQuery<T_STORE_DEPT>(sql).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetStoreDept : " + ex.Message);
                return new List<T_STORE_DEPT>();
            }
        }

        public IList<EMPLOYEE_INFO> GetEmployeeList(EMPLOYEE_COND param)
        {
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "GetEmployeeList"
                                                , param.PAGE_COUNT.ToString("10")
                                                , param.PAGE.ToString("1")
                                                , param.SORT.ToString("A.UPDATE_DATE DESC")
                                                , param.COMPANY_CODE.ToString("")
                                                , param.STORE_CODE.ToString("")
                                                , param.DEPT_SEARCH.ToString("")
                                                , param.DEPT_CODE.ToString("")
                                                , param.PARENT_MEMBER_CODE.ToString("")
                                                , param.MEMBER_CODE.ToString("")
                                                , param.USER_NAME.ToString("")
                                                , param.MOBILE.ToString("")
                                                , param.COMP_POSITION.ToString("")
                                                , param.COMP_TITLE.ToString("")
                                                , param.STR_EMP_AUTH.ToString("")         
                                                , param.FR_BIRTH.ToString("").RemoveDateString()
                                                , param.TO_BIRTH.ToString("").RemoveDateString()
                                                , (param.HIDE == null ? "" : (param.HIDE == true ? "1" : "0"))
                );
                return db.ExecuteQuery<EMPLOYEE_INFO>(sql).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetEmployeeList : " + ex.Message);
                return new List<EMPLOYEE_INFO> ();
            }
        }

       

        public IList<T_STORE_WEBMENU_GROUP> GetMenuGroupList(T_STORE_WEBMENU_GROUP param)
        {
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "GetMenuGroupList"
                                            , param.STORE_CODE.ToString("")     //직책    
                                            , param.GROUP_CODE.ToString("")     //직책
                                            , param.HIDE == null ? "" : param.HIDE == true ? "1" : "0"
                    );
                return db.ExecuteQuery<T_STORE_WEBMENU_GROUP>(sql).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetMenuGroupList : " + ex.Message);
                return new List<T_STORE_WEBMENU_GROUP>();
            }

        }

        public EMPLOYEE_RESPONSE SaveEmployee(EMPLOYEE_INFO Param)
        {

            EMPLOYEE_RESPONSE result = new EMPLOYEE_RESPONSE();
            string sql = string.Empty;

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "SaveEmployee"
                                                    
                                                    , Param.MEMBER_CODE.ToString("-1")
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
                                                    , Param.HIDE == true ? "1" : "0"
                                                    , Param.INSERT_CODE.ToString() /*Admin*/
                                                    , Param.DEPT_CODE.ToString("")
                                                    , Param.PARENT_MEMBER_CODE.ToString("")
                                                    , Param.COMP_POSITION.ToString("")
                                                    , Param.COMP_TITLE.ToString("")
                                                    , Param.EMP_AUTH.ToString("")
                                                    , Param.MENU_GROUP.ToString("")
                                                    , Param.MEMO.ToString("")
                                                    , Param.TELEGRAM_CHAT_ID.ToString("")
                                                    , Param.PROJECT_SITE.ToString("3")
                                                    , Param.STORE_CODE.ToString("0")
                      );

                    int member_code = db.ExecuteQuery<int>(sql).FirstOrDefault();

                    if (member_code >= 0)
                    {
                        result.RESPONSE_CODE = "0";
                        result.RESPONSE_MSG = "";
                        result.MEMBER_CODE = member_code;
                    }
                    else
                    {
                        throw new Exception("직원 정보 저장 실패.");
                    }

                    tran.Complete();
                }
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> SaveEmployee : " + ex.Message);
                result.RESPONSE_CODE = "-99";
                result.RESPONSE_MSG = ex.Message;
                result.MEMBER_CODE = -1;
            }
            return result;
        }

        public RTN_SAVE_DATA loggalMng_MEMBER_JOIN(MEMBER_JOIN Param )
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            T_COMPANY compParam = new T_COMPANY();
            compParam.COMPANY_ID = Param.USER_ID;
            compParam.COMPANY_NAME = (string.IsNullOrEmpty(Param.COMPANY_NAME) ?  Param.USER_NAME : Param.COMPANY_NAME);
            compParam.COMPANY_TYPE = Param.COMPANY_TYPE;
            compParam.COMPANY_TYPE2 = Param.COMPANY_TYPE2;
            compParam.PASSWORD = Param.PASSWORD;
            compParam.PHONE = Param.PHONE;
            compParam.MOBILE = Param.PHONE;
            compParam.OWNER_PHONE = Param.PHONE;
            compParam.OWNER_NAME = Param.USER_NAME;
            compParam.ZIP_CODE = Param.ZIP_CODE;
            compParam.ADDRESS1 = Param.ADDRESS1;
            compParam.ADDRESS2 = Param.ADDRESS2;
            


            compParam.STATUS = 1;
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    rtn = new BasicService().CompanySave_Exec(compParam, db);
                    if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE)) return rtn;
                    if(Param.SHARE_AUTH_NUMBER.ToString().Length != 2)
                    {
                        Param.SHARE_AUTH_NUMBER = "00";
                    }
                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "MemberJoinSave"
                                                          , Param.USER_ID.ToString("")
                                                          , Param.BUSI_REG_NUMBER.ToString("")
                                                          , Param.BIRTH.RemoveDateString()
                                                          , Param.GENDER.ToString("")
                                                          , Param.SHARE_AUTH_NUMBER.ToString("")
                                                          );

                    db.ExecuteCommand(sql);
                    tran.Complete();
                }
            }catch(Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;
        }

        public List<EMPLOYEE_P_DATA> GetEmployeePopupList(SEARCH_COND Param)
        {
            try
            {
                Param.SEARCH_DATA1 = Param.SEARCH_DATA1.ToString("");
                if (!string.IsNullOrEmpty(Param.SEARCH_DATA1))
                {
                    Param.SEARCH_DATA1 = " AND " + Param.SEARCH_DATA1 + " LIKE N'%" + Param.SEARCH_DATA.ToString("") + "%'";
                }
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Member\\T_MEMBER_EMPLOYEE.xml", "GetEmployeePopupList"
                                            , Param.PAGE_COUNT.ToString("10")
                                            , Param.PAGE.ToString("1")
                                            , Param.SORT.ToString("D.USER_NAME")
                                            , Param.SEARCH_DATA.ToString("")
                                            , Param.SEARCH_DATA1
                                            , Param.SEARCH_DATA2.ToString("")
                                            , Param.SEARCH_DATA3.ToString("")
                    );
                return db.ExecuteQuery<EMPLOYEE_P_DATA>(sql).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
