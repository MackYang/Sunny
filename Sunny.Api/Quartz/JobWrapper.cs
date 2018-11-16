using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Sunny.Common.Helper;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sunny.Api.Quartz
{
    public class JobWrapper<T> : IJob where T : IJobEntity
    {
        ILogger logger;
        T job;
      

        public JobWrapper()
        {
            logger = DiHelper.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger(typeof(T));
            job = DiHelper.CreateInstance<T>();
           
        }

        public async Task Execute(IJobExecutionContext context)
        {
            Stopwatch sw = new Stopwatch();

            try
            {
                sw.Start();
                await job.ExecuteAsync();
                sw.Stop();

                logger.LogInformation($"Job（Id: {job.JobId} , Name: {job.JobName} , Describe:{job.Describe}）已执行,耗时:{sw.ElapsedMilliseconds} 毫秒.");

            }
            catch (Exception ex)
            {
                sw.Stop();
                logger.LogError($"Job（Id: {job.JobId} , Name: {job.JobName} , Describe:{job.Describe}）执行时发生异常.", ex);
            }
            finally
            {
                IDisposable disposable = job as IDisposable;
                disposable?.Dispose();
            }
        }
    }
}
