using System.IdentityModel.Tokens.Jwt;

namespace Scheduler.JWT;

public interface IJwtGenerator
{
    JWTTokenDescriptor GenerateToken(string userId, string role);
}