namespace Core.Events.Topics.RemoveTopic;

public class RemoveTopicEvent : BaseEvent
{
    public RemoveTopicEvent() : base(nameof(RemoveTopicEvent))
    {
    }
}