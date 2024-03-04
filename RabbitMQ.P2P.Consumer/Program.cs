using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.P2P.Consumer
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

            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);


            consumer.Received += (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);

            };


        }
    }
}
