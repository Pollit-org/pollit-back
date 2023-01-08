using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users;

namespace Pollit.Infra.EfCore.NpgSql.Configurations;

public static class PropertyBuilderExtensions
{
    public static PropertyBuilder<Email> HasEmailConversion(this PropertyBuilder<Email> propertyBuilder)
    {
        propertyBuilder
            .HasConversion(email => email.ToString(), emailString => new Email(emailString))
            .HasMaxLength(Email.MaxLength);
        
        return propertyBuilder;
    }
    
    public static PropertyBuilder<UserName> HasUserNameConversion(this PropertyBuilder<UserName> propertyBuilder)
    {
        propertyBuilder
            .HasConversion(userName => userName.ToString(), userNameString => new UserName(userNameString))
            .HasMaxLength(UserNameMustNotBeTooLongRule.MaxLength);
        
        return propertyBuilder;
    }
}