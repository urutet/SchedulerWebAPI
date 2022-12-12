namespace Scheduler.Models.Schedule;

public class ScheduleModel
{
    public string Id { get; set; }
    
    public int Week { get; set; }
    
    public string GroupId { get; set; }
    
    public List<SubjectModel> Subjects { get; set; }
}