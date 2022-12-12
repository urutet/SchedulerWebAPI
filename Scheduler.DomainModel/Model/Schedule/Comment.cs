using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.Schedule;

public class Comment : IHasId
{
    public string Id { get; set; }
    
    public string teacherId { get; set; }
    
    public string comment { get; set; }
}