using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Models.Schedule;

public class CreateClassTime
{
    public Day Day { get; set; }
    
    public DateTimeOffset StartTime { get; set; }
    
    public DateTimeOffset EndTime { get; set; }
}