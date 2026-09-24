using System.Text.Json.Serialization;
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
}