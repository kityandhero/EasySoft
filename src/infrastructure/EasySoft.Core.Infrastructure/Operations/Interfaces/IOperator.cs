namespace EasySoft.Core.Infrastructure.Operations.Interfaces;

/// <summary>
/// 运行时操作者, 来自与授权等信息的转换等等
/// </summary>
public interface IOperator
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
}