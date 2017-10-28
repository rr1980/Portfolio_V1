using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System.Collections.Concurrent;

namespace RR.Logger_V1
{
    public class LoggerConfiguration_V1 : ILoggerConfiguration
    {
        public ConcurrentDictionary<string, LogLevel> LogLevel { get; set; }

    }
}
