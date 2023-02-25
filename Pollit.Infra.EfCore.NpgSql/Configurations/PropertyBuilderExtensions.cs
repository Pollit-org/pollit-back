using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.UserNames;

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
            .HasMaxLength(UserName.MaxLength);
        
        return propertyBuilder;
    }
    
    public static PropertyBuilder<TEnumerable> HasDelimiterSeparatedConversion<TEnumerable, TElement>(this PropertyBuilder<TEnumerable> propertyBuilder, string delimiter, Func<TElement, string> toStringFunc, Func<string, TElement> fromStringFunc, Func<IEnumerable<TElement>, TEnumerable> enumerableFactory) where TEnumerable : IEnumerable<TElement>
    {
        propertyBuilder.HasConversion(
            prop => string.Join(delimiter, prop.Select(toStringFunc)), 
            propString => enumerableFactory(propString.Split(delimiter, StringSplitOptions.None).Select(fromStringFunc)));

        return propertyBuilder;
    }
    
    public static PropertyBuilder<HashSet<TElement>> HasHashSetDelimiterSeparatedConversion<TElement>(this PropertyBuilder<HashSet<TElement>> propertyBuilder, string delimiter, Func<TElement, string> toStringFunc, Func<string, TElement> fromStringFunc)
    {
        propertyBuilder.HasDelimiterSeparatedConversion(delimiter, toStringFunc, fromStringFunc, elements => new HashSet<TElement>(elements));

        return propertyBuilder;
    }
}