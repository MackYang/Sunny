using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sunny.Api.DTO.Request.Demo;
using Sunny.Api.DTO.Response;

using Sunny.Api.FluentValidation2;
using Sunny.Common.Enum;
using Sunny.Common.Extend.CollectionQuery;
using Sunny.Common.Helper;
using Sunny.Repository;
using Sunny.Repository.DbModel;
using Sunny.Repository.DbModel.Model;
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
        IMapper mapper;
        //TDbContext tDbContex;
        public ValuesController(EfDbContext efDbContext, ILogger<ValuesController> logger, IMapper mapper)
        {
            this.db = efDbContext;
            this.logger = logger;
            this.mapper = mapper;
            //this.tDbContex = tDbContext;
        }

        [HttpGet]
        public Result<dynamic> Get()
        {
            return this.SuccessDynamic(new { });
        }


        // GET api/values
        [HttpGet("Get1")]
        public Result Get1()
        {

            return this.Success();
        }

        public class A : Customer
        {

            public string FullName { get; set; }
            public decimal Age { get; set; }

            public long MFF { get; set; }

            public DateTime Now { get; set; } = DateTime.Now;
        }

        [HttpGet("Get2")]
        public Result<A> Get2()
        {

            return this.Success(new A { FullName = "AbcYH", Age = 123.123456789m, MFF = long.MaxValue });
        }



        [HttpGet("Get3")]
        public Result<PageData<dynamic>> Get3(string abc, int age)
        {
            IdTest model = new IdTest();
            model.Id = IdHelper.GenId();
            model.requestType = Enums.RequestType.Post;

            IdTest model2 = new IdTest();
            model2.Id = IdHelper.GenId();
            model2.requestType = Enums.RequestType.Get;



            db.IdTest.AddRange(model, model2);
            db.SaveChanges();

            PageInfo pageInfo = new PageInfo { PageIndex = 2, PageSize = 3 };



            var ttt = db.IdTest.Pagination(pageInfo);

            Customer c = mapper.Map<Customer>(db.IdTest.First());

            IdTest id = mapper.Map<IdTest>(new Customer { Id = 123 });
 

            return this.Success(ttt.ToDynamic(x => x.Extend(new { At = DateTime.Now, Sort = DateTime.Now.Millisecond })));
        }


        [HttpGet("Get4")]
        public Result Get3(string abc, [FromQuery]A a)
        {

            return this.Fail("我也不知道为什么");
        }

        [HttpPost("Get6/{id}")]
        public Result<A> Get6(A a, int id)
        {

            return this.Success(a);
        }

        [HttpPost, Route("Get5")]
        public Result<A> Get5(A a)
        {

            return this.Success(a);
        }
       
        // GET api/values
        [HttpGet("GetAutoMapper")]
        public Result<dynamic> GetAutoMapper()
        {
            OrderInfo order = new OrderInfo { Price = 2.3m, ProductCount = 6 };


            Buyer buyer = new Buyer
            {
                Name = "Buyerxxx",
                Order = order,
                Phone = "buy1111111111"
                ,
                Comment = new BuyerComment { Content = "2333" }
            };

            Seller seller = new Seller
            {
                NaMe = "Sellerxxx",
                Phone = "Sel1111222",
                SellTime = DateTime.Now,
                Order = order
                ,
                Comment = new SellerComment { Content = 1119999}
            };


            Buyer getBuyer = mapper.Map<Buyer>(seller);

            Seller getSeller = mapper.Map<Seller>(buyer);


            BuyerSub buyerSub = new BuyerSub
            {
                Address = "BBBAddress",
                Name = "Buyerxxx",
                Order = order,
                Phone = "buy1111111111"
                ,
                Comment = new BuyerComment { Content = "2444" }
            };

            SellerSub sellerSub = new SellerSub
            {
                NaMe = "Sellerxxx",
                Phone = "Sel1111222",
                SellTime = DateTime.Now,
                Order = order
                    ,
                Comment = new SellerComment { Content = 111555},
                Address = "SSS Address"
            };


            BuyerSub getBuyerSub = mapper.Map<BuyerSub>(sellerSub);
            SellerSub getSellerSub = mapper.Map<SellerSub>(buyerSub);





            return this.SuccessDynamic(new { });
        }


        // GET api/values
        [HttpGet("GetAAA")]
        public IResult<Customer> GetB()
        {
            var customer = new Customer();


            //dynamic a = new{ };
            //for (var i = 0; i < 3; i++)
            //{
            //    a.i = 3;

            //}
            if (DateTime.Now.Second % 2 == 0)
            {
                return this.Success(customer);
            }

            return this.Fail("ss");


        }

        [HttpPost("D")]
        public Result<bool> D(MemPua c)
        {


            return this.Success(ModelState.IsValid);
        }

        [HttpPost("C")]
        public Result<Customer> C(Customer c)
        {

            return this.Success(c);
        }



        // GET api/values
        [HttpGet("GetOld")]
        public IEnumerable<string> GetOld()
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
        public void Put(int id, [FromBody]string value, [FromHeader] string f)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
