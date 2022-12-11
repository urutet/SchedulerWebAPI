using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule;

public interface IAuditoriumCreator : ICreator
{
    public Auditorium CreateFrom(CreateAuditorium createAuditorium);
}