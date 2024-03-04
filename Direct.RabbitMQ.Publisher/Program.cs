using RabbitMQ.Client;
using System.Text;

namespace Direct.RabbitMQ.Publisher
{
    public class Program
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

            for (int i = 0; i < 50; i++)
            {
                Task.Delay(400).Wait();

                var message = Encoding.UTF8.GetBytes($"message {i}");

                channel.BasicPublish(
                    exchange: "direct-exchange-example",
                    routingKey: "direct-queue-example",
                    body: message);

                Console.WriteLine(Encoding.UTF8.GetString(message) + "publish edildi ");

            }

            Console.ReadLine();

        }
    }
}
