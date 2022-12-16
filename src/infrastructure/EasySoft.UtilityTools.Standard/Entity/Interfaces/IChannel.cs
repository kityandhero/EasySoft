namespace EasySoft.UtilityTools.Standard.Entity.Interfaces;

/// <summary>
/// IChannel
/// </summary>
public interface IChannel
{
    /// <summary>
    /// 渠道码
    /// </summary>
    [Description("渠道码")]
    int Channel { get; set; }
}