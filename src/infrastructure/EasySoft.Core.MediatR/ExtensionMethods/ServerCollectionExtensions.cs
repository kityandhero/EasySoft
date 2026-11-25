using EasySoft.UtilityTools.Core.Configures;

namespace EasySoft.Core.MediatR.ExtensionMethods;

/// <summary>
/// ServerCollectionExtensions
/// </summary>
public static class ServerCollectionExtensions
{
    /// <summary>
    /// 注入 Mapster
    /// </summary>
    /// <param name="services">服务集</param>
    /// <returns>服务集</returns>
    public static IServiceCollection AddAdvanceMediatR(
        this IServiceCollection services
    )
    {
        services.AddMediatR(mediatRServiceConfiguration =>
            {
                mediatRServiceConfiguration.RegisterServicesFromAssemblies(MediatRConfigure.GetAssemblies().ToArray());
            }
        );

        return services;
    }
}