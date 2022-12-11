using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scheduler.Authorization.Requirements;
using Scheduler.Authorization.Requirements.Roles;
using Scheduler.Common.Options;
using Scheduler.Creators.Schedule;
using Scheduler.Creators.Schedule.ClassTime;
using Scheduler.Creators.University.Department;
using Scheduler.Creators.University.Faculty;
using Scheduler.Creators.University.Group;
using Scheduler.Creators.University.Teacher;
using Scheduler.JWT;
using Scheduler.Repositories.Database;
using Scheduler.Repositories.Database.Initializer.IDatabaseInitializer;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Services.Auth;
using Scheduler.Services.Schedule;
using Scheduler.Services.University;
using Scheduler.Services.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
        };
    });

builder.Services.AddAuthorization(o =>
{
    o.AddPolicy(Policies.IsManager, p => p.IsManager());
    o.AddPolicy(Policies.IsStudent, p => p.IsStudent());
    o.AddPolicy(Policies.IsTeacher, p => p.IsTeacher());
});
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(JwtOptions.SectionName));
builder.Services.Configure<ManagerCredentialsOptions>(builder.Configuration.GetSection(ManagerCredentialsOptions.SectionName));

builder.Services.AddScoped<IAuthorizationHandler, IsManagerRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsStudentRequirementHandler>();
builder.Services.AddScoped<IAuthorizationHandler, IsTeacherRequirementHandler>();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(o =>
    {
        o.Password.RequireNonAlphanumeric = false;
    })
    .AddSignInManager<SignInManager<IdentityUser>>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddUserManager<UserManager<IdentityUser>>()
    .AddEntityFrameworkStores<SchedulerDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddDbContext<SchedulerDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.MigrationsAssembly("Scheduler.Repositories"))
);

builder.Services.AddSingleton<IDatabaseInitializer<SchedulerDbContext>, DatabaseInitializer>();

builder.Services.AddSingleton<IUnitOfWorkFactory<UnitOfWork>, UnitOfWorkFactory>();
builder.Services.AddSingleton<IJwtGenerator, JwtGenerator>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddSingleton<IAuditoriumService, AuditoriumService>();
builder.Services.AddSingleton<IClassTimeService, ClassTimeService>();
builder.Services.AddSingleton<IScheduleService, ScheduleService>();
builder.Services.AddSingleton<ISubjectService, SubjectService>();
builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
builder.Services.AddSingleton<IFacultyService, FacultyService>();
builder.Services.AddSingleton<IGroupService, GroupService>();
builder.Services.AddSingleton<IFacultyCreator, FacultyCreator>();
builder.Services.AddSingleton<IGroupCreator, GroupCreator>();
builder.Services.AddSingleton<IDepartmentCreator, DepartmentCreator>();
builder.Services.AddSingleton<ITeacherCreator, TeacherCreator>();
builder.Services.AddSingleton<IAuditoriumCreator, AuditoriumCreator>();
builder.Services.AddSingleton<IClassTimeCreator, ClassTimeCreator>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(b =>
    {
        var allowedOrigins = builder.Configuration.GetSection("ClientOrigin").Get<string[]>();
        b.WithOrigins(allowedOrigins).AllowAnyMethod().AllowCredentials().AllowAnyHeader();
    });
});


var app = builder.Build();

InitializeDatabase(app.Services);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

app.UseEndpoints(o => o.MapControllers());
app.Run();

static void InitializeDatabase(IServiceProvider serviceProvider)
{
    var databaseInitializer = serviceProvider.GetRequiredService<IDatabaseInitializer<SchedulerDbContext>>();
    var dbContextOptions = serviceProvider.GetRequiredService<DbContextOptions<SchedulerDbContext>>();
    using (var dbContext = new SchedulerDbContext(dbContextOptions))
    {
        databaseInitializer.Initialize(dbContext);
    }
}