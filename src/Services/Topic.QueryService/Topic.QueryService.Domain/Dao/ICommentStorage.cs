using Topic.QueryService.Domain.Entities;

namespace Topic.QueryService.Domain.Dao;

public interface ICommentStorage
{
    Task AddCommentAsync(CommentEntity comment);
    Task<CommentEntity> GetCommentByIdAsync(Guid commentId);
    Task UpdateCommentAsync(CommentEntity comment);
    Task DeleteCommentAsync(Guid commentId);
}