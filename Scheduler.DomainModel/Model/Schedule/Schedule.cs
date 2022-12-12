using Scheduler.DomainModel.Identity;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.DomainModel.Model.Schedule;

public class Schedule : IHasId
{
    public string Id { get; set; }
    
    public int Week { get; set; }
    public string GroupId { get; set; }

    public Group Group { get; set; }
    
    public List<Subject> Subjects { get; set; }
}