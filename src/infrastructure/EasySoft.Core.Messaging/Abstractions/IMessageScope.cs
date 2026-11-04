namespace EasySoft.Core.Messaging.Abstractions;

/// <summary>
/// 观测作用域。
/// </summary>
/// <remarks>
/// 显式区分成功/失败，而不是靠 Dispose 推断，拦截器能把异常精确传给 scope，接入 OTel 或自建埋点时不用猜测。
/// </remarks>
public interface IMessageScope : IAsyncDisposable
{
    /// <summary>标记本次调用成功。在 <see cref="IAsyncDisposable.DisposeAsync"/> 前调用。</summary>
    void MarkSuccess();

    /// <summary>标记本次调用失败。在抛出或吞掉异常前调用。</summary>
    /// <param name="ex">导致失败的异常。</param>
    void MarkFailure(Exception ex);
}