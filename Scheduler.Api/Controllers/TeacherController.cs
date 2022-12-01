using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Authorization.Requirements;
using Scheduler.Models.Auth;
using Scheduler.Models.Errors;
using Scheduler.Repositories.Constants;
using Scheduler.Services.Auth;
using Scheduler.Services.User;

namespace Scheduler.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TeacherController : ControllerBase
{
    private ITeacherService _teacherService;
    
    public TeacherController(
        ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Policy = Policies.IsManager)]
    public async Task<ActionResult<string>> Register([FromBody] RegisterUser register)
    {
        var user = await _teacherService.GetTeacherByEmailAsync(register.Email);

        if (user is not null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.UserWithTheSameEmailAlreadyExist));
        }

        var teacherId = await _teacherService.AddTeacherAsync(register.Email, register.Password);
        
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
}