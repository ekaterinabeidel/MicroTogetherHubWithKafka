namespace Topic.QueryService.Api.Topics.Endpoints;

public class GetTopicsEndpoint : BaseQueryEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/topics", async (
                ILogger<GetTopicsEndpoint> logger,
                IQueryDispatcher<TopicEntity> queryDispatcher) =>
            {
                return await ExecuteQueryAsync(
                    logger,
                    async () => await queryDispatcher.SendAsync(new GetTopicsQuery()),
                    "Ошибка при получении топиков"
                );
            })
            .Produces<QueryTopicsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}