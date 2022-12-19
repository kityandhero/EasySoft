namespace EasySoft.Core.PermissionVerification.Filters;

/// <summary>
/// PermissionFilter
/// </summary>
public class PermissionFilter : PermissionCoreFilter
{
    /// <summary>
    /// PermissionFilter
    /// </summary>
    /// <param name="loggerFactory"></param>
    /// <param name="environment"></param>
    /// <param name="mediator"></param>
    public PermissionFilter(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment,
        IMediator mediator
    ) : base(loggerFactory, environment, mediator)
    {
    }
}