namespace EasySoft.Core.EventBus.Cap.Filters;

/// <summary>
/// CAP订阅消息过滤器（CAP10版本，方法名带Async后缀）
/// 订阅执行前、执行完成、异常捕获回调
/// </summary>
public class DefaultCapFilter : SubscribeFilter
{
    private readonly ILogger<DefaultCapFilter> _logger;

    /// <summary>
    /// 构造注入日志
    /// </summary>
    /// <param name="logger">日志记录器</param>
    public DefaultCapFilter(ILogger<DefaultCapFilter> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// 订阅方法执行之前触发
    /// </summary>
    /// <param name="context">执行上下文</param>
    /// <returns></returns>
    public override Task OnSubscribeExecutingAsync(ExecutingContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 订阅方法正常执行完成后触发
    /// </summary>
    /// <param name="context">执行完成上下文</param>
    /// <returns></returns>
    public override Task OnSubscribeExecutedAsync(ExecutedContext context)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// 订阅方法抛出异常时触发
    /// </summary>
    /// <param name="context">异常上下文</param>
    /// <returns></returns>
    public override Task OnSubscribeExceptionAsync(ExceptionContext context)
    {
        // 传入Exception对象，日志输出完整堆栈，便于排查
        _logger.LogError(
            context.Exception,
            "CAP订阅消费异常：{Message}",
            context.Exception.Message
        );
        return Task.CompletedTask;
    }
}