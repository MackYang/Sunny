using ApiDemo.Job;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using RepositoryDemo;
using Sunny.Api.Controllers;
using Sunny.Api.DTO.Response;
using Sunny.Api.Quartz;
using Sunny.Common.Helper;
using System;
using System.Threading.Tasks;
using RepositoryDemo.DbModel;
using ServiceDemo;
using Sunny.Common.DependencyInjection;

namespace ApiDemo.Api.Controllers
{

    [Route("unAuthApi/[controller]")]
    public class UnAuthController : SunnyController
    {

        IDistributedCache cache;
        MyDbContext db;
        ILogger logger;
        IMapper mapper;
        ISchedulerFactory _schedulerFactory;

        private IScheduler _scheduler;
        //TDbContext tDbContex;
        public UnAuthController(ISchedulerFactory schedulerFactory,MyDbContext efDbContext, ILogger<ValuesController> logger, IMapper mapper, IDistributedCache cache)
        {
            this.db = efDbContext;
            this.logger = logger;
            this.mapper = mapper;
            this.cache = cache;
            this._schedulerFactory = schedulerFactory;
            //this.tDbContex = tDbContext;
        }

        interface IB:ITransient {

            Student GetStudent();

        }

        class B:IB
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
             
            var a = DiHelper.GetService<ValuesController>();
            var b= DiHelper.GetRequiredService<ValuesController>();
            var e = DiHelper.GetService<IStudentServic>();
            var f = DiHelper.GetRequiredService<IStudentServic>();
            var c = DiHelper.CreateInstance<B>();



            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();
            //3、创建一个触发器
            var trigger = TriggerBuilder.Create()
                            .WithCronSchedule("*/10 * * * * ? ")//每两秒执行一次
                            .Build();
            //4、创建任务
            var jobDetail = JobBuilder.Create<JobWrapper<JobA>>()
                            .WithIdentity("job", "group")
                            .Build();

            //5、将触发器和任务器绑定到调度器中
            await _scheduler.ScheduleJob(jobDetail, trigger);



            //var jobDetail2 = JobBuilder.Create<JobWrapper<JobB>>()
            //               .WithIdentity("job2", "group")
            //               .Build();

            //var trigger2 = TriggerBuilder.Create()
            //              .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).WithRepeatCount(1))//每两秒执行一次
            //              .Build();

            //await _scheduler.ScheduleJob(jobDetail2, trigger2);

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
