using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Receive
{
    class Program
    {
        private static string uri = "amqps://bgzwyteg:u2b-rXu07loDj4Yzk3u3hw0vi42H2teO@woodpecker.rmq.cloudamqp.com/bgzwyteg";
        private static string _queue = "hello";
        private static string _routingKey = "hello";
        

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            using (var connection = factory.CreateConnection())
            {
                Console.WriteLine("Open connection {0}", uri.Split('@')[1]);

                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    var consumer = new EventingBasicConsumer(channel);
                    Console.WriteLine("Reading channel:\nQueue: {0}", _queue);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: _queue, autoAck: true, consumer: consumer);

                    Console.WriteLine(" Press [enter] to exit.");
                    Console.ReadLine();
                }
            }
        }
    }
}