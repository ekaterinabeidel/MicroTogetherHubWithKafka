using System.ComponentModel.DataAnnotations.Schema;

namespace Topic.QueryService.Domain.Entities;

[Table("Topics", Schema = "public")]
public class TopicEntity
{
    public Guid Id { get; set; }
    public string AuthorName { get; set; } = default!;
    public string MessageText { get; set; } = default!;
    public DateTime CreatedAt { get; set; }
    public int Likes { get; set; }
    public virtual IEnumerable<CommentEntity> Comments { get; set; }
        = new List<CommentEntity>();
}