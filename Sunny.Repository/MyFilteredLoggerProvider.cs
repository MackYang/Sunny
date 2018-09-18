using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository
{
    public class MyFilteredLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            //if (categoryName.StartsWith("Microsoft.EntityFrameworkCore.Database.Command"))
            //{
            //    return new EFLogger(categoryName);
            //}

            //return   NullLogger.Instance;

            return new EFLogger(categoryName);
        }
        public void Dispose()
        {

        }
    }

    public class EFLogger : ILogger
    {
        private readonly string categoryName;

        public EFLogger(string categoryName) => this.categoryName = categoryName;

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Console.WriteLine($"时间:{DateTime.Now.ToString("o")} 日志级别: {logLevel} {eventId.Id} 产生的类{this.categoryName}");

            try
            {
                Console.WriteLine($"日志内容:{state.ToString()}");
            }
            catch (Exception logex)
            {
                Console.WriteLine($"记录语句时发生异常:{logex.Message}");
            }

        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
