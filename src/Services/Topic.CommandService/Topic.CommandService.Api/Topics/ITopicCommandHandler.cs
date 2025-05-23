using Topic.CommandService.Api.Topics.Commands.CreateTopic;
using Topic.CommandService.Api.Topics.Commands.LikeTopic;
using Topic.CommandService.Api.Topics.Commands.RemoveTopic;
using Topic.CommandService.Api.Topics.Commands.UpdateTopic;

namespace Topic.CommandService.Api.Topics;

public interface ITopicCommandHandler
{
    Task HandleAsync(CreateTopicCommand command, CancellationToken ct);
    Task HandleAsync(RemoveTopicCommand command, CancellationToken ct);
    Task HandleAsync(UpdateTopicCommand command, CancellationToken ct);
    Task HandleAsync(LikeTopicCommand command, CancellationToken ct);
}