using MentalHealthCompanion.Data.DataContext;
using MentalHealthCompanion.Data.Interface;
using MentalHealthCompanion.Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SendGrid.Extensions.DependencyInjection;

namespace MentalHealthCompanion.API.ApplicationExtension
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            #region Database Context
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("Database"));

            });
            #endregion

            #region Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IEmailNotificationService, EmailNotificationService>();
            services.AddScoped<IJwtService, JwtService>();
            #endregion

            #region Send Grid
            var sender = config["SendGrid:Sender"];
            var from = config["SendGrid:From"];
            var apiKey = config["SendGrid:ApiKey"];

            services
                .AddFluentEmail(sender, from)
                .AddSendGridSender(apiKey);

            services
                .AddSendGrid(options =>
                {
                    options.ApiKey = apiKey;
                });
            #endregion

            return services;
        }
    }
}
