using EasySoft.Core.Config.ConfigAssist;
using EasySoft.Core.Infrastructure.Assists;
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

        var debugRule = nlogDefaultConfigDebugToFileSwitch ? GetNlogDefaultDebugRuleConfig() : "";
        var debugTarget = nlogDefaultConfigDebugToFileSwitch ? GetNlogDefaultDebugTargetConfig() : "";

        debugRule = string.IsNullOrWhiteSpace(debugRule) ? "" : $"{debugRule},";
        debugTarget = string.IsNullOrWhiteSpace(debugTarget) ? "" : $"{debugTarget},";

        var traceRule = nlogDefaultConfigTraceToFileSwitch ? GetNlogDefaultTraceRuleConfig() : "";
        var traceTarget = nlogDefaultConfigTraceToFileSwitch ? GetNlogDefaultTraceTargetConfig() : "";

        traceRule = string.IsNullOrWhiteSpace(traceRule) ? "" : $"{traceRule},";
        traceTarget = string.IsNullOrWhiteSpace(traceTarget) ? "" : $"{traceTarget},";

        var consoleMinLevel = nlogDefaultConfigTraceToConsoleSwitch ? "Trace" :
            nlogDefaultConfigDebugToConsoleSwitch ? "Debug" : "Info";

        mainConfig = mainConfig.Replace("###trace-target###", traceTarget)
            .Replace("###debug-target###", debugTarget)
            .Replace("###trace-rule###", traceRule)
            .Replace("###debug-rule###", debugRule)
            .Replace("###console-minLevel###", consoleMinLevel);

        return mainConfig;
    }

    public static string GetNlogDefaultMainConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.main.config.txt");
    }

    public static string GetNlogDefaultDebugRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.debug.rule.txt");
    }

    public static string GetNlogDefaultDebugTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.debug.target.txt");
    }

    public static string GetNlogDefaultTraceRuleConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.trace.rule.txt");
    }

    public static string GetNlogDefaultTraceTargetConfig()
    {
        return GetEmbeddedResourceFileContent("/nlog.trace.target.txt");
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