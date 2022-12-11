using Scheduler.DomainModel.Identity;
using Scheduler.Models.University;

namespace Scheduler.Services.User;

public interface ITeacherService
{
    Task<TeacherUser> GetTeacherByIdAsync(string id);

    Task<TeacherUser> GetTeacherByEmailAsync(string email);

    Task<IReadOnlyCollection<TeacherUser>> GetAllTeachers();

    Task<string> AddTeacherAsync(string email, string password);
    Task<string> AddTeacherAsync(TeacherUser teacherUser, string password);

    Task<bool> DeleteTeacherAsync(string id);

    Task<bool> UpdateTeacher(string id, EditTeacher teacherUser);
}