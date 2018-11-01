using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using RepositoryDemo;
using Sunny.Api.Controllers;
using Sunny.Api.DTO.Response;
using System;

namespace ApiDemo.Api.Controllers
{

    [Route("unAuthApi/[controller]")]
    public class UnAuthController : SunnyController
    {

        IDistributedCache cache;
        MyDbContext db;
        ILogger logger;
        IMapper mapper;
        //TDbContext tDbContex;
        public UnAuthController(MyDbContext efDbContext, ILogger<ValuesController> logger, IMapper mapper, IDistributedCache cache)
        {
            this.db = efDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.cache = cache;

            //this.tDbContex = tDbContext;
        }
        /// <summary>
        /// 设置Token,使以/api开头的方法可以通过验证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Result<bool> Get()
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
