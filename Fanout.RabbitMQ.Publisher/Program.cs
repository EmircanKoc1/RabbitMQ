using RabbitMQ.Client;
using System.Text;

namespace Fanout.RabbitMQ.Publisher
{
    public class Program
    {

        static  void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();


            channel.ExchangeDeclare(exchange: "fanout-exchange-example", type: ExchangeType.Fanout);

            for (int i = 0; i < 100; i++)
            {
                Task.Delay(100).Wait();

                var message = Encoding.UTF8.GetBytes($"Merhaba {i}");

                channel.BasicPublish(
                    exchange: "fanout-exchange-example",
                    routingKey: string.Empty,
                    body: message);

                Console.WriteLine($"Merhaba {i} gönderildi");
            }



            Console.ReadLine();

        }
    }
}
