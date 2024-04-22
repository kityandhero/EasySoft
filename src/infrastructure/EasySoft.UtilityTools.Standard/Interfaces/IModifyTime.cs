namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 最后更新时间
/// </summary>
public interface IModifyTime
{
    /// <summary>
    /// 最后更新时间
    /// </summary>
    [Description("最后更新时间")]
    DateTime ModifyTime { get; set; }
}