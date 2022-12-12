using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.Schedule.Schedule;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.Errors;
using Scheduler.Models.Schedule;
using Scheduler.Services.Schedule;
using System.Linq;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchedulesController : ControllerBase
{
    private IScheduleService _scheduleService;
    private IScheduleCreator _scheduleCreator;
    
    public SchedulesController(IScheduleService scheduleService, IScheduleCreator scheduleCreator)
    {
        _scheduleService = scheduleService;
        _scheduleCreator = scheduleCreator;
    }
    
    [HttpGet("schedules")]
    public async Task<IReadOnlyCollection<ScheduleModel>> GetSchedules()
    {
        var schedules = await _scheduleService.GetSchedules(null);
        var schedulesModels = schedules.Select(_scheduleCreator.CreateFrom).ToList();

        return schedulesModels;
    }
    
    [HttpGet("schedules/{id}")]
    public async Task<IReadOnlyCollection<ScheduleModel>> GetSchedules(string id)
    {
        var schedules = await _scheduleService.GetSchedules(id);
        var schedulesModel = schedules.Select(_scheduleCreator.CreateFrom).ToList();

        return schedulesModel;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> CreateSchedule([FromBody] CreateSchedule createSchedule)
    {
        var schedule = await _scheduleCreator.CreateFrom(createSchedule);
        await _scheduleService.AddSchedule(schedule, createSchedule.SubjectIds.ToList());

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteSchedule(string id)
    {
        var subject = await _scheduleService.GetSchedule(id);
        if (subject is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _scheduleService.DeleteSchedule(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditSchedule(string id, [FromBody] CreateSchedule createSchedule)
    {
        var schedule = await _scheduleCreator.CreateFrom(createSchedule);
        var editSchedule = await _scheduleService.GetSchedule(id);
        if (editSchedule is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _scheduleService.UpdateSchedule(editSchedule.Id, schedule);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
}