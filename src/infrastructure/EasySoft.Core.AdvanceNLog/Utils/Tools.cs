using EasySoft.Core.AdvanceNLog.Assists;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.AdvanceNLog.Utils;

/// <summary>
/// Tools
/// </summary>
public static class Tools
{
    /// <summary>
    /// GetNlogEmbedConfig
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="whetherDevelop">是否开发环境</param>
    /// <returns></returns>
    public static string GetNlogEmbedConfig(
        IConfiguration configuration,
        bool whetherDevelop
    )
    {
        var mainConfig = GetNlogDefaultMainConfig();

        mainConfig = mainConfig
            .SupplementThrowConfigExceptions(whetherDevelop)
            .SupplementInternalLogLevel(configuration)
            .SupplementInternalLogFile(configuration)
            .SupplementGeneralRule(configuration)
            .SupplementColorConsoleConfig(configuration, whetherDevelop)
            .SupplementProductionLogConfig(configuration)
            .SupplementExceptionlessConfig(configuration);

        return mainConfig;
    }

    private static string GetNlogDefaultMainConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.main.config.txt");
    }

    private static string GetNlogDefaultDebugRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.debug.rule.txt");
    }

    private static string GetNlogDefaultDebugTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.debug.target.txt");
    }

    /// <summary>
    /// GetNlogDefaultTraceRuleConfig
    /// </summary>
    /// <returns></returns>
    public static string GetNlogDefaultTraceRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.trace.rule.txt");
    }

    private static string GetNlogDefaultTraceTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.trace.target.txt");
    }

    private static string GetNlogDefaultExceptionlessExtensionsConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.exceptionless-extensions.txt");
    }

    private static string GetNlogDefaultExceptionlessRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.exceptionless.rule.txt");
    }

    private static string GetNlogDefaultExceptionlessTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.exceptionless.target.txt");
    }

    private static string GetNlogWordHighlightingRules()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.word.highlighting.rules.txt");
    }

    private static string GetNlogConsoleFilter()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.filter.txt");
    }

    private static string GetNlogConsoleFilterRepeated()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.filter.repeated.txt");
    }

    private static string GetNlogConsoleLimitingWrapper()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.limiting.wrapper.txt");
    }

    private static string GetNlogConsoleTarget()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.target.txt");
    }

    private static string SupplementThrowConfigExceptions(
        this string config,
        bool whetherDevelop
    )
    {
        return config
            .Replace(
                "###throw-config-exceptions###",
                whetherDevelop
                    ? "true"
                    : "false"
            );
    }

    private static string SupplementInternalLogLevel(
        this string config,
        IConfiguration configuration
    )
    {
        var internalLogLevelConfigValue = LogConfigureAssist.GetInternalLogLevel(configuration);

        StartupConfigMessageAssist.AddConfig(
            internalLogLevelConfigValue.ToArrowDescription()
        );

        return config
            .Replace(
                "###internal-log-level###",
                internalLogLevelConfigValue.Value
            );
    }

    private static string SupplementInternalLogFile(
        this string config,
        IConfiguration configuration
    )
    {
        var internalLogToFileConfigValue = LogConfigureAssist.GetInternalLogToFile(configuration);

        StartupConfigMessageAssist.AddConfig(
            internalLogToFileConfigValue.ToArrowDescription()
        );

        var logToFileSwitch = internalLogToFileConfigValue.Value;

        var internalLogFileConfigValue = LogConfigureAssist.GetInternalLogFile(configuration);

        StartupConfigMessageAssist.AddConfig(
            internalLogFileConfigValue.ToArrowDescription()
        );

        var logFile = internalLogFileConfigValue.Value;

        return config
            .Replace(
                "###internal-log-file###",
                logToFileSwitch && !string.IsNullOrWhiteSpace(logFile)
                    ? $"\"logFiles\":\"{logFile}\","
                    : ""
            );
    }

    private static string SupplementProductionLogConfig(
        this string config,
        IConfiguration configuration
    )
    {
        var productionLogToFileConfigValue = LogConfigureAssist.GetProductionLogToFile(configuration);

        StartupConfigMessageAssist.AddConfig(
            productionLogToFileConfigValue.ToArrowDescription()
        );

        var nlogEmbedConfigProductionLogToToTFileSwitch = productionLogToFileConfigValue.Value;

        if (!nlogEmbedConfigProductionLogToToTFileSwitch)
        {
            return config.Replace(
                    "###production-file-target###",
                    ""
                )
                .Replace(
                    "###production-file-rule###",
                    ""
                );
        }

        var productionLogFileNameConfigValue = LogConfigureAssist.GetProductionLogFileName(configuration);

        StartupConfigMessageAssist.AddConfig(
            productionLogFileNameConfigValue.ToArrowDescription()
        );

        var productionLogArchiveFileNameConfigValue = LogConfigureAssist.GetProductionLogArchiveFileName(configuration);

        StartupConfigMessageAssist.AddConfig(
            productionLogArchiveFileNameConfigValue.ToArrowDescription()
        );

        var target = GetEmbeddedResourceFileContent("/nlog.production.file.target.txt")
            .Replace(
                "###file-name###",
                productionLogFileNameConfigValue.Value
            )
            .Replace(
                "###archive-file-name###",
                productionLogArchiveFileNameConfigValue.Value
            );

        var productionLogLevelConfigValue = LogConfigureAssist.GetProductionLogLevel(configuration);

        StartupConfigMessageAssist.AddConfig(
            productionLogLevelConfigValue.ToArrowDescription()
        );

        var rule = GetEmbeddedResourceFileContent("/nlog.production.file.rule.txt")
            .Replace(
                "###level###",
                productionLogLevelConfigValue.Value
            );

        return config
            .Replace(
                "###production-file-target###",
                target
            )
            .Replace(
                "###production-file-rule###",
                rule
            );
    }

    private static string SupplementGeneralRule(
        this string config,
        IConfiguration configuration
    )
    {
        var debugToFileConfigValue = LogConfigureAssist.GetDebugToFile(configuration);

        StartupConfigMessageAssist.AddConfig(
            debugToFileConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigDebugToFileSwitch = debugToFileConfigValue.Value;

        var traceToFileConfigValue = LogConfigureAssist.GetTraceToFile(configuration);

        StartupConfigMessageAssist.AddConfig(
            traceToFileConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigTraceToFileSwitch = traceToFileConfigValue.Value;

        var debugRule = nlogDefaultConfigDebugToFileSwitch ? GetNlogDefaultDebugRuleConfig() : "";
        var debugTarget = nlogDefaultConfigDebugToFileSwitch ? GetNlogDefaultDebugTargetConfig() : "";

        debugRule = string.IsNullOrWhiteSpace(debugRule) ? "" : $"{debugRule},";
        debugTarget = string.IsNullOrWhiteSpace(debugTarget) ? "" : $"{debugTarget},";

        var traceRule = nlogDefaultConfigTraceToFileSwitch ? GetNlogDefaultTraceRuleConfig() : "";
        var traceTarget = nlogDefaultConfigTraceToFileSwitch ? GetNlogDefaultTraceTargetConfig() : "";

        traceRule = string.IsNullOrWhiteSpace(traceRule) ? "" : $"{traceRule},";
        traceTarget = string.IsNullOrWhiteSpace(traceTarget) ? "" : $"{traceTarget},";

        return config
            .Replace("###trace-target###", traceTarget)
            .Replace("###debug-target###", debugTarget)
            .Replace("###trace-rule###", traceRule)
            .Replace("###debug-rule###", debugRule);
    }

    private static string SupplementColorConsoleConfig(
        this string config,
        IConfiguration configuration,
        bool whetherDevelop
    )
    {
        var debugToConsoleConfigValue = LogConfigureAssist.GetDebugToConsole(configuration);

        StartupConfigMessageAssist.AddConfig(
            debugToConsoleConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigDebugToConsoleSwitch = debugToConsoleConfigValue.Value;

        var traceToConsoleConfigValue = LogConfigureAssist.GetTraceToConsole(configuration);

        StartupConfigMessageAssist.AddConfig(
            traceToConsoleConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigTraceToConsoleSwitch = traceToConsoleConfigValue.Value;

        var consoleRepeatedFilterConfigValue = LogConfigureAssist.GetConsoleRepeatedFilter(configuration);

        StartupConfigMessageAssist.AddConfig(
            consoleRepeatedFilterConfigValue.ToArrowDescription()
        );

        var nlogConsoleRepeatedFilterSwitch = consoleRepeatedFilterConfigValue.Value;

        var nlogConsoleLimitingWrapper = GetNlogConsoleLimitingWrapper();
        var nlogConsoleTarget = GetNlogConsoleTarget();

        string configAdjust;

        var consoleLimitingWrapperConfigValue = LogConfigureAssist.GetConsoleLimitingWrapper(configuration);

        StartupConfigMessageAssist.AddConfig(
            consoleLimitingWrapperConfigValue.ToArrowDescription()
        );

        if (consoleLimitingWrapperConfigValue.Value)
        {
            configAdjust = config.Replace("###console-config###", nlogConsoleTarget);
        }
        else
        {
            configAdjust = config.Replace("###console-config###", nlogConsoleLimitingWrapper)
                .Replace("###console-target###", nlogConsoleTarget);
        }

        var consoleMinLevel = nlogDefaultConfigTraceToConsoleSwitch ? "Trace" :
            nlogDefaultConfigDebugToConsoleSwitch ? "Debug" : "Info";

        var consoleMessageLimitConfigValue = LogConfigureAssist.GetConsoleMessageLimit(configuration);

        StartupConfigMessageAssist.AddConfig(
            consoleMessageLimitConfigValue.ToArrowDescription()
        );

        var consoleMessageLimit = consoleMessageLimitConfigValue.Value;

        var consoleMessageLimitSetting = consoleMessageLimit <= 0 ? "" : $"\"messageLimit\": {consoleMessageLimit},";

        var nlogWordHighlightingRules = whetherDevelop
            ? GetNlogWordHighlightingRules()
            : "";

        var nlogConsoleFilter = GetNlogConsoleFilter();

        var consoleFilters = new List<string>();

        if (nlogConsoleRepeatedFilterSwitch)
        {
            consoleFilters.Add(GetNlogConsoleFilterRepeated());
        }

        return configAdjust
            .Replace("###console-minLevel###", consoleMinLevel)
            .Replace("###messageLimit###", consoleMessageLimitSetting)
            .Replace("###word-highlighting-rules###", nlogWordHighlightingRules)
            .Replace(
                "###console-filter-config###",
                consoleFilters.Any() ? nlogConsoleFilter : ""
            )
            .Replace(
                "###console-filters###",
                consoleFilters.Any() ? consoleFilters.Join(",") : ""
            );
    }

    private static string SupplementExceptionlessConfig(
        this string config,
        IConfiguration configuration
    )
    {
        var exceptionlessTeleportConfigValue = LogConfigureAssist.GetExceptionlessTeleport(configuration);

        StartupConfigMessageAssist.AddConfig(
            exceptionlessTeleportConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigDebugToExceptionlessSwitch = exceptionlessTeleportConfigValue.Value;

        var exceptionlessServerUrlConfigValue = LogConfigureAssist.GetExceptionlessServerUrl(configuration);

        StartupConfigMessageAssist.AddConfig(
            exceptionlessServerUrlConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigExceptionlessServerUrl = exceptionlessServerUrlConfigValue.Value;

        var exceptionlessApiKeyConfigValue = LogConfigureAssist.GetExceptionlessApiKey(configuration);

        StartupConfigMessageAssist.AddConfig(
            exceptionlessApiKeyConfigValue.ToArrowDescription()
        );

        var nlogDefaultConfigExceptionlessApiKey = exceptionlessApiKeyConfigValue.Value;

        var exceptionlessExtensions =
            nlogDefaultConfigDebugToExceptionlessSwitch ? GetNlogDefaultExceptionlessExtensionsConfig() : "";
        var exceptionlessRule =
            nlogDefaultConfigDebugToExceptionlessSwitch ? GetNlogDefaultExceptionlessRuleConfig() : "";
        var exceptionlessTarget =
            nlogDefaultConfigDebugToExceptionlessSwitch ? GetNlogDefaultExceptionlessTargetConfig() : "";

        if (nlogDefaultConfigDebugToExceptionlessSwitch)
        {
            exceptionlessTarget = exceptionlessTarget
                .Replace("###exceptionless-apiKey###", nlogDefaultConfigExceptionlessApiKey)
                .Replace("###exceptionless-serverUrl###", nlogDefaultConfigExceptionlessServerUrl);
        }

        exceptionlessExtensions =
            string.IsNullOrWhiteSpace(exceptionlessExtensions) ? "" : $"{exceptionlessExtensions},";
        exceptionlessRule = string.IsNullOrWhiteSpace(exceptionlessRule) ? "" : $"{exceptionlessRule},";
        exceptionlessTarget = string.IsNullOrWhiteSpace(exceptionlessTarget) ? "" : $"{exceptionlessTarget},";

        return config
            .Replace("###exceptionless-extensions###", exceptionlessExtensions)
            .Replace("###exceptionless-target###", exceptionlessTarget)
            .Replace("###exceptionless-rule###", exceptionlessRule);
    }

    private static string GetEmbeddedResourceFileContent(string path)
    {
        var personEmbeddedFileProvider = new EmbeddedFileProvider(
            typeof(Tools).Assembly
        );

        var f = personEmbeddedFileProvider.GetFileInfo(path);

        return f.CreateReadStream().ReadAll(new UTF8Encoding(false));
    }
}