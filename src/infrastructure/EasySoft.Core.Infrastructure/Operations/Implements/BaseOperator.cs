using EasySoft.Core.Infrastructure.Operations.Interfaces;

namespace EasySoft.Core.Infrastructure.Operations.Implements;

/// <summary>
/// BaseOperator
/// </summary>
public abstract class BaseOperator : IOperator
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// BaseOperator
    /// </summary>
    protected BaseOperator()
    {
        Id = 0;
        Name = string.Empty;
    }
}