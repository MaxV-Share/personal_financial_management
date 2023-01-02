using System;
using System.Collections.Generic;
using System.Text;
using PFM.Localization;
using Volo.Abp.Application.Services;

namespace PFM;

/* Inherit your application services from this class.
 */
public abstract class PFMAppService : ApplicationService
{
    protected PFMAppService()
    {
        LocalizationResource = typeof(PFMResource);
    }
}
