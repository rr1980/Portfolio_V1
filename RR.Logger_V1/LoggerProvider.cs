using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RR.Logger_V1
{
    public class LoggerProvider : ILoggerProvider
    {
        private readonly LoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        public static ILogger SelfLogger { get; private set; }

        public LoggerProvider(LoggerConfiguration config)
        {
            _config = config;
            SelfLogger = new Logger(typeof(Logger).FullName, _getFilterLogLvl(typeof(Logger).FullName));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new Logger(name, _getFilterLogLvl(categoryName), SelfLogger));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }

        private LogLevel _getFilterLogLvl(string categoryName)
        {
            if (_config.LogLevel.TryGetValue(categoryName, out var l))
            {
                return l;
            }
            else
            {
                var strA = categoryName.Split(".");
                for (int i = strA.Length; i > 0; i--)
                {

                    var v = String.Join(".", strA.Take(i));
                    if (_config.LogLevel.TryGetValue(v, out var ll))
                    {
                        return ll;
                    }
                }

            }

            if (_config.LogLevel.TryGetValue("Default", out var lll))
            {
                return l;
            }

            return LogLevel.Trace;
        }
    }
}
