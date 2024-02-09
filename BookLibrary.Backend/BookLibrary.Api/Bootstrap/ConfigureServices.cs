using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace BookLibrary.Api.Bootstrap;

public static class ConfigureServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // TODO: add api key authentication

        return services;
    }   

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: add authentication and authorization using JWT Bearer and claims

        return services;
    }

    public static IServiceCollection AddVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(setup =>
        {
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.ReportApiVersions = true;
        });

        return services;
    }

    public static IServiceCollection AddMyControllers(this IServiceCollection services)
    {
        services.AddHealthChecks(); // TODO: add other custom health checks

        services.AddControllers(); // TODO: add filters for key authorization

        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Book Library", Version = "v1" });

            // TODO: Add Security Definition for API Key
        });

        return services;
    }
}
