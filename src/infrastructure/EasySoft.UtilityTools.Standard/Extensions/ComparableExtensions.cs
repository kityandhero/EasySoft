using EasySoft.UtilityTools.Standard.Comparison;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// ComparableExtensions
/// </summary>
public static class ComparableExtensions
{
    #region Functions

    #region Between

    /// <summary>
    /// Checks if an item is between two values
    /// </summary>
    /// <typeparam name="T">Type of the value</typeparam>
    /// <param name="value">Value to check</param>
    /// <param name="min">Minimum value</param>
    /// <param name="max">Maximum value</param>
    /// <param name="comparer">Comparer used to compare the values (defaults to GenericComparer)"</param>
    /// <returns>True if it is between the values, false otherwise</returns>
    public static bool Between<T>(
        this T value,
        T min,
        T max,
        IComparer<T>? comparer = null
    ) where T : IComparable
    {
        var comparerAdjust = comparer ?? new GenericComparer<T>();

        return comparerAdjust.Compare(max, value) >= 0 && comparerAdjust.Compare(value, min) >= 0;
    }

    #endregion

    #region Clamp

    /// <summary>
    /// Clamps a value between two values
    /// </summary>
    /// <param name="value">Value sent in</param>
    /// <param name="max">Max value it can be (inclusive)</param>
    /// <param name="min">Min value it can be (inclusive)</param>
    /// <param name="comparer">Comparer to use (defaults to GenericComparer)</param>
    /// <returns>The value set between Min and Max</returns>
    public static T Clamp<T>(
        this T value,
        T max,
        T min,
        IComparer<T>? comparer = null
    ) where T : IComparable
    {
        var comparerAdjust = comparer ?? new GenericComparer<T>();

        if (comparerAdjust.Compare(max, value) < 0) return max;

        if (comparerAdjust.Compare(value, min) < 0) return min;

        return value;
    }

    #endregion

    #region Max

    /// <summary>
    /// Returns the maximum value between the two
    /// </summary>
    /// <param name="a">Input A</param>
    /// <param name="b">Input B</param>
    /// <param name="comparer">Comparer to use (defaults to GenericComparer)</param>
    /// <returns>The maximum value</returns>
    public static T Max<T>(
        this T a,
        T b,
        IComparer<T>? comparer = null
    ) where T : IComparable
    {
        var comparerAdjust = comparer ?? new GenericComparer<T>();

        return comparerAdjust.Compare(a, b) < 0 ? b : a;
    }

    #endregion

    #region Min

    /// <summary>
    /// Returns the minimum value between the two
    /// </summary>
    /// <param name="a">Input A</param>
    /// <param name="b">Input B</param>
    /// <param name="comparer">Comparer to use (defaults to GenericComparer)</param>
    /// <returns>The minimum value</returns>
    public static T Min<T>(
        this T a,
        T b,
        IComparer<T>? comparer = null
    )
        where T : IComparable
    {
        var comparerAdjust = comparer ?? new GenericComparer<T>();

        return comparerAdjust.Compare(a, b) > 0 ? b : a;
    }

    #endregion

    #endregion
}