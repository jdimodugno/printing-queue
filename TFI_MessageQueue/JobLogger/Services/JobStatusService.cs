using System;
using System.Dynamic;
using System.Text;
using JobLogger.Data;
using QueueSDK.Definitions;
using QueueSDK.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace JobLogger.Services
{
    public class JobStatusService
    {
        Persistance persistance = null;

        public void Initialize()
        {
            persistance = new Persistance();
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: JobStatusQueue.QueueName,
                                 durable: JobStatusQueue.Durable,
                                 exclusive: JobStatusQueue.Exclusive,
                                 autoDelete: JobStatusQueue.AutoDelete,
                                 arguments: JobStatusQueue.Arguments);

            Console.WriteLine(" [*] Waiting for messages.");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine("[x] Received {0}", message);
                SaveJobStatus(message);
            };

            channel.BasicConsume(
                queue: JobStatusQueue.QueueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }

        private void SaveJobStatus(string jobFromMessage)
        {
            ExpandoObject toPersist = JobSerialization.Deserialize(jobFromMessage);
            persistance.SavePrinting(toPersist);
        }
    }
}