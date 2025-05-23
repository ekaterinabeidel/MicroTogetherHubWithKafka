namespace Topic.CommandService.Domain.Aggregates;

public partial class ContentAggregate
{
    private readonly Dictionary<Guid, (string text, string authorName)> comments = new();

    public void CreateComment(string commentText, string authorName)
    {
        EnsureTopicIsActive();
        EnsureCommentTextIsValid(commentText);

        RegisterEvent(new CreateCommentEvent
        {
            Id = Id,
            CommentId = Guid.NewGuid(),
            Text = commentText,
            AuthorName = authorName,
            CreateDate = DateTime.UtcNow
        });
    }

    public void Apply(CreateCommentEvent createCommentEvent)
    {
        Id = createCommentEvent.Id;
        comments.Add(
            createCommentEvent.CommentId,
            (createCommentEvent.Text, createCommentEvent.AuthorName)
        );
    }

    public void UpdateComment(Guid commentId, string commentText, string authorName)
    {
        EnsureTopicIsActive();
        EnsureCommentTextIsValid(commentText);
        EnsureCommentBelongsToUser(commentId, authorName);

        RegisterEvent(new UpdateCommentEvent
        {
            Id = Id,
            CommentId = commentId,
            Text = commentText,
            AuthorName = authorName,
            UpdateDate = DateTime.UtcNow
        });
    }

    public void Apply(UpdateCommentEvent updateCommentEvent)
    {
        Id = updateCommentEvent.Id;

        comments[updateCommentEvent.CommentId] = (
            updateCommentEvent.Text,
            updateCommentEvent.AuthorName
        );
    }

    public void RemoveComment(Guid commentId, string username)
    {
        EnsureTopicIsActive();
        EnsureCommentBelongsToUser(commentId, username);

        RegisterEvent(new RemoveCommentEvent
        {
            Id = Id,
            CommentId = commentId
        });
    }

    public void Apply(RemoveCommentEvent removeCommentEvent)
    {
        Id = removeCommentEvent.Id;
        comments.Remove(removeCommentEvent.CommentId);
    }
}