using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.Schedule;

public class ClassTime : IHasId
{
    public string Id { get; set; }
    
    public Day Day { get; set; }
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
}