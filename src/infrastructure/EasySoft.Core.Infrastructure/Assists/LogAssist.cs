using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.Logging;

namespace EasySoft.Core.Infrastructure.Assists;

public static class LogAssist
{
    private static ILogger? _logger;

    public static void SetLogger(ILogger logger)
    {
        if (_logger != null)
        {
            throw new Exception("logger has been set, it disallow set more than once.");
        }

        _logger = logger;
    }

    #region string

    public static ILogger GetLogger()
    {
        if (_logger == null)
        {
            throw new Exception("logger has not set yet");
        }

        return _logger;
    }

    /// <summary>
    /// 记录一般信息
    /// </summary>
    public static void Info(string? log)
    {
        if (string.IsNullOrWhiteSpace(log))
        {
            return;
        }

        GetLogger().LogInformation(" {Log}", log.Trim());
    }

    /// <summary>
    /// 记录一般信息集合 【输出多条】
    /// </summary>
    public static void Info(params string?[] logs)
    {
        Info(logs.ToList());
    }

    /// <summary>
    /// 记录一般信息集合 【输出多条】
    /// </summary>
    public static void Info(IEnumerable<string?> logs)
    {
        logs.ToListFilterNullOrWhiteSpace().ForEach(Info);
    }

    /// <summary>
    /// 记录调试信息
    /// </summary>
    public static void Debug(string? log)
    {
        if (string.IsNullOrWhiteSpace(log))
        {
            return;
        }

        GetLogger().LogDebug("{Log}", log.Trim());
    }

    /// <summary>
    /// 记录一般信息集合 【输出多条】
    /// </summary>
    public static void Debug(params string?[] logs)
    {
        Debug(logs.ToList());
    }

    /// <summary>
    /// 记录一般信息集合 【输出多条】
    /// </summary>
    public static void Debug(IEnumerable<string?> logs)
    {
        logs.ToListFilterNullOrWhiteSpace().ForEach(Debug);
    }

    /// <summary>
    /// 记录重要信息
    /// </summary>
    public static void Critical(string? log)
    {
        if (string.IsNullOrWhiteSpace(log))
        {
            return;
        }

        GetLogger().LogCritical("{Log}", log.Trim());
    }

    /// <summary>
    /// 记录重要信息集合 【输出多条】
    /// </summary>
    public static void Critical(params string?[] logs)
    {
        Critical(logs.ToList());
    }

    /// <summary>
    /// 记录重要信息集合 【输出多条】
    /// </summary>
    public static void Critical(IEnumerable<string?> logs)
    {
        logs.ToListFilterNullOrWhiteSpace().ForEach(Critical);
    }

    /// <summary>
    /// 记录警告信息
    /// </summary>
    public static void Warning(string? log)
    {
        if (string.IsNullOrWhiteSpace(log))
        {
            return;
        }

        GetLogger().LogWarning(" {Log}", log.Trim());
    }

    /// <summary>
    /// 记录警告信息集合 【输出多条】
    /// </summary>
    public static void Warning(params string?[] logs)
    {
        Warning(logs.ToList());
    }

    /// <summary>
    /// 记录警告信息集合 【输出多条】
    /// </summary>
    public static void Warning(IEnumerable<string?> logs)
    {
        logs.ToListFilterNullOrWhiteSpace().ForEach(Warning);
    }

    /// <summary>
    /// 记录跟踪信息
    /// </summary>
    public static void Trace(string? log)
    {
        if (string.IsNullOrWhiteSpace(log))
        {
            return;
        }

        GetLogger().LogTrace("{Log}", log.Trim());
    }

    /// <summary>
    /// 记录跟踪信息集合 【输出多条】
    /// </summary>
    public static void Trace(params string?[] logs)
    {
        Trace(logs.ToList());
    }

    /// <summary>
    /// 记录跟踪信息集合 【输出多条】
    /// </summary>
    public static void Trace(IEnumerable<string?> logs)
    {
        logs.ToListFilterNullOrWhiteSpace().ForEach(Trace);
    }

    /// <summary>
    /// 记录错误信息
    /// </summary>
    public static void Error(string? log)
    {
        if (string.IsNullOrWhiteSpace(log))
        {
            return;
        }

        GetLogger().LogError("{Log}", log.Trim());
    }

    /// <summary>
    /// 记录错误信息集合 【输出多条】
    /// </summary>
    public static void Error(params string?[] logs)
    {
        Error(logs.ToList());
    }

    /// <summary>
    /// 记录错误信息集合 【输出多条】
    /// </summary>
    public static void Error(IEnumerable<string?> logs)
    {
        logs.ToListFilterNullOrWhiteSpace().ForEach(Error);
    }

    #endregion

    #region object

    /// <summary>
    /// 记录一般数据 【序列化输出】
    /// </summary>
    public static void InfoData(object? log)
    {
        if (log == null)
        {
            return;
        }

        GetLogger().LogInformation(" {Log}", JsonConvertAssist.Serialize(log));
    }

    /// <summary>
    /// 记录一般数据集合 【序列化输出多条】
    /// </summary>
    public static void InfoData(IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(InfoData);
    }

    /// <summary>
    /// 记录调试数据 【序列化输出】
    /// </summary>
    public static void DebugData(object? log)
    {
        if (log == null)
        {
            return;
        }

        GetLogger().LogDebug("{Log}", JsonConvertAssist.Serialize(log));
    }

    /// <summary>
    /// 记录调试数据集合 【序列化输出多条】
    /// </summary>
    public static void DebugData(IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(DebugData);
    }

    /// <summary>
    /// 记录重要数据 【序列化输出】
    /// </summary>
    public static void CriticalData(object? log)
    {
        if (log == null)
        {
            return;
        }

        GetLogger().LogCritical("{Log}", JsonConvertAssist.Serialize(log));
    }

    /// <summary>
    /// 记录重要数据集合 【序列化输出多条】
    /// </summary>
    public static void CriticalData(IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(CriticalData);
    }

    /// <summary>
    /// 记录警告数据 【序列化输出】
    /// </summary>
    public static void WarningData(object? log)
    {
        if (log == null)
        {
            return;
        }

        GetLogger().LogWarning(" {Log}", JsonConvertAssist.Serialize(log));
    }

    /// <summary>
    /// 记录警告数据集合 【序列化输出多条】
    /// </summary>
    public static void WarningData(IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(WarningData);
    }

    /// <summary>
    /// 记录跟踪数据
    /// </summary>
    public static void TraceData(object? log)
    {
        if (log == null)
        {
            return;
        }

        GetLogger().LogTrace("{Log}", JsonConvertAssist.Serialize(log));
    }

    /// <summary>
    /// 记录跟踪数据集合 【序列化输出多条】
    /// </summary>
    public static void TraceData(IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(TraceData);
    }

    /// <summary>
    /// 记录错误数据
    /// </summary>
    public static void ErrorData(object? log)
    {
        if (log == null)
        {
            return;
        }

        GetLogger().LogError("{Log}", JsonConvertAssist.Serialize(log));
    }

    /// <summary>
    /// 记录错误数据集合 【序列化输出多条】
    /// </summary>
    public static void ErrorData(IEnumerable<object> logs)
    {
        logs.ToListFilterNullable().ForEach(ErrorData);
    }

    #endregion
}