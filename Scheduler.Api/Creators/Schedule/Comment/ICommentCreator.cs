using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule.Comment;

public interface ICommentCreator : ICreator
{
    public DomainModel.Model.Schedule.Comment CreateFrom(CreateComment createComment);
}