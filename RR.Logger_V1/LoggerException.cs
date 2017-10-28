using System;

namespace RR.Logger_V1
{
    public class LoggerException : Exception
    {
        public LoggerException(string msg, Exception ex) : base(msg, ex)
        {

        }
    }
}
