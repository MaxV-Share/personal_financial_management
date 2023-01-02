using Volo.Abp.Modularity;

namespace PFM;

[DependsOn(
    typeof(PFMApplicationModule),
    typeof(PFMDomainTestModule)
    )]
public class PFMApplicationTestModule : AbpModule
{

}
