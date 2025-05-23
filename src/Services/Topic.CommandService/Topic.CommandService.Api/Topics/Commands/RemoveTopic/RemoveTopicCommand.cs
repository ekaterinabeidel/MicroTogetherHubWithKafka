using Core.Commands;

namespace Topic.CommandService.Api.Topics.Commands.RemoveTopic;

public class RemoveTopicCommand : BaseCommand
{
    public string AuthorName { get; set; } = default!;
}