using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Models.Schedule;

public class CreateAuditorium
{
    public string Name { get; set; }
    
    public AuditoriumType Type { get; set; }
}