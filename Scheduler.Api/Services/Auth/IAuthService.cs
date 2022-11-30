using Scheduler.Models.Auth;

namespace Scheduler.Services.Auth;

public interface IAuthService
{
    public Task<AuthData> GetAuthDataAsync(string id);
}