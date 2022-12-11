using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Repositories.Repositories.Schedule;

public class SubjectRepository : Repository<Subject>, ISubjectRepository
{
    public SubjectRepository(DbContext dbContext) : base(dbContext)
    {
    }
}