using Scheduler.DomainModel.Identity;
namespace Scheduler.DomainModel.Model.University;

public class Group : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public int Year { get; set; }
    
    public Schedule.Schedule Schedule { get; set; }
}