using System;

namespace Pollit.SeedWork;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public sealed class ValueObjectEqualityIgnoreMemberAttribute : Attribute
{
}