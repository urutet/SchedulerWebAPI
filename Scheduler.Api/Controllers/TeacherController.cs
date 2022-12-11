using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Creators.University.Teacher;
using Scheduler.DomainModel.Identity;
using Scheduler.Models.Auth;
using Scheduler.Models.Errors;
using Scheduler.Models.University;
using Scheduler.Repositories.Constants;
using Scheduler.Services.Auth;
using Scheduler.Services.User;

namespace Scheduler.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private ITeacherService _teacherService;
    private ITeacherCreator _teacherCreator;
    
    public TeacherController(
        ITeacherService teacherService,
        ITeacherCreator teacherCreator)
    {
        _teacherService = teacherService;
        _teacherCreator = teacherCreator;
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<ActionResult<string>> Register([FromBody] RegisterTeacher registerTeacher)
    {
        var user = await _teacherService.GetTeacherByEmailAsync(registerTeacher.Email);

        if (user is not null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.UserWithTheSameEmailAlreadyExist));
        }

        var teacher = _teacherCreator.CreateFrom(registerTeacher);

        var teacherId = await _teacherService.AddTeacherAsync(teacher, registerTeacher.Password);
        
        return Ok(teacherId);
    }

    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<ActionResult<string>> FireTeacher(string id)
    {
        var user = await _teacherService.GetTeacherByIdAsync(id);
        if (user is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.UserDoesNotExist));
        }

        var isDeleteSuccessful = await _teacherService.DeleteTeacherAsync(id);
        if (!isDeleteSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
    
    [HttpGet("teachers")]
    public async Task<IReadOnlyCollection<TeacherUser>> GetTeachers()
    {
        var teachers = await _teacherService.GetAllTeachers();

        return teachers;
    }
    
    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<IActionResult> EditTeacher(string id, [FromBody] EditTeacher teacher)
    {
        var editTeacher = await _teacherService.GetTeacherByIdAsync(id);
        if (editTeacher is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.DepartmentDoesNotExist));
        }

        var isEditSuccessful = await _teacherService.UpdateTeacher(editTeacher.Id, teacher);
        if (!isEditSuccessful)
        {
            return BadRequest();
        }

        return Ok();
    }
}