using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.P2P.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string queueName = "example-p2p-queue";
            channel.QueueDeclare(
                queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false);

            var message = Encoding.UTF8.GetBytes("merhaba");

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: queueName,
                body: message);

        }
    }
}
