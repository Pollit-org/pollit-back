using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Comments;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Comments;

public class CommentVoteConfiguration : IEntityTypeConfiguration<CommentVote>
{
    public void Configure(EntityTypeBuilder<CommentVote> builder)
    {
        builder.ToTable("Comments.Votes");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).ValueGeneratedNever().IsRequired();

        builder.HasIndex(v => v.VoterId);

        builder.Property(v => v.Id).HasConversion(id => id.Value, id => new CommentVoteId(id));
        builder.Property(v => v.VoterId).HasConversion(id => id.Value, id => new UserId(id));
    }
}