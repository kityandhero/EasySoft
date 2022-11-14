namespace EasySoft.Core.Infrastructure.Configures;

/// <summary>
/// 开发助手配置
/// </summary>
public static class AuxiliaryConfigure
{
    /// <summary>
    /// 输出 Execute 信息
    /// </summary>
    public static bool PromptStartupExecuteMessage { get; set; }

    /// <summary>
    /// 输出配置文件信息
    /// </summary>
    public static bool PromptConfigFileInfo { get; set; }

    static AuxiliaryConfigure()
    {
        PromptStartupExecuteMessage = false;

        PromptConfigFileInfo = false;
    }

    public static IEnumerable<string> BuildHintMessage()
    {
        return new[]
        {
            $"{typeof(AuxiliaryConfigure).FullName}.{nameof(PromptStartupExecuteMessage)} is {PromptStartupExecuteMessage}.",
            $"{typeof(AuxiliaryConfigure).FullName}.{nameof(PromptConfigFileInfo)} is {PromptConfigFileInfo}."
        };
    }
}