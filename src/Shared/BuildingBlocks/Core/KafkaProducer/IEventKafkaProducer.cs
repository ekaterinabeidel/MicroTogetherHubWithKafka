namespace Core.KafkaProducer;

public interface IEventKafkaProducer
{
    Task PublishEventAsync<T>(string topic, T eventSource)
        where T : BaseEvent;
}