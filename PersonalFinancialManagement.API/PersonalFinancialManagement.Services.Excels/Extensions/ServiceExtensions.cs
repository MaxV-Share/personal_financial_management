using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using PersonalFinancialManagement.Services.Excels.Configurations;
using PersonalFinancialManagement.Services.Excels.Repositories;

namespace PersonalFinancialManagement.Services.Excels.Extensions;

public static class ServiceExtensions
{
    private static string GetMongoDbConnectionString(this IServiceCollection services,
        IConfiguration configuration)
    {
        var settings = configuration.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));
        if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            throw new ArgumentNullException("DatabaseSettings is not configured");

        var databaseName = settings.DatabaseName;
        var mongodbConnectionString = settings.ConnectionString + "/" + databaseName +
                                      "?authSource=admin";
        return mongodbConnectionString;
    }

    public static void ConfigureMongoDbClient(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(
                new MongoClient(services.GetMongoDbConnectionString(configuration)))
            .AddScoped(x => x.GetService<IMongoClient>()!.StartSession());
    }

    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        services.AddScoped<IExample, Example>();
        services.AddScoped<IMisaRawDataRepository, MisaRawDataRepository>();
    }

    public static void ConfigureHealthChecks(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseSettings = configuration.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));
        services.AddHealthChecks()
            .AddMongoDb(databaseSettings!.ConnectionString,
                "MongoDb Health",
                HealthStatus.Degraded);
    }
}