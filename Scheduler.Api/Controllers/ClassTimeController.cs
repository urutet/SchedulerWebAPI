using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.Schedule.ClassTime;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.Errors;
using Scheduler.Models.Schedule;
using Scheduler.Models.University;
using Scheduler.Repositories.Database;
using Scheduler.Repositories.Repositories.Schedule;
using Scheduler.Services.Schedule;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassTimeController : ControllerBase
{
    private IClassTimeService _classTimeService;
    private IClassTimeCreator _classTimeCreator;
    
    public ClassTimeController(IClassTimeService classTimeService, IClassTimeCreator classTimeCreator)
    {
        _classTimeService = classTimeService;
        _classTimeCreator = classTimeCreator;
    }
    
    [HttpGet("classtimes")]
    public async Task<IReadOnlyCollection<ClassTime>> GetClassTimes()
    {
        var classTimes = await _classTimeService.GetClassTimes();

        return classTimes;
    }

    [HttpGet("days")]
    public async Task<IReadOnlyCollection<string>> getDays()
    {
        return Enum.GetNames<Day>();
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> CreateClassTime([FromBody] CreateClassTime createClassTime)
    {
        var classTime = _classTimeCreator.CreateFrom(createClassTime);
        await _classTimeService.AddClassTime(classTime);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteClassTime(string id)
    {
        var classTime = await _classTimeService.GetClassTimeById(id);
        if (classTime is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _classTimeService.DeleteClassTime(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditClassTime(string id, [FromBody] CreateClassTime createClassTime)
    {
        var classTime = _classTimeCreator.CreateFrom(createClassTime);
        var editClassTime = await _classTimeService.GetClassTimeById(id);
        if (editClassTime is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _classTimeService.UpdateClassTime(editClassTime.Id, classTime);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }

}