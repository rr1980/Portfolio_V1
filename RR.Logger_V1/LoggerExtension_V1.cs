using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RR.Common_V1;

namespace RR.Logger_V1
{
    public static class LoggerExtension_V1
    {
        public static IServiceCollection AddLogger(this IServiceCollection services, ILoggerConfiguration loggerConfiguration)
        {
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders().AddProvider(new LoggerProvider_V1(loggerConfiguration));
                //loggingBuilder.ClearProviders().AddProvider(new LoggerProvider(new LoggerConfiguration()
                //{
                //    LogLevel = new ConcurrentDictionary<string, LogLevel>(new Dictionary<string, LogLevel>()
                //         {
                //             { "Microsoft", LogLevel.Warning },
                //             { "Microsoft.AspNetCore", LogLevel.Warning },
                //             { "System", LogLevel.Warning },
                //             { "Default", LogLevel.None },
                //             { "AttributeTest", LogLevel.Trace }

                //         })
                //}));
            });

            return services;
        }
    }
}
