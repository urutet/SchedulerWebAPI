using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Repositories.Repositories.Schedule;
using Scheduler.Repositories.Repositories.UnitOfWork;

namespace Scheduler.Services.Schedule;

public class SubjectService : ISubjectService
{
    private IUnitOfWork _unitOfWork;

    public SubjectService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }
    
    public async Task<IReadOnlyCollection<Subject>> GetSubject()
    {
        var subjectsRepository = _unitOfWork.GetRepository<Subject, SubjectRepository>();

        var baseQuery = subjectsRepository.GetQuery();

        var schedules = await baseQuery.ToListAsync();

        return schedules;
    }

    public async Task<Subject> GetSubjectById(string id)
    {
        var subjectsRepository = _unitOfWork.GetRepository<Subject, SubjectRepository>();

        return await subjectsRepository.GetByIdAsync(id);
    }

    public async Task AddSubject(Subject subject)
    {
        var subjectsRepository = _unitOfWork.GetRepository<Subject, SubjectRepository>();
        
        subjectsRepository.Add(subject);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateSubject(string id, Subject subject)
    {
        var subjectsRepository = _unitOfWork.GetRepository<Subject, SubjectRepository>();
        
        var editSubject = await subjectsRepository.GetByIdAsync(id);
        if (editSubject is null)
        {
            return false;
        }

        editSubject.auditoriumId = subject.auditoriumId;
        editSubject.Name = subject.Name;
        editSubject.teacherId = subject.teacherId;
        editSubject.classTimeId = subject.classTimeId;

        subjectsRepository.Update(editSubject);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteSubject(string id)
    {
        var subjectRepository = _unitOfWork.GetRepository<Subject, SubjectRepository>();

        var subject = await subjectRepository.GetByIdAsync(id);
        if (subject is null)
        {
            return false;
        }

        subjectRepository.Delete(subject);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}