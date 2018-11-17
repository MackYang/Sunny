using Quartz;
using ServiceDemo;
using Sunny.Api.Quartz;
using Sunny.Common.ConfigOption;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDemo.Job
{
    public class JobDemo
    {
        private readonly ISchedulerFactory _schedulerFactory;
        static private IScheduler _scheduler;

        public JobDemo(ISchedulerFactory schedulerFactory)
        {
            this._schedulerFactory = schedulerFactory;
        }

        public async void EnableJob()
        {
            var t = new Dictionary<string, object>();
            t.Add("aaa", 123);

            var option = new JobOption { Args = t, JobName = "Test Job", RunAtCron = "/10 * * * * ?" };

            var wrapperType = typeof(JobWrapper<>).MakeGenericType(typeof(JobA));

            //1、通过调度工厂获得调度器
            _scheduler = await _schedulerFactory.GetScheduler();
            //2、开启调度器
            await _scheduler.Start();

            var trigger = TriggerBuilder.Create()
                .WithCronSchedule(option.RunAtCron)
                            .Build();
            //4、创建任务
            var jobDetail = JobBuilder.Create(wrapperType)
                            .WithIdentity("this job a")
                            .WithDescription("descript ")
                            .SetJobData(new JobDataMap(option.Args))
                            .Build();
            //5、将触发器和任务器绑定到调度器中
            _scheduler.ScheduleJob(jobDetail, trigger);
        }
    }


    public class JobA : IJobEntity
    {
        public string JobId => "this job A Id";

        public string JobName => "this job A Name";

        public string Describe => "this job A Describe";

        public async Task ExecuteAsync()
        {
            Console.WriteLine("hello job 这有中 execing...");
            Thread.Sleep(3000);
            Console.WriteLine("job end");

        }
    }

    public class JobB : IJobEntity
    {
        public string JobId => "this job B Id";

        public string JobName => "this job B Name";

        public string Describe => "this job B Describe";

        IStudentServic studentServic;

        public JobB(IStudentServic studentServic)
        {
            this.studentServic = studentServic;

        }


        public async Task ExecuteAsync()
        {
            Console.WriteLine("hello job 这有中 execing...");
            Console.WriteLine(studentServic.GetStudent().StudentName);
            Thread.Sleep(3000);
            Console.WriteLine("job end");

        }
    }
}
