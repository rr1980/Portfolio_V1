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
using RR.Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;

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

            var connection = @"Server=(localdb)\mssqllocaldb;Database=PortfolioV1;Trusted_Connection=True;";

            services.AddEntityFrameworkAAA(connection);
            //services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(connection));

            //        services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>(options =>
            //options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            //services.AddDbContext<ApplicationDbContext>(opt => opt.UseInMemoryDatabase());

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = false;

                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;
                
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
                options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
                options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

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

            //var context = app.ApplicationServices.GetService<ApplicationDbContext>();
            //AddTestData(app).Wait();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
