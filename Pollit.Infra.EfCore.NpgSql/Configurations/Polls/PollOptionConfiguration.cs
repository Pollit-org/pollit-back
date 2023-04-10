using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Poll;
using Pollit.Domain.Poll.PollOptionTitles;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Polls;

public class PollOptionConfiguration : IEntityTypeConfiguration<PollOption>
{
    public void Configure(EntityTypeBuilder<PollOption> builder)
    {
        builder.ToTable("Polls.Options");
        
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasDefaultValueSql("gen_random_uuid ()").IsRequired();
        
        builder.Property(o => o.Id).HasConversion(id => id.Value, id => new PollOptionId(id));

        builder.Property(o => o.Title).HasConversion(title => title.ToString(), titleString => new PollOptionTitle(titleString)).HasMaxLength(PollOptionTitle.MaxLength);

        builder.HasMany(o => o.Votes).WithOne();
    }
}