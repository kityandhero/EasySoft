namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// ISubsidiaryPure
/// </summary>
public interface ISubsidiaryPure
{
    /// <summary>
    /// 缓存键
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// 简称
    /// </summary>
    string ShortName { get; set; }
}