namespace EasySoft.Core.TransceiverCore.Channels;

/// <summary>
/// 自适应空轮退避，用于 Channel 消费循环，降低长期空队列 CPU 周转
/// </summary>
internal sealed class AdaptiveBackoff
{
    private int _step;
    private readonly int _maxMs;

    /// <summary>
    /// 自适应空轮退避
    /// </summary>
    /// <param name="maxMs"></param>
    public AdaptiveBackoff(int maxMs = 64)
    {
        _maxMs = maxMs;
    }

    /// <summary>
    /// 读到消息，重置退避
    /// </summary>
    public void Reset()
    {
        _step = 0;
    }

    /// <summary>
    /// 空轮，获取本次休眠时长
    /// </summary>
    public int GetSleepMs()
    {
        var ms = 1 << _step;
        if (ms >= _maxMs)
        {
            return _maxMs;
        }

        _step++;
        return ms;
    }

    /// <summary>
    /// 执行休眠
    /// </summary>
    public Task Sleep(CancellationToken ct)
    {
        return Task.Delay(GetSleepMs(), ct);
    }
}