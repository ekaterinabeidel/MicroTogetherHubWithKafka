namespace Topic.QueryService.Api.Topics.Queries.GetTopicsByAuthorName;

public class GetTopicsByAuthorNameQuery : BaseQuery
{
    public string AuthorName { get; set; } = default!;
}