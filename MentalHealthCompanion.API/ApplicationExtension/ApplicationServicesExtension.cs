using MentalHealthCompanion.Data.DataContext;
using MentalHealthCompanion.Data.Interface;
using MentalHealthCompanion.Data.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SendGrid.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Text;

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

            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(option =>
            //{
            //    option.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtOptions:SigningKey"]!)),
            //        ValidateIssuer = true,
            //        ValidIssuer = config["JwtOptions:Issuer"]!,
            //        ValidateAudience = true,
            //        ValidAudience = config["JwtOptions:Audience"]!,
            //        RequireExpirationTime = true,
            //        ValidateLifetime = true,
            //        RoleClaimType = ClaimTypes.Role,
            //    };

            //    option.Events = new JwtBearerEvents
            //    {
            //        OnAuthenticationFailed = context =>
            //        {
            //            Console.WriteLine(" Token validation failed: " + context.Exception.Message);
            //            return Task.CompletedTask;
            //        },
            //        OnTokenValidated = context =>
            //        {
            //            Console.WriteLine(" Token successfully validated.");
            //            return Task.CompletedTask;
            //        }
            //    };
            //});
            #region Authentication
            services.AddAuthorization(option =>
            {
                option.AddPolicy("CreateAdmin", config => config.RequireClaim(ClaimTypes.Role, "SuperAdmin"));
                option.AddPolicy("AdminPassword", config => config.RequireClaim(ClaimTypes.Role, "Admin"));
            });
            #endregion

            return services;
        }
    }
}
