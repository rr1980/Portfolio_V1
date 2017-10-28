using RR.Common_V1;

namespace Main
{
    internal class AppSettings : IAppSettings
    {
        public ILoggerConfiguration LoggerConfiguration { get; set; }
    }
}