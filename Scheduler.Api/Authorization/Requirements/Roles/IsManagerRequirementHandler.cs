﻿using Microsoft.AspNetCore.Identity;

namespace Scheduler.Authorization.Requirements.Roles;

public class IsManagerRequirementHandler : IsInRoleAuthorizationHandler<IsManagerRequirement>
{
    public IsManagerRequirementHandler(UserManager<IdentityUser> userManager)
        : base(userManager, Repositories.Constants.Roles.Manager)
    { }
}