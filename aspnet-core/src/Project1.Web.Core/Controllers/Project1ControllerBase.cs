using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Project1.Controllers
{
    public abstract class Project1ControllerBase: AbpController
    {
        protected Project1ControllerBase()
        {
            LocalizationSourceName = Project1Consts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
