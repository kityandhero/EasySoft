using EasySoft.UtilityTools.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.IdentityVerification.Officers;

public interface IOperateOfficer
{
    void AdjustAccessPermission(HttpContext httpContext);

    ExecutiveResult<JsonResult> DoAuthorization(HttpContext httpContext);
}