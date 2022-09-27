namespace EasySoft.Core.EntityFramework.Configures;

public static class ContextConfigure
{
    /// <summary>
    /// 出于性能原因，EF Core 不会在 try-catch 块中包装每个调用以从数据库提供程序读取值。 但是，这有时会导致难以诊断的异常，尤其是当数据库在模型不允许的情况下返回 NULL 时. 
    /// 仅应在开发环境下开启
    /// </summary>
    public static bool EnableDetailedErrors { get; set; }

    /// <summary>
    /// 敏感数据日志, 仅应在开发环境下开启
    /// </summary>
    public static bool EnableSensitiveDataLogging { get; set; }

    static ContextConfigure()
    {
        EnableDetailedErrors = false;
        EnableSensitiveDataLogging = false;
    }
}