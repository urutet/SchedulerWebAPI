using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.Schedule;

public class ClassTime : IHasId
{
    public string Id { get; set; }
    
    public Day Day { get; set; }
    public TimeOnly StartTime { get; set; }
    
    public TimeOnly EndTime { get; set; }
}