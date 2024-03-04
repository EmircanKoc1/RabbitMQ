using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Direct.RabbitMQ.Consumer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();


            channel.ExchangeDeclare(
                exchange: "direct-exchange-example",
                type: ExchangeType.Direct);


            channel.QueueDeclare(
                queue: "direct-queue-example",
                exclusive: false);

            channel.QueueBind(
                queue: "direct-queue-example",
                exchange: "direct-exchange-example",
                routingKey: "direct-queue-example");


            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(
                queue: "direct-queue-example",
                autoAck: true,
                consumer: consumer);

            consumer.Received += (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message + "consume edildi");
            };

            Console.ReadLine();



        }
    }
}
