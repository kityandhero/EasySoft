using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Attributes;
using EasySoft.UtilityTools.Standard.Enums;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// Int32Extensions
/// </summary>
public static class Int32Extensions
{
    /// <summary>
    /// CorrectionDayOfWeek
    /// </summary>
    /// <param name="dayOfWeek"></param>
    /// <returns></returns>
    public static int CorrectionDayOfWeek(int dayOfWeek)
    {
        var result = dayOfWeek;

        if (result == 0) result = 7;

        return result;
    }

    #region In

    /// <summary>
    /// 包含于
    /// </summary>
    /// <param name="input"></param>
    /// <param name="array"></param>
    /// <returns></returns>
    public static bool In(this int input, params int[] array)
    {
        return CollectionAssist.In(input, array);
    }

    /// <summary>
    /// 包含于
    /// </summary>
    /// <param name="input"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool In(this int input, ICollection<int> list)
    {
        return CollectionAssist.In(input, list);
    }

    #endregion In

    /// <summary>
    /// ToObject
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static object ToObject(this int v)
    {
        return v;
    }

    /// <summary>
    /// GetEnumDescription
    /// </summary>
    /// <param name="v"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetEnumDescription<T>(this int v) where T : struct
    {
        var result = "";

        var exist = Enum.TryParse(v.ToString(), out T t);

        if (!exist) return result;

        var name = EnumAssist.GetNameByValue(typeof(T), v);

        if (string.IsNullOrWhiteSpace(name)) return result;

        var type = t.GetType();

        var fieldInfo = type.GetField(name);

        if (fieldInfo == null) return result;

        var descriptionAttribute = fieldInfo.GetCustomAttribute<DescriptionAttribute>(
            "",
            false,
            MemberTypes.Field
        );

        if (descriptionAttribute != null) result = descriptionAttribute.Description;

        return result;
    }

    /// <summary>
    /// GetRenderValuer
    /// </summary>
    /// <param name="v"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetRenderValuer<T>(this int v) where T : struct
    {
        var result = "";

        var exist = Enum.TryParse(v.ToString(), out T t);

        if (!exist) return result;

        var name = EnumAssist.GetNameByValue(typeof(T), v);

        if (string.IsNullOrWhiteSpace(name)) return result;

        var type = t.GetType();

        var fieldInfo = type.GetField(name);

        if (fieldInfo == null) return result;

        var renderValueAttribute = fieldInfo.GetCustomAttribute<RenderValueAttribute>(
            "",
            false,
            MemberTypes.Field
        );

        if (renderValueAttribute != null) result = renderValueAttribute.Description;

        return result;
    }

    /// <summary>
    /// CheckInEnum
    /// </summary>
    /// <param name="v"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool CheckInEnum<T>(this int v) where T : struct
    {
        var values = EnumAssist.GetIntValues<T>();

        return values.Contains(v);
    }

    /// <summary>
    /// CheckInEnum
    /// </summary>
    /// <param name="v"></param>
    /// <param name="items"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool CheckInEnum<T>(this int v, params T[] items) where T : struct
    {
        var values = items.ToList().Cast<int>().ToList();

        return values.Contains(v);
    }

    /// <summary>
    /// EqualEnum
    /// </summary>
    /// <param name="v"></param>
    /// <param name="target"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool EqualEnum<T>(this int v, T target) where T : struct
    {
        var tv = Convert.ToInt32(target);

        return v == tv;
    }

    /// <summary>
    /// CheckInEnumWithExclude
    /// </summary>
    /// <param name="v"></param>
    /// <param name="excludeItems"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static bool CheckInEnumWithExclude<T>(this int v, params T[] excludeItems) where T : struct
    {
        if (excludeItems == null || excludeItems.Length <= 0) throw new Exception("参数缺失，需要指定检测的排除项目");

        var excludeList = excludeItems.Cast<int>().ToList();

        var values = EnumAssist.GetIntValues<T>().Where(o => !excludeList.Contains(o)).ToList();

        return values.Contains(v);
    }

    /// <summary>
    /// ToWhether
    /// </summary>
    /// <param name="v"></param>
    /// <returns></returns>
    public static Whether ToWhether(this int v)
    {
        return v == (int)Whether.Yes ? Whether.Yes : Whether.No;
    }
}