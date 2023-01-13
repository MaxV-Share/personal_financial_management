using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PFM.Data;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PFM.EntityFrameworkCore;

public class EntityFrameworkCorePFMDbSchemaMigrator
    : IPFMDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCorePFMDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the PFMDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<PFMDbContext>()
            .Database
            .MigrateAsync();
    }
}
