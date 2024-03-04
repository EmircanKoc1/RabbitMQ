using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Logging;
using System.Text;

namespace RabbitMQ.RequestResponse.Consumer
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

            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(
                queue: requestQueueName,
                autoAck: true,
                consumer: consumer);

            consumer.Received += (sender, e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                Console.WriteLine(message);
                  
                var responseMessage = Encoding.UTF8.GetBytes(message + "bu response edildi");
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.CorrelationId = e.BasicProperties.CorrelationId;

                channel.BasicPublish(
                    exchange: string.Empty,
                    routingKey: e.BasicProperties.ReplyTo,
                    basicProperties: properties,
                    body: responseMessage);

            };


            Console.ReadLine();

        }

    }
}
