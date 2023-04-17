using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Application.Comments.GetCommentsOfAPoll;
using Pollit.Infra.EfCore.NpgSql.Projections.Comments;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Comments.Projections;

public class GetCommentsOfAPollRawResultItemConfiguration : IEntityTypeConfiguration<GetCommentsOfAPollRawResultItem>
{
    public void Configure(EntityTypeBuilder<GetCommentsOfAPollRawResultItem> builder)
    {
        builder.HasNoKey().ToView(null);
    }
}