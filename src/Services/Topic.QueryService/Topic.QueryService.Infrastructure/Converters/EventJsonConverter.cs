using System.Text.Json;
using System.Text.Json.Serialization;

namespace Topic.QueryService.Infrastructure.Converters;

public class EventJsonConverter : JsonConverter<BaseEvent>
{
    public override bool CanConvert(Type type)
    {
        return type.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
        {
            throw new JsonException($"Ошибка десериализации {nameof(JsonDocument)}");
        }

        if (!doc.RootElement.TryGetProperty("EventType", out var type))
        {
            throw new JsonException("Не удалось определить тип");
        }

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();

        BaseEvent result = typeDiscriminator switch
        {
            nameof(CreateTopicEvent) =>
                JsonSerializer.Deserialize<CreateTopicEvent>(json, options)!,
            nameof(UpdateTopicEvent) =>
                JsonSerializer.Deserialize<UpdateTopicEvent>(json, options)!,
            nameof(LikeTopicEvent) =>
                JsonSerializer.Deserialize<LikeTopicEvent>(json, options)!,
            nameof(RemoveTopicEvent) =>
                JsonSerializer.Deserialize<RemoveTopicEvent>(json, options)!,
            nameof(CreateCommentEvent) =>
                JsonSerializer.Deserialize<CreateCommentEvent>(json, options)!,
            nameof(UpdateCommentEvent) =>
                JsonSerializer.Deserialize<UpdateCommentEvent>(json, options)!,
            nameof(RemoveCommentEvent) =>
                JsonSerializer.Deserialize<RemoveCommentEvent>(json, options)!,
            _ => null!
        };

        if (result is null)
        {
            string errorMessage = $"Неизвестный тип события: {typeDiscriminator}";
            throw new JsonException(errorMessage);
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}