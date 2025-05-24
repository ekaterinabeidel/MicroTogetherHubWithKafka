using Microsoft.EntityFrameworkCore;
using Topic.QueryService.Domain.Dao;
using Topic.QueryService.Domain.Entities;
using Topic.QueryService.Infrastructure.Data;

namespace Topic.QueryService.Infrastructure.Dao;

public class TopicStorage : ITopicStorage
{
    private readonly ApplicationContext appContext;

    public TopicStorage(DbContextFactory dbContextFactory)
    {
        this.appContext = dbContextFactory.CreateDbContext();
    }

    public async Task AddTopicAsync(TopicEntity topic)
    {
        appContext.Topics.Add(topic);
        await appContext.SaveChangesAsync();
    }

    public async Task<TopicEntity> GetTopicByIdAsync(Guid topicId)
    {
        var result = await appContext.Topics
            .Include(i => i.Comments)
            .FirstOrDefaultAsync(x => x.Id == topicId);
        return result!;
    }

    public async Task UpdateTopicAsync(TopicEntity topic)
    {
        appContext.Topics.Update(topic);
        await appContext.SaveChangesAsync();
    }

    public async Task DeleteTopicAsync(Guid topicId)
    {
        var post = await GetTopicByIdAsync(topicId);
        if (post == null) return;
        appContext.Topics.Remove(post);
        await appContext.SaveChangesAsync();
    }

    public async Task<List<TopicEntity>> GetAllTopicsAsync()
    {
        return await appContext.Topics
            .AsNoTracking()
            .Include(i => i.Comments)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<TopicEntity>> GetTopicsWithCommentsAsync()
    {
        return await appContext.Topics.AsNoTracking()
            .Include(i => i.Comments)
            .AsNoTracking()
            .Where(x => x.Comments != null && x.Comments.Any())
            .ToListAsync();
    }

    public async Task<List<TopicEntity>> GetTopicsByAuthorAsync(string authorName)
    {
        return await appContext.Topics
            .AsNoTracking()
            .Include(i => i.Comments)
            .AsNoTracking()
            .Where(x => x.AuthorName.Contains(authorName))
            .ToListAsync();
    }

    public async Task<List<TopicEntity>> GetTopicsWithMinLikesAsync(int count)
    {
        return await appContext.Topics
            .AsNoTracking()
            .Include(i => i.Comments)
            .AsNoTracking()
            .Where(x => x.Likes >= count)
            .ToListAsync();
    }
}