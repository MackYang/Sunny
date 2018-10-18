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

        public static ILoggerFactory AddNetLoggerUseDefaultFilter(this ILoggerFactory factory, NetLoggerOption option)
        {
            Func<string, LogLevel, bool> filter = (category, level) =>
            {

                return (level >= LogLevel.Information && !category.StartsWith("Microsoft")) || (level > LogLevel.Warning);

            };

            factory.AddProvider(new NetLoggerProvider(option, filter, false));
            return factory;
        }

        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, NetLoggerOption option, Func<string, LogLevel, bool> filter, bool includeScopes)
        {
            factory.AddProvider(new NetLoggerProvider(option, filter, includeScopes));
            return factory;
        }
    }
}
