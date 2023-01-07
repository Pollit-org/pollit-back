namespace Pollit.SeedWork;

public abstract class IdValueBase : IIdValue, IEquatable<IdValueBase>
{
    public Guid Value { get; }

    protected IdValueBase(string value) 
        => Value = new Guid(value);
    
    protected IdValueBase(Guid value) 
        => Value = value;

    public override bool Equals(object? obj) 
        => obj is IdValueBase other && Equals(other);

    public override int GetHashCode() 
        => Value.GetHashCode();

    public bool Equals(IdValueBase? other)
    {
        if (other == null || GetType() != other.GetType())
            return false;
        
        return Value == other?.Value;
    }

    public static bool operator ==(IdValueBase? obj1, IdValueBase? obj2) 
        => obj1?.Equals(obj2) ?? Equals(obj2, null);

    public static bool operator !=(IdValueBase x, IdValueBase y)
        => !(x == y);

    public override string ToString()
        => Value.ToString();
}