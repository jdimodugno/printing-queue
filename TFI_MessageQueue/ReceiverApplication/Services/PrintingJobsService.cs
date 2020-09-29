using System;
using System.Text;
using QueueSDK.Definitions;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReceiverApplication.Process;

namespace ReceiverApplication.Services
{
    public class PrintingJobsService
    {
        private readonly Printer _printer;

        public PrintingJobsService(Printer printer)
        {
            _printer = printer;
        }

        public void Initialize()
        {
            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: PrintingJobsQueue.QueueName,
                                 durable: PrintingJobsQueue.Durable,
                                 exclusive: PrintingJobsQueue.Exclusive,
                                 autoDelete: PrintingJobsQueue.AutoDelete,
                                 arguments: PrintingJobsQueue.Arguments);

            Console.WriteLine(" [*] Waiting for messages.");

            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, eventArgs) =>
            {
                byte[] body = eventArgs.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);
                _printer.TryPrint(message);
            };

            channel.BasicConsume(
                queue: PrintingJobsQueue.QueueName,
                autoAck: true,
                consumer: consumer
            );

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
