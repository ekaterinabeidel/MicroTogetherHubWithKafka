namespace Topic.CommandService.Domain.Aggregates;

public partial class ContentAggregate
{
    private void EnsureMessageIsValid(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new InvalidOperationException(
                $@"Значение {nameof(message)} не может быть пустым.
                Пожалуйста, укажите действительный {nameof(message)}"
            );
        }
    }
    private void EnsureCommentTextIsValid(string commentText)
    {
        if (string.IsNullOrWhiteSpace(commentText))
        {
            throw new InvalidOperationException(
                $@"Значение {nameof(commentText)} не может быть пустым. 
                Пожалуйста, укажите действительный {nameof(commentText)}"
            );
        }
    }

    private void EnsureTopicIsActive()
    {
        if (!Active)
        {
            throw new InvalidOperationException(
                "Вы не можете выполнить это действие для неактивного сообщения"
            );
        }
    }

    private void EnsureUserIsAuthor(string authorName)
    {
        if (!author.Equals(authorName,
                StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException(
                @"Вам не разрешается выполнять это действие над сообщением,
                сделанным другим пользователем"
            );
        }
    }

    private void EnsureCommentBelongsToUser(Guid commentId, string authorName)
    {
        if (!comments[commentId].authorName.Equals(authorName,
                StringComparison.CurrentCultureIgnoreCase))
        {
            throw new InvalidOperationException(
                @"Вы не можете выполнить это действие над комментарием, 
                сделанным другим пользователем"
            );
        }
    }
}