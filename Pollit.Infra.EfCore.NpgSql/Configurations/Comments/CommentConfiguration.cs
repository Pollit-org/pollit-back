using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Comments;
using Pollit.Domain.Polls;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Comments;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedNever().IsRequired();
        
        builder.HasIndex(p => p.PollId);
        builder.HasIndex(p => p.ParentCommentId);
        builder.HasIndex(p => p.AuthorId);
        
        builder.Property(p => p.Id).HasConversion(id => id.Value, id => new CommentId(id));
        builder.Property(p => p.PollId).HasConversion(id => id.Value, id => new PollId(id));
        builder.Property(p => p.ParentCommentId).HasConversion(id => id.Value, id => new CommentId(id));
        builder.Property(p => p.AuthorId).HasConversion(id => id.Value, id => new UserId(id));
        builder.Property(p => p.Body).HasConversion(body => body.ToString(), bodyString => new CommentBody(bodyString));
        
        builder.HasMany(c => c.Votes).WithOne();
    }
}