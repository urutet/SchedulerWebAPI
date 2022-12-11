using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.Repositories.Repositories.University;

public class GroupRepository : Repository<Group>, IGroupRepository
{
    public GroupRepository(DbContext dbContext) : base(dbContext)
    {
    }
}