using EasySoft.Core.Domain.Base.Entities.Interfaces;

namespace EasySoft.Core.Domain.Base.Entities.implementations;

public abstract class BaseValueObject : IValueObject
{
    protected static bool EqualOperator(IValueObject left, IValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null)) return false;

        return ReferenceEquals(left, null) || left.Equals(right);
    }

    protected static bool NotEqualOperator(IValueObject left, IValueObject right)
    {
        return !EqualOperator(left, right);
    }

    public abstract IEnumerable<object?> GetAtomicValues();

    public override bool Equals(object? obj)
    {
        if (obj == null || obj.GetType() != GetType()) return false;

        var other = (IValueObject)obj;

        using var thisValues = GetAtomicValues().GetEnumerator();
        using var otherValues = other.GetAtomicValues().GetEnumerator();

        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null)) return false;

            if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current)) return false;
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    public override int GetHashCode()
    {
        return GetAtomicValues()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}