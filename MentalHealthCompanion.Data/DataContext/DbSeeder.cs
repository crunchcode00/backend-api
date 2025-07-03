using MentalHealthCompanion.Data.DatabaseEntities;
using MentalHealthCompanion.Data.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MentalHealthCompanion.Data.DataContext
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;

            var dbContext = services.GetRequiredService<AppDbContext>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();

            var adminRoleName = UserRole.Admin.ToString();

            if (!await roleManager.RoleExistsAsync(adminRoleName))
            {
                var adminRole = new IdentityRole(adminRoleName);
                await roleManager.CreateAsync(adminRole);
            }

            const string adminEmail = "D.abudu@conclaseint.com";
            const string adminPassword = "Admin@123";

            var existingUser = await userManager.FindByEmailAsync(adminEmail);
            IdentityUser identityUser;
            if (existingUser == null)
            {
                identityUser = new IdentityUser
                {
                    UserName = "superadmin",
                    Email = adminEmail,
                    NormalizedEmail = adminEmail.ToUpper(),
                    EmailConfirmed = true
                };
                var createdResult = await userManager.CreateAsync(identityUser, adminPassword);
                if (!createdResult.Succeeded)
                {
                    throw new Exception("Failed to create admin user: " +
                        string.Join(", ", createdResult.Errors.Select(e => e.Description)));
                }
                await userManager.AddToRoleAsync(identityUser, adminRoleName);

            }
            else
            {
                identityUser = existingUser;
            }

            //IdentityUser identityUser = new IdentityUser() { Email = "D.abudu@conclaseint.com" };
            //AppUser adminUser = new AppUser();

            if (!dbContext.AppUsers.Any(u => u.Id == identityUser.Id))
            {
                var adminUser = new AppUser
                {
                    Id = identityUser.Id.ToString(),
                    FirstName = "SuperAdmin",
                    LastName = "User",
                    EmailAddress = "D.abudu@conclaseint.com",
                    Role = adminRoleName
                };
                dbContext.AppUsers.Add(adminUser);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
