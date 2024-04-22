namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 队列消息存储
/// </summary>
public interface IQueueMessageStore : IQueueMessage, ITriggerChannel, IChannelStore, IIdString, IStatus, IIp, IOperate
{
    /// <summary>
    /// 获取标识
    /// </summary>
    /// <returns></returns>
    public string GetId();

    /// <summary>
    /// 获取标识键名
    /// </summary>
    /// <returns></returns>
    public string GetIdentificationName();
}