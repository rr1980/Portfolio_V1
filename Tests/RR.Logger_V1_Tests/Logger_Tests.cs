using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RR.Common_V1;
using RR.Logger_V1;
using System.IO;
using System.Reflection;

namespace RR.Logger_V1_Tests
{
    [TestClass]
    public class Logger_Tests
    {
        private readonly ILogger<Logger_Tests> _logger;

        public Logger_Tests()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var lf = serviceProvider.GetService<ILoggerFactory>();
            _logger = lf.CreateLogger<Logger_Tests>();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Path.GetDirectoryName(Assembly.GetAssembly(typeof(Main.Program)).Location))
                                    .AddJsonFile("appsettings.json", false)
                                    .Build();

            services.AddOptions();

            var ccc = configuration.GetSection("AppSettings");

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            var options = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            services.AddLogger(options.Value.LoggerConfiguration);
        }

        [TestMethod]
        public void LogInformation()
        {
            _logger.LogInformation("LogInformation Test");
        }
    }
}
