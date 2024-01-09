using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.GoogleServices.Interfaces;

namespace PersonalFinancialManagement.GoogleServices.Extensions;

public static class ServiceGoogleExtensions
{
    public static void AddGoogleServicesServices(this IServiceCollection services)
    {
        //services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
        services.AddScoped<IDemoService, DemoService>();
    }
}