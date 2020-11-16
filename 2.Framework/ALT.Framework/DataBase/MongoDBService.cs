using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALT.Framework.DataBase
{
    /// <summary>
    /// 망고 DB 서비스 클래스
    /// </summary>
    public class MongoDBService
    {
        MongoClient client;
        IMongoDatabase database;
        string CollectionName;

        public MongoDBService()
        {
            client = new MongoClient(Global.ConfigInfo.MONGODB_HOST ?? "mongodb://localhost:27017");
            database = client.GetDatabase(Global.ConfigInfo.MONGODB_DATABASE ?? "admin");
        }

        public MongoDBService(string host, string dbName)
        {
            client = new MongoClient(host);
            database = client.GetDatabase(dbName);
        }

        public BsonDocument GetQueryFromString(string jsonQuery)
        {
            return new BsonDocument(BsonSerializer.Deserialize<BsonDocument>(jsonQuery));
        }
        public IList<T> QueryFromString<T>(string jsonQuery, string collectionName = null)
        {
            if (string.IsNullOrEmpty(collectionName))
                collectionName = this.CollectionName;

            var query = GetQueryFromString(jsonQuery);
            var items = database.GetCollection<T>(collectionName).Find(query);

            return items.ToList<T>();
        }


        public IList<T> QueryFromObject<T>(object queryObject, string collectionName = null)
        {
            if (string.IsNullOrEmpty(collectionName))
                collectionName = this.CollectionName;

            var query = new BsonDocument(queryObject.ToBsonDocument());
            var items = database.GetCollection<T>(collectionName).Find(query);

            return items.ToList<T>();
        }
    }
}
