namespace Topic.QueryService.Api.Topics.Queries;

public class TopicQueryHandler(ITopicStorage topicStorage) : ITopicQueryHandler
{
    public async Task<IEnumerable<TopicEntity>> HandleAsync(
        GetTopicsQuery query)
    {
        return await topicStorage.GetAllTopicsAsync();
    }

    public async Task<IEnumerable<TopicEntity>> HandleAsync(
        GetTopicByIdQuery query)
    {
        return new List<TopicEntity>
        {
            await topicStorage.GetTopicByIdAsync(query.Id)
        };
    }

    public async Task<IEnumerable<TopicEntity>> HandleAsync(
        GetTopicsByAuthorNameQuery query)
    {
        return await topicStorage.GetTopicsByAuthorAsync(query.AuthorName);
    }

    public async Task<IEnumerable<TopicEntity>> HandleAsync(
        GetTopicsWithCommentsQuery query)
    {
        return await topicStorage.GetTopicsWithCommentsAsync();
    }

    public async Task<IEnumerable<TopicEntity>> HandleAsync(
        GetTopicsWithLikesQuery query)
    {
        return await topicStorage.GetTopicsWithMinLikesAsync(query.Count);
    }
}