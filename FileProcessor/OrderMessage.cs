using System;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

public class OrderMessage
{
    private static string _routingKey = "orders";
    public IModel Model
    {
        get;
        set;
    }

    public OrderMessage(IModel model)
    {
        Model = model;

    }
    public void Push(Order order)
    {
        var msg = JsonConvert.SerializeObject(order);
        var body = Encoding.UTF8.GetBytes(msg);
        Model.BasicPublish(exchange: "", routingKey: _routingKey, basicProperties: null, body: body);

    }
}