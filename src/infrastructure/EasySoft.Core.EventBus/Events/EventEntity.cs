namespace EasySoft.Core.EventBus.Events;

[Serializable]
public abstract class EventEntity<TData> where TData : class
{
    protected EventEntity()
    {
        Data = default!;
    }

    protected EventEntity(long id, TData data, string source)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Id = id;
        Data = data;
        // ReSharper disable once VirtualMemberCallInConstructor
        EventSource = source;
    }

    /// <summary>
    /// 事件Id
    /// </summary>
    public virtual long Id { get; set; }

    /// <summary>
    /// 事件发生的时间
    /// </summary>
    public virtual DateTime OccurredDate { get; set; } = DateTimeOffset.Now.DateTime;

    /// <summary>
    /// 触发事件的方法
    /// </summary>
    public virtual string EventSource { get; set; } = string.Empty;

    /// <summary>
    /// 处理事件的方法
    /// </summary>
    public virtual string EventTarget { get; set; } = string.Empty;

    /// <summary>
    /// 事件数据
    /// </summary>
    public TData Data { get; set; }
}