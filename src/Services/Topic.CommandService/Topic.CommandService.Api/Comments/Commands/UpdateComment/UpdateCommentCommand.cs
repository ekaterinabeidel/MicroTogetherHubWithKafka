namespace Topic.CommandService.Api.Comments.Commands.UpdateComment;

public class UpdateCommentCommand : BaseCommand
{
    public required Guid CommentId { get; set; }
    public string Text { get; set; } = default!;
    public string AuthorName { get; set; } = default!;
}