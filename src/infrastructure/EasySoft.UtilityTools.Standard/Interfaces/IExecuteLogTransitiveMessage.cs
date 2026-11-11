namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 执行日志中转消息定义（队列传递专用）
/// </summary>
public interface IExecuteLogTransitiveMessage : IExecuteLog, IQueueTransitiveMessage
{
}