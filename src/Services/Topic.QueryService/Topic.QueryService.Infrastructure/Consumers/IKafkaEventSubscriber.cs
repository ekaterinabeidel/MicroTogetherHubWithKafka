namespace Topic.QueryService.Infrastructure.Consumers;

public interface IKafkaEventSubscriber
{
    Task ConsumeAsync(string topic, CancellationToken ct);
}