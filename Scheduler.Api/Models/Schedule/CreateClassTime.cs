using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Models.Schedule;

public class CreateClassTime
{
    public Day Day { get; set; }
    
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}