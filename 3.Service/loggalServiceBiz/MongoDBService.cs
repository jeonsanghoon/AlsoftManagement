using ALT.Framework;
using ALT.Framework.Data;
using ALT.VO.Common;
using ALT.VO.loggal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace loggalServiceBiz
{

    /// <summary>
    /// 몽고DB 관련 광고재생 집계서비스
    /// </summary>
    public class MongoDBService : BaseService
    {

        MongoClient client;
        IMongoDatabase database;
        public MongoDBService()
        {
            MongoCredential credentials = MongoCredential.CreateCredential(
                databaseName: Global.ConfigInfo.MONGODB_DATABASE ,
                username: Global.ConfigInfo.MONGODB_USERNAME,
                password:Global.ConfigInfo.MONGODB_PASSWORD
                
            );
            MongoClientSettings settings =  new MongoClientSettings
            {
               // Credentials = new[] { credentials },
                Server = new MongoServerAddress(Global.ConfigInfo.MONGODB_HOST, Convert.ToInt32(Global.ConfigInfo.MONGODB_PORT))
            };
            client = new MongoClient(settings);
            database = client.GetDatabase(Global.ConfigInfo.MONGODB_DATABASE);
        }

        public RTN_SAVE_DATA AdPlayLogSave(T_AD_PLAY_LOG_MONGO Param, string AD_CODES = null, string AD_TITLES = null)
        {
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            try
            {
                //logger.Debug("AdPlayLogSave >> ");
                var collection = database.GetCollection<BsonDocument>("adPlayLog");
                //logger.Debug("AdPlayLogSave >> 1");
                Param.REG_DATE = DateTime.Now;
                Param.REG_DAY = Param.REG_DATE.ToString("yyyyMMdd");

                
                if (string.IsNullOrEmpty(AD_CODES))
                {
                  //  logger.Debug("AdPlayLogSave >> InsertOne");
                    collection.InsertOne(Param.ToBsonDocument());

                }
                else {
                    String[] arrAdCode = AD_CODES.Split('|');
                    String[] arrAdTitle = AD_TITLES.Split('|');

                    List<BsonDocument> saveList = new List<BsonDocument>();
                    for(var i=0; i< arrAdCode.Length; i++)
                    {
                        T_AD_PLAY_LOG_MONGO data = new T_AD_PLAY_LOG_MONGO();
                        data = Param;
                        data.AD_CODE = Convert.ToInt64(arrAdCode[i]);
                        data.TITLE = arrAdTitle[i];
                        saveList.Add(data.ToBsonDocument());
                    }
                   //logger.Debug("AdPlayLogSave >> InsertMany");
                    collection.InsertMany(saveList);
                }
            }
            catch (Exception ex) {
                rtn.ERROR_MESSAGE = ex.Message;
                logger.Debug("AdPlayLogSave >> " + ex.Message);
            }
            return rtn;
        }

        public T_AD_PLAY_LOG_MONGO_LIST getAdPlayLogList(T_AD_PLAY_LOG_COND Cond)
        {
            T_AD_PLAY_LOG_MONGO_LIST rtn = new T_AD_PLAY_LOG_MONGO_LIST();

            var filterBuilder = Builders<T_AD_PLAY_LOG_MONGO>.Filter;
            var sort = Builders<T_AD_PLAY_LOG_MONGO>.Sort.Ascending("_id");

            if (!string.IsNullOrEmpty(Cond.SORT_ORDER))
            {
                String[] arrSort = Cond.SORT_ORDER.Split(' ');

                if (arrSort.Count() == 2 && arrSort[1].ToUpper() == "DESC")
                {
                    sort = Builders<T_AD_PLAY_LOG_MONGO>.Sort.Descending(arrSort[0]);
                }
                else {
                    sort = Builders<T_AD_PLAY_LOG_MONGO>.Sort.Ascending((arrSort[0]));
                }
            }
            #region 조건
            var filter = filterBuilder.Exists(s=>s._id);
            
            if (Cond.AD_CODE != null)
            {
                filter = filter & filterBuilder.Eq(s => s.AD_CODE, Cond.AD_CODE);
            }
            if (!String.IsNullOrEmpty(Cond.TITLE)) filter = filter & filterBuilder.Regex(s => s.TITLE, new BsonRegularExpression(".*" + Cond.TITLE + ".*", "i" ));

            if (!string.IsNullOrEmpty(Cond.FR_DATE))
            {
                filter = filter & filterBuilder.Gte(s => s.REG_DATE, Cond.FR_DATE.ToDate());
            }

            if (!string.IsNullOrEmpty(Cond.TO_DATE))
            {
                filter = filter & filterBuilder.Lt(s => s.REG_DATE, Cond.TO_DATE.ToDate().Value.AddDays(1));
            }

            if (Cond.BANNER_KIND != null)
            {
                filter = filter & filterBuilder.Eq(s => s.BANNER_KIND, Cond.BANNER_KIND);
            }

            if (Cond.DEVICE_CODE != null)
            {
                filter = filter & filterBuilder.Eq(s => s.DEVICE_CODE, Cond.DEVICE_CODE);
            }

            if (Cond.DEVICE_KIND != null)
            {
                filter = filter & filterBuilder.Eq(s => s.DEVICE_KIND, Cond.DEVICE_KIND);
            }
            if (!String.IsNullOrEmpty(Cond.DEVICE_NAME)) filter = filter & filterBuilder.Regex(s => s.DEVICE_NAME, new BsonRegularExpression(".*" + Cond.DEVICE_NAME + ".*", "i"));
            #endregion

            int firstPage = (int)((Cond.PAGE - 1) * Cond.PAGE_COUNT);
            var collection = database.GetCollection<T_AD_PLAY_LOG_MONGO>("adPlayLog") ;
            var list = collection.Find(filter).Sort(sort);
        
#pragma warning disable CS0618 // 형식 또는 멤버는 사용되지 않습니다.
            rtn.TOTAL_ROWCOUNT = list.Count();
#pragma warning restore CS0618 // 형식 또는 멤버는 사용되지 않습니다.
            rtn.list = list.Skip(firstPage).Limit(Cond.PAGE_COUNT).ToList();
            return rtn;
        }

         
        /// <summary>
        /// 몽고 DB 데이터 Sql 서버로 업데이트(일자별)
        /// </summary>
        /// <param name="FR_DATE"></param>
        /// <param name="TO_DATE"></param>
        /// <returns></returns>
        public RTN_SAVE_DATA MongoDBToSqlServer(string FR_DATE, string TO_DATE = null, string gubun="yyyyMMdd")
        {
            TO_DATE = TO_DATE ?? FR_DATE;
            RTN_SAVE_DATA rtn = new RTN_SAVE_DATA();
            DateTime frDate = Convert.ToDateTime(FR_DATE.ToDate());
            DateTime toDate = Convert.ToDateTime(TO_DATE.ToDate().Value.AddDays(1));
            var collection = database.GetCollection<T_AD_PLAY_LOG_MONGO>("adPlayLog");
         
            List<T_AD_PLAY_LOG_MONGO_DAY> list =  collection.Aggregate().Match(k => k.REG_DATE >= frDate && k.REG_DATE < toDate )
                        .Group(x => new  { x.REG_DAY,
                                          x.AD_CODE,
                                          x.BANNER_TYPE2,
                                          x.DEVICE_CODE,
                                          x.DEVICE_KIND,
                                          x.FRAME_TYPE,
                                          x.PLAY_TYPE,
                                          x.BANNER_KIND
                        }
                                , g=> new T_AD_PLAY_LOG_MONGO_DAY { REG_DAY = g.First().REG_DAY
                                                                    ,REG_DATE = g.First().REG_DATE
                                                                    ,DEVICE_CODE = g.First().DEVICE_CODE
                                                                    ,AD_CODE = g.First().AD_CODE
                                                                    ,BANNER_TYPE2 = g.First().BANNER_TYPE2
                                                                    ,DEVICE_KIND = g.First().DEVICE_KIND
                                                                    ,BANNER_KIND = g.First().BANNER_KIND
                                                                    ,FRAME_TYPE = g.First().FRAME_TYPE
                                                                    ,PLAY_TYPE = g.First().PLAY_TYPE
                                                                    ,TOT_PLAY_TIME = g.Sum(x => x.PLAY_TIME / (decimal)(x.FRAME_TYPE ??1))
                                                                    ,TOT_QTY = g.Count()   }).ToList();
            //일자별 광고재생 로그 저장
            //return null;
            StringBuilder sbSql = new StringBuilder();
            /*"SELECT '{0}' REG_DAY,{1} BANNER_TYPE2,{2} DEVICE_KIND,{3} DEVICE_CODE,{4} AD_CODE,{5} FRAME_TYPE,{6} PLAY_TYPE,{7} TOT_PLAY_TIME,{8} TOT_QTY ,GETDATE() "*/
            string strInsertQueryHeader = "INSERT INTO T_AD_PLAY_LOG_MONGO_DAY(REG_DAY,DEVICE_CODE,AD_CODE,BANNER_TYPE2,DEVICE_KIND,BANNER_KIND, FRAME_TYPE,PLAY_TYPE,TOT_PLAY_TIME,TOT_QTY,INSERT_DATE) ";
            string strInsertQuery = "SELECT '{0}',{1},{2},{3},{4},{5},{6},{7},{8},{9},GETDATE() ";
            try
            {
                if (list.Count() > 0)
                {
                    string deleteQuery = "DELETE FROM T_AD_PLAY_LOG_MONGO_DAY WHERE REG_DAY >='" + FR_DATE + "' AND REG_DAY <  CONVERT(VARCHAR(8), DATEADD(DAY, 1, CONVERT(DATETIME, '" + TO_DATE + "')),112)";
                    db.ExecuteCommand(deleteQuery);

                    for (var i = 0; i < list.Count(); i++)
                    {

                        if (i > 0)
                        {
                            if (i % 50 == 0)
                            {
                                db.ExecuteCommand(strInsertQueryHeader + sbSql.ToString());
                                sbSql.Remove(0, sbSql.Length);
                            }
                            else
                            {
                                sbSql.Append(" UNION ALL ");
                            }

                        }
                        sbSql.Append(string.Format(strInsertQuery, list[i].REG_DAY
                                                      , list[i].DEVICE_CODE.ToString("null")
                                                      , list[i].AD_CODE.ToString()
                                                      , list[i].BANNER_TYPE2.ToString()
                                                      , list[i].DEVICE_KIND.ToString()
                                                      , list[i].BANNER_KIND.ToString()
                                                      , list[i].FRAME_TYPE.ToString()
                                                      , list[i].PLAY_TYPE.ToString()
                                                      , list[i].TOT_PLAY_TIME.ToString()
                                                      , list[i].TOT_QTY.ToString()
                                                      ));
                    }

                    db.ExecuteCommand(strInsertQueryHeader + sbSql.ToString());
                }
            } catch (Exception ex)
            {
                rtn.ERROR_MESSAGE = ex.Message;
            }
            return rtn;

        }
#region >> 데이터 임의 업데이트 관련
        public void mongodbRegDayUpdate(string BASE_DATE)
        {
            var collection = database.GetCollection<T_AD_PLAY_LOG_MONGO>("adPlayLog");

            var filterBuilder = Builders<T_AD_PLAY_LOG_MONGO>.Filter;
            var filter = filterBuilder.Exists(s => s._id);

            filter = filter & filterBuilder.Gte(s => s.REG_DATE, BASE_DATE.ToDate());
            filter = filter & filterBuilder.Lt(s => s.REG_DATE, BASE_DATE.ToDate().Value.AddDays(1));
            collection.UpdateMany(filter, Builders<T_AD_PLAY_LOG_MONGO>.Update.Set("REG_DAY", BASE_DATE));
        }
        /// <summary>
        /// 일자별 배너재생 집계리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AdPlayTotalList> GetAdPlayDailyTotalList(AdPlayTotalCond Cond)
        {

            
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG_MONGO_DAY.xml", "GetAdPlayDailyTotalList"
                                         , Cond.PAGE.ToString("1")
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.SORT_ORDER.ToString("A.REG_DAY DESC")
                                         , Cond.FR_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.TO_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.AD_CODE.ToString("")
                                         , Cond.TITLE.ToString("")
                                         , Cond.BANNER_TYPE2.ToString("")
                                         , Cond.BANNER_KIND.ToString("")
                                         , Cond.DEVICE_KIND.ToString("")
                                         , Cond.PLAY_TYPE.ToString("")
                                         , Cond.FRAME_TYPE.ToString("")
                                         , Cond.STORE_CODE.ToString("")
                                         , Cond.STORE_NAME.ToString("")
                                           );
            return db.ExecuteQuery<AdPlayTotalList>(sql).ToList();
        }

        /// <summary>
        /// 일자별로컬박스재생집계
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AdPlayLocalboxTotalList> GetAdPlayDailyLocalboxTotalList(AdPlayLocalboxTotalCond Cond)
        {


            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG_MONGO_DAY.xml", "GetAdPlayDailyLocalboxTotalList"
                                         , Cond.PAGE.ToString("1")
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.SORT_ORDER.ToString("A.REG_DAY DESC")
                                         , Cond.FR_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.TO_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.DEVICE_CODE.ToString("")
                                         , Cond.DEVICE_NAME.ToString("")
                                         , Cond.STORE_CODE.ToString("")
                                         , Cond.STORE_NAME.ToString("")

                                         );
            return db.ExecuteQuery<AdPlayLocalboxTotalList>(sql).ToList();
        }


        /// <summary>
        /// 광고주별 배너 재생 집계리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AdPlayBusiTotalList> GetAdPlayBusiTotalList(AdPlayTotalCond Cond)
        {


            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG_MONGO_DAY.xml", "GetAdPlayBusiTotalList"
                                         , Cond.PAGE.ToString("1")
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.SORT_ORDER.ToString("STORE_CODE")
                                         , Cond.FR_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.TO_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.STORE_CODE.ToString("")
                                         , Cond.STORE_NAME.ToString("")
                                         );
            return db.ExecuteQuery<AdPlayBusiTotalList>(sql).ToList();
        }

        /// <summary>
        /// 사업주별 광고 플레이 집계리스트
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AdPlayBusiTotalList> GetAdPlayBusiLocalTotalList(AdPlayTotalCond Cond)
        {


            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG_MONGO_DAY.xml", "GetAdPlayBusiLocalTotalList"
                                         , Cond.PAGE.ToString("1")
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.SORT_ORDER.ToString("STORE_CODE")
                                         , Cond.FR_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.TO_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.STORE_CODE.ToString("")
                                         , Cond.STORE_NAME.ToString("")
                                         );
            return db.ExecuteQuery<AdPlayBusiTotalList>(sql).ToList();
        }

        /// <summary>
        /// 월별 광고 플레이 집계리스트 
        /// </summary>
        /// <param name="Cond"></param>
        /// <returns></returns>
        public List<AdPlayBusiTotalList> GetAdPlayMonthTotalList(AdPlayTotalCond Cond)
        {


            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG_MONGO_DAY.xml", "GetAdPlayMonthTotalList"
                                         , Cond.PAGE.ToString("1")
                                         , Cond.PAGE_COUNT.ToString("10")
                                         , Cond.SORT_ORDER.ToString("MAX(A.REG_DAY) DESC")
                                         , Cond.FR_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         , Cond.TO_DATE.RemoveDateString().ToString(DateTime.Now.ToString("yyyyMMdd"))
                                         );
            return db.ExecuteQuery<AdPlayBusiTotalList>(sql).ToList();
        }

        public SCHEDULER_INFO GetAdPlayShedulerInfo()
        {
            string sql = Global.DBAgent.LoadSQL(sqlBasePath + "Advertising\\T_AD_PLAY_LOG_MONGO_DAY.xml", "GetAdPlayShedulerInfo" );
            return db.ExecuteQuery<SCHEDULER_INFO>(sql).FirstOrDefault();
        }
        public void mongodbRegDayDateTypeUpdate()
        {
            /*
             * 
             * 
             * use admin
             * db.adPlayLog.find({ PLAY_TIME: { $type: "string" } }).forEach(function(doc) { 
     print('Updating price for ' + doc._id);
     db.adPlayLog.update( 
        { _id: doc._id },
        { $set : { PLAY_TIME : NumberDecimal(doc.PLAY_TIME) } }
     )
 })
 db.adPlayLog.find({$or:[{BANNER_KIND: null },{BANNER_KIND: 0}] }).forEach(function(doc) { 
     print('Updating price for ' + doc._id);
     db.adPlayLog.update( 
        { _id: doc._id },
        { $set : { BANNER_KIND : 2, BANNER_KIND_NAME:"일반배너" } }
     )
 })

            칼럼삭제 
            db.adPlayLog.update({},{ $unset: {BANNER_KIND2: 1, BANNER_KIND2_NAME: 1}},{ multi: true })

              */
        }
        #endregion

        ~MongoDBService()
        {
            client = null;
            database = null;
        }
    }
}
