using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using QueueSDK.Data;
using QueueSDK.Domain;

namespace API.Data
{
    public class PrintingRepository
    {
        public static List<PrintedDocument> ReadPrintings()
        {
            var printings = MongoConnectionProvider.GetDBCollection();
            try
            {
                return printings.Find(new BsonDocument()).As<PrintedDocument>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public static PrintedDocument CheckPrintRequestStatus(string docName)
        {
            var printings = MongoConnectionProvider.GetDBCollection();
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("DocumentName", docName);
                return printings.Find(filter).As<PrintedDocument>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
