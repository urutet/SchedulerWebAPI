using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Services.Schedule;

public interface IAuditoriumService
{
        Task<IReadOnlyCollection<Auditorium>> GetAuditoria(AuditoriumType? type);
        
        Task<Auditorium> GetAuditoriumById(string id);

        Task AddAuditorium(Auditorium auditorium);

        Task<bool> UpdateAuditorium(string id, Auditorium auditorium);

        Task<bool> DeleteAuditorium(string id);
    
}