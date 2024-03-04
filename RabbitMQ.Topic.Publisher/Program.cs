using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Topic.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            IConnection connection = factory.CreateConnection();
            IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: "topic-exchange-example",
                type: ExchangeType.Topic);

            for (int i = 0; i < 100; i++)
            {
                Task.Delay(100).Wait();
                var message = Encoding.UTF8.GetBytes($"Merhaba {i}");
                Console.Write("Topic belirtiniz");
                string topic = Console.ReadLine();
              
                channel.BasicPublish(
                    exchange: "topic-exchange-example",
                    routingKey: topic,
                    body: message);


            }



        }
    }
}
