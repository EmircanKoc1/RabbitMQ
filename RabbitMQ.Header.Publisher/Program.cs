using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Header.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: "header-exchange-example",
                type: ExchangeType.Headers);

            for (int i = 0; i < 100; i++)
            {
                Task.Delay(100).Wait();

                var message = Encoding.UTF8.GetBytes($"Merhaba {i}");
                Console.WriteLine("Lütfen header valuesini girin");
                var value = Console.ReadLine();
                IBasicProperties basicProperties = channel.CreateBasicProperties();
                basicProperties.Headers = new Dictionary<string, object>()
                {
                    ["no"] = value
                };

                channel.BasicPublish(
                    exchange: "header-exchange-example",
                    routingKey: string.Empty,
                    body : message,
                    basicProperties: basicProperties);

            }

        }
    }
}
