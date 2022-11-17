namespace EasySoft.Core.PermissionVerification.Officers;

/// <summary>
/// IOperateOfficer
/// </summary>
public interface IOperateOfficer
{
    /// <summary>
    /// AdjustAccessPermission
    /// </summary>
    /// <param name="httpContext"></param>
    void AdjustAccessPermission(HttpContext httpContext);

    /// <summary>
    /// DoVerification
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    ExecutiveResult<ApiResult> DoVerification(HttpContext httpContext);
}