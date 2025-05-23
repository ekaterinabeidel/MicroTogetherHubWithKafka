using Core.Commands;

namespace Topic.CommandService.Api.Topics.Commands.UpdateTopic;

public class UpdateTopicCommand : BaseCommand
{
    public string MessageText { get; set; } = default!;
}