using Scheduler.DomainModel.Identity;
namespace Scheduler.DomainModel.Model.University;

public class Group : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public int Year { get; set; }
    
    public string facultyId { get; set; }
    public IReadOnlyCollection<Schedule.Schedule> Schedules { get; set; }
}