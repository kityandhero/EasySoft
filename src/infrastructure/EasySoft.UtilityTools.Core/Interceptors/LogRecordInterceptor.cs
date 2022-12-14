namespace EasySoft.UtilityTools.Core.Interceptors;

/// <summary>
///     日志记录拦截器
/// </summary>
public sealed class LogRecordInterceptor : IInterceptor
{
    private readonly LogRecordAsyncInterceptor _logRecordAsyncInterceptor;

    /// <summary>
    /// 日志记录拦截器
    /// </summary>
    /// <param name="logRecordAsyncInterceptor"></param>
    public LogRecordInterceptor(LogRecordAsyncInterceptor logRecordAsyncInterceptor)
    {
        _logRecordAsyncInterceptor = logRecordAsyncInterceptor;
    }

    /// <summary>
    /// 拦截
    /// </summary>
    /// <param name="invocation"></param>
    public void Intercept(IInvocation invocation)
    {
        _logRecordAsyncInterceptor.ToInterceptor().Intercept(invocation);
    }
}