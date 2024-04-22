namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 触发渠道
/// </summary>
public interface ITriggerChannel
{
    /// <summary>
    /// 触发渠道码
    /// </summary>
    [Description("触发渠道码")]
    public string TriggerChannel { get; set; }
}