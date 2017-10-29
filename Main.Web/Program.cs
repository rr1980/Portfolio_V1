using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using RR.Migration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Main.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            //Seed(host);

            host.Run();
        }

        private static void Seed(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    SampleData.Initialize(services).Wait();
                    // Requires using RazorPagesMovie.Models;
                    //AddTestData(services);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred seeding the DB.");
                }
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();


        //private static async Task AddTestData(IApplicationBuilder app)
        //{
        //    var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
        //    using (var scope = scopeFactory.CreateScope())
        //    {
        //        //var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //        var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        //        var testUser1 = new ApplicationUser
        //        {
        //            UserName = "Abcdefg",
        //            Name = "Luke",
        //            Vorname = "Skywalker"
        //        };
        //        try
        //        {
        //            var result = await _userManager.CreateAsync(testUser1, "12003");
        //        }
        //        catch (Exception ex)
        //        {

        //        }
        //        //context.Users.Add(testUser1);
        //        //context.SaveChanges();
        //    }
        //}
    }

    public class SampleData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<ApplicationDbContext>();

            //string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            //foreach (string role in roles)
            //{
            //    var roleStore = new RoleStore<ApplicationRole>(context);

            //    if (!context.Roles.Any(r => r.Name == role))
            //    {
            //        await roleStore.CreateAsync(new ApplicationRole(role));
            //    }
            //}


            var user = new ApplicationUser
            {
                Name = "Riesner",
                Vorname = "Rene",
                Email = "abdullahnaseer999@gmail.com",
                NormalizedEmail = "ABDULLAHNASEER999@GMAIL.COM",
                UserName = "Owner",
                NormalizedUserName = "OWNER",
                PhoneNumber = "+923366633352",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };


            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);

            }

            //await AssignRoles(serviceProvider, user.Email, roles);

            await context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
            ApplicationUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }

    }
}
