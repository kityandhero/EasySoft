using EasySoft.UtilityTools.Standard.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EasySoft.Core.Infrastructure.ExtensionMethods;

public static class HttpContextExtensions
{
    public static RequestInfo BuildRequestInfo(this HttpContext httpContext)
    {
        return httpContext.Request.BuildRequestInfo();
    }
}