using Scheduler.Models.University;

namespace Scheduler.Creators.University.Department;

public class DepartmentCreator : IDepartmentCreator
{
    public DomainModel.Model.University.Department CreateFrom(CreateDepartment createDepartment)
    {
        return new DomainModel.Model.University.Department
        {
            Id = Guid.NewGuid().ToString(),
            Name = createDepartment.Name
        };
    }
}