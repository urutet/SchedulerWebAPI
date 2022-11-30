using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Scheduler.DomainModel.Identity;
using Scheduler.Repositories.Constants;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Repositories.Repositories.User.Student;

namespace Scheduler.Services.User;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public StudentService(
        IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWorkFactory.Create();
    }


    public async Task<StudentUser> GetStudentByIdAsync(string id)
    {
        var studentRepository = _unitOfWork.GetRepository<StudentUser, StudentRepository>();

        return await studentRepository.GetByIdAsync(id);
    }

    public async Task<StudentUser> GetStudentByEmailAsync(string email)
    {
        var studentRepository = _unitOfWork.GetRepository<StudentUser, StudentRepository>();

        return await studentRepository.GetByEmailAsync(email);
    }

    public async Task<string> AddStudentAsync(string email, string password)
    {
        var studentId = Guid.NewGuid().ToString();
        var student = new StudentUser
        {
            Id = studentId,
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(student, password);
        if (!result.Succeeded)
        {
            // LoggerContext.Current.LogError($"Failed to create customer {customer.Email} - {result.Errors.Select(e => e.Code)}");
            throw new InvalidOperationException($"Failed to create user {student.Email}");
        }

        await _userManager.AddToRoleAsync(student, Roles.Student);

        return studentId;
    }
}