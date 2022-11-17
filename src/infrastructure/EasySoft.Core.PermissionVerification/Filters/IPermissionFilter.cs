namespace EasySoft.Core.PermissionVerification.Filters;

/// <summary>
/// IPermissionFilter
/// </summary>
public interface IPermissionFilter : IActionFilter
{
    /// <summary>
    /// AdjustAccessPermission
    /// </summary>
    /// <param name="filterContext"></param>
    void AdjustAccessPermission(ActionExecutingContext filterContext);
}