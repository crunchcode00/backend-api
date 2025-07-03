using MentalHealthCompanion.Data.DataContext;
using MentalHealthCompanion.Data.Interface;
using MentalHealthCompanion.Data.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MentalHealthCompanion.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenService, AuthenService>();
            services.AddScoped<IJwtService, JwtService>();

            var secretKey = "YourSuperSecretKeyHere";
            var key = Encoding.UTF8.GetBytes(secretKey);

            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };
            return services;
        }

        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            await DbSeeder.SeedAsync(app);
        }
    }
}
