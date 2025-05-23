namespace Topic.CommandService.Api.Comments;

public class CommentCommandHandler(IEventHandler<ContentAggregate> eventHandler)
    : ICommentCommandHandler
{
    public async Task HandleAsync(CreateCommentCommand command, CancellationToken ct)
    {
        var aggregate = await eventHandler.GetAggregateByIdAsync(command.Id, ct);
        aggregate.CreateComment(command.Text, command.AuthorName);
        await eventHandler.SaveAsync(aggregate, ct);
    }

    public async Task HandleAsync(UpdateCommentCommand command, CancellationToken ct)
    {
        var aggregate = await eventHandler.GetAggregateByIdAsync(command.Id, ct);
        aggregate.UpdateComment(command.CommentId, command.Text, command.AuthorName);
        await eventHandler.SaveAsync(aggregate, ct);
    }

    public async Task HandleAsync(RemoveCommentCommand command, CancellationToken ct)
    {
        var aggregate = await eventHandler.GetAggregateByIdAsync(command.Id, ct);
        aggregate.RemoveComment(command.CommentId, command.AuthorName);
        await eventHandler.SaveAsync(aggregate, ct);
    }
}