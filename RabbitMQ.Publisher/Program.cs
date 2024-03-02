using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            //Bağlantı oluşturma
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");


            //Bağlantıyı aktifleştirme ve kanal açma
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            //Queue oluşturma
            channel.QueueDeclare(queue: "example-queue", exclusive: false);

            //Queue ya mesaj gönderme 
            var message = Encoding.UTF8.GetBytes("Merhaba");

            for (int i  = 0; i < 20; i++)
            {

                channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

            }

            Console.ReadLine();


        }
    }
}
