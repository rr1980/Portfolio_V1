//using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace RR.Logger.Extension
{
    internal static class RRLoggerExtensionsHelper
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
}
