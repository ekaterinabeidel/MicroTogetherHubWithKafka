namespace Topic.CommandService.Api.Comments.Endpoints;

public class UpdateCommentEndpoint : BaseEndpoint<UpdateCommentCommand>
{
    protected override string GetSuccessMessage => "Комментарий успешно обновлён";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/topics/{topicId:guid}/comments", async (
                Guid topicId,
                [FromBody] UpdateCommentCommand command,
                ICommandDispatcher commandDispatcher,
                ILogger<UpdateCommentEndpoint> logger) =>
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