using Microsoft.AspNetCore.Identity;

namespace Scheduler.Authorization.Requirements.Roles;

public class IsStudentRequirementHandler : IsInRoleAuthorizationHandler<IsStudentRequirement>
{
    public IsStudentRequirementHandler(UserManager<IdentityUser> userManager)
        : base(userManager, Repositories.Constants.Roles.Student)
    { }
}