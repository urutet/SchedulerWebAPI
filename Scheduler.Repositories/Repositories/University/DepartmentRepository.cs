using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.Repositories.Repositories.University;

public class DepartmentRepository : Repository<Department>, IDepartmentRepository
{
    public DepartmentRepository(DbContext dbContext) : base(dbContext)
    {
    }
}