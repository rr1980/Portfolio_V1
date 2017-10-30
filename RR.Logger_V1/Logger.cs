using Microsoft.Extensions.Logging;
using RR.Common_V1;
using System;
using System.Collections.Concurrent;
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
        private readonly ILogger _selfLogger;
        public string Name { get; private set; }
        //private LoggerConfiguration _config = new LoggerConfiguration();
        private  LogLevel _filter;

        public Logger(string name, LogLevel filter, ILogger selfLogger = null)
        {
            _selfLogger = selfLogger;
            Name = name;
            _filter = filter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _filter;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            LoggerStackTrace str = _getLoggerStackTrace(new StackTrace(), exception);

            Debug.WriteLine($"{DateTime.Now} {logLevel}: [{eventId.Id}] {str.NameSpace}: {str.MethodName} {Environment.NewLine + " \t "} "
                + formatter(state, exception) + Environment.NewLine
                + (exception != null ? _getException(exception) + Environment.NewLine : ""));
        }

        private string _getException(Exception ex)
        {
            var e = ex;
            var message = "\t\t" + e.Message + Environment.NewLine;
            message += _getexceptionStackTrace("\t\t\t", e.StackTrace);
            //message += Environment.NewLine;

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
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }

            var msg = "";
            var rows = str.Split(Environment.NewLine);
            foreach (var item in rows)
            {
                msg += tab + "\t" + item + Environment.NewLine;
            }

            return msg + Environment.NewLine;
        }

        private LoggerStackTrace _getLoggerStackTrace(StackTrace str, Exception ex = null)
        {
            if(string.IsNullOrEmpty(Name))
            {
                throw new NullReferenceException("Name darf nicht NULL sein!");
            }
            if (ex == null || ex.TargetSite == null)
            {
                var frame = str.GetFrames().FirstOrDefault(f => f.GetMethod()?.ReflectedType?.Namespace + "." + f.GetMethod()?.ReflectedType?.Name == Name);
                return new LoggerStackTrace()
                {
                    MethodName = frame?.GetMethod()?.Name,
                    NameSpace = Name
                };
            }
            else
            {
                return new LoggerStackTrace()
                {
                    MethodName = ex.TargetSite?.Name,
                    NameSpace = ex.TargetSite?.DeclaringType.FullName
                };
            }
        }
    }
}
