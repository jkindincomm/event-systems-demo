using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text;
using Microsoft.Extensions.Logging;

namespace NotificationsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        private static string uri =  "amqps://bgzwyteg:u2b-rXu07loDj4Yzk3u3hw0vi42H2teO@woodpecker.rmq.cloudamqp.com/bgzwyteg";
        private static string _queue = "hello";
        private static string _routingKey = "hello";
        private readonly ConnectionFactory factory;
        private readonly ILogger _logger;
        public ValuesController(){
            //_logger = logger;
            factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string msg)
        {
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue:_queue, durable: false, exclusive: false, autoDelete: false, arguments: null);
                var body = Encoding.UTF8.GetBytes(msg);

                channel.BasicPublish(exchange: "", routingKey: _routingKey, basicProperties: null, body: body);
                //_logger.LogInformation("MSg sent to _queue {0}", _queue);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
