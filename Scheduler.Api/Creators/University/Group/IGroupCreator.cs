using Scheduler.Models.University;

namespace Scheduler.Creators.University.Group;

public interface IGroupCreator : ICreator
{
    DomainModel.Model.University.Group CreateFrom(CreateGroup createGroup);

}