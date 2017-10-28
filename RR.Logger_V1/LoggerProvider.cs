using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System.Collections.Concurrent;

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
            SelfLogger = new Logger(typeof(Logger).FullName, config);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new Logger(name, _config, SelfLogger));
        }

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
