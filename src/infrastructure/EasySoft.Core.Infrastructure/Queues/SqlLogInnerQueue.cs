using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.Infrastructure.Queues;

/// <summary>
/// SqlLogInnerQueue
/// </summary>
public static class SqlLogInnerQueue
{
    private static readonly ConcurrentQueue<ISqlLogTransitiveMessage> Queue =
        new ConcurrentLimitedQueue<ISqlLogTransitiveMessage>(10000);

    /// <summary>
    /// 批量发送时间间隔
    /// </summary>
    public static uint SendInterval { get; set; } = 6000;

    /// <summary>
    /// GetQueue
    /// </summary>
    /// <returns></returns>
    public static ConcurrentQueue<ISqlLogTransitiveMessage> GetQueue()
    {
        return Queue;
    }

    /// <summary>
    /// Enqueue
    /// </summary>
    /// <param name="sqlLogTransitiveMessage
    public static void Enqueue(ISqlLogTransitiveMessage sqlLogTransitiveMessage)
    {
        Queue.Enqueue(sqlLogTransitiveMessage);
    }
}