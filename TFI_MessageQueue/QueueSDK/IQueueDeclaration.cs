using System;
using System.Collections.Generic;

namespace QueueSDK
{
    public interface IQueueDeclaration
    {
        static string QueueName { get; }
        static bool Durable { get; }
        static bool Exclusive { get; }
        static bool AutoDelete { get; }
        static Dictionary<string, object> Arguments { get; }
    }
}
