using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Repositories.Repositories.Schedule;

public class ClassTimeRepository : Repository<ClassTime>, IClassTimeRepository
{
    public ClassTimeRepository(DbContext dbContext) : base(dbContext)
    {
    }
}