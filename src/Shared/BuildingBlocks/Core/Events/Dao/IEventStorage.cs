namespace Core.Events.Dao;

public interface IEventStorage
{
    Task SaveAsync(EventModel eventModel, CancellationToken ct);
    Task<IEnumerable<EventModel>> FindByAggregateId(Guid id, CancellationToken ct);
}