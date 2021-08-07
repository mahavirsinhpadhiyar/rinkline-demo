using Abp.Authorization;
using RinkLine.Authorization.Roles;
using RinkLine.Authorization.Users;

namespace RinkLine.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
