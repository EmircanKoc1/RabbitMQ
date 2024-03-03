using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Fanout.RabbitMQ.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();


            channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

            var _queueName = "ozel-kuyruk";

            channel.QueueDeclare(
                queue: _queueName,
                exclusive: false);

            channel.QueueBind(
                queue: _queueName,
                exchange: "fanout-exchange-example",
                routingKey: string.Empty);


            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(queue: _queueName,
                autoAck: true,
                 consumer: consumer);

            consumer.Received += (sender, e) =>
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
            };

            Console.ReadLine();

        }
    }
}
