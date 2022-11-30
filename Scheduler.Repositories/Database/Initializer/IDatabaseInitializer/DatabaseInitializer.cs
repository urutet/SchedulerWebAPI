using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scheduler.Common.Options;
using Scheduler.DomainModel.Identity;
using Scheduler.Repositories.Constants;

namespace Scheduler.Repositories.Database.Initializer.IDatabaseInitializer;

public class DatabaseInitializer : IDatabaseInitializer<SchedulerDbContext>
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly ManagerCredentialsOptions _managerCredentialsOptions;

    public DatabaseInitializer(
        UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<ManagerCredentialsOptions> managerCredentialsOptions)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _managerCredentialsOptions = managerCredentialsOptions.Value;
    }

    public void Initialize(SchedulerDbContext context)
    {
        context.Database.Migrate();
        Seed(context);
        context.SaveChanges();
    }

    private async void Seed(SchedulerDbContext context)
    {
        if (_roleManager.FindByNameAsync(Roles.Manager).Result is null)
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.Manager));
        }

        if (_roleManager.FindByNameAsync(Roles.Student).Result is null)
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.Student));
        }

        if (_roleManager.FindByNameAsync(Roles.Teacher).Result is null)
        {
            await _roleManager.CreateAsync(new IdentityRole(Roles.Teacher));
        }

        if (_userManager.FindByEmailAsync(_managerCredentialsOptions.Email).Result is null)
        {
            var manager = new ManagerUser
            {
                UserName = _managerCredentialsOptions.Email,
                Email = _managerCredentialsOptions.Email,
            };

            await _userManager.CreateAsync(manager, _managerCredentialsOptions.Password);
            await _userManager.AddToRoleAsync(manager, Roles.Manager);
        }
    }
}