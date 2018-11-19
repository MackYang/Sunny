using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Quartz;
using RepositoryDemo;
using RepositoryDemo.DbModel;
using ServiceDemo;
using Sunny.Api.Controllers;
using Sunny.Api.DTO.Response;
using Sunny.Common.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace ApiDemo.Api.Controllers
{

    [Route("unAuthApi/[controller]")]
    public class UnAuthController : SunnyController
    {

        IDistributedCache cache;
        MyDbContext db;
        ILogger logger;
        IMapper mapper;


        private IScheduler _scheduler;
        //TDbContext tDbContex;
        public UnAuthController(MyDbContext efDbContext, ILogger<ValuesController> logger, IMapper mapper, IDistributedCache cache)
        {
            this.db = efDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.cache = cache;

            //this.tDbContex = tDbContext;
        }

        interface IB : ITransient
        {

            Student GetStudent();

        }

        class B : IB
        {
            public IStudentServic studentServic;
            public B(IStudentServic studentServic)
            {
                this.studentServic = studentServic;
            }

            public Student GetStudent()
            {
                return studentServic.GetStudent();
            }
        }

        /// <summary>
        /// 设置Token,使以/api开头的方法可以通过验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<Result<bool>> Get()
        {
            return await Task.Run(() => this.Success(true));
        }

        /// <summary>
        /// 设置Token,使以/api开头的方法可以通过验证
        /// </summary>
        /// <returns></returns>
        [HttpGet("SetToken")]
        public Result<bool> SetToken()
        {

            cache.Set("123", "aweiskOK23");
            return this.Success(true);
        }

        /// <summary>
        /// 测试异常日志记录
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetException")]
        public Result<bool> GetException()
        {

            throw new Exception("Test Excexxxx");
        }

    }
}
