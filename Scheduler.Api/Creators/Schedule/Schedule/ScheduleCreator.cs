using Scheduler.Models.Schedule;
using Scheduler.Services.Schedule;

namespace Scheduler.Creators.Schedule.Schedule;

public class ScheduleCreator : IScheduleCreator
{
    private ISubjectService _subjectService;

    public ScheduleCreator(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }
    
    public async Task<DomainModel.Model.Schedule.Schedule> CreateFrom(CreateSchedule createSchedule)
    {
        return  new DomainModel.Model.Schedule.Schedule
        {
            Id = Guid.NewGuid().ToString(),
            Week = createSchedule.Week,
            GroupId = createSchedule.GroupId,
            Subjects = new List<DomainModel.Model.Schedule.Subject>()
        };
    }
    
    public SubjectModel CreateFrom(DomainModel.Model.Schedule.Subject subject)
    {
        return new SubjectModel
        {
            Id = subject.Id,
            Name = subject.Name,
            auditoriumId = subject.auditoriumId,
            teacherId = subject.teacherId,
            classTimeId = subject.classTimeId,
        };
    }
    
    public ScheduleModel CreateFrom(DomainModel.Model.Schedule.Schedule schedule)
    {
        return new ScheduleModel
        {
            Id = schedule.Id,
            Week = schedule.Week,
            GroupId = schedule.GroupId,
            Subjects = schedule.Subjects.Select(CreateFrom).ToList(),
        };
    }
}