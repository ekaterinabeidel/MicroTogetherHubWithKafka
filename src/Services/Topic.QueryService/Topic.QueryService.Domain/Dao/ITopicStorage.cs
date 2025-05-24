using Topic.QueryService.Domain.Entities;

namespace Topic.QueryService.Domain.Dao;

public interface ITopicStorage
{
    Task AddTopicAsync(TopicEntity topic);
    Task<TopicEntity> GetTopicByIdAsync(Guid topicId);
    Task UpdateTopicAsync(TopicEntity topic);
    Task DeleteTopicAsync(Guid topicId);
    Task<List<TopicEntity>> GetAllTopicsAsync();
    Task<List<TopicEntity>> GetTopicsWithCommentsAsync();
    Task<List<TopicEntity>> GetTopicsByAuthorAsync(string authorName);
    Task<List<TopicEntity>> GetTopicsWithMinLikesAsync(int numberOfLikes);
}