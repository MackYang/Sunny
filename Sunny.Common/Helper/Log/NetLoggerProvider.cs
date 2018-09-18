using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.Helper.Log
{
    class NetLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly ConcurrentDictionary<string, NetLogger> _loggers = new ConcurrentDictionary<string, NetLogger>();

        private readonly Func<string, LogLevel, bool> _filter;
        
        private readonly NetLoggerProcessor _messageQueue = new NetLoggerProcessor();

        private static readonly Func<string, LogLevel, bool> trueFilter = (cat, level) => true;
        private static readonly Func<string, LogLevel, bool> falseFilter = (cat, level) => false;
        private IDisposable _optionsReloadToken;
        private bool _includeScopes;
        private bool _disableColors;
        private IExternalScopeProvider _scopeProvider;

        public NetLoggerProvider() : this(null, false) { }

        public NetLoggerProvider(Func<string, LogLevel, bool> filter, bool includeScopes)
        {
            _filter = filter;
            _includeScopes = includeScopes;
            
        }

       
 

        public ILogger CreateLogger(string name)
        {
            return _loggers.GetOrAdd(name, CreateLoggerImplementation);
        }

        private NetLogger CreateLoggerImplementation(string name)
        {
            return new NetLogger(name, GetFilter( ), null, _messageQueue)
             ;
        }

        private Func<string, LogLevel, bool> GetFilter(  )
        {
            if (_filter != null)
            {
                return _filter;
            }
 

            return trueFilter;
        }

        private IEnumerable<string> GetKeyPrefixes(string name)
        {
            while (!string.IsNullOrEmpty(name))
            {
                yield return name;
                var lastIndexOfDot = name.LastIndexOf('.');
                if (lastIndexOfDot == -1)
                {
                    yield return "Default";
                    break;
                }
                name = name.Substring(0, lastIndexOfDot);
            }
        }

        private IExternalScopeProvider GetScopeProvider()
        {
            if (_includeScopes && _scopeProvider == null)
            {
                _scopeProvider = new LoggerExternalScopeProvider();
            }
            return _includeScopes ? _scopeProvider : null;
        }

        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
            _messageQueue.Dispose();
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }
    }
}
