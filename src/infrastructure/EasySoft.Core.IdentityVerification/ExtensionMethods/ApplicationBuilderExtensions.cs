using EasySoft.Core.IdentityVerification.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.IdentityVerification.ExtensionMethods;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseIdentityVerificationMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<IdentityVerificationMiddleware>();
    }
}