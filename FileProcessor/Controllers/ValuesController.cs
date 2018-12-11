using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FileHelpers;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.IO;

namespace FileProcessor.Controllers
{
   

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static string uri = "amqps://bgzwyteg:u2b-rXu07loDj4Yzk3u3hw0vi42H2teO@woodpecker.rmq.cloudamqp.com/bgzwyteg";
        private readonly ConnectionFactory factory;

        public string FilePath { get; set; }

        public ValuesController(){
            FilePath = "orders.csv";
            factory = new ConnectionFactory
            {
                Uri = new Uri(uri)
            };
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Order>> Get()
        {
            var file = new File();
            var fileInfo =  new FileInfo(FilePath);
            var engine = new FileHelperEngine<Order>();
            var records = engine.ReadFile(fileInfo.FullName);
            file.TotalRecords = engine.TotalRecords;
            file.FilePath = fileInfo.FullName;
            file.LastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
            file.ProcessTimeUtc = DateTime.UtcNow;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "files", durable: true, exclusive: false, autoDelete: false, arguments: null);

                var fileMsg = new FileMessage(channel);
                fileMsg.Push(file);
                channel.QueueDeclare(queue: "orders", durable: false, exclusive: false, autoDelete: false, arguments: null);
                var msg = new OrderMessage(channel);
                foreach (var record in records)
                {
                    msg.Push(record);
                }
            }

            return records;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
