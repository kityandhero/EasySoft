namespace EasySoft.UtilityTools.Core.Extensions;

/// <summary>
/// ServiceProviderExtensions
/// </summary>
public static class ServiceProviderExtensions
{
    /// <summary>
    /// 判断服务是否已注册
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>是否已注册</returns>
    public static bool IsRegistered<T>(this IServiceProvider serviceProvider)
    {
        return serviceProvider.GetService<IServiceProviderIsService>()?.IsService(typeof(T)) ?? false;
    }
}