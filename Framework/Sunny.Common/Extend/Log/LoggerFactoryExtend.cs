using Microsoft.Extensions.Logging.Console;
using Sunny.Common.ConfigOption;
using Sunny.Common.Log;
using System;


namespace Microsoft.Extensions.Logging
{
    public static class LoggerFactoryExtend
    {

        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, NetLoggerOption option)
        {
            factory.AddProvider(new NetLoggerProvider(option));
            return factory;
        }

        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, NetLoggerOption option, Func<string, LogLevel, bool> filter)
        {
            factory.AddProvider(new NetLoggerProvider(option, filter, false));
            return factory;
        }

        /// <summary>
        /// 使用默认的过虑规则启用网络日志记录,默认规则是只记录微软Warning及以上的日志,或非微软Info及以上的日志
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="option">网络日志配置</param>
        /// <returns></returns>
        public static ILoggerFactory AddNetLoggerUseDefaultFilter(this ILoggerFactory factory, NetLoggerOption option)
        {
            Func<string, LogLevel, bool> filter = (category, level) =>
            {

                return (level >= LogLevel.Information && !category.StartsWith("Microsoft")) || (level >= LogLevel.Warning);

            };

            factory.AddProvider(new NetLoggerProvider(option, filter, false));
            return factory;
        }

        /// <summary>
        /// 使用默认的过虑规则启用控制台日志记录,默认规则是只记录微软Warning及以上的日志,或非微软Info及以上的日志,或SQL语句
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static ILoggerFactory AddConsoleLoggerUseDefaultFilter(this ILoggerFactory factory)
        {
            Func<string, LogLevel, bool> filter = (category, level) =>
            {

                return (level >= LogLevel.Information && !category.StartsWith("Microsoft")) || (level >= LogLevel.Warning || category.StartsWith("Microsoft.EntityFrameworkCore.Database.Command"));

            };

            factory.AddProvider(new ConsoleLoggerProvider(filter, false));
            return factory;
        }


        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, NetLoggerOption option, Func<string, LogLevel, bool> filter, bool includeScopes)
        {
            factory.AddProvider(new NetLoggerProvider(option, filter, includeScopes));
            return factory;
        }
    }
}
