namespace EasySoft.Core.Web.Framework.ExtensionMethods;

/// <summary>
/// ServiceCollectionExtensions
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 启用压缩
    /// </summary>
    /// <returns></returns>
    internal static IServiceCollection AddAdvanceResponseCompression(
        this IServiceCollection services
    )
    {
        services.AddResponseCompression();

        ApplicationConfigurator.AddWebApplicationExtraAction(
            new ExtraAction<WebApplication>()
                .SetName("")
                .SetAction(
                    application => { application.UseResponseCompression(); }
                )
        );

        return services;
    }
}