using System.Text;
using EasySoft.Core.NLog.Assists;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

namespace EasySoft.Core.NLog.Utils;

/// <summary>
/// Tools
/// </summary>
public static class Tools
{
    /// <summary>
    /// GetNlogEmbedConfig
    /// </summary>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static string GetNlogEmbedConfig(IConfiguration configuration)
    {
        var mainConfig = GetNlogDefaultMainConfig();

        mainConfig = mainConfig
            .SupplementThrowConfigExceptions()
            .SupplementInternalLogLevel(configuration)
            .SupplementInternalLogFile(configuration)
            .SupplementGeneralRule(configuration)
            .SupplementColorConsoleConfig(configuration)
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

    private static string SupplementThrowConfigExceptions(this string config)
    {
        return config
            .Replace(
                "###throw-config-exceptions###",
                EnvironmentAssist
                    .GetEnvironment()
                    .IsDevelopment()
                    ? "true"
                    : "false"
            );
    }

    private static string SupplementInternalLogLevel(
        this string config,
        IConfiguration configuration
    )
    {
        return config
            .Replace(
                "###internal-log-level###",
                LogConfigureAssist.GetInternalLogLevel(configuration).Value
            );
    }

    private static string SupplementInternalLogFile(
        this string config,
        IConfiguration configuration
    )
    {
        var logToFileSwitch = LogConfigureAssist.GetInternalLogToFile(configuration).Value;
        var logFile = LogConfigureAssist.GetInternalLogFile(configuration).Value;

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
        var nlogEmbedConfigProductionLogToToTFileSwitch =
            LogConfigureAssist.GetProductionLogToFile(configuration).Value;

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

        var target = GetEmbeddedResourceFileContent("/nlog.production.file.target.txt")
            .Replace(
                "###file-name###",
                LogConfigureAssist.GetProductionLogFileName(configuration).Value
            )
            .Replace(
                "###archive-file-name###",
                LogConfigureAssist.GetProductionLogArchiveFileName(configuration).Value
            );

        var rule = GetEmbeddedResourceFileContent("/nlog.production.file.rule.txt")
            .Replace(
                "###level###",
                LogConfigureAssist.GetProductionLogLevel(configuration).Value
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
        var nlogDefaultConfigDebugToFileSwitch = LogConfigureAssist.GetDebugToFile(configuration).Value;
        var nlogDefaultConfigTraceToFileSwitch = LogConfigureAssist.GetTraceToFile(configuration).Value;

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
        IConfiguration configuration
    )
    {
        var nlogDefaultConfigDebugToConsoleSwitch = LogConfigureAssist.GetDebugToConsole(configuration).Value;
        var nlogDefaultConfigTraceToConsoleSwitch = LogConfigureAssist.GetTraceToConsole(configuration).Value;
        var nlogConsoleRepeatedFilterSwitch = LogConfigureAssist.GetConsoleRepeatedFilter(configuration).Value;

        var nlogConsoleLimitingWrapper = GetNlogConsoleLimitingWrapper();
        var nlogConsoleTarget = GetNlogConsoleTarget();

        string configAdjust;

        if (LogConfigureAssist.GetConsoleLimitingWrapper(configuration).Value)
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

        var consoleMessageLimit = LogConfigureAssist.GetConsoleMessageLimit(configuration).Value;

        var consoleMessageLimitSetting = consoleMessageLimit <= 0 ? "" : $"\"messageLimit\": {consoleMessageLimit},";

        var nlogWordHighlightingRules = EnvironmentAssist
            .GetEnvironment()
            .IsDevelopment()
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
        var nlogDefaultConfigDebugToExceptionlessSwitch =
            LogConfigureAssist.GetExceptionlessTeleport(configuration).Value;

        var nlogDefaultConfigExceptionlessServerUrl = LogConfigureAssist.GetExceptionlessServerUrl(configuration).Value;
        var nlogDefaultConfigExceptionlessApiKey = LogConfigureAssist.GetExceptionlessApiKey(configuration).Value;

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