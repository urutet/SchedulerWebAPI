using Scheduler.Models.University;

namespace Scheduler.Creators.University.Department;

public interface IDepartmentCreator : ICreator
{
    public DomainModel.Model.University.Department CreateFrom(CreateDepartment createDepartment);
}