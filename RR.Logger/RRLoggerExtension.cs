using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.Logger.Common;

namespace RR.Logger
{
    public static class RRLoggerExtension
    {
        public static IServiceCollection AddRRLogger(this IServiceCollection services, RRLoggerConfiguration loggerConfiguration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders().AddProvider(new RRLoggerProvider(loggerConfiguration)).SetMinimumLevel(LogLevel.Trace);//.AddFile("Logs/myapp-{Date}.txt");
            });

            return services;
        }
    }
}
