using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using BookLibrary.Application.Interfaces;
using BookLibrary.Infrastructure.Mappers;
using BookLibrary.Infrastructure.Persistence;

namespace BookLibrary.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(ApplicationAutomapperProfile));

        services.AddDbContext<BookLibraryDbContext>(options =>
            options.UseSqlServer(
            configuration.GetConnectionString("DbConnectionString"),
            o => o.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null)));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<BookLibraryDbContext>());

        return services;
    }
}