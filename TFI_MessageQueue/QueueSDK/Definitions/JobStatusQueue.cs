using System;
using System.Collections.Generic;

namespace QueueSDK.Definitions
{
    public class JobStatusQueue : IQueueDeclaration
    {
        public static string QueueName => "JobStatusQueue";

        public static bool Durable => false;

        public static bool Exclusive => false;

        public static bool AutoDelete => false;

        public static Dictionary<string, object> Arguments => new Dictionary<string, object>();
    }
}
