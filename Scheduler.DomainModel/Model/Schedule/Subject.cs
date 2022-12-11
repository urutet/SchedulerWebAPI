using Scheduler.DomainModel.Identity;

namespace Scheduler.DomainModel.Model.Schedule;

public class Subject : IHasId
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    public int Week { get; set; }
    
    public ClassTime ClassTime { get; set; }
    
    public Auditorium Auditorium { get; set; }
    
    public TeacherUser Teacher { get; set; }
}