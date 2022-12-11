using Microsoft.EntityFrameworkCore;
using Scheduler.DomainModel.Model.Schedule;

namespace Scheduler.Repositories.Repositories.Schedule;

public class AuditoriumRepository : Repository<Auditorium>, IAuditoriumRepository
{
   public AuditoriumRepository(DbContext dbContext) : base(dbContext)
   {
      
   }
}