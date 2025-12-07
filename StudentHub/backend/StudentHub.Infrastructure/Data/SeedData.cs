using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using StudentHub.Core.Entities.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace StudentHub.Infrastructure.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            string[] roles = { "Admin", "Teacher", "Student" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            var adminEmail = "admin@local";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                var adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "System",
                    LastName = "Admin"
                };

                var createRes = await userManager.CreateAsync(adminUser, "Admin123!");

                if (createRes.Succeeded)
                    await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}