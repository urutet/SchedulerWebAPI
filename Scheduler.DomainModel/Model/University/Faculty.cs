using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.University;

public class Faculty : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public IReadOnlyCollection<Group> Groups { get; set; }
}