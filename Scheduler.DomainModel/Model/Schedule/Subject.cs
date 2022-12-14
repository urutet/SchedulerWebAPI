using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.Schedule;

public class Subject : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public string classTimeId { get; set; }
    
    public string auditoriumId { get; set; }
    
    public string teacherId { get; set; }
    
    public IReadOnlyCollection<Schedule> Schedules { get; set; }
}