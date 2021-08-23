using System;
using System.Threading.Tasks;
using InstaCrafter.Core.Constants;
using InstaCrafter.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace InstaCrafter.Infrastructure.Identity.Seed
{
    public class ApplicationDbContextDataSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdentityConstants.Roles.Administrator));
            await roleManager.CreateAsync(new IdentityRole(ApplicationIdentityConstants.Roles.Member));

            string adminUserName = "test@test.com";
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                Email = adminUserName,
                IsEnabled = true,
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "Administrator"
            };

            var result = await userManager.CreateAsync(adminUser, ApplicationIdentityConstants.DefaultPassword);
            if (!result.Succeeded)
                throw new Exception(result.ToString());
            adminUser = await userManager.FindByNameAsync(adminUserName);
            await userManager.AddToRoleAsync(adminUser, ApplicationIdentityConstants.Roles.Administrator);
        }
    }
}