using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace RR.Migration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddEntityFrameworkAAA(this IServiceCollection services, string connectionString)
        {
            //var connection = @"Server=(localdb)\mssqllocaldb;Database=PortfolioV1;Trusted_Connection=True;";
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).AssemblyQualifiedName)));
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=PortfolioV1;Trusted_Connection=True;";
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connection);
            return new ApplicationDbContext(builder.Options);
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //this.Database.Log = (message) => Debug.Write(message);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=PortfolioV1;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        public DbSet<UserModel> Users { get; set; }
        //public DbSet<ApplicationRole> Roles { get; set; }
    }
}
