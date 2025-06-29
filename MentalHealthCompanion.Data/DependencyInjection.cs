using Microsoft.Extensions.DependencyInjection;

namespace MentalHealthCompanion.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
