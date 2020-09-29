using System;
using MongoDB.Bson.Serialization.Attributes;

namespace QueueSDK.Domain
{
    [BsonIgnoreExtraElements]
    public class PrintedDocument
    {
        public string DocumentName { get; set; }
        public string SentDate { get; set; }
        public string PrintDate { get; set; }
    }
}
