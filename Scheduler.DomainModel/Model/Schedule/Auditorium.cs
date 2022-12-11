using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.Schedule;

public class Auditorium : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public AuditoriumType Type { get; set; }
    
    public IReadOnlyCollection<Subject> Subjects { get; set; }
}