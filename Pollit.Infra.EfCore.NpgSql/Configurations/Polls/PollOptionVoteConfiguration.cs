using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Poll;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Polls;

public class PollOptionVoteConfiguration : IEntityTypeConfiguration<PollOptionVote>
{
    public void Configure(EntityTypeBuilder<PollOptionVote> builder)
    {
        builder.ToTable("Polls.Options.Votes");
                
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).HasDefaultValueSql("gen_random_uuid ()").IsRequired();
        
        builder.Property(v => v.Id).HasConversion(id => id.Value, id => new PollOptionVoteId(id));
        builder.Property(v => v.VoterId).HasConversion(id => id.Value, id => new UserId(id));
    }
}