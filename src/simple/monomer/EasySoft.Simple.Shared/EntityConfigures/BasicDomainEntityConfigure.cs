using EasySoft.Core.Domain.Base.Contexts.Interfaces;
using EasySoft.Core.Domain.Base.EntityConfigures.Implementations;
using EasySoft.Simple.Shared.Operations;

namespace EasySoft.Simple.Shared.EntityConfigures;

public class BasicDomainEntityConfigure : BaseDomainEntityConfigure<Operator>
{
    public BasicDomainEntityConfigure(IOperatorContext operatorContext) : base(operatorContext)
    {
    }

    protected override Operator InitializeOperator()
    {
        return new Operator();
    }
}