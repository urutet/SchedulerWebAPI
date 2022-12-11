using Scheduler.DomainModel.Model.University;

namespace Scheduler.Services.University;

public interface IGroupService
{
    Task<IReadOnlyCollection<Group>> GetGroups();

    Task<Group> GetGroup(string id);

    Task AddGroup(Group group);

    Task<bool> UpdateGroup(string id, Group group);

    Task<bool> DeleteGroup(string id);
}