using Microsoft.Extensions.Logging;
using Sunny.Common.Helper.Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public static class LoggerFactoryExtend
    {
        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory)
        {
            factory.AddProvider(new NetLoggerProvider());
            return factory;
        }

        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, Func<string, LogLevel, bool> filter)
        {
            factory.AddProvider(new NetLoggerProvider(filter, false));
            return factory;
        }

        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, Func<string, LogLevel, bool> filter, bool includeScopes)
        {
            factory.AddProvider(new NetLoggerProvider(filter,includeScopes));
            return factory;
        }
    }
}
