using Microsoft.AspNetCore.Identity;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Entities.Identities;
using PersonalFinancialManagement.Models.Mapper;
using PersonalFinancialManagement.Repositories.Extensions;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Extensions;

namespace PersonalFinancialManagement.API.Infrastructures.ServicesExtensions;

public static class InjectionServiceExtension
{
    public static void AddInjectedServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperProfile));
        services.AddTransient<DbInitializer>();
        services.AddScoped<UserManager<User>>();
        services.AddScoped(typeof(IUnitOffWork<>), typeof(UnitOffWork<>));
        services.AddRepositories();
        services.AddServices();
    }
}