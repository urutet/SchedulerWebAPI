using Microsoft.EntityFrameworkCore;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.Repositories.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory<UnitOfWork>
{
    private readonly DbContextOptions<SchedulerDbContext> _options;


    public UnitOfWorkFactory(DbContextOptions<SchedulerDbContext> options)
    {
        _options = options;
    }

    public UnitOfWork Create()
    {
        var dbContext = new SchedulerDbContext(_options);
        var unitOfWork = new UnitOfWork(dbContext);

        return unitOfWork;
    }
}