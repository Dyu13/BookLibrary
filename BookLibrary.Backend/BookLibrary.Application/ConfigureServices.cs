using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using BookLibrary.Application.Books;

namespace BookLibrary.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddScoped<IBookService, BookService>();

        // TODO: Add Auth service

        // TODO: Add the handlers with their commands and queries
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}
