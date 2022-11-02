using EasySoft.Core.Domain.Base.Contexts.Interfaces;

namespace EasySoft.Core.Domain.Base.Contexts.Implementations;

/// <summary>
/// 操作者上下文
/// </summary>
public abstract class BaseOperatorContext : IOperatorContext
{
    /// <summary>
    /// 操作者标识
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>  
    public string Name { get; set; }

    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 设备信息
    /// </summary>
    public string Device { get; set; }

    /// <summary>
    /// 操作地址
    /// </summary>
    public string RemoteIpAddress { get; set; }

    protected BaseOperatorContext()
    {
        Id = 0;
        Name = string.Empty;
        Email = string.Empty;
        Device = string.Empty;
        RemoteIpAddress = string.Empty;
    }
}