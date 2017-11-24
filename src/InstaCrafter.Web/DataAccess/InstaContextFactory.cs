using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace InstaCrafter.Web.DataAccess
{
    public class InstaContextFactory : IDesignTimeDbContextFactory<InstaPostgreSqlContext>
    {
        public InstaPostgreSqlContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<InstaPostgreSqlContext>();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("PostgreSqlProviderConnection"));
            optionsBuilder.EnableSensitiveDataLogging();
            return new InstaPostgreSqlContext(optionsBuilder.Options);
        }
    }
}