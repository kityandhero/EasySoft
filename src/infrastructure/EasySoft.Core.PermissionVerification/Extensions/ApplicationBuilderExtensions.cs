using EasySoft.Core.PermissionVerification.Middlewares;

namespace EasySoft.Core.PermissionVerification.Extensions;

/// <summary>
/// ApplicationBuilderExtensions
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// UsePermissionVerificationMiddleware
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UsePermissionVerificationMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<PermissionVerificationMiddleware>();
    }
}