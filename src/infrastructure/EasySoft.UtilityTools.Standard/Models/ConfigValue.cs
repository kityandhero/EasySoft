namespace EasySoft.UtilityTools.Standard.Models;

/// <summary>
/// DataPair
/// </summary>
/// <typeparam name="T"></typeparam>
public class ConfigValue<T>
{
    #region Properties

    /// <summary>
    /// Node
    /// </summary>
    public string Node { get; set; } = "";

    /// <summary>  
    /// Value
    /// </summary>
    public T Value { get; set; } = default;

    /// <summary>  
    /// 有效值描述
    /// </summary>
    public string EffectiveValueDescription { get; set; } = "";

    #endregion

    #region Methods

    /// <summary>
    /// 转为箭头文本描述
    /// </summary>
    /// <returns></returns>
    public string ToArrowDescription()
    {
        var v = Value.ToString();

        return $"{Node} -> {(string.IsNullOrWhiteSpace(v) ? "not set, default value will be used" : v)}.";
    }

    /// <summary>
    /// 转为错误文本描述
    /// </summary>
    /// <param name="description">其他提示</param>
    /// <returns></returns>
    public string ToErrorDescription(string description = "")
    {
        var v = Value.ToString();

        return
            $"[{Node}] 配置无效{(string.IsNullOrWhiteSpace(v) ? "" : $", 有效取值为 {EffectiveValueDescription}")}{(string.IsNullOrWhiteSpace(v) ? "" : $", {description}")}.";
    }

    #endregion
}