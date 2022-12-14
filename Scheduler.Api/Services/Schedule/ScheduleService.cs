using System.Linq;
using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Repositories.Repositories.Schedule;
using Scheduler.Repositories.Repositories.UnitOfWork;

namespace Scheduler.Services.Schedule;

public class ScheduleService : IScheduleService
{
    private IUnitOfWork _unitOfWork;

    public ScheduleService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }
    
    public async Task<IReadOnlyCollection<DomainModel.Model.Schedule.Schedule>> GetSchedules(string? groupId)
    {
        var scheduleRepository = _unitOfWork.GetRepository<DomainModel.Model.Schedule.Schedule, ScheduleRepository>();

        var baseQuery = scheduleRepository.GetQuery();

        if (groupId is not null)
            baseQuery = baseQuery.Where(s => s.GroupId == groupId);

        var schedules = await baseQuery.Include(s => s.Subjects).ToListAsync();

        return schedules;
    }

    public async Task<DomainModel.Model.Schedule.Schedule> GetSchedule(string scheduleId)
    {
        var scheduleRepository = _unitOfWork.GetRepository<DomainModel.Model.Schedule.Schedule, ScheduleRepository>();

        return await scheduleRepository.GetByIdAsync(scheduleId);
    }

    public async Task AddSchedule(DomainModel.Model.Schedule.Schedule schedule, List<string> SubjectIds)
    {
        var scheduleRepository = _unitOfWork.GetRepository<DomainModel.Model.Schedule.Schedule, ScheduleRepository>();
        
        scheduleRepository.Add(schedule);
        var subjectsRepository = _unitOfWork.GetRepository<Subject, SubjectRepository>();
        var subjects = await subjectsRepository.GetQuery().ToListAsync();
        
        var subjectsToAdd = subjects.Where(s => SubjectIds.Contains(s.Id)).ToList();
        schedule.Subjects.AddRange(subjectsToAdd);
        
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateSchedule(string id, DomainModel.Model.Schedule.Schedule schedule)
    {
        var scheduleRepository = _unitOfWork.GetRepository<DomainModel.Model.Schedule.Schedule, ScheduleRepository>();

        var editSchedule = await scheduleRepository.GetByIdAsync(id);
        if (editSchedule is null)
        {
            return false;
        }

        editSchedule.Week = schedule.Week;
        editSchedule.GroupId = schedule.GroupId;
        editSchedule.Subjects = schedule.Subjects;

        scheduleRepository.Update(editSchedule);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSchedule(string id)
    {
        var scheduleRepository = _unitOfWork.GetRepository<DomainModel.Model.Schedule.Schedule, ScheduleRepository>();

        var schedule = await scheduleRepository.GetByIdAsync(id);
        if (schedule is null)
        {
            return false;
        }

        scheduleRepository.Delete(schedule);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}