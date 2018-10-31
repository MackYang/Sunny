using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
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
using System.Threading.Tasks;

namespace Sunny.Api.Controllers
{

    [Route("unAuthApi/[controller]")]
    public class UnAuthController : SunnyController
    {

        IDistributedCache cache;
        EfDbContext db;
        ILogger logger;
        IMapper mapper;
        //TDbContext tDbContex;
        public UnAuthController(EfDbContext efDbContext, ILogger<ValuesController> logger, IMapper mapper, IDistributedCache cache)
        {
            this.db = efDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.cache = cache;
            
            //this.tDbContex = tDbContext;
        }

        [HttpGet]
        public Result<bool> Get()
        {

            cache.Set("123", "aweiskOK23");
            return this.Success(true);
        }

        [HttpGet("GetException")]
        public Result<bool> GetException()
        {

            throw new Exception("Test Excexxxx");
            return this.Success(true);
        }

    }
}
