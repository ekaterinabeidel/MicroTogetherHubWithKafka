namespace Core.Events.Comments.RemoveComment;

public class RemoveCommentEvent : BaseEvent
{
    public required Guid CommentId { get; set; }
    
    public RemoveCommentEvent() : base(nameof(RemoveCommentEvent))
    {
    }
}