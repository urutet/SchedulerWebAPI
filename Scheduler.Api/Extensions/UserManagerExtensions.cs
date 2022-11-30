using Microsoft.AspNetCore.Identity;

namespace Scheduler.Extensions;

public static class UserManagerExtensions
{
    public static async Task<string> GetUserRoleByUserId(this UserManager<IdentityUser> manager, string id)
    {
        var user = await manager.FindByIdAsync(id);
        var roles = await manager.GetRolesAsync(user);

        return roles.FirstOrDefault();
    }
}