using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.PubSub.Publisher
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


            var message = Encoding.UTF8.GetBytes("merhaba");

            for (int i = 0; i < 100; i++)
            {
                channel.BasicPublish(
                     exchange: exchangeName,
                     routingKey: string.Empty,
                     body: message);
            }



        }
    }
}
