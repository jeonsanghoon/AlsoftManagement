using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ALT.VO.loggal;
using ALT.Framework;
using ALT.Framework.Data;
using System.Data.Linq;
using ALT.VO.Common;
using System.Transactions;

namespace loggalServiceBiz
{
    public class CategoryService : BaseService
    {
        public CategoryService() { }
        public CategoryService(DataContext _db) : base(_db) { }

        /// <summary>
        /// 광고카테고리리스트가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<CATEGORY_LIST> GetCategoryList(CATEGORY_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Category.xml", "GetCategoryList"
                                         , Cond.CATEGORY_CODE.ToString("")
                                         , Cond.CATEGORY_TYPE.ToString("")
                                         , Cond.CATEGORY_TYPES.ToString("")
                                         , Cond.PARENT_CATEGORY_CODE.ToString("")
                                         , Cond.LEVEL_DEPTH.ToString("")
                                         , Cond.SEARCH_CATEGORY_CODE.ToString("")
                                         , Cond.CATEGORY_NAME.ToString("")
                                         , (Cond.HIDE == null || Cond.HIDE == false) ? "0" : "1"
             );


            return db.ExecuteQuery<CATEGORY_LIST>(sql).ToList();
        }


        /// <summary>
        /// 광고카테고리리스트가져오기
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public IList<MOBILE_AD_LIST> GetAdList(AD_SEARCH_COND Cond)
        {
			// 디폴트 강남역 위도 경도
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad_SearchMobileBanner.xml", "Ad_SearchMobileBanner2"
										 , Cond.PageCount.ToString("15")
										 , Cond.Page.ToString("1")
										 , Cond.MEMBER_CODE.ToString("-1")
										 , Cond.LATITUDE.ToString("37.4969117")
										 , Cond.LONGITUDE.ToString("127.0329201")
			);

            return db.ExecuteQuery<MOBILE_AD_LIST>(sql).ToList();
        }



        /// <summary>
        /// 모바일 로컬박스별 광고리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public AD_DEVICE_MOBILE_M GetMobileAdDeviceList(AD_DEVICE_MOBILE_COND Cond)
        {
            AD_DEVICE_MOBILE_M rtnData = new AD_DEVICE_MOBILE_M();
            string shide = "";
            if (Cond.HIDE == null)
            {
                shide = "0";
            }
            else if (Cond.HIDE.ToString("") == "3")
            {
                shide = "";
            }
            else
            {
                shide = Cond.HIDE.ToString("");
            }

            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetMobileAdDeviceM"
                                        , Cond.DEVICE_CODE.ToString()
                                        , Cond.USER_ID.ToString("")

            );

            rtnData = db.ExecuteQuery<AD_DEVICE_MOBILE_M>(sql).FirstOrDefault();


            sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Ad.xml", "GetMobileAdDeviceList"
                                         , Cond.PAGE_COUNT.ToString("10000")
                                         , Cond.PAGE.ToString("1")
                                         , Cond.DEVICE_CODE.ToString()
                                         , Cond.USER_ID.ToString("")
                                         , Cond.CATEGORY_TYPE.ToString("3")
                                         , Cond.AD_CODE.ToString("")
                                         , Cond.REP_CATEGORY_CODE.ToString("")
                                         , Cond.KEYWORD_NAME.ToString("")
                                         , Cond.CK_CODE.ToString("")

                                         , shide
             );

            rtnData.AD_LIST = db.ExecuteQuery<AD_DEVICE_MOBILE_LIST>(sql).ToList();

            return rtnData;
        }


        public List<CATEGORY_PAGE_LIST> GetCategoryPageList(CATEGORY_PAGE_COND Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Category.xml", "GetCategoryPageList"
                                                , Cond.PAGE_COUNT.ToString("10")
                                                , Cond.PAGE.ToString("1")
                                                , Cond.CATEGORY_CODE.ToString("")
                                                , Cond.CATEGORY_TYPE.ToString("")
                                                , Cond.CATEGORY_TYPE2.ToString("")
                                                , Cond.PARENT_CATEGORY_CODE.ToString("")
                                                , Cond.LEVEL_DEPTH.ToString("")
                                                , Cond.SEARCH_CATEGORY_CODE.ToString("")
                                                , Cond.CATEGORY_NAME.ToString("")
                                                , (Cond.HIDE == null || Cond.HIDE == false ? "" : "1")
                                               );

           return db.ExecuteQuery<CATEGORY_PAGE_LIST>(sql).ToList();
        }



        public RTN_SAVE_DATA CategorySave(List<T_CATEGORY> list, int? REG_CODE)
        {
            RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
            try
            {

                using (TransactionScope tran = new TransactionScope())
                {
                    
                    string sql = string.Empty;
                    foreach (T_CATEGORY Param in list)
                    {
                        if (Param.MRC_EDIT_MODE != "D")
                        {
                            // if (sbSql.Length != 0) sbSql.Append("\n UNION ALL");

                            string sqlCond = Global.DBAgent.LoadCondSQL(sqlBasePath + "Advertising\\Category.xml", "CategoryListSave_Cond"
                                                          , Param.CATEGORY_CODE.ToString()
                                                          , Param.CATEGORY_TYPE.ToString("1")
                                                          , Param.CATEGORY_TYPE2.ToString("1")
                                                          , Param.PARENT_CATEGORY_CODE.ToString()
                                                          , Param.LEVEL_DEPTH.ToString("1")
                                                          , Param.SEARCH_CATEGORY_CODE.ToString("")
                                                          , Param.CATEGORY_NAME.ToString("")
                                                          , Param.CATEGORY_DISPlAY_NAME.ToString("")
                                                          , Param.ORDER_SEQ.ToString()
                                                          , Param.SEARCH_CATEGORY_CODE.ToString()
                                                          , Param.HIDE == true ? "1" : "0"
                                                          , Param.REMARK.ToString("")
                                                          , REG_CODE

                             );

                            sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Category.xml", "CategoryListSave"
                                                         , sqlCond
                                                         );
                            rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                            if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE)) throw new Exception(rtnData.ERROR_MESSAGE);

                        }
                        else
                        {
                            sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\Category.xml", "CategoryDelete"
                                                           , Param.CATEGORY_CODE.ToString("-1")
                                                           , Param.INSERT_CODE
                              );
                            rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
                        }
                        if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                        {

                            throw new Exception(rtnData.ERROR_MESSAGE);
                        }
                    }
                

                    if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
                    {
                        throw new Exception(rtnData.ERROR_MESSAGE);
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


        public List<T_CATEGORY_KEYWORD> GetCategoryKeywordList(T_CATEGORY_KEYWORD Cond)
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\CategoryKeyword.xml", "GetCategoryKeywordList"
                                                , Cond.CK_CODE
                                                , Cond.CATEGORY_CODE.ToString("")
                                                , Cond.KEYWORD_CODE.ToString("")
                                                , Cond.KEYWORD_NAME.ToString("")
                                                , Cond.HIDE.ToString("")
                                                , (Cond.HIDE == null || Cond.HIDE == true ? "" : "0")
                                               );
            return db.ExecuteQuery<T_CATEGORY_KEYWORD>(sql).ToList();
        }




		public List<T_PLACE_ITEM_GROUP> GetPlaceItemGroupPageList(T_PLACE_ITEM_GROUP_COND Cond)
		{
			string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\PlaceItemGroup.xml", "GetPlaceItemGroupList"
												, Cond.SORT_ORDER.ToString("A.GROUP_CODE")
											   );

			return db.ExecuteQuery<T_PLACE_ITEM_GROUP>(sql).ToList();
		}



		public RTN_SAVE_DATA PlaceItemGroupSave(List<T_PLACE_ITEM_GROUP> list, int? REG_CODE)
		{
			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
			try
			{

				using (TransactionScope tran = new TransactionScope())
				{

					string sql = string.Empty;
					foreach (T_PLACE_ITEM_GROUP Param in list)
					{
						if (Param.MRC_EDIT_MODE != "D")
						{
							sql = Global.DBAgent.LoadCondSQL(sqlBasePath + "Advertising\\PlaceItemGroup.xml", "PlaceItemGroupSave"
														  , Param.GROUP_CODE.ToString("")
														  , Param.GROUP_NAME.ToString("")
														  , Param.GROUP_SEQ.ToString("")
														  , Param.GROUP_TYPE.ToString("")
														  , Param.SALE_TYPE.ToString("")
														  , Param.HIDE == true ? "1" : "0"
														  , Param.REMARK.ToString("")
														  , REG_CODE

							 ) ;

							rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
							if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE)) throw new Exception(rtnData.ERROR_MESSAGE);

						}
						else
						{
							sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\PlaceItemGroup.xml", "PlaceItemGroupDelete"
													  , Param.GROUP_CODE.ToString("")
							  );
							rtnData = db.ExecuteQuery<RTN_SAVE_DATA>(sql).First();
						}
						if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
						{

							throw new Exception(rtnData.ERROR_MESSAGE);
						}
					}


					if (!string.IsNullOrEmpty(rtnData.ERROR_MESSAGE))
					{
						throw new Exception(rtnData.ERROR_MESSAGE);
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

		public RTN_SAVE_DATA PlaceItemGroupSeqSave(List<T_PLACE_ITEM_GROUP> list, int? REG_CODE)
		{
			RTN_SAVE_DATA rtnData = new RTN_SAVE_DATA() { };
			try
			{
				using (TransactionScope tran = new TransactionScope())
				{

					string sql = string.Empty;
					foreach (T_PLACE_ITEM_GROUP Param in list)
					{
						sql = Global.DBAgent.LoadCondSQL(sqlBasePath + "Advertising\\PlaceItemGroup.xml", "PlaceItemGroupSeqSave"
													  , Param.GROUP_CODE.ToString("")
													  , Param.GROUP_SEQ.ToString("")
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
	}
}
