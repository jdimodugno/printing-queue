using System.Text;
using QueueSDK.Definitions;
using QueueSDK.Interfaces;
using RabbitMQ.Client;

namespace ReceiverApplication.Services
{
    public class JobStatusService : INotifier<string>
    {
        private static JobStatusService instance = null;
        readonly ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

        public static JobStatusService GetInstance()
        {
            if (instance == null) instance = new JobStatusService();
            return instance;
        }

        public void SendToQueue(string printedDocument)
        {
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: JobStatusQueue.QueueName,
                                 durable: JobStatusQueue.Durable,
                                 exclusive: JobStatusQueue.Exclusive,
                                 autoDelete: JobStatusQueue.AutoDelete,
                                 arguments: JobStatusQueue.Arguments);

            byte[] body = Encoding.UTF8.GetBytes(printedDocument);

            channel.BasicPublish(exchange: "",
                                 routingKey: JobStatusQueue.QueueName,
                                 basicProperties: null,
                                 body: body);

            channel.Close();
            connection.Close();
        }
    }
}
