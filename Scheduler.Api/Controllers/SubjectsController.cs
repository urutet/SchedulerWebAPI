using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.Schedule.Subject;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.Errors;
using Scheduler.Models.Schedule;
using Scheduler.Services.Schedule;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    
    private ISubjectService _subjectService;
    private ISubjectCreator _subjectCreator;
    
    public SubjectsController(ISubjectService subjectService, ISubjectCreator subjectCreator)
    {
        _subjectService = subjectService;
        _subjectCreator = subjectCreator;
    }
    
    [HttpGet("subjects")]
    public async Task<IReadOnlyCollection<Subject>> GetSubjects()
    {
        var subjects = await _subjectService.GetSubject();

        return subjects;
    }

    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubject createSubject)
    {
        var subject = _subjectCreator.CreateFrom(createSubject);
        await _subjectService.AddSubject(subject);

        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> DeleteSubject(string id)
    {
        var subject = await _subjectService.GetSubjectById(id);
        if (subject is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isDeleteSuccessful = await _subjectService.DeleteSubject(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditSubject(string id, [FromBody] CreateSubject createSubject)
    {
        var subject = _subjectCreator.CreateFrom(createSubject);
        var editSubject = await _subjectService.GetSubjectById(id);
        if (editSubject is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _subjectService.UpdateSubject(editSubject.Id, subject);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
}