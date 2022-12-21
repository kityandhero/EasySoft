namespace EasySoft.Core.PermissionVerification.Officers.Interfaces;

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
    Task<ExecutiveResult<ApiResult>> DoVerificationAsync(HttpContext httpContext);
}