using Microsoft.EntityFrameworkCore;
using PersonalFinancialManagement.Models.DbContexts;
using Serilog;

async Task CreateDbIfNotExistsAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
            var dbInitializer = services.GetService<DBInitializer>();
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
}
var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json")
                  .Build();
Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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