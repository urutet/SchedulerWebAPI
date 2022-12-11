using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.University;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Repositories.Repositories.University;

namespace Scheduler.Services.University;

public class DepartmentService : IDepartmentService
{
    private IUnitOfWork _unitOfWork;

    public DepartmentService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }

    public async Task<IReadOnlyCollection<Department>> GetDepartments()
    {
        var departmentsRepository = _unitOfWork.GetRepository<Department, DepartmentRepository>();

        var baseQuery = departmentsRepository.GetQuery();

        var departments = await baseQuery.ToListAsync();

        return departments;
    }

    public async Task<Department> GetDepartmentById(string id)
    {
        var departmentRepository = _unitOfWork.GetRepository<Department, DepartmentRepository>();

        return  await departmentRepository.GetByIdAsync(id);
    }

    public async Task AddDepartment(Department department)
    {
        var departmentsRepository = _unitOfWork.GetRepository<Department, DepartmentRepository>();
        
        departmentsRepository.Add(department);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateDepartment(string id, Department department)
    {
        var departmentsRepository = _unitOfWork.GetRepository<Department, DepartmentRepository>();
        
        var editDepartment = await departmentsRepository.GetByIdAsync(id);
        if (editDepartment is null)
        {
            return false;
        }

        editDepartment.Name = department.Name;
        editDepartment.Teachers = department.Teachers;

        departmentsRepository.Update(editDepartment);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteDepartment(string id)
    {
        var departmentRepository = _unitOfWork.GetRepository<Department, DepartmentRepository>();

        var department = await departmentRepository.GetByIdAsync(id);
        if (department is null)
        {
            return false;
        }

        departmentRepository.Delete(department);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}