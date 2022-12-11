using Scheduler.DomainModel.Identity;
using Scheduler.Models.Auth;
using Scheduler.Models.University;

namespace Scheduler.Creators.University.Teacher;

public interface ITeacherCreator : ICreator
{
    public TeacherUser CreateFrom(RegisterTeacher teacher);

}