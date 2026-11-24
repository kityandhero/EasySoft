namespace EasySoft.Core.AdvanceTrace.Interceptors;

/// <summary>
/// 同步包装拦截器，兼容旧代码 IInterceptor 入口。
/// 内部复用标准异步拦截器实现，不重复编写拦截逻辑。
/// </summary>
public sealed class TraceRecordInterceptor : IInterceptor
{
    private readonly TraceRecordAsyncInterceptor _asyncInterceptor;

    /// <summary>
    /// TraceRecordInterceptor
    /// </summary>
    /// <param name="asyncInterceptor">标准异步拦截器实例</param>
    public TraceRecordInterceptor(TraceRecordAsyncInterceptor asyncInterceptor)
    {
        _asyncInterceptor = asyncInterceptor;
    }

    public void Intercept(IInvocation invocation)
    {
        _asyncInterceptor.ToInterceptor().Intercept(invocation);
    }
}