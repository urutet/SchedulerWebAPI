using Microsoft.EntityFrameworkCore;

namespace Scheduler.Repositories.Repositories.Schedule;

public class ScheduleRepository : Repository<DomainModel.Model.Schedule.Schedule>, IScheduleRepository
{
    public ScheduleRepository(DbContext dbContext) : base(dbContext)
    {
    }
}