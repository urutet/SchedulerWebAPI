using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Services.Schedule;

public interface IScheduleService
{
    Task<IReadOnlyCollection<DomainModel.Model.Schedule.Schedule>> GetSchedules(string? groupId);
    
    Task<DomainModel.Model.Schedule.Schedule> GetSchedule(string scheduleId);

    Task AddSchedule(DomainModel.Model.Schedule.Schedule schedule, List<string> SubjectIds);

    Task<bool> UpdateSchedule(string id, DomainModel.Model.Schedule.Schedule schedule);

    Task<bool> DeleteSchedule(string id);
}