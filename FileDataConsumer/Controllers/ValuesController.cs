using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace FileDataConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public readonly IMessageListener _listener;
        ValuesController(IMessageListener listener){
            _listener = listener;
        }
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return _listener.Consume();
        }

        
    }
}
