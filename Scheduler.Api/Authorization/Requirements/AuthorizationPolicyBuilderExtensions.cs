using Microsoft.AspNetCore.Authorization;
using Scheduler.Authorization.Requirements.Roles;

namespace Scheduler.Authorization.Requirements;

public static class AuthorizationPolicyBuilderExtensions
{
    public static AuthorizationPolicyBuilder IsManager(this AuthorizationPolicyBuilder authorizationPolicyBuilder)
    {
        return authorizationPolicyBuilder.RequireAuthenticatedUser().AddRequirements(new IsManagerRequirement());
    }

    public static AuthorizationPolicyBuilder IsStudent(this AuthorizationPolicyBuilder authorizationPolicyBuilder)
    {
        return authorizationPolicyBuilder.RequireAuthenticatedUser().AddRequirements(new IsStudentRequirement());
    }

    public static AuthorizationPolicyBuilder IsTeacher(this AuthorizationPolicyBuilder authorizationPolicyBuilder)
    {
        return authorizationPolicyBuilder.RequireAuthenticatedUser().AddRequirements(new IsTeacherRequirement());
    }
}