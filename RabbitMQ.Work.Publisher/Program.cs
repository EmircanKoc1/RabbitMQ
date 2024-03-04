using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Work.Publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {


            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            string queueName = "example-work-queue";

            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false);

            for (int i = 0; i < 100; i++)
            {
                Task.Delay(1000).Wait();
                var message = Encoding.UTF8.GetBytes("merhaba " + i);

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: queueName,
                    body: message);

            }




        }
    }
}
