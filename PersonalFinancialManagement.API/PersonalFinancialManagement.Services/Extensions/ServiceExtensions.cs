using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Common.Interceptors;
using PersonalFinancialManagement.GoogleServices;
using PersonalFinancialManagement.GoogleServices.GoogleSheets;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Services.Jobs;
using PersonalFinancialManagement.Services.Mails;
using PersonalFinancialManagement.Services.Mails.Interfaces;

namespace PersonalFinancialManagement.Services.Extensions;

public static class ServiceExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IProxyGenerator, ProxyGenerator>();
        services.AddScoped<IAsyncInterceptor, MonitoringInterceptor>();
        services
            .AddProxiedScoped<ITransactionCategoryTypeService, TransactionCategoryTypeService>();
        services.AddProxiedScoped<ITransactionCategoryService, TransactionCategoryService>();
        services.AddProxiedScoped<ITransactionService, TransactionService>();
        services.AddProxiedScoped<ICurrencyService, CurrencyService>();
        services.AddProxiedScoped<IPaymentAccountTypeService, PaymentAccountTypeService>();
        services.AddProxiedScoped<IPaymentAccountService, PaymentAccountService>();
        services.AddProxiedScoped<IRawTransactionService, RawTransactionService>();
        services.AddScoped<IVpBankCreditGoogleSheetService, VpBankCreditGoogleSheetService>();
        services.AddScoped<IVpBankCreditGmailService, VpBankCreditGmailService>();
        services.AddScoped<GoogleSheetService, GoogleSheetService>();
        services.AddScoped<VpBankCreditJob, VpBankCreditJob>();

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