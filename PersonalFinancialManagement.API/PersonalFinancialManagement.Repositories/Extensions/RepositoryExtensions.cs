using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.BaseRepository;

namespace PersonalFinancialManagement.Repositories.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddProxiedScoped<IBaseRepository<TransactionCategoryType, Guid>, BaseRepository<TransactionCategoryType, Guid>>();
        services.AddProxiedScoped<IBaseRepository<TransactionCategory, Guid>, BaseRepository<TransactionCategory, Guid>>();
        services.AddProxiedScoped<IBaseRepository<Transaction, Guid>, BaseRepository<Transaction, Guid>>();
        services.AddProxiedScoped<IBaseRepository<Currency, Guid>, BaseRepository<Currency, Guid>>();
        services.AddProxiedScoped<IBaseRepository<PaymentAccountType, Guid>, BaseRepository<PaymentAccountType, Guid>>();
        services.AddProxiedScoped<IBaseRepository<PaymentAccount, Guid>, BaseRepository<PaymentAccount, Guid>>();
        services.AddProxiedScoped<IBaseRepository<RawTransaction, Guid>, BaseRepository<RawTransaction, Guid>>();
    }
}