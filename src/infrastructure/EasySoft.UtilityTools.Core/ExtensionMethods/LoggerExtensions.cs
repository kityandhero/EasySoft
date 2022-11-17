using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.UtilityTools.Core.ExtensionMethods;

/// <summary>
/// LoggerExtensions
/// </summary>
public static class LoggerExtensions
{
    #region Info

    /// <summary>
    /// 记录一般信息
    /// </summary>
    public static void LogAdvanceInfo(this ILogger logger, params string?[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(o =>
        {
            if (string.IsNullOrWhiteSpace(o)) return;

            logger.LogInformation("{Log}", o.Trim());
        });
    }

    /// <summary>
    /// 记录一般信息
    /// </summary>
    public static void LogAdvanceInfo(this ILogger logger, IEnumerable<string?> logs)
    {
        logger.LogAdvanceInfo(logs.ToArray());
    }

    #endregion

    #region Debug

    /// <summary>
    /// 记录调试信息集
    /// </summary>
    public static void LogAdvanceDebug(this ILogger logger, params string?[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(o =>
        {
            if (string.IsNullOrWhiteSpace(o)) return;

            logger.LogDebug("{Log}", o.Trim());
        });
    }

    /// <summary>
    /// 记录调试信息集合 【输出多条】
    /// </summary>
    public static void LogAdvanceDebug(this ILogger logger, IEnumerable<string?> logs)
    {
        logger.LogAdvanceDebug(logs.ToArray());
    }

    #endregion

    #region Critical

    /// <summary>
    /// 记录重要信息
    /// </summary>
    public static void LogAdvanceCritical(this ILogger logger, params string?[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(o =>
        {
            if (string.IsNullOrWhiteSpace(o)) return;

            logger.LogCritical("{Log}", o.Trim());
        });
    }

    /// <summary>
    /// 记录重要信息集合 【输出多条】
    /// </summary>
    public static void LogAdvanceCritical(this ILogger logger, IEnumerable<string?> logs)
    {
        logger.LogAdvanceCritical(logs.ToArray());
    }

    #endregion

    #region Warning

    /// <summary>
    /// 记录警告信息
    /// </summary>
    public static void LogAdvanceWarning(this ILogger logger, params string?[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(o =>
        {
            if (string.IsNullOrWhiteSpace(o)) return;

            logger.LogWarning("{Log}", o.Trim());
        });
    }

    /// <summary>
    /// 记录警告信息集合 【输出多条】
    /// </summary>
    public static void LogAdvanceWarning(this ILogger logger, IEnumerable<string?> logs)
    {
        logger.LogAdvanceWarning(logs.ToArray());
    }

    #endregion

    #region Trace

    /// <summary>
    /// 记录跟踪信息
    /// </summary>
    public static void LogAdvanceTrace(this ILogger logger, params string?[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(o =>
        {
            if (string.IsNullOrWhiteSpace(o)) return;

            logger.LogTrace("{Log}", o.Trim());
        });
    }

    /// <summary>
    /// 记录跟踪信息集合 【输出多条】
    /// </summary>
    public static void LogAdvanceTrace(this ILogger logger, IEnumerable<string?> logs)
    {
        logger.LogAdvanceTrace(logs.ToArray());
    }

    #endregion

    #region Error

    /// <summary>
    /// 记录错误信息
    /// </summary>
    public static void LogAdvanceError(this ILogger logger, params string?[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(o =>
        {
            if (string.IsNullOrWhiteSpace(o)) return;

            logger.LogError("{Log}", o.Trim());
        });
    }

    /// <summary>
    /// 记录错误信息集合 【输出多条】
    /// </summary>
    public static void LogAdvanceError(this ILogger logger, IEnumerable<string?> logs)
    {
        logger.LogAdvanceError(logs.ToArray());
    }

    #endregion

    #region Execute

    /// <summary>
    /// 记录调试时的说明信息
    /// </summary>
    public static void LogAdvanceExecute(this ILogger logger, string? log, bool supplementRoundBracket = false)
    {
        if (string.IsNullOrWhiteSpace(log)) return;

        logger.LogAdvanceTrace(
            $"EXEC: {log.Trim()}{(supplementRoundBracket ? "()" : "")}"
        );
    }

    #endregion

    #region Prompt

    /// <summary>
    /// 记录调试时的说明信息
    /// </summary>
    public static void LogAdvancePrompt(this ILogger logger, string? log)
    {
        if (string.IsNullOrWhiteSpace(log)) return;

        logger.LogAdvanceTrace($"DESC: {log.Trim()}");
    }

    #endregion

    #region Hint

    /// <summary>
    /// 记录调试时的配置信息
    /// </summary>
    public static void LogAdvanceHint(this ILogger logger, params string[] logs)
    {
        if (!logs.Any()) return;

        logs.ForEach(log =>
        {
            if (!string.IsNullOrWhiteSpace(log)) logger.LogAdvanceTrace($"HINT: {log.Trim()}");
        });
    }

    #endregion

    #region object

    /// <summary>
    /// 记录一般数据 【序列化输出】
    /// </summary>
    public static void InfoData(this ILogger logger, object? log, string prefix = "")
    {
        if (log == null) return;

        logger.LogInformation(
            "{Prefix}{Log}",
            prefix,
            JsonConvertAssist.Serialize(log)
        );
    }

    /// <summary>
    /// 记录一般数据集合 【序列化输出多条】
    /// </summary>
    public static void InfoData(this ILogger logger, IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(log => { logger.InfoData(log); });
    }

    /// <summary>
    /// 记录调试数据 【序列化输出】
    /// </summary>
    public static void DebugData(this ILogger logger, object? log, string prefix = "")
    {
        if (log == null) return;

        logger.LogDebug(
            "{Prefix}{Log}",
            prefix,
            JsonConvertAssist.Serialize(log)
        );
    }

    /// <summary>
    /// 记录调试数据集合 【序列化输出多条】
    /// </summary>
    public static void DebugData(this ILogger logger, IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(log => { logger.DebugData(log); });
    }

    /// <summary>
    /// 记录重要数据 【序列化输出】
    /// </summary>
    public static void CriticalData(this ILogger logger, object? log, string prefix = "")
    {
        if (log == null) return;

        logger.LogCritical(
            "{Prefix}{Log}",
            prefix,
            JsonConvertAssist.Serialize(log)
        );
    }

    /// <summary>
    /// 记录重要数据集合 【序列化输出多条】
    /// </summary>
    public static void CriticalData(this ILogger logger, IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(log => { logger.CriticalData(log); });
    }

    /// <summary>
    /// 记录警告数据 【序列化输出】
    /// </summary>
    public static void WarningData(this ILogger logger, object? log, string prefix = "")
    {
        if (log == null) return;

        logger.LogWarning(
            "{Prefix}{Log}",
            prefix,
            JsonConvertAssist.Serialize(log)
        );
    }

    /// <summary>
    /// 记录警告数据集合 【序列化输出多条】
    /// </summary>
    public static void WarningData(this ILogger logger, IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(log => { logger.WarningData(log); });
    }

    /// <summary>
    /// 记录跟踪数据
    /// </summary>
    public static void TraceData(this ILogger logger, object? log, string prefix = "")
    {
        if (log == null) return;

        logger.LogTrace(
            "{Prefix}{Log}",
            prefix,
            JsonConvertAssist.Serialize(log)
        );
    }

    /// <summary>
    /// 记录跟踪数据集合 【序列化输出多条】
    /// </summary>
    public static void TraceData(this ILogger logger, IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(log => { logger.TraceData(log); });
    }

    /// <summary>
    /// 记录错误数据
    /// </summary>
    public static void ErrorData(this ILogger logger, object? log, string prefix = "")
    {
        if (log == null) return;

        logger.LogError(
            "{Prefix}{Log}",
            prefix,
            JsonConvertAssist.Serialize(log)
        );
    }

    /// <summary>
    /// 记录错误数据集合 【序列化输出多条】
    /// </summary>
    public static void ErrorData(this ILogger logger, IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(log => { logger.ErrorData(log); });
    }

    #endregion

    #region Middleware

    /// <summary>
    /// LogMiddlewareInvokeAsyncBefore
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="logs"></param>
    /// <typeparam name="TMiddleware"></typeparam>
    public static void LogMiddlewareInvokeAsyncBefore<TMiddleware>(
        this ILogger logger,
        IWebHostEnvironment environment,
        params string?[] logs
    ) where TMiddleware : IMiddleware
    {
        if (!environment.IsDevelopment()) return;

        logger.LogAdvanceExecute($"{typeof(TMiddleware).Name} InvokeAsync Before");

        logger.LogAdvanceTrace(logs);
    }

    /// <summary>
    /// LogMiddlewareInvokeAsyncAfter
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="environment"></param>
    /// <param name="logs"></param>
    /// <typeparam name="TMiddleware"></typeparam>
    public static void LogMiddlewareInvokeAsyncAfter<TMiddleware>(
        this ILogger logger,
        IWebHostEnvironment environment,
        params string?[] logs
    ) where TMiddleware : IMiddleware
    {
        if (!environment.IsDevelopment()) return;

        logger.LogAdvanceExecute($"{typeof(TMiddleware).Name} InvokeAsync After");

        logger.LogAdvanceTrace(logs);
    }

    #endregion
}