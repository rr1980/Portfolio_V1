using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RR.Logger_V1;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Main
{
    class Program
    {
        public static IConfigurationRoot Configuration { get; set; }

        static void Main(string[] args)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            var lf = serviceProvider.GetService<ILoggerFactory>();
            var logger = lf.CreateLogger<Program>();

            logger.LogDebug("Try start Task");
            var task = Task.Run((Action)app.Run);
            logger.LogInformation("Task started");
            Console.WriteLine("End");

            task.Wait();

        }

        private static void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                                    .AddJsonFile("appsettings.json", false)
                                    .Build();

            services.AddOptions();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            var options = services.BuildServiceProvider().GetService<IOptions<AppSettings>>();

            services.AddLogger(options.Value.LoggerConfiguration);
            services.AddTransient<IExampleService, ExampleService>();

            services.AddTransient<App>();
        }
    }
}
