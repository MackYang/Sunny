using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
           WebHost.CreateDefaultBuilder(args)
           .ConfigureLogging(logging =>
           {
                //logging.SetMinimumLevel(LogLevel.Warning);
                //微软默认添加了console和debue的级别大于Warning的日志输出,不习惯可以清除了自己添加
                logging.ClearProviders();
           })
               .UseStartup<Startup>()
               .Build();
    }
}
