using Scheduler.Models.University;

namespace Scheduler.Creators.University.Group;

public class GroupCreator : IGroupCreator
{
    public DomainModel.Model.University.Group CreateFrom(CreateGroup createGroup)
    {
        return new DomainModel.Model.University.Group
        {
            Id = Guid.NewGuid().ToString(),
            Name = createGroup.Name,
            Year = createGroup.Year,
            facultyId = createGroup.facultyId
        };
    }
}