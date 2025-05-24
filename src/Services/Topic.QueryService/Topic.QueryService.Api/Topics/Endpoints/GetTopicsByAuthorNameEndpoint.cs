namespace Topic.QueryService.Api.Topics.Endpoints;

public class GetTopicsByAuthorNameEndpoint : BaseQueryEndpoint
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/topics/author/{authorName}", async (
                string authorName,
                ILogger<GetTopicsByAuthorNameEndpoint> logger,
                IQueryDispatcher<TopicEntity> queryDispatcher) =>
            {
                return await ExecuteQueryAsync(
                    logger,
                    async () => await queryDispatcher.SendAsync(
                        new GetTopicsByAuthorNameQuery { AuthorName = authorName }
                    ),
                    "Ошибка запроса"
                );
            })
            .WithName("GetTopicsByAuthorName")
            .Produces<QueryTopicsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
    }
}