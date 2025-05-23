using Marten;

namespace Core.Events.Dao;

public class EventStorage(IDocumentSession documentSession) : IEventStorage
{
    public async Task<IEnumerable<EventModel>> FindByAggregateId(Guid id, CancellationToken ct)
    {
        return await documentSession.Query<EventModel>()
            .Where(item => item.AggregateId == id)
            .OrderBy(item => item.Version)
            .ToListAsync(ct);
    }

    public async Task SaveAsync(EventModel eventModel, CancellationToken ct)
    {
        documentSession.Store(eventModel);
        await documentSession.SaveChangesAsync(ct);
    }
}