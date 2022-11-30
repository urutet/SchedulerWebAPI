using Microsoft.EntityFrameworkCore;

namespace Scheduler.Repositories.Database.Initializer.IDatabaseInitializer;

public interface IDatabaseInitializer<in TDbContext> where TDbContext : DbContext
{
    void Initialize(TDbContext context);
}