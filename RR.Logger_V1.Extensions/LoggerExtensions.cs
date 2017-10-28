using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections;

namespace RR.Logger_V1.Extensions
{
    public class TestClass
    {
        public string Name = "Klaus";
    }

    internal static class LoggerExtensionsHelper
    {
        internal static string GetJsonObjString(object[] objs)
        {
            var msg = "";
            foreach (var item in objs)
            {
                var str = JsonConvert.SerializeObject(item);
                var name = "";

                if (item.GetType().GetInterface("IEnumerable") != null)
                {
                    name = item.GetType().Name + "<" + item.GetType().GenericTypeArguments.FirstOrDefault().Name + ">";
                }
                else
                {
                    name = item.GetType().Name;
                }

                msg += "\t\t\t" + name + ": " + str + Environment.NewLine;
            }

            return msg;
        }
    }

    public static class LoggerExtensions
    {
        public static void Log_Controller_Call(this ILogger logger, string message, params object[] objs)
        {
            string objs_string = LoggerExtensionsHelper.GetJsonObjString(objs);
            logger.LogTrace(logger.GetType().GenericTypeArguments.FirstOrDefault().Name + " called..." + Environment.NewLine + objs_string);
        }

        public static void Log_Object(this ILogger logger, string message, params object[] objs)
        {
            try
            {
                string objs_string = LoggerExtensionsHelper.GetJsonObjString(objs);
                logger.LogTrace(message + Environment.NewLine + objs_string);
            }
            catch (Exception ex)
            {
                try
                {
                    throw  new LoggerException("Cannot Objs serialize", ex);
                }
                catch(Exception ex1)
                {
                    LoggerProvider.SelfLogger.LogWarning(ex1, "Log_Object hat Fehler!");
                    logger.LogTrace(message);
                }
            }
        }

        public static void Log_Controller_Start(this ILogger logger)
        {
            logger.LogTrace(logger.GetType().GenericTypeArguments.FirstOrDefault().Name + " init started...");
        }

        public static void Log_Controller_End(this ILogger logger)
        {
            logger.LogTrace(logger.GetType().GenericTypeArguments.FirstOrDefault().Name + " init ends...");
        }
    }
}
