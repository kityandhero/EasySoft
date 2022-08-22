using EasySoft.Core.Infrastructure.Results;
using EasySoft.UtilityTools.Result;
using Microsoft.AspNetCore.Http;

namespace EasySoft.Core.IdentityVerification.Officers;

public interface IOperateOfficer
{
    void AdjustAccessPermission(HttpContext httpContext);

    ExecutiveResult<ApiResult> DoAuthorization(HttpContext httpContext);
}