using Core.Commands;

namespace Topic.CommandService.Api.Comments.Commands.RemoveComment;

public class RemoveCommentCommand : BaseCommand
{
    public required Guid CommentId { get; set; }
    public string AuthorName { get; set; } = default!;
}