using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;
using Sunny.Common.Helper.Log;
using System;
using System.Collections.Generic;
using System.Text;

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

        public static ILoggerFactory AddNetLogger(this ILoggerFactory factory, NetLoggerOption option, Func<string, LogLevel, bool> filter, bool includeScopes)
        {
            factory.AddProvider(new NetLoggerProvider(option, filter, includeScopes));
            return factory;
        }
    }
}
