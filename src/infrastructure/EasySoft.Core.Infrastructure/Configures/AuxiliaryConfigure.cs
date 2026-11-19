namespace EasySoft.Core.Infrastructure.Configures;

/// <summary>
/// 开发助手配置
/// </summary>
public static class AuxiliaryConfigure
{
    /// <summary>
    /// 输出 Execute 信息
    /// </summary>

    // public static bool PromptStartupExecuteMessage { get; set; }
    static AuxiliaryConfigure()
    {
        // PromptStartupExecuteMessage = false;
    }

    /// <summary>
    /// BuildHintMessage
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<string> BuildHintMessage()
    {
        return new List<string>().ToArray();

        // return new[]
        // {
        //     $"{typeof(AuxiliaryConfigure).FullName}.{nameof(PromptStartupExecuteMessage)} is {PromptStartupExecuteMessage}."
        // };
    }
}