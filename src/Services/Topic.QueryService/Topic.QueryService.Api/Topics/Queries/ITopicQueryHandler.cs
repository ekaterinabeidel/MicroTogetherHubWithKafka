using Topic.QueryService.Api.Topics.Queries.GetTopicById;
using Topic.QueryService.Api.Topics.Queries.GetTopics;
using Topic.QueryService.Api.Topics.Queries.GetTopicsByAuthorName;
using Topic.QueryService.Api.Topics.Queries.GetTopicsWithComments;
using Topic.QueryService.Api.Topics.Queries.GetTopicsWithLikes;
using Topic.QueryService.Domain.Entities;

namespace Topic.QueryService.Api.Topics.Queries;

public interface ITopicQueryHandler
{
    Task<IEnumerable<TopicEntity>> HandleAsync(GetTopicsQuery query);
    Task<IEnumerable<TopicEntity>> HandleAsync(GetTopicByIdQuery query);
    Task<IEnumerable<TopicEntity>> HandleAsync(GetTopicsByAuthorNameQuery query);
    Task<IEnumerable<TopicEntity>> HandleAsync(GetTopicsWithCommentsQuery query);
    Task<IEnumerable<TopicEntity>> HandleAsync(GetTopicsWithLikesQuery query);
}