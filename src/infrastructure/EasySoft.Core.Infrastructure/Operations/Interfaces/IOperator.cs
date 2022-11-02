namespace EasySoft.Core.Infrastructure.Operations.Interfaces;

/// <summary>
/// 运行时操作者, 来自与授权等信息的转换等等
/// </summary>
public interface IOperator
{
    public long Id { get; set; }

    public string Name { get; set; }
}