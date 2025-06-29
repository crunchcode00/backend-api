using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

namespace MentalHealthCompanion.API.ApplicationExtension
{
    public static class ApplicationVersioningExtension
    {
        public static IServiceCollection AddApplicationVersioning(this IServiceCollection services)
        {
            //services.ConfigureOptions<SwaggerConfig>();

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = ApiVersion.Default;
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();

            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }
    }
}
