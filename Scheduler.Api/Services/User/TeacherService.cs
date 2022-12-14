using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Identity;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.University;
using Scheduler.Repositories.Constants;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Repositories.Repositories.User.Teacher;

namespace Scheduler.Services.User;

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public TeacherService(
        IUnitOfWorkFactory<UnitOfWork> unitOfWorkFactory,
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _unitOfWork = unitOfWorkFactory.Create();
    }
    
    public async Task<TeacherUser> GetTeacherByIdAsync(string id)
    {
        var teacherRepository = _unitOfWork.GetRepository<TeacherUser, TeacherRepository>();

        return  await teacherRepository.GetByIdAsync(id);
    }

    public async Task<TeacherUser> GetTeacherByEmailAsync(string email)
    {
        var teacherRepository = _unitOfWork.GetRepository<TeacherUser, TeacherRepository>();

        return await teacherRepository.GetByEmailAsync(email);
    }

    public async Task<IReadOnlyCollection<TeacherUser>> GetAllTeachers()
    {
        var teacherRepository = _unitOfWork.GetRepository<TeacherUser, TeacherRepository>();

        return await teacherRepository.GetQuery().Include(t => t.Comments).ToListAsync();
    }

    public async Task<string> AddTeacherAsync(string email, string password)
    {
        var teacherId = Guid.NewGuid().ToString();
        var teacher = new TeacherUser
        {
            Id = teacherId,
            UserName = email,
            Email = email,
        };

        var result = await _userManager.CreateAsync(teacher, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to create user {teacher.Email}");
        }

        await _userManager.AddToRoleAsync(teacher, Roles.Teacher);

        return teacherId;
    }
    

    public async Task<string> AddTeacherAsync(TeacherUser teacherUser, string password)
    {
        var result = await _userManager.CreateAsync(teacherUser, password);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException($"Failed to create user {teacherUser.Email}");
        }

        await _userManager.AddToRoleAsync(teacherUser, Roles.Teacher);

        return teacherUser.Id;
    }

    public async Task<bool> DeleteTeacherAsync(string id)
    {
        var teacher = await GetTeacherByIdAsync(id);
        if (teacher is null)
        {
            return false;
        }

        var result = await _userManager.DeleteAsync(teacher);
        if (!result.Succeeded)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateTeacher(string id, EditTeacher teacherUser)
    {
        var teacherRepository = _unitOfWork.GetRepository<TeacherUser, TeacherRepository>();
        
        var editTeacher = await teacherRepository.GetByIdAsync(id);
        if (editTeacher is null)
        {
            return false;
        }

        editTeacher.Name = teacherUser.Name;
        editTeacher.Email = teacherUser.Email;
        editTeacher.departmentId = teacherUser.departmentId;
        editTeacher.Photo = teacherUser.Photo;

        teacherRepository.Update(editTeacher);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }

    public async Task<bool> AddCommentToTeacher(string id, Comment comment)
    {
        var teacherRepository = _unitOfWork.GetRepository<TeacherUser, TeacherRepository>();

        var editTeacher = await teacherRepository.GetQuery()
            .Include(t => t.Comments).FirstOrDefaultAsync(t => t.Id == id);
        if (editTeacher is null)
        {
            return false;
        }

        editTeacher.Comments.Add(comment);

        teacherRepository.Update(editTeacher);
        await _unitOfWork.SaveChangesAsync();

        return true;
    }
}