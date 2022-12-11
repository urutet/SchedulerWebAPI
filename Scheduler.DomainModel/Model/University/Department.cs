using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.University;

public class Department : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public IReadOnlyCollection<TeacherUser> Teachers { get; set; }
}