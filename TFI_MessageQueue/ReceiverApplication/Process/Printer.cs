using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using QueueSDK.Interfaces;
using QueueSDK.Serialization;
using ReceiverApplication.Services;

namespace ReceiverApplication.Process
{
    public class Printer
    {
        private INotifier<string> notifier = null;
        private static Printer _instance = null;
        Random rnd = new Random();

        private Printer()
        {
            notifier = JobStatusService.GetInstance();
        }

        public static Printer GetInstance()
        {
            if (_instance == null) _instance = new Printer();
            return _instance;
        }

        public void TryPrint(string doc) {
            double successProbability = rnd.NextDouble();
            Console.WriteLine($"Trying to print {doc}");
            if (successProbability > 0.35 && notifier != null) Print(doc);
        }

        private void Print(string doc)
        {
            int delay = rnd.Next(1000, 3500);
            Thread t = new Thread(
                () =>
                {
                    Thread.Sleep(delay);
                    ExpandoObject printRequest = JobSerialization.Deserialize(doc);
                    printRequest.TryAdd("PrintDate", DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss"));
                    notifier.SendToQueue(JobSerialization.Serialize(printRequest));
                }
            );
            t.Start();
        }
    }
}
