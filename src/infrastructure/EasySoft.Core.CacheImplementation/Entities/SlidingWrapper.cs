namespace EasySoft.Core.CacheImplementation.Entities;

internal class SlidingWrapper<T>
{
    /// <summary>
    /// SlidingWrapper
    /// </summary>
    public SlidingWrapper()
    {
        SlidingTime = TimeSpan.Zero;
        Value = default;
    }

    public T Value { get; set; }

    public TimeSpan SlidingTime { get; set; }
}