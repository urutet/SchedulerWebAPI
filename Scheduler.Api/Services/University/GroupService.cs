using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.University;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Repositories.Repositories.University;

namespace Scheduler.Services.University;

public class GroupService : IGroupService
{
    private IUnitOfWork _unitOfWork;

    public GroupService(IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory)
    {
        _unitOfWork = unitOfWorkFactory.Create();
    }
    
    public async Task<IReadOnlyCollection<Group>> GetGroups()
    {
        var groupsRepository = _unitOfWork.GetRepository<Group, GroupRepository>();

        var baseQuery = groupsRepository.GetQuery();

        var groups = await baseQuery.ToListAsync();

        return groups;
    }

    public async Task<Group> GetGroup(string id)
    {
        var groupsRepository = _unitOfWork.GetRepository<Group, GroupRepository>();

        return await groupsRepository.GetByIdAsync(id);
    }

    public async Task AddGroup(Group group)
    {
        var groupsRepository = _unitOfWork.GetRepository<Group, GroupRepository>();
        
        groupsRepository.Add(group);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<bool> UpdateGroup(string id, Group group)
    {
        var groupsRepository = _unitOfWork.GetRepository<Group, GroupRepository>();
        
        var editGroup = await groupsRepository.GetByIdAsync(id);
        if (editGroup is null)
        {
            return false;
        }

        editGroup.Name = group.Name;
        editGroup.Schedules = group.Schedules;
        editGroup.Year = group.Year;
        editGroup.facultyId = group.facultyId;

        groupsRepository.Update(editGroup);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteGroup(string id)
    {
        var groupsRepository = _unitOfWork.GetRepository<Group, GroupRepository>();

        var group = await groupsRepository.GetByIdAsync(id);
        if (group is null)
        {
            return false;
        }

        groupsRepository.Delete(group);

        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}