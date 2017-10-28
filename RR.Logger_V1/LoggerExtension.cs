using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.Common_V1;

namespace RR.Logger_V1
{
    public static class LoggerExtension
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, LoggerConfiguration loggerConfiguration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders().AddProvider(new LoggerProvider(loggerConfiguration)).SetMinimumLevel(LogLevel.Trace);
            });

            return services;
        }
    }
}
