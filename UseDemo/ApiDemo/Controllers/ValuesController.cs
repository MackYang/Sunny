using ApiDemo.DTO.Request.Demo;
using ApiDemo.FluentValidation2;
using ApiDemo.Job;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using RepositoryDemo;
using RepositoryDemo.DbModel;
using ServiceDemo;
using Sunny.Api.Controllers;

using Sunny.Api.DTO.Response;
using Sunny.Common.ConfigOption;
using Sunny.Common.Enum;
using Sunny.Common.Extend.CollectionQuery;
using Sunny.Common.Helper;

using System;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Api.Controllers
{

    [Route("api/[controller]")]
    public class ValuesController : SunnyController
    {
        IDistributedCache cache;
        //这里只是为了演示,实际使用时建议使用各业务的Service
        MyDbContext db;
       
        ILogger logger;
        IMapper mapper;
        ISchedulerFactory schedulerFactory;
        //学生业务的Service
        public IStudentServic studentServic;
        SomeoneClass someone;
        MailOption mailOption;
        IpInfoQueryOption ipQueryOption;
        //TDbContext tDbContex;
        public ValuesController(IOptions<MailOption> mailOption,IOptions<IpInfoQueryOption> ipQueryOption, IStudentServic studentServic,SomeoneClass someone,ISchedulerFactory schedulerFactory, MyDbContext efDbContext, ILogger<ValuesController> logger, IMapper mapper, IDistributedCache cache)
        {

            this.mailOption = mailOption.Value;
            this.ipQueryOption = ipQueryOption.Value;
            this.db = efDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.cache = cache;
            //this.tDbContex = tDbContext;
            this.schedulerFactory = schedulerFactory;
            this.studentServic = studentServic;
            this.someone = someone;
            
        }

        [HttpGet("BizExceptionTest")]
        public async Task<Result> BizExceptionTest()
        {
            await studentServic.BizExceptionTest();
            return this.Success();
        }



        [HttpGet("IpQuery")]
        public Result<IPInfo> IpQuery()
        {
            //var ip=NetHelper.GetClientIP(this.HttpContext);
            var ip = "171.214.202.111";
            return this.Success(NetHelper.QueryIpInfo(ip, ipQueryOption));
        }


        //[HttpGet("SmsTest")]
        //public Result<string> SmsTest()
        //{
        //    SMSInfo info = new SMSInfo();
        //    info.SMSContent = "你好,这是测试短信";
        //    info.ToPhone = "15287152672";
        //    info.OperaterID = "yh";
        //    info.OperaterIP = NetHelper.GetClientIP(this.HttpContext);



        //    NetHelper.AsyncSendSMS(info, smsOption);
        //    return this.Success("ok");
        //}


        [HttpGet("MailTest")]
        public  Result<string> MailTest()
        {
            MailInfo mailInfo = new MailInfo();
            mailInfo.Content = "hello";
            mailInfo.OperaterID = "yh";
            mailInfo.OperaterIP = NetHelper.GetClientIP(this.HttpContext);
            mailInfo.Title = "this is test mail";
            mailInfo.ToMail = "someone@qq.com";

            NetHelper.AsyncSendEmail(mailInfo, mailOption);
            return this.Success("ok");
        }

        [HttpGet]
        public Result<string> Get()
        {
            var s = DiHelper.CreateInstance<SomeoneClass>();

            var x = DiHelper.GetService<IStudentServic>();

            return this.Success(someone.SomeoneMethod()+s.SomeoneMethod()+x.GetStudent2().Result.StudentName);
        }
        /// <summary>
        /// Session测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSession")]
        public Result<string> GetSession()
        {
            HttpContext.Session.Set("a", new byte[] { 1, 3, 4 });
            HttpContext.Session.SetString("bbb", "bxxx");
            return this.Success(HttpContext.Session.GetString("bbb"));
        }

        /// <summary>
        /// Redis测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRedis")]
        public async Task<Result<dynamic>> GetRedis()
        {
            cache.SetString("aaa", "A杨家勇A");

            cache.Set("customer", new Customer());

            await cache.SetAsync("customerAsync", new Customer() { Address = "Async" });

            var cus = cache.Get<Customer>("customer");

            var cusAsync = await cache.GetAsync<Customer>("customerAsync");

            return this.SuccessDynamic(new { Cus = cus, CusAsync = cusAsync });
        }

        /// <summary>
        /// 不带返回值的成功场景测试
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 带返回值的成功场景测试,测试模型验证
        /// </summary>
        /// <returns></returns>
        [HttpPost("Get2")]
        public Result<A> Get2(Customer customer)
        {

            return this.Success(new A { FullName = "AbcYH", Age = 123.123456789m, MFF = long.MaxValue });
        }


        /// <summary>
        /// 带返回值,且值为动态扩展对象的场景测试,分页测试
        /// </summary>
        /// <param name="abc"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpPost("pageTest")]
        public Result<PageData<dynamic>> pageTest(PageInfo pageInfo)
        {
            var pageList = db.IdTest.Pagination(pageInfo);
            //让列表中返回的每一项都有At和Sort属性         
            return this.Success(pageList.ToDynamic(x => x.Extend(new { At = DateTime.Now, Sort = DateTime.Now.Millisecond })));
        }

        /// <summary>
        /// 带查询条件分页测试
        /// </summary>
        /// <param name="abc"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpPost("pageTestCondition")]
        public Result<PageData<Student>> pageTestCondition(PageQuery<Student> pageQuery)
        {
            var pageList = db.Student.Where(x=>x.StudentName==pageQuery.Condition.StudentName).Pagination(pageQuery.PageInfo);
            return this.Success(pageList);
        }

        /// <summary>
        /// 失败返回值测试
        /// </summary>
        /// <param name="abc"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpGet("GetFail")]
        public Result GetFail()
        {

            return this.Fail("我也不知道为什么");
        }

        /// <summary>
        /// 路由测试
        /// </summary>
        /// <param name="a"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("Get6/{id}")]
        public Result<A> Get6(A a, int id)
        {

            return this.Success(a);
        }

        /// <summary>
        /// 模型绑定测试
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [HttpPost, Route("Get5")]
        public Result<A> Get5(A a)
        {

            return this.Success(a);
        }

        /// <summary>
        /// AutoMapper测试
        /// </summary>
        /// <returns></returns>
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
                Comment = new SellerComment { Content = 1119999 }
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
                Comment = new SellerComment { Content = 111555 },
                Address = "SSS Address"
            };


            BuyerSub getBuyerSub = mapper.Map<BuyerSub>(sellerSub);
            SellerSub getSellerSub = mapper.Map<SellerSub>(buyerSub);





            return this.SuccessDynamic(new { });
        }


        /// <summary>
        /// 随机返回成功和失败测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAAA")]
        public Result<Customer> GetB()
        {
            var customer = new Customer();


            if (DateTime.Now.Second % 2 == 0)
            {
                return this.Success(customer);
            }

            return this.Fail("ss");


        }

        /// <summary>
        /// 模型验证测试
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost("D")]
        public Result<bool> D(MemPua c)
        {
            return this.Success(ModelState.IsValid);
        }



        /// <summary>
        /// 数据存取和日志记录测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOld")]
        public async Task<Result<Student>> GetOld()
        {



            //throw new Exception("Test EEEEE!");
            //var builder = new DbContextOptionsBuilder<EfDbContext>();




            Student student = new Student();
            student.Id = IdHelper.GenId();
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
            db.Add(student);

            db.SaveChanges();


            var x = await studentServic.GetStudent();

            var y = await studentServic.GetStudent2();

            DateTime now = DateTime.Now;

            logger.LogInformation("this is test AAAA");

            var log1 = DateTime.Now - now;
            logger.LogWarning("QWWWWWWW");
            var log2 = DateTime.Now - now;



            return this.Success(y);
        }


    }
}
