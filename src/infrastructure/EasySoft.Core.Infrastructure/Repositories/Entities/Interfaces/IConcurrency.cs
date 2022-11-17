namespace EasySoft.Core.Infrastructure.Repositories.Entities.Interfaces;

/// <summary>
/// IConcurrency
/// </summary>
public interface IConcurrency
{
    /// <summary>
    /// 并发控制列
    /// </summary>
    public byte[] RowVersion { get; set; }
}