using Microsoft.AspNetCore.Builder;

namespace EasySoft.Core.EntityFramework.ExtensionMethods;

public static class WebApplicationExtensions
{
    internal static WebApplication UseAdvanceMigrationsEndPoint(
        this WebApplication application
    )
    {
        if (EnvironmentAssist.GetEnvironment().IsDevelopment()) application.UseMigrationsEndPoint();

        return application;
    }

    /// <summary>
    /// 使用自动迁移, 仅在开发环境下可用
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplication UseAutoMigrate(
        this WebApplication application
    )
    {
        if (!EnvironmentAssist.GetEnvironment().IsDevelopment()) return application;

        if (ContextConfigure.AutoMigrate && ContextConfigure.AutoEnsureCreated)
            throw new Exception(" 不能同时启用AutoMigrate, AutoEnsureCreated");

        if (!ContextConfigure.AutoMigrate) return application;

        LogAssist.Execute($"{nameof(UseAutoMigrate)}().");

        LogAssist.Hint(
            $"{typeof(ContextConfigure).FullName}.{nameof(ContextConfigure.AutoMigrate)} is {ContextConfigure.AutoMigrate}."
        );

        var context = ServiceAssist.GetServiceProvider().GetService<DbContext>();

        context?.Database.Migrate();

        return application;
    }

    /// <summary>
    /// 使用自动创建, 仅在开发环境下可用
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static WebApplication UseAutoEnsureCreated(
        this WebApplication application
    )
    {
        if (!EnvironmentAssist.GetEnvironment().IsDevelopment()) return application;

        if (ContextConfigure.AutoMigrate && ContextConfigure.AutoEnsureCreated)
            throw new Exception(" 不能同时启用AutoMigrate, AutoEnsureCreated");

        if (!ContextConfigure.AutoEnsureCreated) return application;

        LogAssist.Execute($"{nameof(UseAutoEnsureCreated)}().");

        LogAssist.Hint(
            $"{typeof(ContextConfigure).FullName}.{nameof(ContextConfigure.AutoEnsureCreated)} is {ContextConfigure.AutoEnsureCreated}."
        );

        var context = ServiceAssist.GetServiceProvider().GetService<DbContext>();

        context?.Database.EnsureCreated();

        return application;
    }
}