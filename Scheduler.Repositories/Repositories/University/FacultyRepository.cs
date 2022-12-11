using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.Repositories.Repositories.University;

public class FacultyRepository : Repository<Faculty>, IFacultyRepository
{
    public FacultyRepository(DbContext dbContext) : base(dbContext)
    {
    }
}