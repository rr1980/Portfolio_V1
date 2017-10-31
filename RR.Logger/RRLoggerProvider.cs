using Microsoft.Extensions.Logging;
using RR.Logger.Common;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace RR.Logger
{
    public class RRLoggerProvider : ILoggerProvider
    {
        private readonly int _logServerPort;
        private readonly RRLoggerConfiguration _config;
        private readonly ConcurrentDictionary<string, ILogger> _loggers = new ConcurrentDictionary<string, ILogger>();
        public static ILogger SelfLogger { get; private set; }

        public RRLoggerProvider(RRLoggerConfiguration config, int logServerPort)
        {
            _config = config;
            _logServerPort = logServerPort;
            SelfLogger = new RRLogger(typeof(RRLogger).FullName, _getFilterLogLvl(typeof(RRLogger).FullName), logServerPort);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new RRLogger(name, _getFilterLogLvl(categoryName), _logServerPort, SelfLogger));
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
