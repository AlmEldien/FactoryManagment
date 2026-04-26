using FactoryManagment.Application;
using FactoryManagment.Infrastructure;
using Serilog;
using Serilog.Events;

// Add Logging with Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .WriteTo.Console(
        outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
    )
    .WriteTo.File(
        path: "logs/app-.log",
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        rollOnFileSizeLimit: true,
        fileSizeLimitBytes: 10_000_000,
        outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] [{SourceContext}]{NewLine}{Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Replace the built-in logging with the Serilog logging
    builder.Host.UseSerilog();

    // Add services to the container.
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();
    builder.Services.AddApplication();
    builder.Services.AddInfrastructure();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to run!");
}

finally 
{
    Log.CloseAndFlush();
}