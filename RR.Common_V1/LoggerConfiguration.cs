using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System.Collections.Concurrent;

namespace RR.Common_V1
{
    public class LoggerConfiguration 
    {
        public ConcurrentDictionary<string, LogLevel> LogLevel { get; set; }

    }
}
