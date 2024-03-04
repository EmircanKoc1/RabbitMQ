using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Work.Consumer
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

            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(
                queue: queueName,
                autoAck: true,
                consumer: consumer);

            channel.BasicQos(
                prefetchCount: 1,
                prefetchSize: 0,
                global: false);


            consumer.Received += (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
            };

        }
    }
}
