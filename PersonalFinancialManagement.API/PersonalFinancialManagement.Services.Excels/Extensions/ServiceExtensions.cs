using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;
using PersonalFinancialManagement.Services.Excels.Configurations;
using PersonalFinancialManagement.Services.Excels.Extensions;
using PersonalFinancialManagement.Services.Excels.Repositories;

namespace PersonalFinancialManagement.Services.Excels.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddConfigurationSettings(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(MongoDbSettings))
            .Get<MongoDbSettings>();
        services.AddSingleton(databaseSettings);
        
        return services;
    }
    
    private static string getMongoConnectionString(this IServiceCollection services)
    {
        var settings = services.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));
        if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
            throw new ArgumentNullException("DatabaseSettings is not configured");

        var databaseName = settings.DatabaseName;
        var mongodbConnectionString = settings.ConnectionString + "/" + databaseName +
                                      "?authSource=admin";
        return mongodbConnectionString;
    }

    public static void ConfigureMongoDbClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(
            new MongoClient(getMongoConnectionString(services)))
            .AddScoped(x => x.GetService<IMongoClient>()!.StartSession());
    }

    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        services.AddScoped<IExample, Example>();
        services.AddScoped<IMisaRawDataRepository, MisaRawDataRepository>();
    }
    
    public static void ConfigureHealthChecks(this IServiceCollection services)
    {
        var databaseSettings = services.GetOptions<MongoDbSettings>(nameof(MongoDbSettings));
        services.AddHealthChecks()
            .AddMongoDb(databaseSettings.ConnectionString,
                name: "MongoDb Health",
                failureStatus: HealthStatus.Degraded);
    }
}
