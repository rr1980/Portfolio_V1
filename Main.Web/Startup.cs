using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RR.Common_V1;
using Microsoft.Extensions.Options;
using RR.Logger_V1;
using RR.AttributeService_V1;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using RR.ExceptionHandler;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace Main.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            var sp = services.BuildServiceProvider();

            var settings = sp.GetService<IOptions<AppSettings>>();

            services.AddLogger(settings.Value.LoggerConfiguration);
            //loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            services.AddSingleton<IAttributeService<ViewModelAttribute>, AttributeService>();
            services.AddSingleton<IValidationAttributeAdapterProvider, ViewModelAttributeAdapterProvider>();

            var mvcBuilder = services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            //services.AddExceptionHandler(mvcBuilder);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");

                app.UseBrowserLink();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePages("text/plain", "<h1>Status code page, status code: {0}</h1>");

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
