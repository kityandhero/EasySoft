namespace EasySoft.Core.Data.Attributes;

/// <summary>
/// 工作单元标记
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class UnitOfWorkAttribute : Attribute
{
    /// <summary>
    ///     需要把事务共享给CAP
    /// </summary>
    public bool SharedToCap { get; set; }
}