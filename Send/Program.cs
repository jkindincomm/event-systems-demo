using System;
using RabbitMQ.Client;
using System.Text;

namespace Send
{
    class Program
    {
        private static string uri =  "amqps://bgzwyteg:u2b-rXu07loDj4Yzk3u3hw0vi42H2teO@woodpecker.rmq.cloudamqp.com/bgzwyteg";
        private static string _queue = "hello";
        private static string _routingKey = "hello";
        
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue:_queue, durable: false, exclusive: false, autoDelete: false, arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "", routingKey: _routingKey, basicProperties: null, body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}