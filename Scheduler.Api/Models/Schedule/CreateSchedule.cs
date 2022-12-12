using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Models.Schedule;

public class CreateSchedule
{
    public int Week { get; set; }
    
    public string GroupId { get; set; }
    
    public IReadOnlyCollection<string> SubjectIds { get; set; }
}