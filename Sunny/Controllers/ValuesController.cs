using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;
using Sunny.Common.DependencyInjection;

namespace Sunny.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {

        TestOption  options { get; set; }


        IPrintService a;
        IB b;
        IC c;
        public ValuesController(IOptionsSnapshot<TestOption> op, IPrintService a,IB b,IC c ) {

            options = op.Value;
            this.a = a;
            this.b = b;
            this.c = c;
            
        }

        public string Hello()
        {
            return "hello";
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            return new string[] {a.Print(),b.Print(),c.Print(), "value2"};


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
