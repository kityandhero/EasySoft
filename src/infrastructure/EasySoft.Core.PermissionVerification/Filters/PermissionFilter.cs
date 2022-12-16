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
    public PermissionFilter(
        ILoggerFactory loggerFactory,
        IWebHostEnvironment environment
    ) : base(loggerFactory, environment)
    {
    }
}