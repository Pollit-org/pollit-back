using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Users.ResetPasswordLinks;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Users;

public class ResetPasswordLinkConfiguration : IEntityTypeConfiguration<ResetPasswordLink>
{
    public void Configure(EntityTypeBuilder<ResetPasswordLink> builder)
    {
        builder.ToTable("Users.ResetPasswordLinks");
        
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).ValueGeneratedNever().IsRequired();
        builder.Property(u => u.Id).HasConversion(id => id.Value, id => new ResetPasswordLinkId(id));

        builder.Property(x => x.Token).HasConversion(t => t.Value, t => new PasswordResetToken(t));
    }
}