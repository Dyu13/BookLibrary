using Serilog;

using BookLibrary.Infrastructure;
using BookLibrary.Application;

namespace BookLibrary.Api.Bootstrap;

public static class ApplicationBuilder
{
    private static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

    public static WebApplicationBuilder AddLogging(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console() // TODO: write to ElasticSearch or a log database
                .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(hostingContext.Configuration);
        });

        return builder;
    }

    public static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        // TODO: allow only specific origin
        builder.Services.AddCors(options =>
                        options.AddPolicy(MyAllowSpecificOrigins,
                            builder =>
                            {
                                builder
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin()
                                    .AllowAnyHeader()
                                    .AllowAnyMethod();
                            }));

        // Add services to the container.
        builder.Services
            .AddInfrastructureServices(builder.Configuration)
            .AddApplicationServices()
            .AddApiServices()
            .AddAuth(builder.Configuration)
            .AddVersioning()
            .AddMyControllers()
            .AddSwagger();

        return builder;
    }

    public static WebApplication Configure(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        // TODO: use authorization and authentication

        app.UseCors(MyAllowSpecificOrigins);

        app.MapHealthChecks("/healthy");

        app.MapControllers();

        return app;
    }
}
