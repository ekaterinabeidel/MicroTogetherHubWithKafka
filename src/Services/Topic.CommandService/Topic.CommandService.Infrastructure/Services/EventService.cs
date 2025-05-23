using Core.Events;
using Core.Events.Dao;
using Core.Exceptions;
using Core.Services;
using Topic.CommandService.Domain.Aggregates;

public class EventService(
    IEventStorage eventStorage)
    : IEventService
{
    public async Task<IEnumerable<BaseEvent>> GetEventsAsync(
        Guid aggregateId,
        CancellationToken ct)
    {
        var events = await eventStorage
            .FindByAggregateId(aggregateId, ct);

        if (events is null || !events.Any())
        {
            throw new AggregateNotFoundException();
        }

        var result = events
            .OrderBy(x => x.Version)
            .Select(x => x.EventData)
            .ToList();

        return result;
    }

    public async Task SaveEventsAsync(
        Guid aggregateId,
        IEnumerable<BaseEvent> events,
        int expectedVersion,
        CancellationToken ct)
    {

        var eventStream = await eventStorage
            .FindByAggregateId(aggregateId, ct);

        if (expectedVersion != 0
            && eventStream.Last().Version != expectedVersion)
        {
            throw new VersionConflictException();
        }

        var version = expectedVersion;

        foreach (var item in events)
        {
            version++;
            item.Version = version;
            var eventType = item.GetType().Name;

            var eventModel = new EventModel(
                Id: Guid.NewGuid().ToString(),
                CreatedAt: DateTime.Now,
                AggregateId: aggregateId,
                AggregateType: nameof(ContentAggregate),
                Version: version,
                EventType: eventType,
                EventData: item
            );

            await eventStorage.SaveAsync(eventModel, ct);
        }
    }
}