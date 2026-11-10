using Microsoft.Extensions.Configuration;

namespace EasySoft.Core.DependencyInjection.IocAssists;
// 建议改命名空间，去掉 AutoFac 字样

[Description("控制台应用的依赖注入容器创建辅助")]
public static class ConsoleServiceProviderAssist
{
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

        configureServices(services, config);

        // validateScopes: 从根容器解析 Scoped 服务时直接抛异常（对标 ASP.NET Core 开发环境默认行为）
        // validateOnBuild: BuildServiceProvider 时校验所有注册是否可构造（提前暴露"依赖未注册"问题）
        return services.BuildServiceProvider(
            new ServiceProviderOptions
            {
                ValidateScopes = validateScopes,
                ValidateOnBuild = validateOnBuild
            }
        );
    }
}