using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.Extensions;

namespace EasySoft.Core.Config.Utils;

/// <summary>
/// Tools
/// </summary>
public static class Tools
{
    /// <summary>
    /// GetConfigureDirectory
    /// </summary>
    /// <returns></returns>
    public static string GetConfigureDirectory()
    {
        var configureFolderPath = $"{AppContextAssist.GetBaseDirectory()}/configures/";

        return configureFolderPath;
    }

    /// <summary>
    /// GetNlogEmbedConfig
    /// </summary>
    /// <returns></returns>
    public static string GetNlogEmbedConfig()
    {
        var mainConfig = GetNlogDefaultMainConfig();

        mainConfig = mainConfig
            .SupplementThrowConfigExceptions()
            .SupplementInternalLogLevel()
            .SupplementInternalLogFile()
            .SupplementGeneralRule()
            .SupplementColorConsoleConfig()
            .SupplementProductionLogConfig()
            .SupplementExceptionlessConfig();

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

    private static string SupplementInternalLogLevel(this string config)
    {
        return config
            .Replace(
                "###internal-log-level###",
                GeneralConfigAssist.GetNlogEmbedConfigInternalLogLevel()
            );
    }

    private static string SupplementInternalLogFile(this string config)
    {
        var logToFileSwitch = GeneralConfigAssist.GetNlogEmbedConfigInternalLogToFileSwitch();
        var logFile = GeneralConfigAssist.GetNlogEmbedConfigInternalLogFile();

        return config
            .Replace(
                "###internal-log-file###",
                logToFileSwitch && !string.IsNullOrWhiteSpace(logFile)
                    ? $"\"internalLogFile\":\"{logFile}\","
                    : ""
            );
    }

    private static string SupplementProductionLogConfig(this string config)
    {
        var nlogEmbedConfigProductionLogFileSwitch = GeneralConfigAssist.GetNlogEmbedConfigProductionLogFileSwitch();

        if (!nlogEmbedConfigProductionLogFileSwitch)
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
                GeneralConfigAssist.GetNlogEmbedConfigProductionLogFileName()
            )
            .Replace(
                "###archive-file-name###",
                GeneralConfigAssist.GetNlogEmbedConfigProductionLogArchiveFileName()
            );

        var rule = GetEmbeddedResourceFileContent("/nlog.production.file.rule.txt")
            .Replace(
                "###level###",
                GeneralConfigAssist.GetNlogEmbedConfigProductionLogLevel()
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

    private static string SupplementGeneralRule(this string config)
    {
        var nlogDefaultConfigDebugToFileSwitch = GeneralConfigAssist.GetNlogEmbedConfigDebugToFileSwitch();
        var nlogDefaultConfigTraceToFileSwitch = GeneralConfigAssist.GetNlogEmbedConfigTraceToFileSwitch();

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

    private static string SupplementColorConsoleConfig(this string config)
    {
        var nlogDefaultConfigDebugToConsoleSwitch = GeneralConfigAssist.GetNlogEmbedConfigDebugToConsoleSwitch();
        var nlogDefaultConfigTraceToConsoleSwitch = GeneralConfigAssist.GetNlogEmbedConfigTraceToConsoleSwitch();
        var nlogConsoleRepeatedFilterSwitch = GeneralConfigAssist.GetNlogConsoleRepeatedFilterSwitch();

        var nlogConsoleLimitingWrapper = GetNlogConsoleLimitingWrapper();
        var nlogConsoleTarget = GetNlogConsoleTarget();

        string configAdjust;

        if (GeneralConfigAssist.GetNlogConsoleLimitingWrapperSwitch())
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

        var consoleMessageLimit = GeneralConfigAssist.GetNlogConsoleMessageLimit();

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

    private static string SupplementExceptionlessConfig(this string config)
    {
        var nlogDefaultConfigDebugToExceptionlessSwitch = GeneralConfigAssist.GetExceptionlessSwitch();

        var nlogDefaultConfigExceptionlessServerUrl = GeneralConfigAssist.GetExceptionlessServerUrl();
        var nlogDefaultConfigExceptionlessApiKey = GeneralConfigAssist.GetExceptionlessApiKey();

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
            typeof(Tools).Assembly,
            typeof(ConstCollection).Namespace
        );

        var f = personEmbeddedFileProvider.GetFileInfo(path);

        return f.CreateReadStream().ReadAll(new UTF8Encoding(false));
    }
}