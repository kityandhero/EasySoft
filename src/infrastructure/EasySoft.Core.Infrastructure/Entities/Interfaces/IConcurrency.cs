namespace EasySoft.Core.Infrastructure.Entities.Interfaces;

public interface IConcurrency
{
    /// <summary>
    /// 并发控制列
    /// </summary>
    public byte[] RowVersion { get; set; }
}