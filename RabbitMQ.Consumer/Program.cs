using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new();
            factory.Uri = new("amqps://owfxtlir:YDA64RKMRrIw4-JlYjN1t0JMtW2hs8VV@shark.rmq.cloudamqp.com/owfxtlir");

            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            channel.QueueDeclare(queue: "example-queue", exclusive: false);

            //queue dan mesaj okuma
            EventingBasicConsumer consumer = new(channel);

            channel.BasicConsume(queue: "example-queue", false, consumer);

            consumer.Received += (sender, e) =>
            {
                //kuyruğa gelen mesajın işlendiği yerdir
                //e.body  kuyruktaki mesajın verisini bütünsel olarak getirecektir 
                //e.body.span  veya e.body.toarray() kuyruktaki mesajın byte verisini getirecektir

                Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

            };

            Console.ReadLine();
        }
    }
}
