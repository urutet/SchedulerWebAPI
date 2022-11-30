namespace Scheduler.Repositories.Repositories.UnitOfWork;

public interface IUnitOfWorkFactory<T> where T : IUnitOfWork
{
    T Create();
}
