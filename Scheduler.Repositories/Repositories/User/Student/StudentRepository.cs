using Scheduler.DomainModel.Identity;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.Repositories.User.Student;

public class StudentRepository: UserRepository<StudentUser>, IStudentRepository
{
    public StudentRepository(SchedulerDbContext dbContext) : base(dbContext)
    {
    }
}