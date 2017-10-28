using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace RR.ExceptionHandler
{
    public static class ExceptionHandlerExtension
    {
        public static IServiceCollection AddExceptionHandler(this IServiceCollection services, IMvcBuilder builder)
        {
            var sp = services.BuildServiceProvider();
            var logger = sp.GetService<ILoggerFactory>();

            
            builder.AddMvcOptions(o => { o.Filters.Add(new GlobalExceptionFilter(logger)); });

            return services;
        }
    }

    public class GlobalExceptionFilter : IExceptionFilter, IDisposable
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<GlobalExceptionFilter>();
            _logger.LogInformation("GlobalExceptionFilter created!");
        }

        public void OnException(ExceptionContext context)
        {
            var response = new ErrorResponse()
            {
                Message = context.Exception.Message,
                Place = context.ActionDescriptor.DisplayName,
                StackTrace = context.Exception.StackTrace
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = 500,
                DeclaredType = typeof(ErrorResponse)
            };

            this._logger.LogError("GlobalExceptionFilter", context.Exception);
        }

        public void Dispose()
        {

        }
    }
}
