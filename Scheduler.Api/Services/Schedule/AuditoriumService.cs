using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Repositories.Database;
using Scheduler.Repositories.Repositories.Schedule;
using Scheduler.Repositories.Repositories.UnitOfWork;

namespace Scheduler.Services.Schedule;

public class AuditoriumService : IAuditoriumService
{
    private UnitOfWork _unitOfWork;

    public AuditoriumService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }

    public async Task<IReadOnlyCollection<Auditorium>> GetAuditoria(AuditoriumType? type)
    {
        var auditoriumRepository = _unitOfWork.GetRepository<Auditorium, AuditoriumRepository>();

        var baseQuery = auditoriumRepository.GetQuery();

        if (type is not null)
            baseQuery = baseQuery.Where(a => a.Type == type);


        var auditoriaOfType = await baseQuery.ToListAsync();

        return auditoriaOfType;
    }

    public async Task<Auditorium> GetAuditoriumById(string id)
    {
        var auditoriumRepository = _unitOfWork.GetRepository<Auditorium, AuditoriumRepository>();

        var auditorium = await auditoriumRepository.GetByIdAsync(id);

        return auditorium;
    }

    public async Task AddAuditorium(Auditorium auditorium)
    {
        var auditoriumRepository = _unitOfWork.GetRepository<Auditorium, AuditoriumRepository>();
        
        auditoriumRepository.Add(auditorium);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateAuditorium(string id, Auditorium auditorium)
    {
        var auditoriumRepository = _unitOfWork.GetRepository<Auditorium, AuditoriumRepository>();

        var editAuditorium = await auditoriumRepository.GetByIdAsync(id);
        if (editAuditorium is null)
        {
            return false;
        }

        editAuditorium.Name = auditorium.Name;
        editAuditorium.Subjects = auditorium.Subjects;
        editAuditorium.Type = auditorium.Type;

        auditoriumRepository.Update(editAuditorium);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAuditorium(string id)
    {
        var auditoriumRepository = _unitOfWork.GetRepository<Auditorium, AuditoriumRepository>();

        var auditorium = await auditoriumRepository.GetByIdAsync(id);
        if (auditorium is null)
        {
            return false;
        }

        auditoriumRepository.Delete(auditorium);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}