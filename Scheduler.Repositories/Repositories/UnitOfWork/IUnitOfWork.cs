using Scheduler.DomainModel.Identity;

namespace Scheduler.Repositories.Repositories.UnitOfWork;

public interface IUnitOfWork
{
    TRepository GetRepository<TEntity, TRepository>()
        where TEntity : class, IHasId
        where TRepository : Repository<TEntity>;

    Task SaveChangesAsync();
}