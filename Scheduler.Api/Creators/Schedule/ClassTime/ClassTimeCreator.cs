using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule.ClassTime;

public class ClassTimeCreator : IClassTimeCreator
{
    public DomainModel.Model.Schedule.ClassTime CreateFrom(CreateClassTime createClassTime)
    {
        return new DomainModel.Model.Schedule.ClassTime
        {
            Id = Guid.NewGuid().ToString(),
            Day = createClassTime.Day,
            StartTime = createClassTime.StartTime.LocalDateTime,
            EndTime = createClassTime.EndTime.LocalDateTime
        };
    }
}