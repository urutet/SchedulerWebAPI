using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule.Schedule;

public interface IScheduleCreator : ICreator
{
    public Task<DomainModel.Model.Schedule.Schedule> CreateFrom(CreateSchedule createSchedule);
    
    public SubjectModel CreateFrom(DomainModel.Model.Schedule.Subject subject);

    public ScheduleModel CreateFrom(DomainModel.Model.Schedule.Schedule schedule);
}