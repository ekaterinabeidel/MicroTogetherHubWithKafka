namespace Topic.CommandService.Api.Topics.Endpoints;

public class RemoveTopicEndpoint : BaseEndpoint<RemoveTopicCommand>
{
    protected override string GetSuccessMessage => "Топик успешно удалён";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/topics/{topicId:guid}", async (
                Guid topicId,
                [FromBody] RemoveTopicCommand command,
                ILogger<RemoveTopicEndpoint> logger,
                ICommandDispatcher commandDispatcher) =>
            {
                command.Id = topicId;
                return await ExecuteCommandAsync(command,
                    cmd => commandDispatcher.SendCommandAsync(cmd),
                    logger);
            })
            .Produces<ResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}