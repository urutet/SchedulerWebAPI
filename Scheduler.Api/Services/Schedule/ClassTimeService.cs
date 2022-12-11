using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Repositories.Repositories.Schedule;
using Scheduler.Repositories.Repositories.UnitOfWork;

namespace Scheduler.Services.Schedule;

public class ClassTimeService : IClassTimeService
{
    private IUnitOfWork _unitOfWork;

    public ClassTimeService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }
    
    public async Task<IReadOnlyCollection<ClassTime>> GetClassTimes()
    {
        var classTimeRepository = _unitOfWork.GetRepository<ClassTime, ClassTimeRepository>();

        var baseQuery = classTimeRepository.GetQuery();

        var classTimes = await baseQuery.ToListAsync();

        return classTimes;
    }

    public async Task<ClassTime> GetClassTimeById(string id)
    {
        var classTimeRepository = _unitOfWork.GetRepository<ClassTime, ClassTimeRepository>();

        return await classTimeRepository.GetByIdAsync(id);
    }

    public async Task AddClassTime(ClassTime classTime)
    {
        var classTimeRepository = _unitOfWork.GetRepository<ClassTime, ClassTimeRepository>();
        
        classTimeRepository.Add(classTime);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateClassTime(string id, ClassTime classTime)
    {
        var classTimeRepository = _unitOfWork.GetRepository<ClassTime, ClassTimeRepository>();

        var editClassTime = await classTimeRepository.GetByIdAsync(id);
        if (editClassTime is null)
        {
            return false;
        }

        editClassTime.StartTime = classTime.StartTime;
        editClassTime.EndTime = classTime.EndTime;
        
        classTimeRepository.Update(editClassTime);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteClassTime(string id)
    {
        var classTimeRepository = _unitOfWork.GetRepository<ClassTime, ClassTimeRepository>();

        var classTime = await classTimeRepository.GetByIdAsync(id);
        if (classTime is null)
        {
            return false;
        }

        classTimeRepository.Delete(classTime);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}