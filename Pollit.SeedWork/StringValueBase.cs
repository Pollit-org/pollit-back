using System;

namespace Pollit.SeedWork;

public abstract class StringValueBase : IStringValue, IEquatable<StringValueBase>
{
    protected StringValueBase(string value, bool caseSensitive = true)
    {
        Value = value;
        CaseSensitive = caseSensitive;
    }
    
    public string Value { get; }
    public bool CaseSensitive { get; }

    public override bool Equals(object? obj) 
        => obj is StringValueBase other && Equals(other);

    public override int GetHashCode() 
        => Value.GetHashCode();

    public bool Equals(StringValueBase? other)
    {
        if (other == null || GetType() != other.GetType())
            return false;

        if (!CaseSensitive || !other.CaseSensitive)
            return string.Equals(Value, other.Value, StringComparison.InvariantCultureIgnoreCase);
        
        return Value == other?.Value;
    }

    public static bool operator ==(StringValueBase? obj1, StringValueBase? obj2) 
        => obj1?.Equals(obj2) ?? Equals(obj2, null);

    public static bool operator !=(StringValueBase x, StringValueBase y)
        => !(x == y);

    public override string ToString() 
        => Value;
}