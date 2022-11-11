namespace EasySoft.Core.EventBus.Cap.Filters;

public class DefaultCapFilter : SubscribeFilter
{
    private readonly ILogger<DefaultCapFilter> _logger;

    public DefaultCapFilter(ILogger<DefaultCapFilter> logger)
    {
        _logger = logger;
    }

    public override void OnSubscribeExecuting(ExecutingContext context)
    {
    }

    public override void OnSubscribeExecuted(ExecutedContext context)
    {
    }

    public override void OnSubscribeException(ExceptionContext context)
    {
        _logger.LogError("{Log}", context.Exception.Message);
    }
}