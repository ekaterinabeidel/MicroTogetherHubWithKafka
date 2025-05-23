namespace Core.Events.Topics.LikeTopic;

public class LikeTopicEvent : BaseEvent
{
    public LikeTopicEvent() : base(nameof(LikeTopicEvent))
    {
    }
}