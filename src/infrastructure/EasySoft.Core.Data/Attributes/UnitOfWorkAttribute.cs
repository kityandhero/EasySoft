namespace EasySoft.Core.Data.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class UnitOfWorkAttribute : Attribute
{
    /// <summary>
    ///     需要把事务共享给CAP
    /// </summary>
    public bool SharedToCap { get; set; }
}