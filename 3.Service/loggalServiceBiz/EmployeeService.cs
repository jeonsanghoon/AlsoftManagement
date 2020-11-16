using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALT.VO.Common;
using ALT.Framework;
using ALT.Framework.Data;
using System.Transactions;

namespace loggalServiceBiz
{
    public class EmployeeService : BaseService
    {
        public EmployeeService() { }
        public EmployeeService(System.Data.Linq.DataContext _db) : base(_db) { }
        public IList<T_STORE_POSITION> GetStorePosition(T_STORE_POSITION param)
        {
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Employee\\Employee.xml", "GetStorePosition"
                                                    , param.HIDE == null ? "" : (param.HIDE == false ? "0" : "1")
                                                    , param.POSITION_TYPE.ToString("")
                );
                return db.ExecuteQuery<T_STORE_POSITION>(sql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetStorePosition : " + ex.Message);
                return null;
            }

        }

        public IList<T_STORE_DEPT> GetStoreDept(T_STORE_DEPT param)
        {
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Employee\\Employee.xml", "GetStoreDept"
                                                , param.PARENT_DEPT_CODE.ToString("")
                            );
                return db.ExecuteQuery<T_STORE_DEPT>(sql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetStoreDept : " + ex.Message);
                return null;
            }
        }

      
        public IList<T_STORE_WEBMENU_GROUP> GetMenuGroupList(T_STORE_WEBMENU_GROUP param)
        {
            try
            {
                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Employee\\Employee.xml", "GetMenuGroupList"
                                                , param.GROUP_CODE.ToString("")     //직책
                    );
                return db.ExecuteQuery<T_STORE_WEBMENU_GROUP>(sql.ToString()).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("EmployeeService >> GetMenuGroupList : " + ex.Message);
                return null;
            }

        }

        public EMPLOYEE_RESPONSE SaveEmployee(EMPLOYEE Param)
        {

            EMPLOYEE_RESPONSE result = new EMPLOYEE_RESPONSE();
            string sql = string.Empty;

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    sql = Global.DBAgent.LoadSQL(sqlBasePath + "Employee\\Employee.xml", "SaveEmployee"
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
                      );

                    int member_code = db.ExecuteQuery<int>(sql.ToString()).FirstOrDefault();

                    if (member_code > 0)
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
    }
}
