namespace Topic.CommandService.Api.Topics.Endpoints;

public class CreateTopicEndpoint : BaseEndpoint<CreateTopicCommand>
{
    protected override string GetSuccessMessage => "Топик успешно создан";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/topics", async (
                [FromBody] CreateTopicCommand command,
                ILogger<CreateTopicEndpoint> logger,
                ICommandDispatcher commandDispatcher) =>
            {
                command.Id = Guid.NewGuid();
                return await ExecuteCommandAsync(command,
                    cmd => commandDispatcher.SendCommandAsync(cmd), logger);
            })
            .Produces<ResponseDto>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}