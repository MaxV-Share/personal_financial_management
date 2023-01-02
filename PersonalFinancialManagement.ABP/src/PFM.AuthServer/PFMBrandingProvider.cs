using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace PFM;

[Dependency(ReplaceServices = true)]
public class PFMBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "PFM";
}
