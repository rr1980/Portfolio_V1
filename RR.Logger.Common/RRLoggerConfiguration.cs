using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;

namespace RR.Logger.Common
{

    public class RRLoggerConfiguration
    {
        public bool UseLoggerServer { get; set; }
        public ConcurrentDictionary<string, LogLevel> LogLevel { get; set; } = new ConcurrentDictionary<string, Microsoft.Extensions.Logging.LogLevel>();
    }
}
