using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.Repositories.User;

public class UserRepository<T> : Repository<T>, IUserRepository<T> where T : IdentityUser
{
    public UserRepository(SchedulerDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<T> GetByEmailAsync(string email)
    {
        var user = await GetQuery().Where(e => e.Email == email).FirstOrDefaultAsync();

        return user;
    }
}