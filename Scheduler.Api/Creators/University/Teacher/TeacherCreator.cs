using Scheduler.DomainModel.Identity;
using Scheduler.Models.Auth;
using Scheduler.Models.University;

namespace Scheduler.Creators.University.Teacher;

public class TeacherCreator : ITeacherCreator
{
    public TeacherUser CreateFrom(RegisterTeacher teacher)
    {
        return new TeacherUser
        {
            Id = Guid.NewGuid().ToString(),
            Name = teacher.Name,
            UserName = teacher.Email,
            Email = teacher.Email,
            departmentId = teacher.departmentId,
            Photo = teacher.Photo
        };
    }
}