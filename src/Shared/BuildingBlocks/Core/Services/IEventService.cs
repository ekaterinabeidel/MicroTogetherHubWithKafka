namespace Core.Services;

public interface IEventService
{
    Task<IEnumerable<BaseEvent>> GetEventsAsync(
        Guid aggregateId,
        CancellationToken ct
    );

    Task SaveEventsAsync(
        Guid aggregateId,
        IEnumerable<BaseEvent> events,
        int expectedVersion,
        CancellationToken ct
    );
}