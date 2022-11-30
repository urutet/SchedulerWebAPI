using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.DesignTime;

public class SchedulerDesignTimeDbcontextFactory : IDesignTimeDbContextFactory<SchedulerDbContext>
{
    private const string DesignTimeConnectionString = "Server=(localdb)\\mssqllocaldb;Database=Scheduler;Trusted_Connection=True;Integrated Security=True;";

    public SchedulerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SchedulerDbContext>().UseSqlServer(DesignTimeConnectionString);

        return new SchedulerDbContext(optionsBuilder.Options);
    }
}