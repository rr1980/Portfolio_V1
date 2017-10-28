using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System;
using System.Diagnostics;
using System.Linq;

namespace RR.Logger_V1
{
    public class LoggerStackTrace
    {
        public string MethodName { get; internal set; }
        public string NameSpace { get; internal set; }

    }
    public class Logger : ILogger
    {
        private string name;
        private LoggerConfiguration _config = new LoggerConfiguration();

        public Logger(string name, LoggerConfiguration config)
        {
            this.name = name;
            _config = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            if (_config.LogLevel.TryGetValue(name, out var l))
            {
                return logLevel >= l;
            }
            else
            {
                var strA = name.Split(".");
                for (int i = strA.Length; i > 0; i--)
                {

                    var v = String.Join(".", strA.Take(i));
                    if (_config.LogLevel.TryGetValue(v, out var ll))
                    {
                        return logLevel >= ll;
                    }
                }

            }

            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            LoggerStackTrace str = _getLoggerStackTrace(new StackTrace(), 4, exception);

            Debug.WriteLine(DateTime.Now + " - " + logLevel.ToString() + " - " + name + ": " + str.MethodName + Environment.NewLine + " \t "
                + state + Environment.NewLine
                + (exception != null ? _getException(exception) + Environment.NewLine : ""));
        }

        private string _getException(Exception ex)
        {
            var e = ex;
            var message = "\t\t" + e.Message + Environment.NewLine;
            message += _getexceptionStackTrace("\t\t\t", e.StackTrace);
            message += Environment.NewLine;

            int tabCount = 3;
            while (e.InnerException != null)
            {
                var tab = "";
                e = e.InnerException;
                for (int i = 0; i <= tabCount; i++)
                {
                    tab += "\t";
                }
                message += tab + e.Message + Environment.NewLine;
                message += _getexceptionStackTrace(tab, e.StackTrace);
                message += Environment.NewLine;

                tabCount++;
            }
            return message;
        }

        private string _getexceptionStackTrace(string tab, string str)
        {
            var msg = "";
            var rows = str.Split(Environment.NewLine);
            foreach (var item in rows)
            {
                msg += tab + "\t" + item + Environment.NewLine;
            }

            return msg;
        }

        private LoggerStackTrace _getLoggerStackTrace(StackTrace str, int frameIndex, Exception ex = null)
        {
            if (ex == null)
            {
                var frame = str.GetFrame(frameIndex);
                return new LoggerStackTrace()
                {
                    MethodName = frame.GetMethod().Name,
                    NameSpace = frame.GetMethod().DeclaringType.FullName
                };
            }
            else
            {
                return new LoggerStackTrace()
                {
                    MethodName = ex.TargetSite.Name,
                    NameSpace = ex.TargetSite.DeclaringType.FullName
                };
            }
        }
    }
}
