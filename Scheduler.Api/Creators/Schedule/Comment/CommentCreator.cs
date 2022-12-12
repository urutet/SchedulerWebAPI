using Scheduler.Models.Schedule;

namespace Scheduler.Creators.Schedule.Comment;

public class CommentCreator : ICommentCreator
{
    public DomainModel.Model.Schedule.Comment CreateFrom(CreateComment createComment)
    {
        return new DomainModel.Model.Schedule.Comment
        {
            Id = Guid.NewGuid().ToString(),
            teacherId = createComment.teacherId,
            comment = createComment.comment
        };
    }
}