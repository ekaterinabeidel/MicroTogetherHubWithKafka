using Core.Queries;

namespace Core.MediatR;

public interface IQueryDispatcher<E>
{
    void RegisterHandler<T>(Func<T, Task<IEnumerable<E>>> handler)
        where T : BaseQuery;

    Task<IEnumerable<E>> SendAsync(BaseQuery query);
}