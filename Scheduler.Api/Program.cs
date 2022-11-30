using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Scheduler.Authorization.Requirements;
using Scheduler.Authorization.Requirements.Roles;
using Scheduler.Common.Options;
using Scheduler.JWT;
using Scheduler.Repositories.Database;
using Scheduler.Repositories.Database.Initializer.IDatabaseInitializer;
using Scheduler.Repositories.Repositories.UnitOfWork;
using Scheduler.Services.Auth;
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
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt.Key"]))
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

builder.Services.AddMvc();
builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

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