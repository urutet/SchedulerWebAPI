using System.ComponentModel.DataAnnotations;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.Models.Auth;

public class RegisterTeacher
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    
    public string Name { get; set; }
    
    public string departmentId { get; set; }
    
    public string Photo { get; set; }
}