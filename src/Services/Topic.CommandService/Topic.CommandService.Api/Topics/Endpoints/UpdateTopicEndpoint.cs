namespace Topic.CommandService.Api.Topics.Endpoints;

public class UpdateTopicEndpoint : BaseEndpoint<UpdateTopicCommand>
{
    protected override string GetSuccessMessage => "Топик успешно обновлён";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/topics/{topicId:guid}", async (
                Guid topicId,
                [FromBody] UpdateTopicCommand command,
                ILogger<UpdateTopicEndpoint> logger,
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