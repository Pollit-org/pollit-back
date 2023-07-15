using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Infra.EfCore.NpgSql.Projections.Polls;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Polls.Projections;

public class PollResultsRawItemConfiguration : IEntityTypeConfiguration<PollResultsRawItem>
{
    public void Configure(EntityTypeBuilder<PollResultsRawItem> builder)
    {
        builder.HasNoKey().ToView(null);
    }
}