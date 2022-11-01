namespace EasySoft.Core.Domain.Base.Entities.Interfaces;

public interface IValueObject
{
    IEnumerable<object?> GetAtomicValues();

    bool Equals(object? obj);

    int GetHashCode();
}