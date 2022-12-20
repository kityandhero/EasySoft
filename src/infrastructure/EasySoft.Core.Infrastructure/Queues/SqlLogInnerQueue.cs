using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.Infrastructure.Queues;

/// <summary>
/// SqlLogInnerQueue
/// </summary>
public static class SqlLogInnerQueue
{
    private static readonly ConcurrentQueue<ISqlExecutionRecord> Queue =
        new ConcurrentLimitedQueue<ISqlExecutionRecord>(10000);

    /// <summary>
    /// 批量发送时间间隔
    /// </summary>
    public static uint SendInterval { get; set; } = 6000;

    /// <summary>
    /// GetQueue
    /// </summary>
    /// <returns></returns>
    public static ConcurrentQueue<ISqlExecutionRecord> GetQueue()
    {
        return Queue;
    }

    /// <summary>
    /// Enqueue
    /// </summary>
    /// <param name="sqlExecutionRecord"></param>
    public static void Enqueue(ISqlExecutionRecord sqlExecutionRecord)
    {
        Queue.Enqueue(sqlExecutionRecord);
    }
}