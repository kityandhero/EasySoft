namespace EasySoft.UtilityTools.Standard.Entity.Interfaces;

/// <summary>
/// 最后更新人
/// </summary>
public interface IModifyBy
{
    /// <summary>
    /// 最后更新人
    /// </summary>
    [Description("最后更新人")]
    long ModifyBy { get; set; }
}