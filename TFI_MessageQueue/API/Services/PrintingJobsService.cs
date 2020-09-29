using System;
using System.Text;
using API.Domain;
using QueueSDK.Definitions;
using QueueSDK.Domain;
using QueueSDK.Interfaces;
using QueueSDK.Serialization;
using RabbitMQ.Client;

namespace API.Services
{
    public class PrintingJobsService : INotifier<Payload>
    {
        static PrintingJobsService instance = null;
        readonly ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        public static PrintingJobsService GetInstance()
        {
            if (instance == null) instance = new PrintingJobsService();
            return instance;
        }

        public void SendToQueue(Payload doc)
        {
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: PrintingJobsQueue.QueueName,
                                 durable: PrintingJobsQueue.Durable,
                                 exclusive: PrintingJobsQueue.Exclusive,
                                 autoDelete: PrintingJobsQueue.AutoDelete,
                                 arguments: PrintingJobsQueue.Arguments);

            string message = JobSerialization.Serialize(
                new PrintRequest() { DocumentName = doc.Path, SentDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") }
            );

            byte[] body = Encoding.UTF8.GetBytes(message);

            IBasicProperties properties = channel.CreateBasicProperties();
            properties.Priority = Convert.ToByte(doc.Priority);

            channel.BasicPublish(exchange: "",
                                 routingKey: PrintingJobsQueue.QueueName,
                                 basicProperties: properties,
                                 body: body);

            channel.Close();
            connection.Close();
        }
    }
}
