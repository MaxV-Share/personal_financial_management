using PFM.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PFM;

[DependsOn(
    typeof(PFMEntityFrameworkCoreTestModule)
    )]
public class PFMDomainTestModule : AbpModule
{

}
