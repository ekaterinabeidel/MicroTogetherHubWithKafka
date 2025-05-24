using Microsoft.EntityFrameworkCore;
using Topic.QueryService.Domain.Entities;

namespace Topic.QueryService.Infrastructure.Data;

public class ApplicationContext : DbContext
{
    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }

    public ApplicationContext(DbContextOptions options)
        : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CommentEntity>()
            .HasOne<TopicEntity>()
            .WithMany(topic => topic.Comments)
            .HasForeignKey(comments => comments.TopicId)
            .HasConstraintName("FK_Comment_Topic_TopicId")
            .IsRequired();
    }
}