using PFM.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PFM.Permissions;

public class PFMPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PFMPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(PFMPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PFMResource>(name);
    }
}
