using Scheduler.DomainModel.Identity;

namespace Scheduler.Services.User;

public interface IStudentService
{
    Task<StudentUser> GetStudentByIdAsync(string id);

    Task<StudentUser> GetStudentByEmailAsync(string email);

    Task<string> AddStudentAsync( string email, string password);
}