using Core.Events.Comments.CreateComment;
using Core.Events.Comments.RemoveComment;
using Core.Events.Comments.UpdateComment;
using Core.Events.Topics.CreateTopic;
using Core.Events.Topics.LikeTopic;
using Core.Events.Topics.RemoveTopic;
using Core.Events.Topics.UpdateTopic;

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