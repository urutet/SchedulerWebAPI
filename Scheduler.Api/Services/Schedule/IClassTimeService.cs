using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Services.Schedule;

public interface IClassTimeService
{
    Task<IReadOnlyCollection<ClassTime>> GetClassTimes();

    Task<ClassTime> GetClassTimeById(string id);

    Task AddClassTime(ClassTime classTime);

    Task<bool> UpdateClassTime(string id, ClassTime classTime);

    Task<bool> DeleteClassTime(string id);
}