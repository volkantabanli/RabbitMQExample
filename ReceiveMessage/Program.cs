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

            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queueName, true, consumer);

            consumer.Received += Consumer_Received;

            Console.ReadLine();
        }
    }

    private static void Consumer_Received(object? sender, BasicDeliverEventArgs e)
    {
        Console.WriteLine("Gelen Mesaj: " + Encoding.UTF8.GetString(e.Body.ToArray()));
    }
}
