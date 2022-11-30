using Scheduler.DomainModel.Identity;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.Repositories.User.Manager;

public class ManagerRepository : UserRepository<ManagerUser>, IManagerRepository
{
    public ManagerRepository(SchedulerDbContext dbContext) : base(dbContext)
    {
    }
}