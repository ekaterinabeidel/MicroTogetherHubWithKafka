namespace Core.Events;

public record EventModel(
    string Id,
    DateTime CreatedAt,
    Guid AggregateId,
    string AggregateType,
    int Version,
    string EventType,
    BaseEvent EventData
);