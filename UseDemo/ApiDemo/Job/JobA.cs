using Quartz;
using Sunny.Api.Quartz;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiDemo.Job
{
    public class JobA : IJobEntity
    {

        public string JobName => "名字随便取,你能看懂就行,最终会在日志中出现";

        public string Describe => "这个Job是干嘛用的..最终会在日志中出现";

        public async Task ExecuteAsync(IJobExecutionContext jobContext)
        {
            Console.WriteLine("hello job 这有中文 execing...");
            await Task.Run(() => Thread.Sleep(3000));
            Console.WriteLine("job end");

        }
    }
}
