using Microsoft.AspNetCore.Identity;

namespace Scheduler.Authorization.Requirements.Roles;

public class IsTeacherRequirementHandler : IsInRoleAuthorizationHandler<IsTeacherRequirement>
{
    public IsTeacherRequirementHandler(UserManager<IdentityUser> userManager)
        : base(userManager, Repositories.Constants.Roles.Teacher)
    { }
}