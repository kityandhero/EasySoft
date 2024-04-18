using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Exceptions;

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
    public static AdvanceTableMapperAttribute? GetAdvanceTableMapperAttribute<T>(
        bool triggerExceptionWhenNonexistence = true
    )
    {
        var type = typeof(T);
        var m = type.Create();

        if (m == null)
        {
            throw new UnhandledException("type create instance is null");
        }

        var advanceTableMapperAttribute = m.GetCustomAttribute<AdvanceTableMapperAttribute>();

        if (advanceTableMapperAttribute == null && !triggerExceptionWhenNonexistence)
        {
            return null;
        }

        if (advanceTableMapperAttribute == null)
        {
            throw new UnhandledException($" {type.Name} 缺少 AdvanceTableMapperAttribute 特性");
        }

        if (string.IsNullOrWhiteSpace(advanceTableMapperAttribute.Name))
        {
            throw new UnhandledException($" {type.Name} 的特性 AdvanceTableMapperAttribute 赋值错误");
        }

        return advanceTableMapperAttribute;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="triggerExceptionWhenNonexistence"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="UnhandledException"></exception>
    /// <exception cref="Exception"></exception>
    public static AdvanceTableMapperAttribute? GetAdvanceTableMapperAttribute<T>(
        T? model,
        bool triggerExceptionWhenNonexistence = true
    )
    {
        if (model == null)
        {
            throw new UnhandledException("type create instance is null");
        }

        var type = model.GetType();

        var advanceTableMapperAttribute = model.GetCustomAttribute<AdvanceTableMapperAttribute>();

        if (advanceTableMapperAttribute == null && !triggerExceptionWhenNonexistence)
        {
            return null;
        }

        if (advanceTableMapperAttribute == null)
        {
            throw new UnhandledException($" {type.Name} 缺少 AdvanceTableMapperAttribute 特性");
        }

        if (string.IsNullOrWhiteSpace(advanceTableMapperAttribute.Name))
        {
            throw new UnhandledException($" {type.Name} 的特性 AdvanceTableMapperAttribute 赋值错误");
        }

        return advanceTableMapperAttribute;
    }

    /// <summary>
    /// GetAdvanceColumnMapperAttribute
    /// </summary>
    /// <param name="property"></param>
    /// <param name="throwExceptionWhenNoAttribute"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static AdvanceColumnMapperAttribute? GetAdvanceColumnMapperAttribute(
        PropertyInfo? property,
        bool throwExceptionWhenNoAttribute = true
    )
    {
        if (property == null)
        {
            throw new UnhandledException("property not allow null");
        }

        var advanceColumnMapperAttribute = property.TryGetCustomAttribute<AdvanceColumnMapperAttribute>();

        if (advanceColumnMapperAttribute == null)
        {
            if (!throwExceptionWhenNoAttribute)
            {
                return advanceColumnMapperAttribute;
            }

            if (property.DeclaringType != null)
            {
                throw new Exception(
                    $"类型{property.DeclaringType.Name}属性${property.Name}缺少 AdvanceColumnMapperAttribute 特性"
                );
            }

            throw new Exception(
                $"属性${property.Name}缺少 AdvanceColumnMapperAttribute 特性"
            );
        }
        else
        {
            if (!string.IsNullOrWhiteSpace(advanceColumnMapperAttribute.Name))
            {
                return advanceColumnMapperAttribute;
            }

            if (property.DeclaringType != null)
            {
                throw new Exception(
                    $"类型{property.DeclaringType.Name}属性${property.Name}的特性 AdvanceColumnMapperAttribute 赋值错误"
                );
            }

            throw new Exception(
                $"属性${property.Name}的特性 AdvanceColumnMapperAttribute 赋值错误"
            );
        }
    }
}