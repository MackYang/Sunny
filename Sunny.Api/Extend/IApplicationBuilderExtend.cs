using Microsoft.Extensions.Configuration;
using Quartz;
using Sunny.Api.Quartz;
using Sunny.Common.ConfigOption;
using Sunny.Common.Helper;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNetCore.Builder
{
    static public class IApplicationBuilderExtend
    {
        /// <summary>
        /// 初始化DiHelper中的ServiceProvider
        /// </summary>
        /// <param name="builder"></param>
        static public void InitServiceProvider(this IApplicationBuilder builder)
        {
            DiHelper.ServiceProvider = builder.ApplicationServices;
        }

        /// <summary>
        /// 启用Job
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configuration"></param>
        /// <param name="schedulerFactory"></param>
        static public async void EnableJob(this IApplicationBuilder builder, IConfiguration configuration, ISchedulerFactory schedulerFactory)
        {
            //1、通过调度工厂获得调度器
            var scheduler = await schedulerFactory.GetScheduler();
            //2、开启调度器
            await scheduler.Start();

            var option = configuration.GetSection("SunnyOptions:JobOption").Get<JobOption[]>();


            var types = DiHelper.GetCustomizeTypes(t => typeof(IJobEntity).IsAssignableFrom(t) && !t.GetTypeInfo().IsAbstract);



            foreach (var item in option)
            {
                var tArr = types.Where(x => x.Name == item.JobClassName);
                foreach (var t in tArr)
                {
                    //3、创建一个触发器
                    var trigger = TriggerBuilder.Create()
                                    .WithCronSchedule(item.RunAtCron)
                                    .Build();

                    var wrapperType = typeof(JobWrapper<>).MakeGenericType(t.GetTypeInfo());

                    //4、创建任务
                    var jobDetail = JobBuilder.Create(wrapperType)
                                    .WithIdentity(item.JobClassName, item.JobGroup)
                                  .SetJobData(item.Args == null ? new JobDataMap() : new JobDataMap(item.Args))
                                    .Build();


                    //5、将触发器和任务器绑定到调度器中
                    await scheduler.ScheduleJob(jobDetail, trigger);

                }


            }



        }
    }
}
