using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Header.Consumer
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


            Console.WriteLine("lütfen header valuesini giriniz : ");
            string value = Console.ReadLine();
            string queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(
                queue: queueName,
                exchange: "header-exchange-example",
                routingKey: string.Empty,
                new Dictionary<string, object>
                {
                    //["x-match"] ="any",
                    //["x-match"] ="all",
                    ["no"] = value
                });

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
