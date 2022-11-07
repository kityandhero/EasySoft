namespace EasySoft.Core.EntityFramework.Configures;

public static class ContextConfigure
{
    /// <summary>
    /// 出于性能原因，EF Core 不会在 try-catch 块中包装每个调用以从数据库提供程序读取值。 但是，这有时会导致难以诊断的异常，尤其是当数据库在模型不允许的情况下返回 NULL 时. 
    /// 仅在开发环境下可用
    /// </summary>
    public static bool EnableDetailedErrors { get; set; }

    /// <summary>
    /// 敏感数据日志, 仅在开发环境下可用
    /// </summary>
    public static bool EnableSensitiveDataLogging { get; set; }

    /// <summary>
    /// 自动迁移, 不能与 AutoEnsureCreated 同时启用, 仅在开发环境下可用
    /// </summary>
    public static bool AutoMigrate { get; set; }

    /// <summary>
    /// 不存在时自动创建数据结构（可能需要赋予账户权限）, 不能与 AutoMigrate 同时启用, 仅在开发环境下可用
    /// </summary>
    public static bool AutoEnsureCreated { get; set; }

    static ContextConfigure()
    {
        EnableDetailedErrors = false;
        EnableSensitiveDataLogging = false;
        AutoMigrate = false;
        AutoEnsureCreated = false;
    }
}