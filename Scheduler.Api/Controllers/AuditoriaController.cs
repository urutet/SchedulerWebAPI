using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.Schedule;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.Errors;
using Scheduler.Models.Schedule;
using Scheduler.Models.University;
using Scheduler.Repositories.Repositories.Schedule;
using Scheduler.Services.Schedule;

namespace Scheduler.Controllers;

[ApiController]
[Route(("api/[controller]"))]
public class AuditoriaController : ControllerBase
{
    private IAuditoriumService _auditoriumService;
    private IAuditoriumCreator _auditoriumCreator;

    public AuditoriaController(IAuditoriumService auditoriumService, IAuditoriumCreator auditoriumCreator)
    {
        _auditoriumService = auditoriumService;
        _auditoriumCreator = auditoriumCreator;
    }
    
    [HttpGet("auditoria")]
    public async Task<IReadOnlyCollection<Auditorium>> GetAuditoria()
    {
        return await _auditoriumService.GetAuditoria(null);
    }

    [HttpGet("auditoriumTypes")]
    public async Task<IReadOnlyCollection<string>> GetAuditoriumTypes()
    {
        return Enum.GetNames<AuditoriumType>();
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> AddAuditorium([FromBody] CreateAuditorium createAuditorium)
    {
        var auditorium = _auditoriumCreator.CreateFrom(createAuditorium);
        await _auditoriumService.AddAuditorium(auditorium);

        return Ok();
    }
    
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteAuditorium(string id)
    {
        var auditorium = await _auditoriumService.GetAuditoriumById(id);
        if (auditorium is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _auditoriumService.DeleteAuditorium(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditAuditorium(string id, [FromBody] CreateAuditorium createAuditorium)
    {
        var auditorium = _auditoriumCreator.CreateFrom(createAuditorium);
        var editAuditorium = await _auditoriumService.GetAuditoriumById(id);
        if (editAuditorium is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _auditoriumService.UpdateAuditorium(editAuditorium.Id, auditorium);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
}