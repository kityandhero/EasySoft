using System.Collections.Generic;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

public static class StructExtensions
{
    public static bool In<T>(this T source, params T[] array) where T : struct
    {
        return source.In(array.ToList());
    }

    public static bool In<T>(this T source, ICollection<T> collection) where T : struct
    {
        if (collection.Count <= 0)
        {
            return false;
        }

        foreach (var item in collection)
        {
            if (Equals(source, item))
            {
                return true;
            }
        }

        return false;
    }
}