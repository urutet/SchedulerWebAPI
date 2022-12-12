using Scheduler.DomainModel.Identity;
using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Models.Schedule;

public class CreateSubject
{
    public string Name { get; set; }
    
    public string ClassTimeId { get; set; }
    
    public string AuditoriumId { get; set; }
    
    public string TeacherId { get; set; }
}