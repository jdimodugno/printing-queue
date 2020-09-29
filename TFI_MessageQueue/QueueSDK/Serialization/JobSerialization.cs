using System.Collections.Generic;
using System.Dynamic;
using QueueSDK.Domain;

namespace QueueSDK.Serialization
{
    public class JobSerialization
    {
        public static string Serialize(PrintRequest job)
        {
            string result = string.Empty;
            foreach (var prop in job.GetType().GetProperties())
                result += $"{prop.Name}={prop.GetValue(job)};";

            return result;
        }

        public static string Serialize(ExpandoObject job)
        {
            string result = string.Empty;
            foreach (KeyValuePair<string, object> prop in job)
                result += $"{prop.Key}={prop.Value};";
            return result;
        }


        public static ExpandoObject Deserialize(string job)
        {
            string[] fieldPairs = job.Split(";");
            ExpandoObject deserialized = new ExpandoObject();

            foreach (string fieldPair in fieldPairs)
            {
                if (!string.IsNullOrEmpty(fieldPair))
                {
                    string[] keyValue = fieldPair.Split("=");
                    deserialized.TryAdd(keyValue[0], keyValue[1]);
                }
            }

            return deserialized;
        }
    }
}
