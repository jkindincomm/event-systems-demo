using System;
using System.IO;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;

public class FileMessage
{
    private static string _routingKey = "files";
    public IModel Model
    {
        get;
        set;
    }

    public FileMessage(IModel model)
    {
        Model = model;

    }
    public void Push(File file)
    {
        var msg = JsonConvert.SerializeObject(file);
        var body = Encoding.UTF8.GetBytes(msg);
        Model.BasicPublish(exchange: "", routingKey: _routingKey, basicProperties: null, body: body);

    }
}