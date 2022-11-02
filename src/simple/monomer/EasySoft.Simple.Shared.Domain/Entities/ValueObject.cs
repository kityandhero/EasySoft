using EasySoft.Core.Domain.Base.Entities.Implementations;

namespace EasySoft.Simple.Shared.Domain.Entities;

public class ValueObject : BaseValueObject
{
    public override IEnumerable<object?> GetAtomicValues()
    {
        throw new NotImplementedException();
    }
}