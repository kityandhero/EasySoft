using EasySoft.Core.JsonWebToken.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.JsonWebToken.ExtensionMethods;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseJsonWebTokenMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<JsonWebTokenMiddleware>();
    }
}