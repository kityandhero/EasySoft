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

using System.Threading;
using EasySoft.UtilityTools.Standard.Assists;
using EasySoft.UtilityTools.Standard.Comparison;

#endregion

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

/// <summary>
/// Generic extensions dealing with objects
/// </summary>
public static class GenericObjectExtensions
{
    #region Functions

    #region Chain

    /// <summary>
    /// Allows actions to be chained together with the caveat that if Object is null,
    /// it is replaced with the DefaultObjectValue specified.
    /// If the Action or Object (once replaced with the default object value) is null, it will return the object.
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <param name="source">Object to run the action on</param>
    /// <param name="action">Action to run</param>
    /// <param name="defaultObjectValue">Default object value</param>
    /// <returns>The original object</returns>
    public static T? Chain<T>(this T? source, Action<T>? action, T? defaultObjectValue = default)
    {
        source = source.Check(defaultObjectValue);

        if (action == null || source == null) return source;

        action(source);

        return source;
    }

    /// <summary>
    /// Allows actions to be chained together. It also has a couple of checks in there:
    /// 1) If the function is null, it returns the default return value specified.
    /// 2) If the object is null, it will replace it with the default object value specified.
    /// 3) If the object, once replaced with the default object value specified, is null, it will return the default return value specified.
    /// 4) If the return value from the function is null, it returns the default return value specified.
    /// </summary>
    /// <typeparam name="T">The type of the object</typeparam>
    /// <typeparam name="TR">Return type</typeparam>
    /// <param name="source">Object to run the action on</param>
    /// <param name="function">Function to run</param>
    /// <param name="defaultObjectValue">Default object value</param>
    /// <param name="defaultReturnValue">Default return value</param>
    /// <returns>The result from the function</returns>
    public static TR? Chain<T, TR>(
        this T? source,
        Func<T, TR>? function,
        TR? defaultReturnValue = default!,
        T? defaultObjectValue = default
    )
    {
        source = source.Check(defaultObjectValue);

        if (function == null || source == null) return defaultReturnValue;

        return function(source).Check(defaultReturnValue);
    }

    #endregion

    #region Execute

    /// <summary>
    /// Executes a function, repeating it a number of times in case it fails
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="function">Function to run</param>
    /// <param name="attempts">Number of times to attempt it</param>
    /// <param name="retryDelay">The amount of milliseconds to wait between tries</param>
    /// <param name="timeOut">Max amount of time to wait for the function to run (waits for the current attempt to finish before checking)</param>
    /// <returns>The returned value from the function</returns>
    public static T Execute<T>(
        this Func<T> function,
        int attempts = 3,
        int retryDelay = 0,
        int timeOut = int.MaxValue
    )
    {
        if (function.IsNull()) throw new ArgumentNullException(nameof(function));

        Exception? holder = null;

        long start = Environment.TickCount;

        while (attempts > 0)
        {
            try
            {
                return function();
            }
            catch (Exception? e)
            {
                holder = e;
            }

            if (Environment.TickCount - start > timeOut) break;

            Thread.Sleep(retryDelay);

            --attempts;
        }

        throw holder!;
    }

    /// <summary>
    /// Executes an action, repeating it a number of times in case it fails
    /// </summary>
    /// <param name="action">Action to run</param>
    /// <param name="attempts">Number of times to attempt it</param>
    /// <param name="retryDelay">The amount of milliseconds to wait between tries</param>
    /// <param name="timeOut">Max amount of time to wait for the function to run (waits for the current attempt to finish before checking)</param>
    public static void Execute(this Action action, int attempts = 3, int retryDelay = 0, int timeOut = int.MaxValue)
    {
        if (action.IsNull()) throw new ArgumentNullException(nameof(action));

        Exception? holder = null;

        long start = Environment.TickCount;

        while (attempts > 0)
        {
            try
            {
                action();
            }
            catch (Exception? e)
            {
                holder = e;
            }

            if (Environment.TickCount - start > timeOut) break;

            Thread.Sleep(retryDelay);

            --attempts;
        }

        if (holder != null) throw holder;
    }

    #endregion

    #region Check

    /// <summary>
    /// Checks to see if the object meets all the criteria. If it does, it returns the object. If it does not, it returns the default object
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="source">Object to check</param>
    /// <param name="defaultValue">The default value to return</param>
    /// <param name="predicate">Predicate to check the object against</param>
    /// <returns>The default object if it fails the criteria, the object otherwise</returns>
    public static T? Check<T>(this T? source, Predicate<T> predicate, T? defaultValue = default)
    {
        if (predicate.IsNull()) throw new ArgumentNullException(nameof(predicate));

        return source != null && predicate(source) ? source : defaultValue;
    }

    /// <summary>
    /// Checks to see if the object meets all the criteria. If it does, it returns the object. If it does not, it returns the default object
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="source">Object to check</param>
    /// <param name="defaultValue">The default value to return</param>
    /// <param name="predicate">Predicate to check the object against</param>
    /// <returns>The default object if it fails the criteria, the object otherwise</returns>
    public static T Check<T>(this T source, Predicate<T> predicate, Func<T> defaultValue)
    {
        if (predicate.IsNull()) throw new ArgumentNullException(nameof(predicate));

        if (defaultValue.IsNull()) throw new ArgumentNullException(nameof(defaultValue));

        return predicate(source) ? source : defaultValue();
    }

    /// <summary>
    /// Checks to see if the object is null. If it is, it returns the default object, otherwise the object is returned.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="source">Object to check</param>
    /// <param name="defaultValue">The default value to return</param>
    /// <returns>The default object if it is null, the object otherwise</returns>
    public static T? Check<T>(this T source, T? defaultValue = default)
    {
        return source.Check(x => !x.IsNull(), defaultValue);
    }

    /// <summary>
    /// Checks to see if the object is null. If it is, it returns the default object, otherwise the object is returned.
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="source">Object to check</param>
    /// <param name="defaultValue">The default value to return</param>
    /// <returns>The default object if it is null, the object otherwise</returns>
    public static T Check<T>(this T source, Func<T> defaultValue)
    {
        if (defaultValue.IsNull()) throw new ArgumentNullException(nameof(defaultValue));

        // ReSharper disable CompareNonConstrainedGenericWithNull
        return source.Check(x => x != null, defaultValue);
        // ReSharper restore CompareNonConstrainedGenericWithNull
    }

    #endregion

    #region Is

    /// <summary>
    /// Determines if the object passes the predicate passed in
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="source">Object to test</param>
    /// <param name="predicate">Predicate to test</param>
    /// <returns>True if the object passes the predicate, false otherwise</returns>
    public static bool Is<T>(this T source, Predicate<T> predicate)
    {
        if (predicate.IsNull()) throw new ArgumentNullException(nameof(predicate));

        return predicate(source);
    }

    /// <summary>
    /// Determines if the object is equal to a specific value
    /// </summary>
    /// <typeparam name="T">Object type</typeparam>
    /// <param name="source">Object to test</param>
    /// <param name="comparisonObject">Comparison object</param>
    /// <param name="comparer">Comparer</param>
    /// <returns>True if the object passes the predicate, false otherwise</returns>
    public static bool Is<T>(this T source, T comparisonObject, IEqualityComparer<T>? comparer = null)
    {
        comparer = comparer.Check(() => new GenericEqualityComparer<T>());

        return comparer != null && comparer.Equals(source, comparisonObject);
    }

    #endregion

    #region ThrowIf

    /// <summary>
    /// Throws the specified exception if the predicate is true for the item
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is true</param>
    /// <returns>the original Item</returns>
    public static T ThrowIf<T>(this T item, Predicate<T> predicate, Func<Exception> exception)
    {
        if (predicate(item))
            throw exception();
        return item;
    }

    /// <summary>
    /// Throws the specified exception if the predicate is true for the item
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is true</param>
    /// <returns>the original Item</returns>
    public static T ThrowIf<T>(this T item, Predicate<T> predicate, Exception exception)
    {
        if (predicate(item)) throw exception;

        return item;
    }

    #endregion

    #region ThrowIfNot

    /// <summary>
    /// Throws the specified exception if the predicate is false for the item
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The item</param>
    /// <param name="predicate">Predicate to check</param>
    /// <param name="exception">Exception to throw if predicate is false</param>
    /// <returns>the original Item</returns>
    public static T ThrowIfNot<T>(this T item, Predicate<T> predicate, Exception exception)
    {
        return item.ThrowIf(x => !predicate(x), exception);
    }

    #endregion

    #region ThrowIfNotNull

    /// <summary>
    /// Determines if the object is not null and throws an  if it is
    /// </summary>
    /// <param name="item">The object to check</param>
    /// <param name="name">Name of the argument</param>
    /// <returns>Returns Item</returns>
    public static T ThrowIfNotNull<T>(this T item, string name)
    {
        return item.ThrowIfNotNull(new ArgumentException(name));
    }

    /// <summary>
    /// Determines if the object is not null and throws the exception passed in if it is
    /// </summary>
    /// <param name="item">The object to check</param>
    /// <param name="exception">Exception to throw</param>
    /// <returns>Returns Item</returns>
    public static T ThrowIfNotNull<T>(this T item, Exception exception)
    {
        return item.ThrowIf(x => x != null && !Convert.IsDBNull(x), exception);
    }

    #endregion

    #region ThrowIfNull

    /// <summary>
    /// Determines if the object is null and throws an ArgumentNullException if it is
    /// </summary>
    /// <param name="item">The object to check</param>
    /// <param name="name">Name of the argument</param>
    /// <returns>Returns Item</returns>
    public static T ThrowIfNull<T>(this T item, string name)
    {
        return item.ThrowIfNull(new ArgumentNullException(name));
    }

    /// <summary>
    /// Determines if the object is null and throws the exception passed in if it is
    /// </summary>
    /// <param name="item">The object to check</param>
    /// <param name="exception">Exception to throw</param>
    /// <returns>Returns Item</returns>
    public static T ThrowIfNull<T>(this T item, Exception exception)
    {
        // ReSharper disable CompareNonConstrainedGenericWithNull
        return item.ThrowIf(x => x == null || Convert.IsDBNull(x), exception);
        // ReSharper restore CompareNonConstrainedGenericWithNull
    }

    #endregion

    #region ThrowIfNotNullOrEmpty

    /// <summary>
    /// Determines if the IEnumerable is not null or empty and throws an  if it is
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The object to check</param>
    /// <param name="name">Name of the argument</param>
    /// <returns>Returns Item</returns>
    public static IEnumerable<T> ThrowIfNotNullOrEmpty<T>(this IEnumerable<T> item, string name)
    {
        return item.ThrowIfNotNullOrEmpty(new ArgumentException(name));
    }

    /// <summary>
    /// Determines if the IEnumerable is not null or empty and throws the exception passed in if it is
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The object to check</param>
    /// <param name="exception">Exception to throw</param>
    /// <returns>Returns Item</returns>
    public static IEnumerable<T> ThrowIfNotNullOrEmpty<T>(this IEnumerable<T> item, Exception exception)
    {
        return item.ThrowIf(x => x.Any(), exception);
    }

    #endregion

    #region ThrowIfNullOrEmpty

    /// <summary>
    /// Determines if the IEnumerable is null or empty and throws an ArgumentNullException if it is
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The object to check</param>
    /// <param name="name">Name of the argument</param>
    /// <returns>Returns Item</returns>
    public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T> item, string name)
    {
        return item.ThrowIfNullOrEmpty(new ArgumentNullException(name));
    }

    /// <summary>
    /// Determines if the IEnumerable is null or empty and throws the exception passed in if it is
    /// </summary>
    /// <typeparam name="T">Item type</typeparam>
    /// <param name="item">The object to check</param>
    /// <param name="exception">Exception to throw</param>
    /// <returns>Returns Item</returns>
    public static IEnumerable<T> ThrowIfNullOrEmpty<T>(this IEnumerable<T> item, Exception exception)
    {
        return item.ThrowIf(x => !x.Any(), exception);
    }

    #endregion

    #region Times

    /// <summary>
    /// Runs a function based on the number of times specified and returns the results
    /// </summary>
    /// <typeparam name="T">Type that gets returned</typeparam>
    /// <param name="count">Number of times the function should run</param>
    /// <param name="function">The function that should run</param>
    /// <returns>The results from the function</returns>
    public static IEnumerable<T> Times<T>(this int count, Func<int, T> function)
    {
        var returnValue = new List<T>();

        for (var x = 0; x < count; ++x) returnValue.Add(function(x));

        return returnValue;
    }

    /// <summary>
    /// Runs an action based on the number of times specified
    /// </summary>
    /// <param name="count">Number of times to run the action</param>
    /// <param name="action">Action to run</param>
    public static void Times(this int count, Action<int> action)
    {
        for (var x = 0; x < count; ++x)
            action(x);
    }

    #endregion

    #endregion
}