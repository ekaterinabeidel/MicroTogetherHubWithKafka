namespace Topic.QueryService.Api.ResponseDtos;

public class QueryTopicsResponse : BaseResponse
{
    public IEnumerable<TopicEntity> Topics { get; set; } = default!;
}