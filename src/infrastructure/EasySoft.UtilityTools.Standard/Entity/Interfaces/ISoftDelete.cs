namespace EasySoft.UtilityTools.Standard.Entity.Interfaces;

/// <summary>
/// ISoftDelete
/// </summary>
public interface ISoftDelete
{
    /// <summary>
    /// 逻辑删除标记
    /// </summary>
    int Deleted { get; set; }
}