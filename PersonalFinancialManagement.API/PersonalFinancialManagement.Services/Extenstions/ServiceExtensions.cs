using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Common.Interceptors;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services.Extenstions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IProxyGenerator, ProxyGenerator>();
            services.AddScoped<IAsyncInterceptor, MonitoringInterceptor>();
            services.AddProxiedScoped<ITransactionCategoryTypeService, TransactionCategoryTypeService>();
            services.AddProxiedScoped<ICurrencyService, CurrencyService>();

            //services.AddProxiedScoped<IAuthenticationService, AuthenticationService>();
            //services.AddProxiedScoped<IBillDetailService, BillDetailService>();
            //services.AddProxiedScoped<IBillService, BillService>();
            //services.AddProxiedScoped<ICategoryService, CategoryService>();
            //services.AddProxiedScoped<ICommandService, CommandService>();
            //services.AddProxiedScoped<ICommandInFunctionService, CommandInFunctionService>();
            //services.AddProxiedScoped<ICustomerService, CustomerService>();
            //services.AddProxiedScoped<IDiscountService, DiscountService>();
            //services.AddProxiedScoped<IStorageService, FileStorageService>();
            //services.AddProxiedScoped<IFunctionDetailService, FunctionDetailService>();
            //services.AddProxiedScoped<IFunctionService, FunctionService>();
            //services.AddProxiedScoped<ILangService, LangService>();
            //services.AddProxiedScoped<IProductService, ProductService>();
            //services.AddProxiedScoped<IRevenueService, RevenueService>();
            //services.AddProxiedScoped<IRoleService, RoleService>();
            //services.AddProxiedScoped<IUserService, UserService>();
        }
    }
}
