using MentalHealthCompanion.Data.DataContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MentalHealthCompanion.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            return services;
        }

        public static async Task SeedDatabaseAsync(this WebApplication app)
        {
            await DbSeeder.SeedAsync(app);
        }
    }
}
