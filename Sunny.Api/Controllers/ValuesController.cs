using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sunny.Repository;
using Sunny.Repository.DbModel;

namespace Sunny.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : SunnyController
    {

        EfDbContext db;
        ILogger logger;
        //TDbContext tDbContex;
        public ValuesController(EfDbContext efDbContext, ILogger<ValuesController> logger)
        {
            this.db = efDbContext;
            this.logger = logger;
            //this.tDbContex = tDbContext;

            this.GetResult(new { }, 1, "");
            this.Fail("");
            this.Success(2);
            this.Success();
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            throw new Exception("Test EEEEE!");
            //var builder = new DbContextOptionsBuilder<EfDbContext>();

            //builder.UseMySql("server=localhost;database=test;user=root;password=myAdmin.;");
            //EfDbContext db = new EfDbContext(builder.Options);

          // var t= tDbContex.Student.FirstOrDefault();

            //// var b = db.Query<User>();
            //var c = db.Uesr2s.First();

            Student student = new Student();
            student.Id = 23;
            student.StudentName = "YH";

            //StudentAddress address = new StudentAddress();
            //address.Student = student;
            //address.Zipcode = 999;
            //address.Id = 111;

            //StudentAddress address2 = new StudentAddress();
            //address2.Student = student;
            //address2.Zipcode = 001;
            //address2.Id = 222;

            //db.AddRange(student, address, address2);
            //db.Add(student);

            //db.SaveChanges();


            var x=db.Student.FirstOrDefault();

            Console.WriteLine("hello");
            logger.LogInformation("this is test AAAA");
            logger.LogWarning("QWWWWWWW");

            return new string[] { "value1", "value2",x.ToString() };
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
