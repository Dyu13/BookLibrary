using BookLibrary.Api.Bootstrap;

var builder = WebApplication.CreateBuilder(args);

var app = builder
    .AddLogging()
    .AddServices()
    .Build();

app.Configure();

await app.RunAsync();