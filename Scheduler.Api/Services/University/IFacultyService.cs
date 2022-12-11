using Scheduler.DomainModel.Model.University;

namespace Scheduler.Services.University;

public interface IFacultyService
{
    Task<IReadOnlyCollection<Faculty>> GetFaculties();
    Task<Faculty> GetFaculty(string id);

    Task AddFaculty(Faculty faculty);

    Task<bool> UpdateFaculty(string id, Faculty faculty);

    Task<bool> DeleteFaculty(string id);
}