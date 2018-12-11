using System;
using System.Text;
using System.Diagnostics;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public interface IMessageListener
{
    string Consume();
}
public class RabbitListener : IMessageListener
{
    private static string uri = "amqps://bgzwyteg:u2b-rXu07loDj4Yzk3u3hw0vi42H2teO@woodpecker.rmq.cloudamqp.com/bgzwyteg";
    private static string _queue = "files";
    ConnectionFactory factory { get; set; }
    IConnection connection { get; set; }
    IModel channel { get; set; }

    public void Register()
    {
        channel.QueueDeclare(queue: _queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
    }

    public string Consume()
    {
        var message = "";
        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body;
            message = Encoding.UTF8.GetString(body);
           
        };
        channel.BasicConsume(queue: _queue, autoAck: true, consumer: consumer);
        return message;
    }

    public void Deregister()
    {
        this.connection.Close();
    }

    public RabbitListener()
    {
        this.factory = new ConnectionFactory();
        factory.Uri = new Uri(uri);
        this.connection = factory.CreateConnection();
        this.channel = connection.CreateModel();
    }
}