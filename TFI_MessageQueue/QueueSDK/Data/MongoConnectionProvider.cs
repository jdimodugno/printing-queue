using MongoDB.Bson;
using MongoDB.Driver;

namespace QueueSDK.Data
{
    public class MongoConnectionProvider
    {
        public static IMongoCollection<BsonDocument> GetDBCollection()
        {
            MongoClient dbClient = new MongoClient("mongodb://127.0.0.1:27017");
            IMongoDatabase db = dbClient.GetDatabase("printer");
            return db.GetCollection<BsonDocument>("printings");
        }
    }
}
