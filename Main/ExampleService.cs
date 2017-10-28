using Microsoft.Extensions.Logging;
using System;

namespace Main
{
    public interface IExampleService
    {
        void DoSomeWork();
    }

    public class ExampleService : IExampleService
    {
        readonly ILogger<ExampleService> _logger;

        public ExampleService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExampleService>();

            _logger.LogInformation("ExampleService configured");
        }

        public void DoSomeWork()
        {
            _logger.LogDebug("ExampleService DoSomeWork");
            Console.WriteLine("inside ExampleService.DoSomeWork()");

            Console.ReadLine();
            _logger.LogDebug("ExampleService DoSomeWork ends");
        }
    }
}
