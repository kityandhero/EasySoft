using EasySoft.Core.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.Infrastructure.Assists;

/// <summary>
/// 控制台应用的依赖注入容器创建辅助
/// </summary>
[Description("控制台应用的依赖注入容器创建辅助")]
public static class ConsoleServiceProviderAssist
{
    /// <summary>
    /// CreateServiceProvider
    /// </summary>
    /// <param name="configureServices"></param>
    /// <param name="validateScopes"></param>
    /// <param name="validateOnBuild"></param>
    /// <returns></returns>
    public static ServiceProvider CreateServiceProvider(
        Action<IServiceCollection, IConfiguration> configureServices,
        bool validateScopes = true,
        bool validateOnBuild = true
    )
    {
        // 构建 IConfiguration
        var config = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                false,
                false
            )
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production"}.json",
                true
            )
            .AddEnvironmentVariables()
            .Build();

        var services = new ServiceCollection();

        services.AddSingleton<IConfiguration>(config);

        services.AddInfrastructureConfigureAssist(config);

        configureServices(services, config);

        // validateScopes: 从根容器解析 Scoped 服务时直接抛异常（对标 ASP.NET Core 开发环境默认行为）
        // validateOnBuild: BuildServiceProvider 时校验所有注册是否可构造（提前暴露"依赖未注册"问题）
        var serviceProvider = services.BuildServiceProvider(
            new ServiceProviderOptions
            {
                ValidateScopes = validateScopes,
                ValidateOnBuild = validateOnBuild
            }
        );

        ServiceProviderAssist.SetProvider(serviceProvider);

        return serviceProvider;
    }
}