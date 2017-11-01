using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RR.Common_V1;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using RR.AttributeService_V1;
using RR.SoundService.Common;
using Microsoft.AspNetCore.Mvc;

namespace RR.SoundService.SoundServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAttributeService<ViewModelAttribute>, AttributeService>();
            services.AddSingleton<IValidationAttributeAdapterProvider, ViewModelAttributeAdapterProvider>();
            services.AddSingleton<ISoundService, SoundService>();

            //services.AddCors();

            //services.AddMvc();
            var mvcBuilder = services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Error");
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            //app.Use(async (context, next) =>
            //{
            //    //context.Response.Headers.Add("X-Frame-Options", "ALLOW-FROM http://rrsound.de");
            //    //context.Response.Headers.Add("X-Frame-Options", "ALLOW-FROM http://localhost:49985/");
            //    context.Response.Headers.Add("X-Frame-Options", "ALLOW");
            //    //context.Response.Headers.Add("Content-Security-Policy", "default-src 'self'");
            //    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            //    await next();
            //});
            //app.UseCors(builder => builder.AllowAnyOrigin());
            //app.UseCors(builder =>
            //    builder.WithOrigins("http://localhost:80")
            //    .AllowAnyHeader());

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
