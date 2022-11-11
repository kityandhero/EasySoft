using EasySoft.Core.Config.ConfigAssist;
using EasySoft.UtilityTools.Standard.ExtensionMethods;

namespace EasySoft.Core.Config.Utils;

public static class Tools
{
    public static string GetConfigureDirectory()
    {
        var configureFolderPath = $"{AppContextAssist.GetBaseDirectory()}/configures/";

        return configureFolderPath;
    }

    public static string GetNlogDefaultConfig()
    {
        var mainConfig = GetNlogDefaultMainConfig();

        var nlogDefaultConfigDebugToFileSwitch = GeneralConfigAssist.GetNlogDefaultConfigDebugToFileSwitch();
        var nlogDefaultConfigTraceToFileSwitch = GeneralConfigAssist.GetNlogDefaultConfigTraceToFileSwitch();
        var nlogDefaultConfigDebugToConsoleSwitch = GeneralConfigAssist.GetNlogDefaultConfigDebugToConsoleSwitch();
        var nlogDefaultConfigTraceToConsoleSwitch = GeneralConfigAssist.GetNlogDefaultConfigTraceToConsoleSwitch();
        var nlogDefaultConfigDebugToExceptionlessSwitch = GeneralConfigAssist.GetExceptionlessSwitch();

        var nlogDefaultConfigExceptionlessServerUrl = GeneralConfigAssist.GetExceptionlessServerUrl();
        var nlogDefaultConfigExceptionlessApiKey = GeneralConfigAssist.GetExceptionlessApiKey();
        var nlogConsoleRepeatedFilterSwitch = GeneralConfigAssist.GetNlogConsoleRepeatedFilterSwitch();

        var debugRule = nlogDefaultConfigDebugToFileSwitch ? GetNlogDefaultDebugRuleConfig() : "";
        var debugTarget = nlogDefaultConfigDebugToFileSwitch ? GetNlogDefaultDebugTargetConfig() : "";

        debugRule = string.IsNullOrWhiteSpace(debugRule) ? "" : $"{debugRule},";
        debugTarget = string.IsNullOrWhiteSpace(debugTarget) ? "" : $"{debugTarget},";

        var traceRule = nlogDefaultConfigTraceToFileSwitch ? GetNlogDefaultTraceRuleConfig() : "";
        var traceTarget = nlogDefaultConfigTraceToFileSwitch ? GetNlogDefaultTraceTargetConfig() : "";

        traceRule = string.IsNullOrWhiteSpace(traceRule) ? "" : $"{traceRule},";
        traceTarget = string.IsNullOrWhiteSpace(traceTarget) ? "" : $"{traceTarget},";

        var exceptionlessExtensions =
            nlogDefaultConfigDebugToExceptionlessSwitch ? GetNlogDefaultExceptionlessExtensionsConfig() : "";
        var exceptionlessRule =
            nlogDefaultConfigDebugToExceptionlessSwitch ? GetNlogDefaultExceptionlessRuleConfig() : "";
        var exceptionlessTarget =
            nlogDefaultConfigDebugToExceptionlessSwitch ? GetNlogDefaultExceptionlessTargetConfig() : "";

        if (nlogDefaultConfigDebugToExceptionlessSwitch)
            exceptionlessTarget = exceptionlessTarget
                .Replace("###exceptionless-apiKey###", nlogDefaultConfigExceptionlessApiKey)
                .Replace("###exceptionless-serverUrl###", nlogDefaultConfigExceptionlessServerUrl);

        exceptionlessExtensions =
            string.IsNullOrWhiteSpace(exceptionlessExtensions) ? "" : $"{exceptionlessExtensions},";
        exceptionlessRule = string.IsNullOrWhiteSpace(exceptionlessRule) ? "" : $"{exceptionlessRule},";
        exceptionlessTarget = string.IsNullOrWhiteSpace(exceptionlessTarget) ? "" : $"{exceptionlessTarget},";

        var nlogConsoleLimitingWrapper = GetNlogConsoleLimitingWrapper();
        var nlogConsoleTarget = GetNlogConsoleTarget();

        if (GeneralConfigAssist.GetNlogConsoleLimitingWrapperSwitch())
            mainConfig = mainConfig.Replace("###console-config###", nlogConsoleTarget);
        else
            mainConfig = mainConfig.Replace("###console-config###", nlogConsoleLimitingWrapper)
                .Replace("###console-target###", nlogConsoleTarget);

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

        if (nlogConsoleRepeatedFilterSwitch) consoleFilters.Add(GetNlogConsoleFilterRepeated());

        mainConfig = mainConfig
            .Replace("###exceptionless-extensions###", exceptionlessExtensions)
            .Replace("###trace-target###", traceTarget)
            .Replace("###debug-target###", debugTarget)
            .Replace("###exceptionless-target###", exceptionlessTarget)
            .Replace("###trace-rule###", traceRule)
            .Replace("###debug-rule###", debugRule)
            .Replace("###exceptionless-rule###", exceptionlessRule)
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

    // ReSharper disable once MemberCanBePrivate.Global
    public static string GetNlogDefaultTraceRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.trace.rule.txt");
    }

    private static string GetNlogDefaultTraceTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.trace.target.txt");
    }

    public static string GetNlogDefaultExceptionlessExtensionsConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.exceptionless-extensions.txt");
    }

    public static string GetNlogDefaultExceptionlessRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.exceptionless.rule.txt");
    }

    public static string GetNlogDefaultExceptionlessTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.exceptionless.target.txt");
    }

    public static string GetNlogWordHighlightingRules()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.word.highlighting.rules.txt");
    }

    public static string GetNlogConsoleFilter()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.filter.txt");
    }

    public static string GetNlogConsoleFilterRepeated()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.filter.repeated.txt");
    }

    public static string GetNlogConsoleLimitingWrapper()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.limiting.wrapper.txt");
    }

    public static string GetNlogConsoleTarget()
    {
        return GetEmbeddedResourceFileContent("/nlog.console.target.txt");
    }

    private static string GetEmbeddedResourceFileContent(string path)
    {
        var personEmbeddedFileProvider = new EmbeddedFileProvider(
            typeof(Tools).Assembly,
            typeof(ConstCollection).Namespace
        );

        var f = personEmbeddedFileProvider.GetFileInfo(path);

        return f.CreateReadStream().ReadAll();
    }
}