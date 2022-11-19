using App.Models.Mapper;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.Models.Entities.Identities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Repositories.Extenstions;
using PersonalFinancialManagement.Services.Extenstions;

namespace PersonalFinancialManagement.API.Infrastructures.ServicesExtensions
{
    public static class InjectionServiceExtension
    {
        public static void AddInjectedServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            services.AddRepositories();
            services.AddTransient<DBInitializer>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped(typeof(IUnitOffWork<>), typeof(UnitOffWork<>));
            services.AddServices();
        }
    }
}
