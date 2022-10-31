namespace EasySoft.Core.Infrastructure.Entities.Interfaces;

public interface ISoftDelete
{
    /// <summary>
    /// 逻辑删除标记
    /// </summary>
    bool IsDeleted { get; set; }
}