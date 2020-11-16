using ALT.VO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALT.Framework;
using ALT.Framework.Data;
using System.Transactions;
using System.Threading.Tasks;

namespace ALT.BizService
{
    public class CommonService : BaseService
    {
        public CommonService() { }
        public CommonService(System.Data.Linq.DataContext _db) : base(_db) { }


        #region >> 공통코드
        /// <summary>
        /// 공통코드 가져오기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_COMMON> GetCommon(T_COMMON_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\Common.xml", "GetCommon"
                                                  , Param.MAIN_CODE.ToString("")
                                                  , Param.SUB_CODE.ToString("")
                                                  , Param.NAME.ToString("")
                                                  , Param.REF_DATA1.ToString("")
                                                  , Param.REF_DATA2.ToString("")
                                                  , Param.REF_DATA3.ToString("")
                                                  , Param.REF_DATA4.ToString("")
                                                  , Param.HIDE == null ? "" : (Param.HIDE == false ? "0" : "1")
                                                  , Param.ADD_COND


               );
            return db.ExecuteQuery<T_COMMON>(sql).ToList();
        }

        /// <summary>
        /// 공통코드 가져오기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_COMMON> GetCommonPageList(T_COMMON_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\Common.xml", "GetCommonPageList"
                                                 , Param.PAGE.ToString("1")
                                                 , Param.PAGE_COUNT.ToString("10")
                                                 , Param.SORT.ToString("A.MAIN_CODE")
                                                  , Param.MAIN_CODE.ToString("")
                                                  , Param.SUB_CODE.ToString("")
                                                  , Param.NAME.ToString("")
                                                  , Param.REF_DATA1.ToString("")
                                                  , Param.REF_DATA2.ToString("")
                                                  , Param.REF_DATA3.ToString("")
                                                  , Param.REF_DATA4.ToString("")
                                                  , Param.HIDE == null ? "" : (Param.HIDE == false ? "0" : "1")
                                                  , Param.ADD_COND


               );
            return db.ExecuteQuery<T_COMMON>(sql).ToList();
        }

        public RTN_SAVE_DATA CommonSave(List<T_COMMON> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();

            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_COMMON data in list)
                    {
                        if (data.MRC_EDIT_MODE != "D")
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\Common.xml", "CommonSave"
                                            , data.IDX.ToString("0")
                                            , data.MAIN_CODE.ToString("")
                                            , data.SUB_CODE.ToString("")
                                            , data.NAME.ToString("")
                                            , data.LANGUAGE_CODE.ToString("")
                                            , data.ORDER_SEQ.ToString("")
                                            , data.REF_DATA1.ToString("")
                                            , data.REF_DATA2.ToString("")
                                            , data.REF_DATA3.ToString("")
                                            , data.REF_DATA4.ToString("")
                                            , (data.HIDE == null || data.HIDE == false ? "0" : "1")
                                            , REG_CODE.ToString()
                                        );

                            rtnData = db.ExecuteQuery< RTN_SAVE_DATA>(sql).First();

                            if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                            {
                                throw new Exception(rtnData.ERROR_MESSAGE);
                            }
                        }else
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\Common.xml", "CommonDelete"
                                          , data.IDX.ToString("0")
                                      );

                            rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                            if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                            {
                                throw new Exception(rtnData.ERROR_MESSAGE);
                            }
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
        #endregion 공통코드
        #region >> 지점별 웹메뉴 관리
        /// <summary>
        /// 웹메뉴조회
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_WEBMENU> GetWebMenuList(T_WEBMENU_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_WEBMENU.xml", "GetWebMenu"
                                              , Param.PROJECT_SITE.ToString("")
                                              , Param.MENU_CODE.ToString("")
                                              , Param.SEARCH_CODE.ToString("")
                                              , Param.NAME.ToString("")
                                              , Param.LEVEL.ToString("")
                                              , Param.HIDE == null ? "" : ((bool)Param.HIDE ? "1" : "0")
              );

            return db.ExecuteQuery<T_WEBMENU>(sql.ToString()).ToList();
        }

        /// <summary>
        /// 웹메뉴 등록
        /// </summary>
        /// <param name="list"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA WebMenuSave(List<T_WEBMENU> list, int PROJECT_SITE, int? REG_CODE = 0)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            StringBuilder sbSql = new StringBuilder();
            
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_WEBMENU data in list)
                    {
                 
                        if (data.MRC_EDIT_MODE != "D")
                        {
                            #region >> 저장
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_WEBMENU.xml", "WebMenuSave"
                                                                                                                , data.PROJECT_SITE.ToString("3")
                                                                                                                , data.MENU_CODE.ToString("0")
                                                                                                                , data.MENU_TYPE.ToString("1")
                                                                                                                , data.MENU_AUTH.ToString("1")
                                                                                                                , data.MENU_COMPANY_CODE.ToString("")
                                                                                                                , data.MENU_STORE_CODE.ToString("")
                                                                                                                , data.SEARCH_CODE.ToString("")
                                                                                                                , data.PARENT_CODE.ToString("")
                                                                                                                , data.LEVEL.ToString("1")
                                                                                                                , data.SEQ.ToString("1")
                                                                                                                , data.NAME.ToString("")
                                                                                                                , data.FULL_NAME.ToString("")
                                                                                                                , data.MENU_URL.ToString("")
                                                                                                                , data.TEMPLEATE_PAGE.ToString("")
                                                                                                                , data.MENU_CLASS.ToString("")
                                                                                                                , data.USER_AUTH.ToString("")
                                                                                                                , data.REMARK.ToString("")
                                                                                                                , (data.HIDE == null || data.HIDE == false) ? "0" : "1"
                                                                                                                , REG_CODE


                                                                                                            );

                            rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                            #endregion >> 저장
                        }
                        else
                        {
                            #region >> 삭제
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_WEBMENU.xml", "WebMenuDelete"
                                                                                                              , data.PROJECT_SITE.ToString("3")
                                                                                                              , data.MENU_CODE.ToString("0")
                                                                                                              , REG_CODE


                                                                                                          );

                            rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                            #endregion >> 삭제
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
        #endregion


        #region >> 지점별 웹메뉴 관리
        /// <summary>
        /// 웹메뉴조회
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_STORE_WEBMENU> GetStoreWebMenuList(T_STORE_WEBMENU Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU.xml", "GetStoreWebMenu"
                                              , Param.PROJECT_SITE.ToString("")
                                              , Param.STORE_CODE.ToString("")
                                              , Param.MENU_CODE.ToString("")
                                              , Param.SEARCH_CODE.ToString("")
                                              , Param.NAME.ToString("")
                                              , Param.LEVEL.ToString("")
                                              , Param.HIDE == null ? "" : ((bool)Param.HIDE ? "1" : "0")
              );

            return db.ExecuteQuery<T_STORE_WEBMENU>(sql.ToString()).ToList();
        }

        /// <summary>
        /// 웹메뉴 등록
        /// </summary>
        /// <param name="list"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA StoreWebMenuSave(List<T_STORE_WEBMENU> list, int PROJECT_SITE, int? REG_CODE = 0)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            StringBuilder sbSql = new StringBuilder();
            string saveMode = "U";
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_WEBMENU data in list)
                    {
                        saveMode = data.MRC_EDIT_MODE;
                        if (sbSql.ToString() != "")
                        {
                            sbSql.Append(" UNION ALL");

                        }
                        /* STORE_CODE, MENU_CODE, SEARCH_CODE, NAME, MENU_CLASS, HIDE, REG_CODE, ROW_NUM*/
                        sbSql.Append(" SELECT " + PROJECT_SITE.ToString() + ", " + data.STORE_CODE.ToString("1") + ", " + data.MENU_CODE.ToString("-1") + " ,'" + data.SEARCH_CODE.ToString("") + "'");
                        sbSql.Append(" ,N'" + data.NAME.ToString("") + "', '" + data.MENU_CLASS + "', '" + data.MENU_URL + "'");
                        sbSql.Append("," + ((data.HIDE == null || data.HIDE == false) ? "0" : "1") + "," + REG_CODE + "," + data.ROW_NUM.ToString("0"));
                        sbSql.Append("\n");

                    }

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU.xml", "StoreWebMenuSave"
                                           , saveMode
                                           , sbSql.ToString()
                        );

                    rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).FirstOrDefault();
                    if (rtnData != null & rtnData.ERROR_MESSAGE == "")
                    {
                        tran.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                rtnData.ERROR_MESSAGE = ex.Message;
            }

            return rtnData;
        }
        #endregion

        #region >> 웹메뉴그룹 관리
        /// <summary>
        /// 웹메뉴그룹 조회
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_STORE_WEBMENU_GROUP> GetWebGroupList(T_STORE_WEBMENU_GROUP Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_GROUP.xml", "GetWebGroupList"
                                              , Param.PROJECT_SITE.ToString("")
                                              , Param.STORE_CODE.ToString("")
                                              , Param.GROUP_CODE.ToString("")
                                              , Param.GROUP_NAME.ToString("")
                                              , Param.DEPT_AUTH.ToString("")
                                              , Param.COMP_POSITION_AUTH.ToString("")
                                              , Param.COMP_TITLE_AUTH.ToString("")
                                              , Param.HIDE == null ? "" : ((bool)Param.HIDE ? "1" : "0")
              );

            return db.ExecuteQuery<T_STORE_WEBMENU_GROUP>(sql.ToString()).ToList();
        }

        /// <summary>
        /// 웹메뉴그룹 등록
        /// </summary>
        /// <param name="list"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA menuGroupSave(List<T_STORE_WEBMENU_GROUP> list, int? PROJECT_SITE,  int? REG_CODE = 0)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            StringBuilder sbSql = new StringBuilder();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_WEBMENU_GROUP data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_GROUP.xml", "menuGroupSave"
                                        , PROJECT_SITE.ToString()
                                        , data.STORE_CODE.ToString("")
                                        , data.GROUP_CODE.ToString("")
                                        , data.GROUP_NAME.ToString("")
                                        , data.ORDER_SEQ.ToString("1")
                                        , data.DEPT_AUTH.ToString("")
                                        , data.COMP_POSITION_AUTH.ToString("")
                                        , data.COMP_TITLE_AUTH.ToString("")
                                        , data.REMARK.ToString("")
                                        , (data.HIDE == null || data.HIDE == false ? "0" : "1")
                                        , REG_CODE.ToString()
                                        , data.MRC_EDIT_MODE.ToString("")
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
        #endregion

        #region >> 그룹 별 메뉴 관리
        /// <summary>
        /// 그룹별 메뉴 조회
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_STORE_WEBMENU_GROUP_MENU> GetGroupMenuList(T_STORE_WEBMENU_GROUP_MENU Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_GROUP_MENU.xml", "GetGroupMenuList"
                                              , Param.PROJECT_SITE.ToString("")
                                              , Param.STORE_CODE.ToString("")
                                              , Param.GROUP_CODE.ToString("")

              );

            return db.ExecuteQuery<T_STORE_WEBMENU_GROUP_MENU>(sql.ToString()).ToList();
        }

        /// <summary>
        /// 그룹별메뉴 등록
        /// </summary>
        /// <param name="list"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA GroupMenuSave(List<T_STORE_WEBMENU_GROUP_MENU> list, int? REG_CODE = 0)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_WEBMENU_GROUP_MENU data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_GROUP_MENU.xml", "GroupMenuSave"
                                        , data.PROJECT_SITE.ToString("")
                                        , data.STORE_CODE.ToString("")
                                        , data.GROUP_CODE.ToString("")
                                        , data.MENU_CODE.ToString("")
                                        , data.INSERT_AUTH == true ? "1" : "0"
                                        , data.UPDATE_AUTH == true ? "1" : "0"
                                        , data.EXCEL_AUTH == true ? "1" : "0"
                                        , data.PRINT_AUTH == true ? "1" : "0"
                                        , (data.HIDE == null || data.HIDE == true ? "0" : "1")
                                        , data.REMARK.ToString("")
                                        , REG_CODE.ToString()
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
        #endregion

        #region >> 웹메뉴 그룹등록
        /// <summary>
        /// 웹메뉴그룹 등록
        /// </summary>
        /// <param name="list"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA groupMemberSave(GROUP_MEMBER_SAVE Param)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            StringBuilder sbSql = new StringBuilder();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_GROUP_MENU.xml", "groupMemberSave"
                                    , Param.PROJECT_SITE.ToString()
                                    , Param.STORE_CODE.ToString("1")
                                    , Param.GROUP_CODE.ToString("")
                                    , Param.MEMBER_CODES.ToString("-1")
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
        #endregion

        #region >> 사용자별 메뉴 조회 / 등록
        /// <summary>
        /// 사용자별 메뉴 조회
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public List<T_STORE_WEBMENU_EMPLOYEE_MENU> GetMemberMenuList(T_STORE_WEBMENU_EMPLOYEE_MENU Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_EMPLOYEE_MENU.xml", "GetMemberMenuList"
                                              , Param.PROJECT_SITE.ToString("")
                                              , Param.STORE_CODE.ToString("")
                                              , Param.MEMBER_CODE.ToString("")

              );

            return db.ExecuteQuery<T_STORE_WEBMENU_EMPLOYEE_MENU>(sql.ToString()).ToList();
        }
        /// <summary>
        /// 사용자별 등록
        /// </summary>
        /// <param name="list"></param>
        /// <param name="REG_CODE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA MemberMenuSave(List<T_STORE_WEBMENU_EMPLOYEE_MENU> list,  int? REG_CODE = 0)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            StringBuilder sbSql = new StringBuilder();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_WEBMENU_EMPLOYEE_MENU data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_STORE_WEBMENU_EMPLOYEE_MENU.xml", "MemberMenuSave"
                                        , data.PROJECT_SITE.ToString("")
                                        , data.STORE_CODE.ToString("")
                                        , data.MENU_CODE.ToString("")
                                        , data.MEMBER_CODE.ToString("")
                                        , data.INSERT_AUTH == true ? "1" : "0"
                                        , data.UPDATE_AUTH == true ? "1" : "0"
                                        , data.EXCEL_AUTH == true ? "1" : "0"
                                        , data.PRINT_AUTH == true ? "1" : "0"
                                        , (data.HIDE == null || data.HIDE == true ? "0" : "1")
                                        , data.REMARK.ToString("")
                                        , REG_CODE.ToString()
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
        #endregion

        #region >> 로그정보

        public List<T_LOG> GetLogList(T_LOG_COND Cond)
        {

            try
            {

                string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_LOG.xml", "GetLogList"
                                                , Cond.PAGE.ToString("1")
                                                , Cond.PAGE_COUNT.ToString("15")
                                                , Cond.SORT_ORDER.ToString("A.IDX DESC")
                                                , Cond.COMPANY_CODE.ToString("")
                                                , Cond.STORE_CODE.ToString("")
                                                , Cond.DEPT_SEARCH.ToString("")
                                                , Cond.PARENT_MEMBER_CODE.ToString("")
                                                , Cond.LOGIN_MEMBER_CODE.ToString("")
                                                , Cond.FR_DATE.ToString("").ToFormatDate("yyyyMMdd")
                                                , Cond.TO_DATE.ToString("").ToFormatDate("yyyyMMdd")
                                                , Cond.LOG_TYPE.ToString("")
                                                , Cond.LOG_DATA1.ToString("")
                                                , Cond.LOG_DATA2.ToString("")
                                                //  , Cond.LOG_DESC.ToString("")
                                                , Cond.INSERT_NAME.ToString("")

                );

                return db.ExecuteQuery<T_LOG>(sql).ToList();
            }
            catch (Exception ex)
            {
                logger.Debug("CommonService >> GetLogList : " + ex.Message);
                return null;
            }
        }

        public RTN_SAVE_DATA SaveLog(T_LOG Param)
        {

            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA();
            StringBuilder sbSql = new StringBuilder();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_LOG.xml", "LogSave"
                                    , Param.IDX.ToString()
                                    , Param.STORE_CODE.ToString()
                                    , Param.LOG_DATE.ToString("")
                                    , Param.LOG_TYPE.ToString("")
                                    , Param.LOG_DATA1.ToString("")
                                    , Param.LOG_DATA2.ToString("")
                                    , Param.LOG_DATA3.ToString("")
                                    , Param.LOG_DESC.ToString("")
                                    , Param.USE_IP.ToString("")
                                    , Param.LOG_TABLE.ToString("")
                                    , Param.INSERT_CODE.ToString()
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
        #endregion >> 로그정보


        #region >> 부서정보
        /// <summary>
        /// 부서조회
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T_STORE_DEPT> GetDept(T_STORE_DEPT Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_DEPT.xml", "GetDeptList"
                                    , Cond.STORE_CODE.ToString("0")
                                    , Cond.DEPT_CODE.ToString("")
                                    , Cond.DEPT_NAME.ToString("")
                                    , Cond.HIDE == null ? "" : (Cond.HIDE == false ? "0" : "1")
                                    );
            return db.ExecuteQuery<T_STORE_DEPT>(sql).ToList();
        }
        /// <summary>
        /// 부서저장
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA DeptSave(List<T_STORE_DEPT> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_DEPT data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_DEPT.xml", "DeptSave"
                                                            , data.STORE_CODE.ToString("0")
                                                            , data.DEPT_CODE.ToString("0")
                                                            , data.PARENT_DEPT_CODE.ToString("0")
                                                            , data.DEPT_NAME.ToString("")
                                                            , data.LEVEL.ToString("0")
                                                            , data.DEPT_SEARCH.ToString("")
                                                            , data.REMARK.ToString("")
                                                            , ((data.HIDE == null || data.HIDE == false) ? "0" : "1")
                                                            , REG_CODE.ToString("0")
                                                            , data.MRC_EDIT_MODE
                                                            );
                        rtn = db.ExecuteQuery< RTN_SAVE_DATA>(sql).First();
                        if (!string.IsNullOrEmpty(rtn.ERROR_MESSAGE))
                        {
                            throw new Exception(rtn.ERROR_MESSAGE);
                        }
                    }
                    tran.Complete();
                }
            }catch(Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }

            return rtn;
        }
        /// <summary>
        /// 부서삭제
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA DeptDelete(List<T_STORE_DEPT> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_DEPT data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_DEPT.xml", "DeptDelete"
                                            , data.STORE_CODE.ToString("0")
                                            , data.DEPT_CODE.ToString("0")
                                         
                                            , REG_CODE.ToString("0")
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
        #endregion >> 부서정보


        #region >> 직급/직책정보
        /// <summary>
        /// 직급/직책조회
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T_STORE_POSITION> GetPosition(T_STORE_POSITION Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_POSITION.xml", "GetPositionList"
                                    , Cond.STORE_CODE.ToString("0")
                                    , Cond.POSITION_TYPE.ToString("")
                                    , Cond.POSITION_CODE.ToString("")
                                    , Cond.HIDE == null ? "" : (Cond.HIDE == false ? "0" : "1")
                                    );
            return db.ExecuteQuery<T_STORE_POSITION>(sql).ToList();
        }
        /// <summary>
        /// 직급/직책저장
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA PositionSave(List<T_STORE_POSITION> list, int REG_CODE)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_POSITION data in list)
                    {
                        if (data.MRC_EDIT_MODE != "D")
                        {
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_POSITION.xml", "PositionSave"
                                                                , data.STORE_CODE.ToString()
                                                                , data.POSITION_CODE.ToString()
                                                                , data.POSITION_TYPE.ToString()
                                                                , data.SEQ.ToString()
                                                                , data.NAME.ToString("")
                                                                , ((data.HIDE == null || data.HIDE == false) ? "0" : "1")
                                                                , data.REMARK.ToString("")
                                                                , REG_CODE
                                                                );
                            db.ExecuteCommand(sql);
                        }
                        else if(data.MRC_EDIT_MODE == "D"){
                            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_POSITION.xml", "PositionDelete"
                                                                , data.STORE_CODE.ToString()
                                                                , data.POSITION_CODE.ToString()
                                                                , data.POSITION_TYPE.ToString()
                                                                , REG_CODE.ToString()
                                                                );
                            db.ExecuteCommand(sql);
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
        /// 직급/직책 삭제
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA PositionDelete(List<T_STORE_DEPT> list)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {
                    foreach (T_STORE_DEPT data in list)
                    {
                        string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Basic\\T_STORE_POSITION.xml", "PositionDelete"
                                            , data.STORE_CODE.ToString("0")
                                            , data.DEPT_CODE.ToString("0")
                                            , data.INSERT_CODE
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
        #endregion >> 직급/직책정보

        #region >> 파일정보 저장
        /// <summary>                                                                                      
        /// T_FILE 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_FILE> GetFileList(T_FILE Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_FILE.xml", "GetFileList"
                                            , Param.TABLE_NAME.ToString("10")
                                            , Param.TABLE_KEY.ToString("1")
                                            , Param.FILE_SEQ.ToString()
                                              );
            return db.ExecuteQuery<T_FILE>(sql).ToList();
        }
       

        /// <summary>                                                                                      
        /// T_FILE 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA FileSave(T_FILE Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                string sql = string.Empty;
                using (TransactionScope tran = new TransactionScope())
                {
                    if (Param.SAVE_TYPE == "D")
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_FILE.xml", "FileDelete"
                                                       , Param.TABLE_NAME.ToString("-1")
                                                       , Param.TABLE_KEY.ToString("-1")
                                                       , Param.FILE_SEQ.ToString("-1")
                                                      

                         );
                    }
                    else
                    {
                        sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_FILE.xml", "FileSave"
                                                       , Param.TABLE_NAME.ToString("-1")
                                                       , Param.TABLE_KEY.ToString("-1")
                                                       , Param.FILE_SEQ.ToString("-1")
                                                       , Param.FILE_TYPE.ToString("1")
                                                       , Param.FILE_NAME.ToString("")
                                                       , Param.FILE_EXT.ToString("")
                                                       , Param.FILE_URL.ToString("")
                                                       , Param.REF_DATA1.ToString("")
                                                       , Param.REF_DATA2.ToString("")
                                                       , Param.REMARK.ToString("")
                                                       , Param.INSERT_CODE.ToString("0")

                         );
                    }
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
        #endregion >> 파일저장


        #region >> 메모정보	        /// <summary>                                                                                      
        /// T_MEMO 조회하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public List<T_MEMO> GetMemoList(T_MEMO_COND Param)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_MEMO.xml", "GetMemoList"
                                            , Param.PAGE_COUNT.ToString("10")
                                            , Param.PAGE.ToString("1")
                                            , Param.SORT_ORDER.ToString("A.IDX DESC")
                                            , Param.IDX.ToString("")
                                            , Param.TABLE_NAME.ToString("")
                                            , Param.TABLE_KEY.ToString("")
                                              );
            return db.ExecuteQuery<T_MEMO>(sql).ToList();
        }

        /// <summary>                                                                                      
        /// T_MEMO 저장하기																	  
        /// </summary>																					  
        /// <param name="Param"></param>																	  
        /// <returns></returns>																			  
        public RTN_SAVE_DATA MemoSave(T_MEMO Param)
        {

            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                using (TransactionScope tran = new TransactionScope())
                {

                    string sql = MemoSaveSqlData(Param);
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
        /// 메모 저장 쿼리 가져오기
        /// </summary>
        /// <param name="Param"></param>
        /// <returns></returns>
        public string MemoSaveSqlData(T_MEMO Param)
        {
            string sql = string.Empty;
            if (Param.SAVE_TYPE == "D")
            {
                sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_MEMO.xml", "MemoDelete"
                                                  , Param.IDX.ToString("-1")
                                                  );
            }
            else
            {
                sql = Global.DBAgent.LoadSQL(sqlBasePath + "BizService\\Common\\T_MEMO.xml", "MemoSave"
                                                , Param.IDX.ToString("-1")
                                                , Param.TABLE_NAME.ToString("")
                                                , Param.TABLE_KEY.ToString("")
                                                , Param.MEMO.ToString("")
                                                , Param.MEMO1.ToString("")
                                                , Param.MEMO2.ToString("")
                                                , Param.INSERT_CODE.ToString("")

                  );
             
            }
            return sql;
        }

		#endregion

		public List<string> GetTableDesc()
		{
		
			return db.ExecuteQuery<string>(" EXEC dbo.SP_TableDescHtmlList").ToList();
		}


	}
}
