using Abp.Authorization;
using Project1.Authorization.Roles;
using Project1.Authorization.Users;

namespace Project1.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
