namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 驻留消息变动
/// </summary>
public interface IHostServiceChangeTransitiveMessage : IHostService, IQueueTransitiveMessage
{
    /// <summary>
    /// 变动类型
    /// </summary>
    public int ChangeType { get; set; }
}