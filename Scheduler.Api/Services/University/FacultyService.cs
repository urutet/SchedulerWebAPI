using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.University;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Repositories.Repositories.University;

namespace Scheduler.Services.University;

public class FacultyService : IFacultyService
{
    private IUnitOfWork _unitOfWork;

    public FacultyService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }
    
    public async Task<IReadOnlyCollection<Faculty>> GetFaculties()
    {
        var facultiesRepository = _unitOfWork.GetRepository<Faculty, FacultyRepository>();

        var baseQuery = facultiesRepository.GetQuery();

        var faculties = await baseQuery.ToListAsync();

        return faculties;
    }

    public async Task<Faculty> GetFaculty(string id)
    {
        var facultyRepository = _unitOfWork.GetRepository<Faculty, FacultyRepository>();

        return await facultyRepository.GetByIdAsync(id);
    }

    public async Task AddFaculty(Faculty faculty)
    {
        var facultyRepository = _unitOfWork.GetRepository<Faculty, FacultyRepository>();
        
        facultyRepository.Add(faculty);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateFaculty(string id, Faculty faculty)
    {
        var facultyRepository = _unitOfWork.GetRepository<Faculty, FacultyRepository>();
        
        var editFaculty = await facultyRepository.GetByIdAsync(id);
        if (editFaculty is null)
        {
            return false;
        }

        editFaculty.Name = faculty.Name;
        editFaculty.Groups = faculty.Groups;

        facultyRepository.Update(editFaculty);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteFaculty(string id)
    {
        var facultyRepository = _unitOfWork.GetRepository<Faculty, FacultyRepository>();

        var faculty = await facultyRepository.GetByIdAsync(id);
        if (faculty is null)
        {
            return false;
        }

        facultyRepository.Delete(faculty);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}