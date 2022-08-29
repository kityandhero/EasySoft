using EasySoft.UtilityTools.Core.Results;
using EasySoft.UtilityTools.Standard.Result;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.PermissionVerification.Officers;

public interface IOperateOfficer
{
    void AdjustAccessPermission(HttpContext httpContext);

    ExecutiveResult<ApiResult> DoVerification(HttpContext httpContext);
}