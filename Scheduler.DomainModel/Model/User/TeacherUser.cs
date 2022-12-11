using Microsoft.AspNetCore.Identity;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.DomainModel.Identity;

public class TeacherUser : IdentityUser, IHasId
{
    public string Name { get; set; }
    
    public string Photo { get; set; }
    
    public Schedule Schedule { get; set; }
    public string departmentId { get; set; }
}