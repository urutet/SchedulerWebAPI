using Scheduler.DomainModel.Model.University;

namespace Scheduler.Services.University;

public interface IDepartmentService
{
    Task<IReadOnlyCollection<Department>> GetDepartments();

    Task<Department> GetDepartmentById(string id);

    Task AddDepartment(Department department);

    Task<bool> UpdateDepartment(string id, Department department);

    Task<bool> DeleteDepartment(string id);
}