using Carter;

namespace Topic.QueryService.Api.Topics.Endpoints;

public abstract class BaseQueryEndpoint : ICarterModule
{
    public abstract void AddRoutes(IEndpointRouteBuilder app);

    protected static IResult NormalResponse(IEnumerable<TopicEntity> topics)
    {
        if (topics is null || !topics.Any())
            return Results.NoContent();

        var count = topics.Count();
        return Results.Ok(new QueryTopicsResponse
        {
            Topics = topics,
            Message = $"Возвращено топиков: {count}"
        });
    }

    protected static IResult ErrorResponse(
        ILogger logger,
        Exception ex,
        string errorMessage)
    {
        logger.LogError(ex, errorMessage);

        return Results.Json(new BaseResponse
        {
            Message = errorMessage
        }, statusCode: StatusCodes.Status500InternalServerError);
    }

    protected async Task<IResult> ExecuteQueryAsync(
        ILogger logger,
        Func<Task<IEnumerable<TopicEntity>>> query,
        string errorMessage)
    {
        try
        {
            var result = await query();
            return NormalResponse(result);
        }
        catch (Exception ex)
        {
            return ErrorResponse(logger, ex, errorMessage);
        }
    }
}