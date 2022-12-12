using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule.Subject;

public interface ISubjectCreator : ICreator
{
    public DomainModel.Model.Schedule.Subject CreateFrom(CreateSubject createSubject);
}