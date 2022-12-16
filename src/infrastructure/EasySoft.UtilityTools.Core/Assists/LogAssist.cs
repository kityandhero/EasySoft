using EasySoft.UtilityTools.Core.Extensions;

namespace EasySoft.UtilityTools.Core.Assists;

/// <summary>
/// LogAssist
/// </summary>
public static class LogAssist
{
    private static ILogger? _logger;

    /// <summary>
    /// SetLogger
    /// </summary>
    /// <param name="logger"></param>
    /// <exception cref="Exception"></exception>
    public static void SetLogger(ILogger logger)
    {
        if (_logger != null) throw new Exception("logger has been set, it disallow set more than once.");

        _logger = logger;
    }

    #region string

    /// <summary>
    /// GetLogger
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static ILogger GetLogger()
    {
        if (_logger == null) throw new Exception("logger has not set yet");

        return _logger;
    }

    #region Common

    #region Info

    /// <summary>
    /// 记录一般信息
    /// </summary>
    public static void Info(string? log)
    {
        if (string.IsNullOrWhiteSpace(log)) return;

        GetLogger().LogAdvanceInfo(log);
    }

    /// <summary>
    /// 记录一般信息集合 【输出多条】
    /// </summary>
    public static void Info(params string?[] logs)
    {
        GetLogger().LogAdvanceInfo(logs);
    }

    /// <summary>
    /// 记录一般信息集合 【输出多条】
    /// </summary>
    public static void Info(IEnumerable<string?> logs)
    {
        GetLogger().LogAdvanceInfo(logs);
    }

    #endregion

    #region Debug

    /// <summary>
    /// 记录调试信息
    /// </summary>
    public static void Debug(string? log)
    {
        GetLogger().LogAdvanceDebug(log);
    }

    /// <summary>
    /// 记录调试信息集合 【输出多条】
    /// </summary>
    public static void Debug(params string?[] logs)
    {
        GetLogger().LogAdvanceDebug(logs);
    }

    /// <summary>
    /// 记录调试信息集合 【输出多条】
    /// </summary>
    public static void Debug(IEnumerable<string?> logs)
    {
        GetLogger().LogAdvanceDebug(logs);
    }

    #endregion

    #region Critical

    /// <summary>
    /// 记录重要信息
    /// </summary>
    public static void Critical(string? log)
    {
        GetLogger().LogAdvanceCritical(log);
    }

    /// <summary>
    /// 记录重要信息集合 【输出多条】
    /// </summary>
    public static void Critical(params string?[] logs)
    {
        GetLogger().LogAdvanceCritical(logs);
    }

    /// <summary>
    /// 记录重要信息集合 【输出多条】
    /// </summary>
    public static void Critical(IEnumerable<string?> logs)
    {
        GetLogger().LogAdvanceCritical(logs);
    }

    #endregion

    #region Warning

    /// <summary>
    /// 记录警告信息
    /// </summary>
    public static void Warning(string? log)
    {
        GetLogger().LogAdvanceWarning(log);
    }

    /// <summary>
    /// 记录警告信息集合 【输出多条】
    /// </summary>
    public static void Warning(params string?[] logs)
    {
        GetLogger().LogAdvanceWarning(logs);
    }

    /// <summary>
    /// 记录警告信息集合 【输出多条】
    /// </summary>
    public static void Warning(IEnumerable<string?> logs)
    {
        GetLogger().LogAdvanceWarning(logs);
    }

    #endregion

    #region Trace

    /// <summary>
    /// 记录跟踪信息
    /// </summary>
    public static void Trace(string? log)
    {
        GetLogger().LogAdvanceTrace(log);
    }

    /// <summary>
    /// 记录跟踪信息集合 【输出多条】
    /// </summary>
    public static void Trace(params string?[] logs)
    {
        GetLogger().LogAdvanceTrace(logs);
    }

    /// <summary>
    /// 记录跟踪信息集合 【输出多条】
    /// </summary>
    public static void Trace(IEnumerable<string?> logs)
    {
        GetLogger().LogAdvanceTrace(logs);
    }

    #endregion

    /// <summary>
    /// 记录错误信息
    /// </summary>
    public static void Error(string? log)
    {
        GetLogger().LogAdvanceError(log);
    }

    /// <summary>
    /// 记录错误信息集合 【输出多条】
    /// </summary>
    public static void Error(params string?[] logs)
    {
        GetLogger().LogAdvanceError(logs);
    }

    /// <summary>
    /// 记录错误信息集合 【输出多条】
    /// </summary>
    public static void Error(IEnumerable<string?> logs)
    {
        GetLogger().LogAdvanceError(logs);
    }

    #endregion

    #region Special

    /// <summary>
    /// 记录调试时的说明信息
    /// </summary>
    public static void Execute(string? log, bool supplementRoundBracket = false)
    {
        GetLogger().LogAdvanceExecute(log, supplementRoundBracket);
    }

    /// <summary>
    /// 记录调试时的说明信息
    /// </summary>
    public static void Prompt(string? log)
    {
        GetLogger().LogAdvancePrompt(log);
    }

    /// <summary>
    /// 记录调试时的配置信息
    /// </summary>
    public static void Hint(params string[] logs)
    {
        GetLogger().LogAdvanceHint(logs);
    }

    #endregion

    #endregion

    #region object

    /// <summary>
    /// 记录一般数据 【序列化输出】
    /// </summary>
    public static void InfoData(object? log, string prefix = "")
    {
        GetLogger().InfoData(log, prefix);
    }

    /// <summary>
    /// 记录一般数据集合 【序列化输出多条】
    /// </summary>
    public static void InfoData(IEnumerable<object> logs)
    {
        GetLogger().InfoData(logs);
    }

    /// <summary>
    /// 记录调试数据 【序列化输出】
    /// </summary>
    public static void DebugData(object? log, string prefix = "")
    {
        GetLogger().DebugData(log, prefix);
    }

    /// <summary>
    /// 记录调试数据集合 【序列化输出多条】
    /// </summary>
    public static void DebugData(IEnumerable<object> logs)
    {
        GetLogger().DebugData(logs);
    }

    /// <summary>
    /// 记录重要数据 【序列化输出】
    /// </summary>
    public static void CriticalData(object? log, string prefix = "")
    {
        GetLogger().CriticalData(log, prefix);
    }

    /// <summary>
    /// 记录重要数据集合 【序列化输出多条】
    /// </summary>
    public static void CriticalData(IEnumerable<object> logs)
    {
        GetLogger().CriticalData(logs);
    }

    /// <summary>
    /// 记录警告数据 【序列化输出】
    /// </summary>
    public static void WarningData(object? log, string prefix = "")
    {
        GetLogger().WarningData(log, prefix);
    }

    /// <summary>
    /// 记录警告数据集合 【序列化输出多条】
    /// </summary>
    public static void WarningData(IEnumerable<object> logs)
    {
        GetLogger().WarningData(logs);
    }

    /// <summary>
    /// 记录跟踪数据
    /// </summary>
    public static void TraceData(object? log, string prefix = "")
    {
        GetLogger().TraceData(log, prefix);
    }

    /// <summary>
    /// 记录跟踪数据集合 【序列化输出多条】
    /// </summary>
    public static void TraceData(IEnumerable<object> logs)
    {
        GetLogger().TraceData(logs);
    }

    /// <summary>
    /// 记录错误数据
    /// </summary>
    public static void ErrorData(object? log, string prefix = "")
    {
        GetLogger().ErrorData(log, prefix);
    }

    /// <summary>
    /// 记录错误数据集合 【序列化输出多条】
    /// </summary>
    public static void ErrorData(IEnumerable<object> logs)
    {
        GetLogger().ErrorData(logs);
    }

    #endregion
}