using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Users;
using Pollit.Domain.Users.EncryptedPasswords;

namespace Pollit.Infra.SqlServer.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid ()").IsRequired();

        builder.HasIndex(u => u.Email);
        
        builder.Property(u => u.EncryptedPassword)
            .HasConversion(encryptedPassword => encryptedPassword.ToString(), encryptedPasswordString => EncryptedPassword.Parse(encryptedPasswordString))
            .HasMaxLength(1024);
    }
}