using Microsoft.Extensions.DependencyInjection;

namespace MentalHealthCompanion.BackgroundService
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            // Register background services here
            // Example: services.AddHostedService<MyBackgroundService>();
            return services;
        }
    }
}
