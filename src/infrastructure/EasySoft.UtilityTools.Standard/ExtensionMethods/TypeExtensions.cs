using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Attributes;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

public static class TypeExtensions
{
    public static KeyValueDefinitionAttribute GetKeyValueDefinitionAttribute<T>(this T t) where T : struct
    {
        var type = t.GetType();

        var fieldInfo = type.GetField(t.ToString() ?? string.Empty);

        if (fieldInfo == null) throw new Exception("未找到KeyValueDefinitionAttribute");

        var keyValueDefinitionAttribute = fieldInfo.GetAttribute<KeyValueDefinitionAttribute>(
            "",
            false,
            MemberTypes.Field
        );

        if (keyValueDefinitionAttribute == null)
            throw new Exception($"{nameof(KeyValueDefinitionAttribute)} do not exist");

        return keyValueDefinitionAttribute;
    }

    #region Enum

    /// <summary>
    /// 判断是否包含该类型特性
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <returns></returns>
    public static bool ContainAttribute<T>(this Enum source) where T : class
    {
        var field = source.GetType().GetField(source.ToString());

        return field != null && field.ContainAttribute<T>("", false, MemberTypes.Field);
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="source">调用源</param>
    /// <param name="nameFilter">筛选名称</param>
    /// <returns></returns>
    public static T? GetAttribute<T>(this Enum source, string nameFilter = "") where T : class
    {
        var field = source.GetType().GetField(source.ToString());

        if (field == null) throw new Exception("field not allow null");

        return field.GetAttribute<T>(nameFilter, false, MemberTypes.Field);
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <param name="source">调用源</param>
    /// <param name="nameFilter">筛选名称</param>
    /// <returns></returns>
    public static List<object> GetAttribute(this Enum source, string nameFilter = "")
    {
        var field = source.GetType().GetField(source.ToString());

        if (field == null) throw new Exception("field not allow null");

        return field.GetAttribute(nameFilter, false, MemberTypes.Field);
    }

    #endregion

    #region Object

    public static string GetClassName<T>(this T o)
    {
        if (o == null) throw new Exception("o not allow null");

        return o.GetType().Name;
    }

    /// <summary>
    /// 判断是否包含该类型特性
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <returns></returns>
    public static bool ExistAttribute<T>(
        this object source
    ) where T : class
    {
        return source.ContainAttribute<T>();
    }

    /// <summary>
    /// 判断是否包含该类型特性
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <returns></returns>
    public static bool ContainAttribute<T>(
        this object source,
        string nameFilter = "",
        bool inherit = false,
        MemberTypes memberTypes = MemberTypes.All
    ) where T : class
    {
        var result = default(T);
        var list = source.GetAttribute(nameFilter, inherit, memberTypes);

        foreach (var attr in list.Where(attr => attr.GetType().FullName == typeof(T).FullName)) result = (T)attr;

        return result != null;
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="source">调用源</param>
    /// <param name="nameFilter">筛选名称</param>
    /// <param name="inherit">继承</param>
    /// <param name="memberTypes">成员类型</param>
    /// <returns></returns>
    public static T? TryGetAttribute<T>(
        this object source,
        string nameFilter = "",
        bool inherit = false,
        MemberTypes memberTypes = MemberTypes.All
    ) where T : class
    {
        var result = default(T);
        var list = source.GetAttribute(nameFilter, inherit, memberTypes);

        foreach (var attr in list.Where(attr => attr.GetType().FullName == typeof(T).FullName)) result = (T)attr;

        return result;
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <typeparam name="T">目标类型</typeparam>
    /// <param name="source">调用源</param>
    /// <param name="nameFilter">筛选名称</param>
    /// <param name="inherit">继承</param>
    /// <param name="memberTypes">成员类型</param>
    /// <returns></returns>
    public static T? GetAttribute<T>(
        this object source,
        string nameFilter = "",
        bool inherit = false,
        MemberTypes memberTypes = MemberTypes.All
    ) where T : class
    {
        var result = default(T);
        var list = source.GetAttribute(nameFilter, inherit, memberTypes);

        foreach (var attr in list.Where(attr => attr.GetType().FullName == typeof(T).FullName)) result = (T)attr;

        return result;
    }

    /// <summary>
    /// 获取特性
    /// </summary>
    /// <param name="source">调用源</param>
    /// <param name="nameFilter">筛选名称</param>
    /// <param name="inherit">继承</param>
    /// <param name="memberTypes">成员类型</param>
    /// <returns></returns>
    public static List<object> GetAttribute(
        this object source,
        string nameFilter = "",
        bool inherit = false,
        MemberTypes memberTypes = MemberTypes.All
    )
    {
        if (source as PropertyInfo != null)
        {
            var tryPropertyInfo = source as PropertyInfo;

            var attrs = Enumerable.ToList(tryPropertyInfo?.GetCustomAttributes(inherit) ?? Array.Empty<object>());

            return attrs;
        }
        else if (source as FieldInfo != null)
        {
            var tryPropertyInfo = source as FieldInfo;

            var attrs = Enumerable.ToList(tryPropertyInfo?.GetCustomAttributes(inherit) ?? Array.Empty<object>());

            return attrs;
        }
        else if (source as MemberInfo != null)
        {
            var tryPropertyInfo = source as MemberInfo;

            var attrs = Enumerable.ToList(tryPropertyInfo?.GetCustomAttributes(inherit) ?? Array.Empty<object>());

            return attrs;
        }
        else
        {
            var type = source.GetType();

            var attrs = new List<object>();

            switch (memberTypes)
            {
                case MemberTypes.All:
                    attrs = Enumerable.ToList(type.GetCustomAttributes(inherit));
                    break;

                case MemberTypes.Field:
                    var field = type.GetField(nameFilter);
                    attrs = field.IsNull()
                        ? attrs
                        : Enumerable.ToList(field?.GetCustomAttributes(inherit) ?? Array.Empty<object>());
                    break;

                case MemberTypes.Method:
                    var method = type.GetMethod(nameFilter);
                    attrs = method.IsNull()
                        ? attrs
                        : Enumerable.ToList(method?.GetCustomAttributes(inherit) ?? Array.Empty<object>());
                    break;

                case MemberTypes.Property:
                    var property = type.GetProperty(nameFilter);
                    attrs = property.IsNull()
                        ? attrs
                        : Enumerable.ToList(property?.GetCustomAttributes(inherit) ?? Array.Empty<object>());
                    break;
            }

            return attrs;
        }
    }

    #endregion

    public static string GetGenericTypeName(this Type type)
    {
        string typeName;

        if (type.IsGenericType)
        {
            var genericTypes = string.Join(",", type.GetGenericArguments().Select(t => t.Name).ToArray());

            typeName = $"{type.Name.Remove(type.Name.IndexOf('`'))}<{genericTypes}>";
        }
        else
        {
            typeName = type.Name;
        }

        return typeName;
    }

    public static string GetGenericTypeName(this object target)
    {
        return target.GetType().GetGenericTypeName();
    }

    public static string GetDescriptionAttributeText(this object target)
    {
        var descriptionAttribute = target.GetAttribute<DescriptionAttribute>();

        return descriptionAttribute == null ? "" : descriptionAttribute.Description;
    }
}