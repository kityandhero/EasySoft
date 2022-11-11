using EasySoft.Core.PermissionVerification.Middlewares;

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