using System.Threading.Tasks;
using InstaCrafter.Infrastructure.Identity;
using InstaCrafter.Infrastructure.Identity.Models;
using InstaCrafter.Infrastructure.Identity.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace InstaCrafter.Infrastructure.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static async Task SeedIdentityDataAsync(this IApplicationBuilder builder)
        {
            using var serviceScope =
                builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var services = serviceScope.ServiceProvider;

            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            await ApplicationDbContextDataSeed.SeedAsync(userManager, roleManager);
        }

        public static void EnsureIdentityDbIsCreated(this IApplicationBuilder builder)
        {
            using var serviceScope =
                builder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var services = serviceScope.ServiceProvider;
            var dbContext = services.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }
    }
}