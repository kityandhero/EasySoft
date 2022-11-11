namespace EasySoft.Core.PermissionVerification.Officers;

public interface IOperateOfficer
{
    void AdjustAccessPermission(HttpContext httpContext);

    ExecutiveResult<ApiResult> DoVerification(HttpContext httpContext);
}