namespace EasySoft.Core.EasyCaching.Entities;

internal class SlidingWrapper<T>
{
    public SlidingWrapper()
    {
        SlidingTime = TimeSpan.Zero;
        Value = default;
    }

    public T? Value { get; set; }

    public TimeSpan SlidingTime { get; set; }
}