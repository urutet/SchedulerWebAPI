using Scheduler.Models.University;

namespace Scheduler.Creators.University.Faculty;

public interface IFacultyCreator : ICreator
{
    DomainModel.Model.University.Faculty CreateFrom(CreateFaculty createFaculty);
}