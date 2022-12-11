using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scheduler.Models.Auth;
using Scheduler.Models.Errors;
using Scheduler.Repositories.Repositories.User.Student;
using Scheduler.Services.Auth;
using Scheduler.Services.User;

namespace Scheduler.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private SignInManager<IdentityUser> _signInManager;
    private IAuthService _authService;
    private IStudentService _studentService;
    private UserManager<IdentityUser> _userManager;

    public AuthController(
        SignInManager<IdentityUser> signInManager,
        IAuthService authService,
        UserManager<IdentityUser> userManager,
        IStudentService studentService)
    {
        _signInManager = signInManager;
        _authService = authService;
        _userManager = userManager;
        _studentService = studentService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthData>> Login([FromBody] LoginUser loginUser)
    {
        var user = await _userManager.FindByEmailAsync(loginUser.Email);
        if (user is null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.UserDoesNotExist));
        }

        var isPasswordValidResult = _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash,loginUser.Password);
        if (isPasswordValidResult != PasswordVerificationResult.Success)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.PasswordIsInvalid));
        }

        var authData = await _authService.GetAuthDataAsync(user.Id);

        return authData;
    }
    
    [HttpPost("signup")]
    public async Task<ActionResult<AuthData>> Register([FromBody] RegisterUser register)
    {
        var user = await _studentService.GetStudentByEmailAsync(register.Email);

        if (user is not null)
        {
            return BadRequest(ErrorResponse.CreateFromApiError(ApiError.UserWithTheSameEmailAlreadyExist));
        }

        var studentId = await _studentService.AddStudentAsync(register.Email, register.Password);

        var authData = await _authService.GetAuthDataAsync(studentId);

        return authData;
    }
}