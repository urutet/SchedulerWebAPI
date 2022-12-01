using Scheduler.DomainModel.Identity;

namespace Scheduler.Services.User;

public interface ITeacherService
{
    Task<TeacherUser> GetTeacherByIdAsync(string id);

    Task<TeacherUser> GetTeacherByEmailAsync(string email);

    Task<string> AddTeacherAsync(string email, string password);

    Task<bool> DeleteTeacherAsync(string id);
}