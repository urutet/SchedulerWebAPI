using Scheduler.DomainModel.Model.Schedule;
using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule;

public class AuditoriumCreator : IAuditoriumCreator
{
    public Auditorium CreateFrom(CreateAuditorium createAuditorium)
    {
        return new Auditorium
        {
            Id = Guid.NewGuid().ToString(),
            Name = createAuditorium.Name,
            Type = createAuditorium.Type
        };
    }
}