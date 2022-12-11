using Scheduler.Models.University;

namespace Scheduler.Creators.University.Faculty;

public class FacultyCreator : IFacultyCreator
{
    public DomainModel.Model.University.Faculty CreateFrom(CreateFaculty createFaculty)
    {
        return new DomainModel.Model.University.Faculty
        {
            Id = Guid.NewGuid().ToString(),
            Name = createFaculty.Name
        };
    }
}