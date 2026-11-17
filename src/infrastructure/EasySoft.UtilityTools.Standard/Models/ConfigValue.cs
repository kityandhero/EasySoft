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

    #endregion

    #region Methods

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public string ToArrowDescription()
    {
        var v = Value.ToString()?.ToLower();

        return $"[Configure] {Node} -> {(string.IsNullOrWhiteSpace(v) ? "not set, default value will be used" : v)}.";
    }

    #endregion
}