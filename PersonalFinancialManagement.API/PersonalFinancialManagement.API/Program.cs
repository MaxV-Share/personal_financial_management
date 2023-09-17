using Microsoft.EntityFrameworkCore;
using PersonalFinancialManagement.API.Infrastructures.ServicesExtensions;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos;
using PersonalFinancialManagement.Services.Excels.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;

async Task CreateDbIfNotExistsAsync(IHost host)
{
    using var scope = host.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await context.Database.MigrateAsync();
        var dbInitializer = services.GetService<DbInitializer>();
        if (dbInitializer == null)
        {
            logger.LogError("dbInitializer is null");
            return;
        }

        await dbInitializer.Seed();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json")
    .Build();
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddDbContext<ApplicationDbContext>();
var corsSection = configuration.GetSection("CorsOptions");
if (corsSection == null) throw new ArgumentNullException(nameof(corsSection));
var corsOption = corsSection.Get<CorsOptions>();
var policyName = corsOption!.PolicyName.Nullify("AppCorsPolicy");
builder.AddGeneralConfigurations(policyName, corsOption);
builder.Services.AddConfigurationSettings(builder.Configuration);
builder.Services.AddInjectedServices();
builder.Services.ConfigureMongoDbClient();
builder.Services.AddInfrastructureServices();
builder.Services.ConfigureHealthChecks();
builder.Services.AddHealthChecks();
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    var prefixIndexFormat = hostingContext.Configuration.GetValue<string>("ElasticConfiguration:PrefixIndexFormat");
    var elasticUri = hostingContext.Configuration.GetValue<string>("ElasticConfiguration:Uri");
    var username = hostingContext.Configuration.GetValue<string>("ElasticConfiguration:Username");
    var password = hostingContext.Configuration.GetValue<string>("ElasticConfiguration:Password");
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .WriteTo.Elasticsearch(
            new ElasticsearchSinkOptions(new Uri(elasticUri))
            {
                IndexFormat = $"{prefixIndexFormat}-{DateTime.UtcNow:yyyy-MM}",
                AutoRegisterTemplate = true,
                NumberOfReplicas = 1,
                NumberOfShards = 2,
                ModifyConnectionSettings = x => x.BasicAuthentication(username, password),
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                TypeName = null
            })
        .Enrich.FromLogContext()
        .Enrich.WithMachineName();
});
var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHealthChecks("/ping");

app.UseHttpsRedirection();

app.UseCors(policyName);

app.UseRouting().UseEndpoints(endpoints =>
{
    endpoints.Map("/", context => Task.Run(() =>
        context.Response.Redirect("/swagger/index.html")));
});

app.UseAuthorization();

app.MapControllers();

await CreateDbIfNotExistsAsync(app);

try
{
    app.Run();
}
finally
{
    Log.CloseAndFlush();
}