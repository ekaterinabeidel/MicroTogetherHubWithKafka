using System.Text.Json;
using Confluent.Kafka;
using Core.Events;
using Core.KafkaProducer;
using Microsoft.Extensions.Options;

namespace Topic.CommandService.Infrastructure.KafkaProducer;

public class EventKafkaProducer(IOptions<ProducerConfig> config)
    : IEventKafkaProducer
{
    public async Task PublishEventAsync<T>(string topic, T eventSource)
        where T : BaseEvent
    {
        using var producer = new ProducerBuilder<string, string>(config.Value)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        var message = new Message<string, string>
        {
            Key = Guid.NewGuid().ToString(),
            Value = JsonSerializer.Serialize(eventSource, eventSource.GetType())
        };

        var result = await producer.ProduceAsync(topic, message);

        if (result.Status == PersistenceStatus.NotPersisted)
        {
            string msg = String.Format(@"Не удалось отправить {0} 
            сообщение в топик - {1} по причине: {2}",
                eventSource.GetType().Name,
                topic,
                result.Message);
            throw new Exception(msg);
        }
    }
}