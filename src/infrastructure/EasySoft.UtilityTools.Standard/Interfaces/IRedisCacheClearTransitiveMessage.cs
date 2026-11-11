namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// Redis缓存清理传输中转消息定义（队列传递专用）
/// </summary>
public interface IRedisCacheClearTransitiveMessage : IRedisCacheClear, IQueueTransitiveMessage
{
}