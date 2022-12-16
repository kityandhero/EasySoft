/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings

using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Comparison;

#endregion

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// IEnumerable extensions
/// </summary>
public static class EnumerableExtensions
{
    #region Functions

    #region Concat

    /// <summary>
    /// Combines multiple IEnumerables together and returns a new IEnumerable containing all of the values
    /// </summary>
    /// <typeparam name="T">Type of the data in the IEnumerable</typeparam>
    /// <param name="enumerable1">IEnumerable 1</param>
    /// <param name="additions">IEnumerables to concat onto the first item</param>
    /// <returns>A new IEnumerable containing all values</returns>
    /// <example>
    /// <code>
    ///  int[] TestObject1 = new int[] { 1, 2, 3 };
    ///  int[] TestObject2 = new int[] { 4, 5, 6 };
    ///  int[] TestObject3 = new int[] { 7, 8, 9 };
    ///  TestObject1 = TestObject1.Concat(TestObject2, TestObject3).ToArray();
    /// </code>
    /// </example>
    public static IEnumerable<T> Concat<T>(this IEnumerable<T> enumerable1, params IEnumerable<T>[] additions)
    {
        if (enumerable1.IsNull()) throw new ArgumentNullException(nameof(enumerable1));

        if (additions.IsNull()) throw new ArgumentNullException(nameof(additions));

        var results = new List<T>();
        results.AddRange(enumerable1);
        foreach (var t in additions)
            results.AddRange(t);
        return results;
    }

    #endregion

    #region ElementsBetween

    /// <summary>
    /// Returns elements starting at the index and ending at the end index
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">List to search</param>
    /// <param name="start">Start index (inclusive)</param>
    /// <param name="end">End index (exclusive)</param>
    /// <returns>The items between the start and end index</returns>
    public static IEnumerable<T> ElementsBetween<T>(this IEnumerable<T> list, int start, int end)
    {
        var enumerable = list as T[] ?? list.ToArray();

        if (end > enumerable.Length) end = enumerable.Length;

        if (start < 0) start = 0;

        var returnList = new List<T>();

        for (var x = start; x < end; ++x) returnList.Add(enumerable.ElementAt(x));

        return returnList;
    }

    #endregion

    #region For

    /// <summary>
    /// Does an action for each item in the IEnumerable between the start and end indexes
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="start">Item to start with</param>
    /// <param name="end">Item to end with</param>
    /// <param name="action">Action to do</param>
    /// <returns>The original list</returns>
    public static IEnumerable<T> For<T>(this IEnumerable<T> list, int start, int end, Action<T> action)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        var enumerable = list as T[] ?? list.ToArray();
        foreach (var item in enumerable.ElementsBetween(start, end + 1))
            action(item);
        return enumerable;
    }

    /// <summary>
    /// Does a function for each item in the IEnumerable between the start and end indexes and returns an IEnumerable of the results
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="TR">Return type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="start">Item to start with</param>
    /// <param name="end">Item to end with</param>
    /// <param name="function">Function to do</param>
    /// <returns>The resulting list</returns>
    public static IEnumerable<TR> For<T, TR>(this IEnumerable<T> list, int start, int end, Func<T, TR> function)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        var returnValues = new List<TR>();
        // ReSharper disable LoopCanBeConvertedToQuery
        foreach (var item in list.ElementsBetween(start, end + 1))
            // ReSharper restore LoopCanBeConvertedToQuery
            returnValues.Add(function(item));
        return returnValues;
    }

    #endregion

    #region ForEach

    /// <summary>
    /// Does an action for each item in the IEnumerable
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="action">Action to do</param>
    /// <returns>The original list</returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        var forEach = list as T[] ?? list.ToArray();
        foreach (var item in forEach)
            action(item);
        return forEach;
    }

    /// <summary>
    /// Does a function for each item in the IEnumerable, returning a list of the results
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="TR">Return type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="function">Function to do</param>
    /// <returns>The resulting list</returns>
    public static IEnumerable<TR?> ForEach<T, TR>(this IEnumerable<T> list, Func<T, TR?> function)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        var returnValues = new List<TR?>();
        // ReSharper disable LoopCanBeConvertedToQuery
        foreach (var item in list)
            // ReSharper restore LoopCanBeConvertedToQuery
            returnValues.Add(function(item));
        return returnValues;
    }

    /// <summary>
    /// Does an action for each item in the IEnumerable
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="action">Action to do</param>
    /// <param name="catchAction">Action that occurs if an exception occurs</param>
    /// <returns>The original list</returns>
    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> list, Action<T> action,
        Action<T, Exception> catchAction)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        if (catchAction.IsNull()) throw new ArgumentNullException(nameof(catchAction));

        var forEach = list as T[] ?? list.ToArray();
        foreach (var item in forEach)
            try
            {
                action(item);
            }
            catch (Exception e)
            {
                catchAction(item, e);
            }

        return forEach;
    }

    /// <summary>
    /// Does a function for each item in the IEnumerable, returning a list of the results
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="TR">Return type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="function">Function to do</param>
    /// <param name="catchAction">Action that occurs if an exception occurs</param>
    /// <returns>The resulting list</returns>
    public static IEnumerable<TR> ForEach<T, TR>(this IEnumerable<T> list, Func<T, TR> function,
        Action<T, Exception> catchAction)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        if (catchAction.IsNull()) throw new ArgumentNullException(nameof(catchAction));

        var returnValues = new List<TR>();
        foreach (var item in list)
            try
            {
                returnValues.Add(function(item));
            }
            catch (Exception e)
            {
                catchAction(item, e);
            }

        return returnValues;
    }

    #endregion

    #region ForParallel

    /// <summary>
    /// Does an action for each item in the IEnumerable between the start and end indexes in parallel
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="start">Item to start with</param>
    /// <param name="end">Item to end with</param>
    /// <param name="action">Action to do</param>
    /// <returns>The original list</returns>
    public static IEnumerable<T> ForParallel<T>(this IEnumerable<T> list, int start, int end, Action<T> action)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        Parallel.For(start, end + 1, x => action(list.ElementAt(x)));
        return list;
    }

    /// <summary>
    /// Does an action for each item in the IEnumerable between the start and end indexes in parallel
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="TR">Results type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="start">Item to start with</param>
    /// <param name="end">Item to end with</param>
    /// <param name="function">Function to do</param>
    /// <returns>The resulting list</returns>
    public static IEnumerable<TR> ForParallel<T, TR>(this IEnumerable<T> list, int start, int end,
        Func<T, TR> function)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        var results = new TR[end + 1 - start];
        Parallel.For(start, end + 1, x => results[x - start] = function(list.ElementAt(x)));
        return results;
    }

    #endregion

    #region ForEachParallel

    /// <summary>
    /// Does an action for each item in the IEnumerable in parallel
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="action">Action to do</param>
    /// <returns>The original list</returns>
    public static IEnumerable<T> ForEachParallel<T>(this IEnumerable<T> list, Action<T> action)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        var forEachParallel = list as T[] ?? list.ToArray();
        Parallel.ForEach(forEachParallel, action);
        return forEachParallel;
    }

    /// <summary>
    /// Does an action for each item in the IEnumerable in parallel
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="TR">Results type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="function">Function to do</param>
    /// <returns>The results in an IEnumerable list</returns>
    public static IEnumerable<TR> ForEachParallel<T, TR>(this IEnumerable<T> list, Func<T, TR> function)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        var enumerable = list as T[] ?? list.ToArray();
        return enumerable.ForParallel(0, enumerable.Length - 1, function);
    }

    /// <summary>
    /// Does an action for each item in the IEnumerable
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="action">Action to do</param>
    /// <param name="catchAction">Action that occurs if an exception occurs</param>
    /// <returns>The original list</returns>
    public static IEnumerable<T> ForEachParallel<T>(this IEnumerable<T> list, Action<T> action,
        Action<T, Exception> catchAction)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        if (catchAction.IsNull()) throw new ArgumentNullException(nameof(catchAction));

        var forEachParallel = list as T[] ?? list.ToArray();
        Parallel.ForEach(forEachParallel, delegate(T item)
        {
            try
            {
                action(item);
            }
            catch (Exception e)
            {
                catchAction(item, e);
            }
        });
        return forEachParallel;
    }

    /// <summary>
    /// Does a function for each item in the IEnumerable, returning a list of the results
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <typeparam name="TR">Return type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="function">Function to do</param>
    /// <param name="catchAction">Action that occurs if an exception occurs</param>
    /// <returns>The resulting list</returns>
    public static IEnumerable<TR> ForEachParallel<T, TR>(this IEnumerable<T> list, Func<T, TR> function,
        Action<T, Exception> catchAction)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        if (catchAction.IsNull()) throw new ArgumentNullException(nameof(catchAction));

        var returnValues = new List<TR>();
        Parallel.ForEach(list, delegate(T item)
        {
            try
            {
                returnValues.Add(function(item));
            }
            catch (Exception e)
            {
                catchAction(item, e);
            }
        });
        return returnValues;
    }

    #endregion

    #region Last

    /// <summary>
    /// Returns the last X number of items from the list
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">IEnumerable to iterate over</param>
    /// <param name="count">Numbers of items to return</param>
    /// <returns>The last X items from the list</returns>
    public static IEnumerable<T> Last<T>(this IEnumerable<T> list, int count)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        var enumerable = list as T[] ?? list.ToArray();
        return enumerable.ElementsBetween(enumerable.Length - count, enumerable.Length);
    }

    #endregion

    #region PositionOf

    /// <summary>
    /// Determines the position of an object if it is present, otherwise it returns -1
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="list">List of objects to search</param>
    /// <param name="value">Object to find the position of</param>
    /// <param name="equalityComparer">Equality comparer used to determine if the object is present</param>
    /// <returns>The position of the object if it is present, otherwise -1</returns>
    public static int PositionOf<T>(
        this IEnumerable<T> list,
        T value,
        IEqualityComparer<T>? equalityComparer = null
    )
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        equalityComparer = equalityComparer.Check(() => new GenericEqualityComparer<T>());
        var count = 0;
        foreach (var item in list)
        {
            if (equalityComparer != null && equalityComparer.Equals(value, item)) return count;

            ++count;
        }

        return -1;
    }

    #endregion

    #region Remove

    /// <summary>
    /// Removes values from a list that meet the criteria set forth by the predicate
    /// </summary>
    /// <typeparam name="T">Value type</typeparam>
    /// <param name="value">List to cull items from</param>
    /// <param name="predicate">Predicate that determines what items to remove</param>
    /// <returns>An IEnumerable with the objects that meet the criteria removed</returns>
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> value, Func<T, bool> predicate)
    {
        if (predicate.IsNull()) throw new ArgumentNullException(nameof(predicate));

        return value.Where(x => !predicate(x));
    }

    #endregion

    #region ToArray

    /// <summary>
    /// Converts a list to an array
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TTArget">Target type</typeparam>
    /// <param name="list">List to convert</param>
    /// <param name="convertingFunction">Function used to convert each item</param>
    /// <returns>The array containing the items from the list</returns>
    public static TTArget?[] ToArray<TSource, TTArget>(this IEnumerable<TSource> list,
        Func<TSource, TTArget?> convertingFunction)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (convertingFunction.IsNull()) throw new ArgumentNullException(nameof(convertingFunction));

        return list.ForEach(convertingFunction).ToArray();
    }

    #endregion

    #region ToDataTable

    /// <summary>
    /// Converts the IEnumerable to a DataTable
    /// </summary>
    /// <typeparam name="T">Type of the objects in the IEnumerable</typeparam>
    /// <param name="list">List to convert</param>
    /// <param name="columns">Column names (if empty, uses property names)</param>
    /// <returns>The list as a DataTable</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> list, params string?[] columns)
    {
        var returnValue = new DataTable { Locale = CultureInfo.CurrentCulture };
        var enumerable = list as T[] ?? list.ToArray();

        if (!enumerable.Any()) return returnValue;

        var properties = typeof(T).GetProperties();

        if (columns.Length == 0) columns = properties.ToArray(x => x.Name);

        columns.ForEach(x =>
        {
            var firstOrDefault = properties.FirstOrDefault(z => z.Name == x);
            return firstOrDefault != null ? returnValue.Columns.Add(x, firstOrDefault.PropertyType) : null;
        });

        var row = new object?[columns.Length];

        foreach (var item in enumerable)
        {
            for (var x = 0; x < row.Length; ++x)
            {
                var firstOrDefault = properties.FirstOrDefault(z => z.Name == columns[x]);

                if (firstOrDefault != null) row[x] = firstOrDefault.GetValue(item, Array.Empty<object>());
            }

            returnValue.Rows.Add(row);
        }

        return returnValue;
    }

    #endregion

    #region ToList

    /// <summary>
    /// Converts an IEnumerable to a list
    /// </summary>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TTarget">Target type</typeparam>
    /// <param name="list">IEnumerable to convert</param>
    /// <param name="convertingFunction">Function used to convert each item</param>
    /// <returns>The list containing the items from the IEnumerable</returns>
    public static List<TTarget?> ToList<TSource, TTarget>(this IEnumerable<TSource> list,
        Func<TSource, TTarget?> convertingFunction)
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        if (convertingFunction.IsNull()) throw new ArgumentNullException(nameof(convertingFunction));

        return list.ForEach(convertingFunction).ToList();
    }

    #endregion

    #region ToString

    /// <summary>
    /// Converts the list to a string where each item is seperated by the Seperator
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="list">List to convert</param>
    /// <param name="itemOutput">Used to convert the item to a string (defaults to calling ToString)</param>
    /// <param name="separator">Seperator to use between items (defaults to ,)</param>
    /// <returns>The string version of the list</returns>
    public static string ToString<T>(
        this IEnumerable<T> list,
        Func<T, string>? itemOutput = null,
        string? separator = ","
    )
    {
        if (list.IsNull()) throw new ArgumentNullException(nameof(list));

        separator = separator.Check("");
        itemOutput = itemOutput.Check(x => x?.ToString() ?? "");

        var builder = new StringBuilder();
        var tempSeparator = "";

        list.ForEach(x =>
        {
            builder.Append(tempSeparator).Append(itemOutput?.Invoke(x));
            tempSeparator = separator;
        });

        return builder.ToString();
    }

    #endregion

    #region ThrowIfAll

    /// <summary>
    /// Throws the specified exception if the predicate is true for all items
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="list">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is true</param>
    /// <returns>the original Item</returns>
    public static IEnumerable<T> ThrowIfAll<T>(this IEnumerable<T> list, Predicate<T> predicate,
        Func<Exception> exception)
    {
        var throwIfAll = list as T[] ?? list.ToArray();

        if (throwIfAll.Any(item => !predicate(item))) return throwIfAll;

        throw exception();
    }

    /// <summary>
    /// Throws the specified exception if the predicate is true for all items
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="list">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is true</param>
    /// <returns>the original Item</returns>
    public static IEnumerable<T> ThrowIfAll<T>(this IEnumerable<T> list, Predicate<T> predicate,
        Exception exception)
    {
        var throwIfAll = list as T[] ?? list.ToArray();

        if (throwIfAll.Any(item => !predicate(item))) return throwIfAll;

        throw exception;
    }

    #endregion

    #region ThrowIfAny

    /// <summary>
    /// Throws the specified exception if the predicate is true for any items
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="list">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is true</param>
    /// <returns>the original Item</returns>
    public static IEnumerable<T> ThrowIfAny<T>(this IEnumerable<T> list, Predicate<T> predicate,
        Func<Exception> exception)
    {
        var throwIfAny = list as T[] ?? list.ToArray();

        if (throwIfAny.Any(item => predicate(item))) throw exception();

        return throwIfAny;
    }

    /// <summary>
    /// Throws the specified exception if the predicate is true for any items
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="list">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is true</param>
    /// <returns>the original Item</returns>
    public static IEnumerable<T> ThrowIfAny<T>(this IEnumerable<T> list, Predicate<T> predicate,
        Exception exception)
    {
        var throwIfAny = list as T[] ?? list.ToArray();

        if (throwIfAny.Any(item => predicate(item))) throw exception;

        return throwIfAny;
    }

    #endregion

    /// <summary>
    /// IsNullOrEmpty
    /// </summary>
    /// <param name="source"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
    {
        return source == null || !source.Any();
    }

    #endregion
}