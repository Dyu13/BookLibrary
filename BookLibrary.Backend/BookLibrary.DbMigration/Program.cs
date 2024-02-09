using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Serilog;

using BookLibrary.Application.Interfaces;
using BookLibrary.DbMigration.Services;
using BookLibrary.Infrastructure;

var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
var host = CreateDefaultBuilder(args, environmentName).Build();

if (IsLocalEnvironment(environmentName))
{
    host.Services.CreateScope();
}

var dbContext = host.Services.GetRequiredService<IApplicationDbContext>();
var config = host.Services.GetRequiredService<IConfiguration>();

Log.Information("Start Database Migration.");
dbContext.Database.Migrate();
Log.Information("Finished Database Migration.");

var seedService = host.Services.GetRequiredService<ISeedService>();

var books = await dbContext.Books.ToListAsync();
if (!books.Any())
{
    Log.Information("Start Database Seeding.");
    await seedService.SeedAsync();
    Log.Information("Finished Database Seeding.");
}

Environment.Exit(0);

static IHostBuilder CreateDefaultBuilder(string[] args, string environmentName)
{
    return Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((hostingContext, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            config.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
            config.AddEnvironmentVariables();
        })
        .ConfigureServices((hostContext, services) =>
        {
            services.AddInfrastructureServices(hostContext.Configuration);
            services.AddSingleton<ISeedService, SeedService>();
        }); 
}

static bool IsLocalEnvironment(string environmentName)
{
    return environmentName is not null && environmentName.Equals("Local", StringComparison.InvariantCulture);
}