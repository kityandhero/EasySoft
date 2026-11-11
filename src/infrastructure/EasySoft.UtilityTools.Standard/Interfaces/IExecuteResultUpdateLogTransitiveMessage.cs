namespace EasySoft.UtilityTools.Standard.Interfaces;

/// <summary>
/// 执行结果更新日志中转消息定义（队列传递专用）
/// </summary>
public interface IExecuteResultUpdateLogTransitiveMessage : IExecuteResultUpdateLog, IQueueTransitiveMessage
{
}