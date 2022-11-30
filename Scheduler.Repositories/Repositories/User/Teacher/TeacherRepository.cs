using Scheduler.DomainModel.Identity;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.Repositories.User.Teacher;

public class TeacherRepository : UserRepository<TeacherUser>, ITeacherRepository
{
    public TeacherRepository(SchedulerDbContext dbContext) : base(dbContext)
    {
    }
}