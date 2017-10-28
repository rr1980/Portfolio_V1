using Microsoft.Extensions.Logging;
using System;

namespace Main
{
    public class App
    {
        readonly ILogger<App> _logger;
        private readonly IExampleService _exampleService;

        public App(ILoggerFactory loggerFactory, IExampleService exampleService)
        {
            _logger = loggerFactory.CreateLogger<App>();
            _exampleService = exampleService;
            _logger.LogInformation("App configured");
        }

        public void Run()
        {
            try
            {
                _logger.LogInformation("App started");

                _logger.LogDebug("BÄM!");
                _exampleService.DoSomeWork();


                _logger.LogInformation("App ends");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception while running the Example Service.");
            }
        }

    }
}
