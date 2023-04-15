using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Application.Polls.GetPollFeed;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Polls.Views;

public class GetPollFeedQueryResultItemConfiguration : IEntityTypeConfiguration<GetPollFeedQueryResultItem>
{
    public void Configure(EntityTypeBuilder<GetPollFeedQueryResultItem> builder)
    {
        builder.ToView("Polls.GetPollFeed").HasKey(x => x.PollId);

        builder.Property(x => x.Options).HasColumnType("jsonb");
    }
}