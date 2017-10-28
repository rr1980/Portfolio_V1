using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System.Collections.Concurrent;

namespace RR.Logger_V1
{
    public class LoggerProvider_V1 : ILoggerProvider
    {
        private readonly ILoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();

        public LoggerProvider_V1(ILoggerConfiguration config)
        {
            _config = config;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new Logger_V1(name, _config));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
