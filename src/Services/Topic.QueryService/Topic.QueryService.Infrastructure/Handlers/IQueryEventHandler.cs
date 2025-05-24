namespace Topic.QueryService.Infrastructure.Handlers;

public interface IQueryEventHandler
{
    Task On(CreateTopicEvent createTopicEvent);
    Task On(UpdateTopicEvent updateTopicEvent);
    Task On(LikeTopicEvent likeTopicEvent);
    Task On(CreateCommentEvent createCommentEvent);
    Task On(UpdateCommentEvent updateCommentEvent);
    Task On(RemoveCommentEvent removeCommentEvent);
    Task On(RemoveTopicEvent removeTopicEvent);
}