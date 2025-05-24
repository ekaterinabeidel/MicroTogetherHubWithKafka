namespace Topic.QueryService.Infrastructure.MediatR;

public class QueryDispatcher : IQueryDispatcher<TopicEntity>
{
    private readonly Dictionary<
        Type,
        Func<BaseQuery, Task<IEnumerable<TopicEntity>>>> handlers = new();

    public void RegisterHandler<T>(Func<T, Task<IEnumerable<TopicEntity>>> handler)
        where T : BaseQuery
    {
        if (handlers.ContainsKey(typeof(T)))
        {
            throw new InvalidOperationException(
                "Такой обработчик уже зарегистрирован.");
        }

        handlers.Add(typeof(T), query =>
        {
            if (query is T typedQuery)
            {
                return handler(typedQuery);
            }

            throw new ArgumentException(
                $"Некорректный тип запроса: {query.GetType().Name}");
        });
    }

    public async Task<IEnumerable<TopicEntity>> SendAsync(BaseQuery query)
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query),
                "Запрос не может быть null");
        }

        if (handlers.TryGetValue(query.GetType(), out var handler))
        {
            return await handler(query);
        }

        throw new InvalidOperationException(
            @$"Обработчик для запроса типа {query.GetType().Name} не зарегистрирован.");
    }
}