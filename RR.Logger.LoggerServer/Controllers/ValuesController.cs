using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RR.Common_V1;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using RR.Logger.Common;

namespace RR.Logger.LoggerServer.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ValuesController>();
            _logger.LogInformation("ValuesController created!");
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Log([FromBody]RRLoggerMsg value)
        {
            //_logger.LogInformation("ValuesController created!");
            _logger.LogInformation("Receive Msg: "+ value.Msg);
            Debug.WriteLine(value.Msg);
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
