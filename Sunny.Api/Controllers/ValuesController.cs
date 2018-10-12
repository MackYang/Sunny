using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sunny.Api.DTO.Response;
using Sunny.Repository;
using Sunny.Repository.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;

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
        }


        // GET api/values
        [HttpGet("Get1")]
        public Result Get1()
        {

            return this.Success();
        }

        public class A
        {

            public string FullName { get; set; }
            public decimal Age { get; set; }

            public long MFF { get; set; }

            public DateTime Now { get; set; } = DateTime.Now;
        }

        [HttpGet("Get2")]
        public Result<A> Get2()
        {

            return this.Success(new A { FullName = "AbcYH", Age = 123.123456789m,MFF=long.MaxValue });
        }

        [HttpGet("Get3")]
        public Result Get3(string abc,int age)
        {

            return this.Fail("我也不知道为什么");
        }


        [HttpGet("Get4")]
        public Result Get3(string abc, [FromQuery]A a)
        {

            return this.Fail("我也不知道为什么");
        }

        [HttpPost("Get6/{id}")]
        public Result<A> Get6(A a,int id)
        {

            return this.Success(a);
        }

        [HttpPost,Route("Get5")]
        public Result<A> Get5(  A a)
        {
            
            return this.Success(a);
        }


        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {



            //throw new Exception("Test EEEEE!");
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


            var x = db.Student.FirstOrDefault();

            Console.WriteLine("hello");
            logger.LogInformation("this is test AAAA");
            logger.LogWarning("QWWWWWWW");

            return new string[] { "value1", "value2", x.ToString() };
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
        public void Put(int id, [FromBody]string value,[FromHeader] string f)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
