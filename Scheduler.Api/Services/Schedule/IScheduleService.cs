using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Services.Schedule;

public interface IScheduleService
{
    Task<IReadOnlyCollection<DomainModel.Model.Schedule.Schedule>> GetSchedules(string? groupId);

    Task AddSchedule(DomainModel.Model.Schedule.Schedule schedule);

    Task<bool> UpdateSchedule(string id, DomainModel.Model.Schedule.Schedule schedule);

    Task<bool> DeleteSchedule(string id);
}