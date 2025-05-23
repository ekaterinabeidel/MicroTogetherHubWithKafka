using Core.Commands;

namespace Topic.CommandService.Api.Topics.Commands.CreateTopic;

public class CreateTopicCommand : BaseCommand
{
    public string AuthorName { get; set; } = default!;
    public string MessageText { get; set; } = default!;
}
