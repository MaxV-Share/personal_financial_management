using Microsoft.Extensions.DependencyInjection;
using System;
using PersonalFinancialManagement.Repositories.BaseRepository;

namespace PersonalFinancialManagement.Repositories.Extenstions
{
    public static class RepositoryExtenstions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddProxiedScoped<IBaseRepository<BillDetail, int>, BaseRepository<BillDetail, int>>();
        }
    }
}
