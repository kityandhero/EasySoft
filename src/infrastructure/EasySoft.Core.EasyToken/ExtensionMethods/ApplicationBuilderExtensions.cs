using EasySoft.Core.EasyToken.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EasyToken.ExtensionMethods;

public static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseEasyTokenMiddleware(
        this IApplicationBuilder builder
    )
    {
        return builder.UseMiddleware<EasyTokenMiddleware>();
    }
}