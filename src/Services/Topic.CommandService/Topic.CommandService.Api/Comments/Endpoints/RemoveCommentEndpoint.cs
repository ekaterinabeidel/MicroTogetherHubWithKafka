namespace Topic.CommandService.Api.Comments.Endpoints;

public class RemoveCommentEndpoint : BaseEndpoint<RemoveCommentCommand>
{
    protected override string GetSuccessMessage => "Комментарий успешно удалён";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/topics/{topicId:guid}/comments", async (
                Guid topicId,
                [FromBody] RemoveCommentCommand command,
                ICommandDispatcher commandDispatcher,
                ILogger<RemoveCommentEndpoint> logger) =>
            {
                command.Id = topicId;
                return await ExecuteCommandAsync(command,
                    cmd => commandDispatcher.SendCommandAsync(cmd), logger);
            })
            .Produces<ResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}