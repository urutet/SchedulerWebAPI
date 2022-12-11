using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Services.Schedule;

public interface ISubjectService
{
    Task<IReadOnlyCollection<Subject>> GetSubject();

    Task AddSubject(Subject subject);

    Task<bool> UpdateSubject(string id, Subject subject);

    Task<bool> DeleteSubject(string id);
}