using Scheduler.DomainModel.Identity;
using Scheduler.Repositories.Database;

namespace Scheduler.Repositories.Repositories.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool _disposed = false;
    private readonly SchedulerDbContext _dbContext;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(SchedulerDbContext dbContext)
    {
        _dbContext = dbContext;
        _repositories = new Dictionary<Type, object>();
    }


    public TRepository GetRepository<TEntity, TRepository>()
        where TEntity : class, IHasId
        where TRepository : Repository<TEntity>
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return _repositories[typeof(TEntity)] as TRepository;
        }

        var repository = (TRepository)Activator.CreateInstance(typeof(TRepository), args: _dbContext);
        _repositories.Add(typeof(TEntity), repository);

        return repository;
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}