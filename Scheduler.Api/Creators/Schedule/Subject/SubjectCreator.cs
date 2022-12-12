using Scheduler.Models.Schedule;
using Scheduler.Services.Schedule;
using Scheduler.Services.User;

namespace Scheduler.Creators.Schedule.Subject;

public class SubjectCreator : ISubjectCreator
{

    public DomainModel.Model.Schedule.Subject CreateFrom(CreateSubject createSubject)
    {

        return new DomainModel.Model.Schedule.Subject
        {
            Id = Guid.NewGuid().ToString(),
            Name = createSubject.Name,
            auditoriumId = createSubject.AuditoriumId,
            classTimeId = createSubject.ClassTimeId,
            teacherId = createSubject.TeacherId
        };
    }
}