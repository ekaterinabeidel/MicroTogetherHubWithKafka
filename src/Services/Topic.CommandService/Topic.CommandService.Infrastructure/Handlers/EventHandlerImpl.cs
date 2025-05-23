using Core.Aggregates;
using Core.Handlers;
using Core.Services;
using Topic.CommandService.Domain.Aggregates;

namespace Topic.CommandService.Infrastructure.Handlers;

public class EventHandlerImpl(
    IEventService eventService)
    : IEventHandler<ContentAggregate>
{
    public async Task<ContentAggregate> GetAggregateByIdAsync(Guid id,
        CancellationToken ct)
    {
        var contentAggregate = new ContentAggregate();
        var events = await eventService.GetEventsAsync(id, ct);
        if (events is null || !events.Any())
        {
            return contentAggregate;
        }
        contentAggregate.RebuildState(events);

        var lastVersion = events
            .Select(x => x.Version)
            .Max();

        contentAggregate.Version = lastVersion;

        return contentAggregate;
    }

    public async Task SaveAsync(BaseAggregate item, CancellationToken ct)
    {
        await eventService
            .SaveEventsAsync(
                item.Id,
                item.GetPendingEvents,
                item.Version,
                ct);

        item.ClearPendingEvents();
    }
}