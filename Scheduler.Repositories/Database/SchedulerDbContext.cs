using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Identity;
using Scheduler.DomainModel.Model.Schedule;
using Scheduler.DomainModel.Model.Schedule.Converters;
using Scheduler.DomainModel.Model.University;

namespace Scheduler.Repositories.Database;

public class SchedulerDbContext : IdentityDbContext
{
    public DbSet<ManagerUser> Managers { get; set; }

    public DbSet<StudentUser> Students { get; set; }

    public DbSet<TeacherUser> Teachers { get; set; }
    
    public DbSet<ClassTime> ClassTimes { get; set; }
    
    public DbSet<Auditorium> Auditoria { get; set; }
    
    public DbSet<Schedule> Schedules { get; set; }
    
    public DbSet<Subject> Subjects { get; set; }
    
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<Faculty> Faculties { get; set; }
    
    public DbSet<Group> Groups { get; set; }

    public SchedulerDbContext(DbContextOptions<SchedulerDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        builder.Entity<IdentityUser>().ToTable("Users");
        builder.Entity<IdentityRole>().ToTable("Roles");
        builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
        builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
        builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

        builder.Entity<ClassTime>(builder =>
        {
            builder.Property(x => x.StartTime)
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
            
            builder.Property(x => x.EndTime)
                .HasConversion<TimeOnlyConverter, TimeOnlyComparer>();
        });
        
        builder.Entity<AuditoriumTypes>(entityBuilder =>
        {
            entityBuilder.Property(e => e.Id).HasConversion<string>();
        });
        
        builder.Entity<AuditoriumTypes>().HasData(
            new AuditoriumTypes {Id = AuditoriumType.Laboratory},
            new AuditoriumTypes {Id = AuditoriumType.Lecture},
            new AuditoriumTypes {Id = AuditoriumType.Practical});

        builder.Entity<Auditorium>().HasOne<AuditoriumTypes>()
            .WithMany()
            .HasForeignKey(k => k.Type);
        
        builder.Entity<Days>(entityBuilder =>
        {
            entityBuilder.Property(e => e.Id).HasConversion<string>();
        });

        builder.Entity<Days>().HasData(
            new Days {Id = Day.Monday},
            new Days {Id = Day.Tuesday},
            new Days {Id = Day.Wednesday},
            new Days {Id = Day.Thursday},
            new Days {Id = Day.Friday},
            new Days {Id = Day.Saturday},
            new Days {Id = Day.Sunday});

        builder.Entity<ClassTime>().HasOne<Days>()
            .WithMany()
            .HasForeignKey(k => k.Day);
    }
}

public sealed class AuditoriumTypes
{
    public AuditoriumType Id { get; set; }
}

public sealed class Days
{
    public Day Id { get; set; }
}