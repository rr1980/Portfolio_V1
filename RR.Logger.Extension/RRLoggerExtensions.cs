using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.Logger.Common;
using System;
using System.Linq;

namespace RR.Logger.Extension
{
    public static class RRLoggerExtensions
    {
            public static void Log_Controller_Call(this ILogger logger, string message, params object[] objs)
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    string objs_string = RRLoggerExtensionsHelper.GetJsonObjString(objs);
                    logger.LogTrace(logger.GetType().GenericTypeArguments.FirstOrDefault().Name + " called..." + Environment.NewLine + objs_string);
                }
            }

            public static void Log_Object(this ILogger logger, string message, params object[] objs)
            {
                try
                {
                    if (logger.IsEnabled(LogLevel.Trace))
                    {
                        string objs_string = RRLoggerExtensionsHelper.GetJsonObjString(objs);
                        logger.LogTrace(message + Environment.NewLine + objs_string);
                    }
                }
                catch (Exception ex)
                {
                    try
                    {
                        throw new RRLoggerException("Cannot Objs serialize", ex);
                    }
                    catch (Exception ex1)
                    {
                        RRLoggerProvider.SelfLogger.LogWarning(ex1, "Log_Object hat Fehler!");
                        logger.LogTrace(message);
                    }
                }
            }

            public static void Log_Controller_Start(this ILogger logger)
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace(logger.GetType().GenericTypeArguments.FirstOrDefault().Name + " init started...");
                }
            }

            public static void Log_Controller_End(this ILogger logger)
            {
                if (logger.IsEnabled(LogLevel.Trace))
                {
                    logger.LogTrace(logger.GetType().GenericTypeArguments.FirstOrDefault().Name + " init ends...");
                }
            }
    }
}
