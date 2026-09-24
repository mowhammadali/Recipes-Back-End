using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Recipes.Api.Data;
using Recipes.Api.Data.Seed;
using Serilog;

namespace Recipes.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(
                new JsonStringEnumConverter());
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static ConfigureHostBuilder AddLoggingConfiguration(this ConfigureHostBuilder host)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File(
                "logs/recipe-app-.log",
                rollingInterval: RollingInterval.Day)
            .CreateLogger();

        host.UseSerilog();
        return host;
    }

    public static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresDb") ??
                               throw new NullReferenceException("The connection string is null.");

        services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(connectionString); });

        return services;
    }

    public static async Task SeedDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var logger = scope.ServiceProvider
            .GetRequiredService<ILogger<Program>>();

        await DatabaseSeeder.SeedAsync(dbContext, logger);
    }
}