using PFM.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PFM.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class PFMController : AbpControllerBase
{
    protected PFMController()
    {
        LocalizationResource = typeof(PFMResource);
    }
}
