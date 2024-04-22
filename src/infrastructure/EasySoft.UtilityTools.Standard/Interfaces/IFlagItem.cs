namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// IFlagItem
/// </summary>
public interface IFlagItem
{
    /// <summary>
    /// Key  
    /// </summary>
    string Key { get; set; }

    /// <summary>
    /// 标记值
    /// </summary>
    string Flag { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    string Name { get; set; }

    /// <summary>
    /// 可用性
    /// </summary>
    int Availability { get; set; }
}