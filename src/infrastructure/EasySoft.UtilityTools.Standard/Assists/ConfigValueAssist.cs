namespace EasySoft.UtilityTools.Standard.Assists;

/// <summary>
/// ConfigValueAssist
/// </summary>
public static class ConfigValueAssist
{
    /// <summary>
    /// 构建错误文本描述
    /// </summary>
    /// <param name="node">配置节点</param>
    /// <param name="value">值</param>
    /// <param name="effectiveValueDescription">有效值描述</param>
    /// <param name="description">其他提示</param>
    /// <returns></returns>
    public static string BuildErrorDescription(
        string node,
        string value,
        string effectiveValueDescription,
        string description = ""
    )
    {
        return
            $"[{node}] 配置无效{(string.IsNullOrWhiteSpace(value) ? "" : $", 有效取值为 {effectiveValueDescription}")}{(string.IsNullOrWhiteSpace(description) ? "" : $", {description}")}.";
    }
}