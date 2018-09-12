using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sunny.Repository;
using Sunny.Repository.DbModel;

namespace Sunny.Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {


        public ValuesController()
        {

        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {

            var builder = new DbContextOptionsBuilder<MyDbContext>();

            builder.UseMySql("server=localhost;database=test;user=root;password=myAdmin.;");
            MyDbContext db = new MyDbContext(builder.Options);

            //db.Update(new User());
            //var a=db.Uesrs;
            //db.Query<User>();

            //// var b = db.Query<User>();
            //var c = db.Uesr2s.First();

            Student student = new Student();
            student.StudentId = 23;
            student.StudentName = "YH";

            StudentAddress address = new StudentAddress();
            address.Student = student;
            address.Zipcode = 999;
            address.StudentAddressId = 111;

            StudentAddress address2 = new StudentAddress();
            address2.Student = student;
            address2.Zipcode = 001;
            address2.StudentAddressId = 222;

            db.AddRange(student, address, address2);

            db.SaveChanges();

            

        
            
           
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
