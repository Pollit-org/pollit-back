using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Shared.Email;

namespace Pollit.Infra.EfCore.NpgSql.Configurations;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<Email> HasEmailConversion(this PropertyBuilder<Email> propertyBuilder)
    {
        propertyBuilder
            .HasConversion(email => email.ToString(), emailString => new Email(emailString))
            .HasMaxLength(Email.EmailMaxLength);
        
        return propertyBuilder;
    }
}