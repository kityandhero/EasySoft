using EasySoft.UtilityTools.Core.Extensions;
using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.UtilityTools.Core.Interceptors;

/// <summary>
///     日志记录拦截器
/// </summary>
public sealed class LogRecordAsyncInterceptor : IAsyncInterceptor
{
    private readonly ILoggerFactory _loggerFactory;

    /// <summary>
    /// 日志记录拦截器
    /// </summary>
    /// <param name="loggerFactory"></param>
    public LogRecordAsyncInterceptor(ILoggerFactory loggerFactory)
    {
        _loggerFactory = loggerFactory;
    }

    /// <summary>
    ///     同步拦截器
    /// </summary>
    /// <param name="invocation"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void InterceptSynchronous(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronousWithoutUnitOfWork(invocation);
    }

    /// <summary>
    ///     异步拦截器 无返回值
    /// </summary>
    /// <param name="invocation"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void InterceptAsynchronous(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronousWithoutUnitOfWork(invocation);
    }

    /// <summary>
    ///     异步拦截器 有返回值
    /// </summary>
    /// <param name="invocation"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <exception cref="NotImplementedException"></exception>
    public void InterceptAsynchronous<TResult>(IInvocation invocation)
    {
        invocation.ReturnValue = InternalInterceptAsynchronousWithoutUnitOfWork<TResult>(invocation);
    }

    /// <summary>
    ///     异步拦截器无事务处理-无返回值
    /// </summary>
    /// <param name="invocation"></param>
    /// <returns></returns>
    private async Task InternalInterceptAsynchronousWithoutUnitOfWork(IInvocation invocation)
    {
        var attribute = GetCustomAttribute(invocation);

        if (attribute != null)
        {
            _loggerFactory
                .CreateLogger<LogRecordAsyncInterceptor>()
                .LogAdvanceExecute($"{invocation.TargetType.Name}.{invocation.Method.Name}");
        }

        invocation.Proceed();

        var task = (Task)invocation.ReturnValue;

        await task;
    }

    /// <summary>
    ///     异步拦截器无事务处理-有返回值
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="invocation"></param>
    /// <returns></returns>
    private async Task<TResult> InternalInterceptAsynchronousWithoutUnitOfWork<TResult>(IInvocation invocation)
    {
        var attribute = GetCustomAttribute(invocation);

        if (attribute != null)
        {
            _loggerFactory
                .CreateLogger<LogRecordAsyncInterceptor>()
                .LogAdvanceExecute($"{invocation.TargetType.Name}.{invocation.Method.Name}");
        }

        invocation.Proceed();

        var task = (Task<TResult>)invocation.ReturnValue;

        var result = await task;

        return result;
    }

    /// <summary>
    ///     获取拦截器 attribute
    /// </summary>
    /// <param name="invocation"></param>
    /// <returns></returns>  
    private static LogRecordAttribute? GetCustomAttribute(IInvocation invocation)
    {
        var methodInfo = invocation.Method ?? invocation.MethodInvocationTarget;
        var attribute = methodInfo.GetCustomAttribute<LogRecordAttribute>();

        return attribute;
    }
}