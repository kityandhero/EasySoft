using EasySoft.UtilityTools.Standard.Entities.Interfaces;

namespace EasySoft.Core.Infrastructure.Queues;

/// <summary>
/// SqlLogInnerQueue
/// </summary>
public static class SqlLogInnerQueue
{
    private static readonly ConcurrentQueue<ISqlExecutionRecord> _queue =
        new ConcurrentLimitedQueue<ISqlExecutionRecord>(10000);

    /// <summary>
    /// GetQueue
    /// </summary>
    /// <returns></returns>
    public static ConcurrentQueue<ISqlExecutionRecord> GetQueue()
    {
        return _queue;
    }

    /// <summary>
    /// Enqueue
    /// </summary>
    /// <param name="sqlExecutionRecord"></param>
    public static void Enqueue(ISqlExecutionRecord sqlExecutionRecord)
    {
        _queue.Enqueue(sqlExecutionRecord);
    }
}