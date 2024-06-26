﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Users;
using Pollit.Domain.Users.EncryptedPasswords;
using Pollit.Domain.Users.UserNames;

namespace Pollit.Infra.EfCore.NpgSql.Configurations.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasDefaultValueSql("gen_random_uuid ()").IsRequired();

        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.UserName);
        
        builder.Property(u => u.EncryptedPassword)
            .HasConversion(encryptedPassword => encryptedPassword.ToString(), encryptedPasswordString => EncryptedPassword.Parse(encryptedPasswordString))
            .HasMaxLength(1024);

        builder.Property(u => u.Id)
            .HasConversion(id => id.Value, id => new UserId(id));

        builder.Property(u => u.Email).HasEmailConversion();

        builder.Property(u => u.UserName).HasStringValueConversion(str => new UserName(str), UserName.MaxLength);
        
        builder.Property(u => u.RefreshTokens).HasHashSetDelimiterSeparatedConversion(" ", token => token.ToString(), s => new RefreshToken(s));
        
        builder.Property(u => u.GoogleProfile).HasColumnType("jsonb");

        builder.Property(u => u.Birthdate).HasNullableBirthdateConversion();
        
        builder.Property(u => u.EmailVerificationToken).HasConversion(t => t.Value, t => new EmailVerificationToken(t));
        
        builder.HasMany(u => u.ResetPasswordLinks).WithOne();
    }
}