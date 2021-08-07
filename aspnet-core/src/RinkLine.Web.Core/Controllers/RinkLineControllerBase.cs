using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace RinkLine.Controllers
{
    public abstract class RinkLineControllerBase: AbpController
    {
        protected RinkLineControllerBase()
        {
            LocalizationSourceName = RinkLineConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
