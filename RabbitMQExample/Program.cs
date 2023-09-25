using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory();
        factory.Uri = new Uri("amqps://zmidlbtr:test@rat.rmq2.cloudamqp.com/test");

        using (var connection = factory.CreateConnection())
        using (var channel = connection.CreateModel())
        {
            string queueName = "example";

            channel.QueueDeclare(queue: queueName,
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var mesaj = "RabbitMQ test örneğidir.";
            var body = Encoding.UTF8.GetBytes(mesaj);
            channel.BasicPublish(String.Empty, queueName, null, body);

            Console.WriteLine("Mesajları almak için bekleniyor. Çıkmak için herhangi bir tuşa basın.");
            Console.ReadLine();
        }
    }
}
