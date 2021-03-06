﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RR.AttributeService_V1;
using RR.Common_V1;
using RR.Logger;
using RR.Logger.Common;
using RR.Migration;
//using RR.SoundService;
//using RR.SoundService.Common;
using RR.WebsocketService_V1;
using System;

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
            services.Configure<RRLoggerConfiguration>(Configuration.GetSection("AppSettings").GetSection("RRLoggerConfiguration"));

            var connection = @"Server=(localdb)\mssqllocaldb;Database=PortfolioV1;Trusted_Connection=True;";

            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));

            var sp = services.BuildServiceProvider();
            var loggerSettings = sp.GetService<IOptions<RRLoggerConfiguration>>();
            var appSettings = sp.GetService<IOptions<AppSettings>>();

            services.AddRRLogger(loggerSettings.Value, appSettings.Value.Ports["Log"]);
            //loggerFactory.AddFile("Logs/myapp-{Date}.txt");

            services.AddSingleton<IAttributeService<ViewModelAttribute>, AttributeService>();
            services.AddSingleton<IValidationAttributeAdapterProvider, ViewModelAttributeAdapterProvider>();
            //services.AddSingleton<ISoundService, SoundService>();

            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            //.AddCookie();
            .AddCookie(options =>
            {
                options.Cookie.Name = "AuthCookie";
                options.Cookie.HttpOnly = true;
                //options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.SameSite = SameSiteMode.None; //<THIS!!!
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;

                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;

                //options.Events = new CookieAuthenticationEvents
                //{
                //    OnValidatePrincipal = LastChangedValidator.ValidateAsync
                //};
            });

            //services.AddCors();

            var mvcBuilder = services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));

            //services.AddWebSocketLoggerService();
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

            //app.UseCors(builder => builder.AllowAnyOrigin());
            //app.UseCors(builder =>
            //    builder.WithOrigins("http://rrsound.de")
            //    .AllowAnyHeader());

            app.UseStaticFiles();


            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.UseWebSocketLoggerService();
        }
    }
}