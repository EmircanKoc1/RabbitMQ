using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.RequestResponse.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            string requestQueueName = "example-request-response-queue";

            channel.QueueDeclare(
                queue: requestQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false);


            string replyQueueName = channel.QueueDeclare().QueueName;
            string correlationId = Guid.NewGuid().ToString();

            IBasicProperties properties = channel.CreateBasicProperties();

            properties.CorrelationId = correlationId;
            properties.ReplyTo = replyQueueName;

            for (int i = 0; i < 100; i++)
            {

                var message = Encoding.UTF8.GetBytes("merhaba " + i);
                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: requestQueueName,
                    basicProperties: properties,
                    body: message);

            }

            EventingBasicConsumer consumer = new(channel);
            channel.BasicConsume(
                queue: replyQueueName,
                autoAck: true,
                consumer: consumer);

            consumer.Received += (sender, e) =>
            {
                if(e.BasicProperties.CorrelationId == correlationId)
                {
                    var stringMessage = Encoding.UTF8.GetString(e.Body.Span);
                    Console.WriteLine(stringMessage);
                }


            };


            Console.ReadLine();
        }
    }
}
