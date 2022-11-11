using EasySoft.Core.EasyToken.Middlewares;

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