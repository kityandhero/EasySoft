using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ObjectExtensions
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// 安全转换为字符串，去除两端空格，当值为null时返回""
    /// </summary>
    /// <param name="input">输入值</param>
    public static string SafeString(this object? input)
    {
        return input?.ToString().Trim() ?? string.Empty;
    }

    /// <summary>
    /// 检测对象是否为null,为null则抛出<see cref="ArgumentNullException"/>异常
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="parameterName">参数名</param>
    public static void CheckNull(this object? obj, string parameterName)
    {
        if (obj == null) throw new ArgumentNullException(parameterName);
    }

    /// <summary>
    /// ConvertTo
    /// </summary>
    /// <param name="value"></param>
    /// <param name="defaultValue"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T ConvertTo<T>(this object? value, object? defaultValue = null) where T : new()
    {
        return ConvertAssist.ObjectTo<T>(value, defaultValue)!;
    }

    /// <summary>
    /// 转换为ExpandoObject
    /// </summary>
    /// <param name="source"></param>
    /// <param name="keyFirstLetterToLower"></param>
    /// <returns></returns>
    public static ExpandoObject ToExpandoObject(this object? source, bool keyFirstLetterToLower = true)
    {
        var result = new ExpandoObject();

        var type = source?.GetType();
        var properties = type?.GetProperties();

        if (properties == null) return result;

        foreach (var p in properties)
            result.AddKeyValuePair(
                new KeyValuePair<string, object?>(
                    keyFirstLetterToLower ? p.Name.ToLowerFirst() : p.Name,
                    p.GetValue(source, null)
                )
            );

        return result;
    }
}