using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace PFM;

[Dependency(ReplaceServices = true)]
public class PFMBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "PFM";
}
