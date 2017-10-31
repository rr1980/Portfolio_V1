using System;

namespace RR.Logger.Common
{
    public class RRLoggerException : Exception
    {
        public RRLoggerException(string msg, Exception ex) : base(msg, ex)
        {

        }
    }
}
