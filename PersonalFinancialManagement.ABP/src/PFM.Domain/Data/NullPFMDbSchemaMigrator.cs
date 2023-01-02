using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace PFM.Data;

/* This is used if database provider does't define
 * IPFMDbSchemaMigrator implementation.
 */
public class NullPFMDbSchemaMigrator : IPFMDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
