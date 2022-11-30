﻿using Microsoft.AspNetCore.Identity;
using Scheduler.Extensions;
using Scheduler.JWT;
using Scheduler.Models.Auth;

namespace Scheduler.Services.Auth;

public class AuthService : IAuthService
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AuthService(IJwtGenerator jwtGenerator, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _jwtGenerator = jwtGenerator;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AuthData> GetAuthDataAsync(string id)
    {
        var role = await _userManager.GetUserRoleByUserId(id);
        var tokenDescriptor = _jwtGenerator.GenerateToken(id, role);

        return new AuthData
        {
            Id = id,
            Token = tokenDescriptor.Token,
            TokenExpirationTime = ((DateTimeOffset)tokenDescriptor.ExpirationDate).ToUnixTimeMilliseconds(),
            Role = role
        };
    }
}