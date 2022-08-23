using EasySoft.Core.PermissionVerification.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.PermissionVerification.ExtensionMethods;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UsePermissionVerificationMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<PermissionVerificationMiddleware>();
    }
}