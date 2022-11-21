namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// StructExtensions
/// </summary>
public static class StructExtensions
{
    /// <summary>
    /// In
    /// </summary>
    /// <param name="source"></param>
    /// <param name="array"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool In<T>(this T source, params T[] array) where T : struct
    {
        return source.In(array.ToList());
    }

    /// <summary>
    /// In
    /// </summary>
    /// <param name="source"></param>
    /// <param name="collection"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool In<T>(this T source, ICollection<T> collection) where T : struct
    {
        return collection.Count > 0 && Enumerable.Contains(collection, source);
    }
}