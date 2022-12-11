using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.University.Group;
using Scheduler.DomainModel.Model.University;
using Scheduler.Models.Errors;
using Scheduler.Models.University;
using Scheduler.Services.University;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController : ControllerBase
{
    private IGroupService _groupService;
    private IGroupCreator _groupCreator;
    
    public GroupsController(IGroupService groupService, IGroupCreator groupCreator)
    {
        _groupService = groupService;
        _groupCreator = groupCreator;
    }
    
    [HttpGet("groups")]
    public async Task<IReadOnlyCollection<Group>> GetGroups()
    {
        var groups = await _groupService.GetGroups();
        
        return groups;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> AddGroup([FromBody] CreateGroup createGroup)
    {
        var group = _groupCreator.CreateFrom(createGroup);
        await _groupService.AddGroup(group);

        return Ok();
    }
    
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteGroup(string id)
    {
        var department = await _groupService.GetGroup(id);
        if (department is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _groupService.DeleteGroup(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditGroup(string id, [FromBody] CreateGroup createGroup)
    {
        var group = _groupCreator.CreateFrom(createGroup);
        var editGroup = await _groupService.GetGroup(id);
        if (editGroup is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _groupService.UpdateGroup(editGroup.Id, group);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
}