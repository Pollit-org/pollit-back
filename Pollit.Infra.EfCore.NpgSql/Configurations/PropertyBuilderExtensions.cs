using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pollit.Domain.Shared.Email;
using Pollit.Domain.Users.Birthdates;
using Pollit.Domain.Users.UserNames;
using Pollit.SeedWork;

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

    public static PropertyBuilder<T> HasStringValueConversion<T>(this PropertyBuilder<T> propertyBuilder, Func<string, T> conversionFromString, int maxLength) where T : IStringValue
    {
        propertyBuilder
            .HasConversion(userName => userName.ToString(), str => conversionFromString(str))
            .HasMaxLength(maxLength);
        
        return propertyBuilder;
    }
    
    public static PropertyBuilder<Birthdate> HasBirthdateConversion(this PropertyBuilder<Birthdate> propertyBuilder)
    {
        propertyBuilder
            .HasConversion(birthdate => birthdate.ToString(), birthdateString => Birthdate.Parse(birthdateString))
            .HasMaxLength(UserName.MaxLength);
        
        return propertyBuilder;
    }
    
    public static PropertyBuilder<Birthdate?> HasNullableBirthdateConversion(this PropertyBuilder<Birthdate?> propertyBuilder)
    {
        propertyBuilder
            .HasConversion(birthdate => birthdate!.ToString(), birthdateString => string.IsNullOrWhiteSpace(birthdateString) ? null : Birthdate.Parse(birthdateString))
            .HasMaxLength(11)
            .IsRequired(false);

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