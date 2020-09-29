using System;
using System.Dynamic;
using MongoDB.Bson;
using QueueSDK.Data;

namespace JobLogger.Data
{
    public class Persistance
    {
        public void SavePrinting(ExpandoObject job)
        {
            var printings = MongoConnectionProvider.GetDBCollection();
            try
            {
                printings.InsertOne(job.ToBsonDocument());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
