using Volo.Abp.Settings;

namespace PFM.Settings;

public class PFMSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(PFMSettings.MySetting1));
    }
}
