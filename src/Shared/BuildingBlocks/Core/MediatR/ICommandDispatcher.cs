namespace Core.MediatR;

public interface ICommandDispatcher
{
    void RegisterHandler<T>(Func<T, Task> handler)
        where T : BaseCommand;
    Task SendCommandAsync(BaseCommand command);
}