using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.PubSub.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string exchangeName = "example-pub-sub-exchange";

            channel.ExchangeDeclare(
                exchange: exchangeName,
                type: ExchangeType.Fanout);

            string queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(
                queue: queueName,
                exchange: exchangeName,
                routingKey: string.Empty);


            //channel.BasicQos(
            //    prefetchSize: 0,
            //    prefetchCount: 1,
            //    global: false);


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
