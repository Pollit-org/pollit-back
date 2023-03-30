using System;
using System.Globalization;

namespace Pollit.SeedWork;

public abstract class DateTimeValueBase : IDateTimeValue, IEquatable<DateTimeValueBase>
{
    protected DateTimeValueBase(DateTime value)
    {
        Value = value;
    }
    
    public DateTime Value { get; }

    public override bool Equals(object? obj) 
        => obj is DateTimeValueBase other && Equals(other);

    public override int GetHashCode() 
        => Value.GetHashCode();

    public bool Equals(DateTimeValueBase? other)
    {
        if (other == null || GetType() != other.GetType())
            return false;

        return Value == other?.Value;
    }

    public static bool operator ==(DateTimeValueBase? obj1, DateTimeValueBase? obj2) 
        => obj1?.Equals(obj2) ?? Equals(obj2, null);

    public static bool operator !=(DateTimeValueBase? x, DateTimeValueBase? y)
        => !(x == y);

    public override string ToString() 
        => Value.ToString(CultureInfo.InvariantCulture);
}