namespace EasySoft.Core.Mapster.ExtensionMethods;

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
    public static IServiceCollection AddAdvanceMapster(
        this IServiceCollection services
    )
    {
        services.AddSingleton<IMapper, Mapper>();

        return services;
    }
}