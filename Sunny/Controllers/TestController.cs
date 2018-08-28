using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;

namespace Sunny.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : Controller
    {
 

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2", TestOption.Name , TestOption.Age.ToString()};


        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

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
