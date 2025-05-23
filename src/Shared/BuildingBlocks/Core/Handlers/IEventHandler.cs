using Core.Aggregates;

namespace Core.Handlers;

public interface IEventHandler<T>
{
    Task<T> GetAggregateByIdAsync(Guid Id, CancellationToken ct);
    Task SaveAsync(BaseAggregate item, CancellationToken ct);
}