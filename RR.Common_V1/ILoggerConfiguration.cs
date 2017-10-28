using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace RR.Common_V1
{
    public interface ILoggerConfiguration
    {
        ConcurrentDictionary<string, LogLevel> LogLevel { get; set; }
    }
}
