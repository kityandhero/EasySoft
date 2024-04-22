namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 渠道标记
/// </summary>
public interface IChannelSymbol
{
    /// <summary>
    /// 渠道码
    /// </summary>
    [Description("渠道码")]
    string Channel { get; set; }
}