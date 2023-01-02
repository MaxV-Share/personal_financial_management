using PFM.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace PFM.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(PFMEntityFrameworkCoreModule),
    typeof(PFMApplicationContractsModule)
    )]
public class PFMDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
