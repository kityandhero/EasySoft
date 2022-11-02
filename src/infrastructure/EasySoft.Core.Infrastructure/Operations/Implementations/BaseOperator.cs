using EasySoft.Core.Infrastructure.Operations.Interfaces;

namespace EasySoft.Core.Infrastructure.Operations.Implementations;

public abstract class BaseOperator : IOperator
{
    public long Id { get; set; }

    public string Name { get; set; }

    protected BaseOperator()
    {
        Id = 0;
        Name = string.Empty;
    }
}