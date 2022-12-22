using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.Core.Sql.Common;

/// <summary>
/// 
/// </summary>
public static class Tools
{
    /// <summary>
    /// GetTableAttribute
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AdvanceTableAttribute? GetAdvanceTableAttribute<T>()
    {
        var type = typeof(T);

        var attribute = type.GetCustomAttribute<AdvanceTableAttribute>();

        if (attribute == null) return null;

        if (string.IsNullOrWhiteSpace(attribute.Name))
            throw new Exception(
                $"{type.Name}的特性 AdvanceTableAttribute 赋值错误"
            );

        return attribute;
    }

    /// <summary>
    /// GetAdvanceTableAttribute
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AdvanceTableAttribute? GetAdvanceTableAttribute<T>(T model)
    {
        if (model == null) throw new Exception("model disallow null");

        var tableAttribute = model.GetCustomAttribute<AdvanceTableAttribute>();

        if (tableAttribute == null) return null;

        if (string.IsNullOrWhiteSpace(tableAttribute.Name))
            throw new Exception($"{model.GetType().Name}的特性 TableAttribute 赋值错误");

        return tableAttribute;
    }

    /// <summary>
    /// GetAdvanceColumnAttribute
    /// </summary>
    /// <param name="property"></param>
    /// <param name="throwExceptionWhenNoAttribute"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AdvanceColumnAttribute? GetAdvanceColumnAttribute(
        PropertyInfo property,
        bool throwExceptionWhenNoAttribute = true
    )
    {
        var attribute = property.TryGetCustomAttribute<AdvanceColumnAttribute>();

        if (attribute == null)
        {
            if (!throwExceptionWhenNoAttribute) return attribute;

            if (property.DeclaringType != null)
                throw new Exception(
                    $"类型{property.DeclaringType.Name}属性${property.Name}缺少 AdvanceColumnAttribute 特性"
                );

            throw new Exception(
                $"属性${property.Name}缺少 AdvanceColumnAttribute 特性"
            );
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(attribute.Name)) return attribute;

            if (property.DeclaringType != null)
                throw new Exception(
                    $"类型{property.DeclaringType.Name}属性${property.Name}的特性 AdvanceColumnAttribute 赋值错误"
                );

            throw new Exception(
                $"属性${property.Name}的特性 AdvanceColumnAttribute 赋值错误"
            );
        }
    }
}