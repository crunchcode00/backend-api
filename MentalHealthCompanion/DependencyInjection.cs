using Microsoft.Extensions.DependencyInjection;

namespace MentalHealthCompanion.BackgroundService
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            return services;
        }
    }
}
