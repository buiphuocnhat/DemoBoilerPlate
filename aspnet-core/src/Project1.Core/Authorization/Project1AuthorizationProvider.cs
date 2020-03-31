using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Project1.Authorization
{
    public class Project1AuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.Pages_Users, L("Users"));
            context.CreatePermission(PermissionNames.Pages_Roles, L("Roles"));
            context.CreatePermission(PermissionNames.Pages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.Pages_Cars, L("Cars"));
            context.CreatePermission(PermissionNames.Pages_Cars_Create, L("CreateCar"));
            context.CreatePermission(PermissionNames.Pages_Cars_Update, L("UpdateCar"));
            context.CreatePermission(PermissionNames.Pages_Cars_Delete, L("DeleteCar"));

            context.CreatePermission(PermissionNames.Pages_Companies, L("Companies"));
            context.CreatePermission(PermissionNames.Pages_Companies_Create, L("CreateCompany"));
            context.CreatePermission(PermissionNames.Pages_Companies_Update, L("UpdateCompany"));
            context.CreatePermission(PermissionNames.Pages_Companies_Delete, L("DeleteCompany"));

        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, Project1Consts.LocalizationSourceName);
        }
    }
}
