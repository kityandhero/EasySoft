using EasySoft.UtilityTools.Core;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;
using EasySoft.UtilityTools.Standard.Extensions;
using EasySoft.UtilityTools.Standard.Models;

namespace EasySoft.Core.AdvanceNLog.Assists;

public class LogConfigureAssist
{
    #region Fields

    /// <summary>
    /// NlogConfigureNode
    /// </summary>
    private const string NlogConfigureNode = "NLog";

    /// <summary>
    /// InternalLogLevelConfigureNode
    /// </summary>
    private const string InternalLogLevelConfigureNode = "InternalLogLevel";

    /// <summary>
    /// InternalLogToFileConfigureNode
    /// </summary>
    private const string InternalLogToFileConfigureNode = "InternalLogToFile";

    /// <summary>
    /// TraceToConsoleConfigureNode
    /// </summary>
    private const string TraceToConsoleConfigureNode = "TraceToConsole";

    /// <summary>
    /// TraceToFileConfigureNode
    /// </summary>
    private const string TraceToFileConfigureNode = "TraceToFile";

    /// <summary>
    /// DebugToConsoleConfigureNode
    /// </summary>
    private const string DebugToConsoleConfigureNode = "DebugToConsole";

    /// <summary>
    /// DebugToFileConfigureNode
    /// </summary>
    private const string DebugToFileConfigureNode = "DebugToFile";

    /// <summary>
    /// ProductionLogFileConfigureNode
    /// </summary>
    private const string ProductionLogToFileConfigureNode = "ProductionLogToFile";

    /// <summary>
    /// ProductionLogFileNameConfigureNode
    /// </summary>
    private const string ProductionLogFileNameConfigureNode = "ProductionLogFileName";

    /// <summary>
    /// ProductionLogLevelConfigureNode
    /// </summary>
    private const string ProductionLogLevelConfigureNode = "ProductionLogLevel";

    /// <summary>
    /// ProductionLogArchiveFileNameConfigureNode
    /// </summary>
    private const string ProductionLogArchiveFileNameConfigureNode = "ProductionLogArchiveFileName";

    /// <summary>
    /// ConsoleMessageLimitConfigureNode
    /// </summary>
    private const string ConsoleMessageLimitConfigureNode = "ConsoleMessageLimit";

    /// <summary>
    /// ConsoleLimitingWrapperConfigureNode
    /// </summary>
    private const string ConsoleLimitingWrapperConfigureNode = "ConsoleLimitingWrapper";

    /// <summary>
    /// ConsoleRepeatedFilterConfigureNode
    /// </summary>
    private const string ConsoleRepeatedFilterConfigureNode = "ConsoleRepeatedFilter";

    #endregion

    #region Properties

    public IConfiguration Configuration { get; private set; }

    #endregion

    #region Constructors

    /// <summary>
    /// LogConfigureAssist
    /// </summary>
    /// <param name="configuration"></param>
    public LogConfigureAssist(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    #endregion

    #region Methods

    #region GetInternalLogLevel

    /// <summary>
    /// 内嵌Nlog内部日志的级别, Trace|Debug|Info|Warn|Error|Fatal|Off, Off 表示关闭
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetInternalLogLevel(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                InternalLogLevelConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "Off";

        if (!v.In(
                "Trace",
                "Debug",
                "Info",
                "Warn",
                "Error",
                "Fatal",
                "Off"
            ))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 Trace|Debug|Info|Warn|Error|Fatal|Off"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 内嵌Nlog内部日志的级别, Trace|Debug|Info|Warn|Error|Fatal|Off, Off 表示关闭
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetInternalLogLevel()
    {
        return GetInternalLogLevel(Configuration);
    }

    #endregion

    #region GetInternalLogToFile

    /// <summary>
    /// 获取日志是否写入文件配置
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetInternalLogToFile(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                InternalLogToFileConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 获取日志是否写入文件配置
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetInternalLogToFile()
    {
        return GetInternalLogToFile(Configuration);
    }

    #endregion

    #region GetInternalLogFile

    /// <summary>
    /// 日志写入到的文件目录, 默认 logFiles
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetInternalLogFile(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                InternalLogLevelConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 日志写入到的文件目录, 默认 logFiles
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetInternalLogFile()
    {
        return GetInternalLogFile(Configuration);
    }

    #endregion

    #region GetTraceToConsole

    /// <summary>
    /// 是否启用 Trace 日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTraceToConsole(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                TraceToConsoleConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 是否启用 Trace 日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTraceToConsole()
    {
        return GetTraceToConsole(Configuration);
    }

    #endregion

    #region GetTraceToFile

    /// <summary>
    /// 是否启用Trace日志写入, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetTraceToFile(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                TraceToFileConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 获取 Trace 日志是否写入文件配置
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetTraceToFile()
    {
        return GetTraceToFile(Configuration);
    }

    #endregion

    #region GetDebugToConsole

    /// <summary>
    /// 是否启用 Debug 日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetDebugToConsole(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                DebugToConsoleConfigureNode
            ],
            ":"
        );

        if (GetTraceToConsole(configuration).Value)
        {
            return new ConfigValue<bool>
            {
                Node = node,
                Value = true
            };
        }

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 是否启用 Debug 日志控制台显示, 默认关闭, 使用任意自定义配置时该设置无效, 以自定义配置为准
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetDebugToConsole()
    {
        return GetDebugToConsole(Configuration);
    }

    #endregion

    #region GetDebugToFile

    /// <summary>
    /// 获取 Debug 日志是否写入文件配置
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetDebugToFile(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                DebugToFileConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 获取 Debug 日志是否写入文件配置
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetDebugToFile()
    {
        return GetDebugToFile(Configuration);
    }

    #endregion

    #region GetProductionLogToFile

    /// <summary>
    /// 生产环境日志是否写入文件配置
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetProductionLogToFile(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ProductionLogToFileConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 生产环境是否写入文件配置
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetProductionLogToFile()
    {
        return GetProductionLogToFile(Configuration);
    }

    #endregion

    #region GetProductionLogLevel

    /// <summary>
    /// 生产环境日志级别, Trace|Debug|Info|Warn|Error|Fatal
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetProductionLogLevel(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ProductionLogLevelConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "Info";

        if (!v.In(
                "Trace",
                "Debug",
                "Info",
                "Warn",
                "Error",
                "Fatal"
            ))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 Trace|Debug|Info|Warn|Error|Fata"
            );
        }

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 生产环境日志级别, Trace|Debug|Info|Warn|Error|Fatal
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetProductionLogLevel()
    {
        return GetProductionLogLevel(Configuration);
    }

    #endregion

    #region GetProductionLogFileName

    /// <summary>
    /// 生产环境日志输出目标文件
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetProductionLogFileName(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ProductionLogFileNameConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 生产环境日志输出目标文件
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetProductionLogFileName()
    {
        return GetProductionLogFileName(Configuration);
    }

    #endregion

    #region GetProductionLogArchiveFileName

    /// <summary>
    /// 生产环境日志压缩输出目标文件
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetProductionLogArchiveFileName(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ProductionLogArchiveFileNameConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "";

        return new ConfigValue<string>
        {
            Node = node,
            Value = v
        };
    }

    /// <summary>
    /// 生产环境日志压缩输出目标文件
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetProductionLogArchiveFileName()
    {
        return GetProductionLogArchiveFileName(Configuration);
    }

    #endregion

    #region GetConsoleMessageLimit

    /// <summary>
    /// 获取控制台消息条目限制
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public static ConfigValue<int> GetConsoleMessageLimit(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ConsoleMessageLimitConfigureNode
            ],
            ":"
        );

        var v = configuration[node] ?? "0";

        var errorMessage = $"[{node}]配置无效，如需要配置，请设置数字 value > 0";

        if (!v.IsInt())
        {
            throw new ConfigErrorException(
                errorMessage
            );
        }

        var valueAdjust = v.ToInt();

        if (valueAdjust < 0)
        {
            throw new ConfigErrorException(
                errorMessage
            );
        }

        return new ConfigValue<int>
        {
            Node = node,
            Value = valueAdjust
        };
    }

    /// <summary>
    /// 获取控制台消息条目限制
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">配置异常</exception>
    public ConfigValue<int> GetConsoleMessageLimit()
    {
        return GetConsoleMessageLimit(Configuration);
    }

    #endregion

    #region GetConsoleLimitingWrapper

    /// <summary>
    /// 控制台日志节流开关, 默认不启用
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetConsoleLimitingWrapper(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ConsoleLimitingWrapperConfigureNode
            ],
            ":"
        );

        if (GetTraceToConsole(configuration).Value)
        {
            return new ConfigValue<bool>
            {
                Node = node,
                Value = true
            };
        }

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 控制台日志节流开关, 默认不启用
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetConsoleLimitingWrapper()
    {
        return GetConsoleLimitingWrapper(Configuration);
    }

    #endregion

    #region GetConsoleRepeatedFilter

    /// <summary>
    /// 控制台忽略重复输出, 默认不启用, 需要前置开启 ConsoleLimitingWrapper
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetConsoleRepeatedFilter(IConfiguration configuration)
    {
        var node = StringAssist.Join(
            [
                NlogConfigureNode,
                ConsoleRepeatedFilterConfigureNode
            ],
            ":"
        );

        if (GetConsoleLimitingWrapper(configuration).Value)
        {
            return new ConfigValue<bool>
            {
                Node = node,
                Value = false
            };
        }

        if (GetTraceToConsole(configuration).Value)
        {
            return new ConfigValue<bool>
            {
                Node = node,
                Value = true
            };
        }

        var v = configuration[node] ?? "false";

        if (!v.In("true", "false"))
        {
            throw new ConfigErrorException(
                $"[{node}]配置无效，如需要配置，请设置 true|false"
            );
        }

        return new ConfigValue<bool>
        {
            Node = node,
            Value = v == "true"
        };
    }

    /// <summary>
    /// 控制台忽略重复输出, 默认不启用, 需要前置开启 ConsoleLimitingWrapper
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetConsoleRepeatedFilter()
    {
        return GetConsoleRepeatedFilter(Configuration);
    }

    #endregion

    #region GetExceptionlessTeleport

    /// <summary>
    /// GetExceptionlessTeleport
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<bool> GetExceptionlessTeleport(IConfiguration configuration)
    {
        return ConfigureNodeCollection.GetExceptionlessTeleport(configuration);
    }

    /// <summary>
    /// GetExceptionlessTeleport
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<bool> GetExceptionlessTeleport()
    {
        return GetExceptionlessTeleport(Configuration);
    }

    #endregion

    #region GetExceptionlessServerUrl

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetExceptionlessServerUrl(IConfiguration configuration)
    {
        return ConfigureNodeCollection.GetExceptionlessServerUrl(configuration);
    }

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetExceptionlessServerUrl()
    {
        return GetExceptionlessServerUrl(Configuration);
    }

    #endregion

    #region GetExceptionlessApiKey

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <param name="configuration">配置集</param>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public static ConfigValue<string> GetExceptionlessApiKey(IConfiguration configuration)
    {
        return ConfigureNodeCollection.GetExceptionlessApiKey(configuration);
    }

    /// <summary>
    /// GetExceptionlessServerUrl
    /// </summary>
    /// <exception cref="ConfigErrorException">配置异常</exception>
    public ConfigValue<string> GetExceptionlessApiKey()
    {
        return GetExceptionlessApiKey(Configuration);
    }

    #endregion

    #endregion
}