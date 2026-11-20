namespace EasySoft.Core.AdvanceFileRecord;

/// <summary>
/// IFileRecorderMetrics 空实现，默认使用
/// 所有方法无任何操作，避免不需要监控时额外开销
/// </summary>
public sealed class NullFileRecorderMetrics : IFileRecorderMetrics
{
    /// <inheritdoc />
    public void IncrementWriteSuccess()
    {
    }

    /// <inheritdoc />
    public void IncrementSerializeFailed()
    {
    }

    /// <inheritdoc />
    public void IncrementFlush()
    {
    }

    /// <inheritdoc />
    public void IncrementFlushFailed()
    {
    }

    /// <inheritdoc />
    public void IncrementCleanDeleteFile()
    {
    }

    /// <inheritdoc />
    public void IncrementCleanError()
    {
    }

    /// <inheritdoc />
    public void IncrementDiscardByDiskPressure()
    {
    }
}