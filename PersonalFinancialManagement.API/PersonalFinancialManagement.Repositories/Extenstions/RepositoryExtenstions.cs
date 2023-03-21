using Microsoft.Extensions.DependencyInjection;
using PersonalFinancialManagement.Repositories.BaseRepository;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Models.Entities;

namespace PersonalFinancialManagement.Repositories.Extenstions
{
    public static class RepositoryExtenstions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddProxiedScoped<IBaseRepository<TransactionCategoryType, Guid>, BaseRepository<TransactionCategoryType, Guid>>();
            services.AddProxiedScoped<IBaseRepository<Currency, Guid>, BaseRepository<Currency, Guid>>();
            services.AddProxiedScoped<IBaseRepository<PaymentAccountType, Guid>, BaseRepository<PaymentAccountType, Guid>>();
            services.AddProxiedScoped<IBaseRepository<PaymentAccount, Guid>, BaseRepository<PaymentAccount, Guid>>();
        }
    }
}
