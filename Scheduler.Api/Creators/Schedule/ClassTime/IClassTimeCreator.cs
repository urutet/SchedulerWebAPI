using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule.ClassTime;

public interface IClassTimeCreator : ICreator
{
    public DomainModel.Model.Schedule.ClassTime CreateFrom(CreateClassTime createClassTime);
}