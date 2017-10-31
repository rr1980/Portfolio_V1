using Microsoft.Extensions.Logging;
using RR.Logger.Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace RR.Logger
{
    public class RRLogger : ILogger
    {
        private readonly int _logServerPort;
        private readonly ILogger _selfLogger;
        public string Name { get; private set; }
        private LogLevel _filter;
        private readonly HttpClient _client;

        public RRLogger(string name, LogLevel filter, int logServerPort, ILogger selfLogger = null)
        {
            _selfLogger = selfLogger;
            Name = name;
            _filter = filter;
            _logServerPort = logServerPort;

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

            RRLoggerStackTrace str = _getLoggerStackTrace(new StackTrace(), exception);


            var msg = $"{DateTime.Now} {logLevel}: [{eventId.Id}] {str.NameSpace}: {str.MethodName} {Environment.NewLine + " \t "} "
                + formatter(state, exception) + Environment.NewLine
                + (exception != null ? _getException(exception) + Environment.NewLine : "");

            //Debug.WriteLine(msg);

            Task.Run(() => ProcessRepositories(msg));
        }



        private async Task ProcessRepositories(string msg)
        {
            var stringTask = _client.PostAsync("http://localhost" + ":" + _logServerPort + "/api/values", new JsonContent<RRLoggerMsg>(new RRLoggerMsg()
            {
                Msg = msg
            }));
            var result = await stringTask;
        }

        private string _getException(Exception ex)
        {
            var e = ex;
            var message = "\t\t" + e.Message + Environment.NewLine;
            message += _getexceptionStackTrace("\t\t\t", e.StackTrace);

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

        private RRLoggerStackTrace _getLoggerStackTrace(StackTrace str, Exception ex = null)
        {
            if (string.IsNullOrEmpty(Name))
            {
                throw new NullReferenceException("Name darf nicht NULL sein!");
            }
            if (ex == null || ex.TargetSite == null)
            {
                var frame = str.GetFrames().FirstOrDefault(f => f.GetMethod()?.ReflectedType?.Namespace + "." + f.GetMethod()?.ReflectedType?.Name == Name);
                return new RRLoggerStackTrace()
                {
                    MethodName = frame?.GetMethod()?.Name,
                    NameSpace = Name
                };
            }
            else
            {
                return new RRLoggerStackTrace()
                {
                    MethodName = ex.TargetSite?.Name,
                    NameSpace = ex.TargetSite?.DeclaringType.FullName
                };
            }
        }
    }
    }
