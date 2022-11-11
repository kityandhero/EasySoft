using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
using EasySoft.UtilityTools.Core.Assists;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.ExtensionMethods;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;

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

        var consoleMinLevel = nlogDefaultConfigTraceToConsoleSwitch ? "Trace" :
            nlogDefaultConfigDebugToConsoleSwitch ? "Debug" : "Info";

        var consoleMessageLimit = GeneralConfigAssist.GetNlogConsoleMessageLimit();

        var consoleMessageLimitSetting = consoleMessageLimit <= 0 ? "" : $"\"messageLimit\": {consoleMessageLimit},";

        var nlogWordHighlightingRules = EnvironmentAssist
            .GetEnvironment()
            .IsDevelopment()
            ? GetNlogWordHighlightingRules()
            : "";

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
            .Replace("###word-highlighting-rules###", nlogWordHighlightingRules);

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
        return GetEmbeddedResourceFileContent("/nlog.wordHighlightingRules.txt");
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