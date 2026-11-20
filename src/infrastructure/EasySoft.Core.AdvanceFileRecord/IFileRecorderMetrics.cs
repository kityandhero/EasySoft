namespace EasySoft.Core.AdvanceFileRecord;

/// <summary>
/// 文件记录器运行指标收集接口
/// 所有指标为增量计数，仅用于观测、排查异常，不影响主写入链路
/// </summary>
public interface IFileRecorderMetrics
{
    /// <summary>
    /// 成功写入一条记录
    /// </summary>
    void IncrementWriteSuccess();

    /// <summary>
    /// 对象序列化失败
    /// </summary>
    void IncrementSerializeFailed();

    /// <summary>
    /// 缓冲区执行一次刷盘落地
    /// </summary>
    void IncrementFlush();

    /// <summary>
    /// 刷盘IO异常
    /// </summary>
    void IncrementFlushFailed();

    /// <summary>
    /// 目录自维护：成功删除一个旧文件
    /// </summary>
    void IncrementCleanDeleteFile();

    /// <summary>
    /// 目录自维护过程发生异常
    /// </summary>
    void IncrementCleanError();

    /// <summary>
    /// 因磁盘剩余空间不足，记录被丢弃
    /// </summary>
    void IncrementDiscardByDiskPressure();
}