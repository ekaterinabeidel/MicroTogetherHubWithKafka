namespace Topic.CommandService.Api.Topics.Endpoints;

public class LikeTopicEndpoint : BaseEndpoint<LikeTopicCommand>
{
    protected override string GetSuccessMessage => "Лайк успешно поставлен";

    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/topics/{topicId:guid}/likes", async (
                Guid topicId,
                ICommandDispatcher commandDispatcher,
                ILogger<LikeTopicEndpoint> logger) =>
            {
                return await ExecuteCommandAsync(
                    new LikeTopicCommand { Id = topicId },
                    cmd => commandDispatcher.SendCommandAsync(cmd),
                    logger);
            })
            .Produces<ResponseDto>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}