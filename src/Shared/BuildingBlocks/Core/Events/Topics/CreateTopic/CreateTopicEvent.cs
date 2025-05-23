namespace Core.Events.Topics.CreateTopic;

public class CreateTopicEvent : BaseEvent
{
    public string AuthorName { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public DateTime CreateDate { get; set; }
    public CreateTopicEvent() : base(nameof(CreateTopicEvent))
    {
    }
}