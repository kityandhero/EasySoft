﻿/*
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

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using UtilityTools.Assists;
using UtilityTools.Comparison;

#endregion

namespace UtilityTools.ExtensionMethods
{
    /// <summary>
    /// IDictionary extensions
    /// </summary>
    public static class DictionaryExtensions
    {
        #region Functions

        #region ToExpando

        /// <summary>
        /// ToExpando
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static ExpandoObject ToExpandoObject(this IDictionary<string, object?> dictionary)
        {
            var expandoObject = new ExpandoObject();

            foreach (var kvp in dictionary)
            {
                expandoObject.AddKeyValuePair(kvp);
            }

            return expandoObject;
        }

        #endregion

        #region GetValue

        /// <summary>
        /// Gets the value from a dictionary or the default value if it isn't found
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to get the value from</param>
        /// <param name="key">Key to look for</param>
        /// <param name="defaultValue">Default value if the key is not found</param>
        /// <returns>The value associated with the key or the default value if the key is not found</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the dictionary is null</exception>
        public static TValue? GetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue? defaultValue = default
        )
        {
            if (dictionary.IsNull())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.TryGetValue(key, out var returnValue) ? returnValue : defaultValue;
        }

        #endregion

        #region SetValue

        /// <summary>
        /// Sets the value in a dictionary
        /// </summary>
        /// <typeparam name="TKey">Key type</typeparam>
        /// <typeparam name="TValue">Value type</typeparam>
        /// <param name="dictionary">Dictionary to set the value in</param>
        /// <param name="key">Key to look for</param>
        /// <param name="value">Value to add</param>
        /// <returns>The dictionary</returns>
        /// <exception cref="System.ArgumentNullException">Thrown if the dictionary is null</exception>
        public static IDictionary<TKey, TValue> SetValue<TKey, TValue>(this IDictionary<TKey, TValue> dictionary,
            TKey key, TValue value)
        {
            if (dictionary.IsNull())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (dictionary.ContainsKey(key))
                dictionary[key] = value;
            else
                dictionary.Add(key, value);
            return dictionary;
        }

        #endregion

        #region Sort

        /// <summary>
        /// Sorts a dictionary
        /// </summary>
        /// <typeparam name="T1">Key type</typeparam>
        /// <typeparam name="T2">Value type</typeparam>
        /// <param name="dictionary">Dictionary to sort</param>
        /// <returns>The sorted dictionary</returns>
        public static IDictionary<T1, T2> Sort<T1, T2>(
            this IDictionary<T1, T2> dictionary
        )
            where T1 : IComparable
        {
            if (dictionary.IsNull())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.Sort(x => x.Key);
        }

        /// <summary>
        /// Sorts a dictionary
        /// </summary>
        /// <typeparam name="T1">Key type</typeparam>
        /// <typeparam name="T2">Value type</typeparam>
        /// <param name="dictionary">Dictionary to sort</param>
        /// <param name="comparer">Comparer used to sort (defaults to GenericComparer)</param>
        /// <returns>The sorted dictionary</returns>
        public static IDictionary<T1, T2> Sort<T1, T2>(
            this IDictionary<T1, T2> dictionary,
            IComparer<T1> comparer
        )
            where T1 : IComparable
        {
            if (dictionary.IsNull())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            return dictionary.Sort(x => x.Key, comparer);
        }

        /// <summary>
        /// Sorts a dictionary
        /// </summary>
        /// <typeparam name="T1">Key type</typeparam>
        /// <typeparam name="T2">Value type</typeparam>
        /// <typeparam name="T3">Order by type</typeparam>
        /// <param name="dictionary">Dictionary to sort</param>
        /// <param name="orderBy">Function used to order the dictionary</param>
        /// <returns>The sorted dictionary</returns>
        public static IDictionary<T1, T2> Sort<T1, T2, T3>(
            this IDictionary<T1, T2> dictionary,
            Func<KeyValuePair<T1, T2>, T3> orderBy
        ) where T3 : IComparable where T1 : notnull
        {
            if (dictionary.IsNull())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (orderBy.IsNull())
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            return dictionary.OrderBy(orderBy)
                .ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Sorts a dictionary
        /// </summary>
        /// <typeparam name="T1">Key type</typeparam>
        /// <typeparam name="T2">Value type</typeparam>
        /// <typeparam name="T3">Order by type</typeparam>
        /// <param name="dictionary">Dictionary to sort</param>
        /// <param name="orderBy">Function used to order the dictionary</param>
        /// <param name="comparer">Comparer used to sort (defaults to GenericComparer)</param>
        /// <returns>The sorted dictionary</returns>
        public static IDictionary<T1, T2> Sort<T1, T2, T3>(
            this IDictionary<T1, T2> dictionary,
            Func<KeyValuePair<T1, T2>, T3> orderBy,
            IComparer<T3> comparer
        ) where T3 : IComparable where T1 : notnull
        {
            if (dictionary.IsNull())
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            if (orderBy.IsNull())
            {
                throw new ArgumentNullException(nameof(orderBy));
            }

            return dictionary.OrderBy(orderBy, comparer.Check(() => new GenericComparer<T3>()))
                .ToDictionary(x => x.Key, x => x.Value);
        }

        #endregion

        #endregion
    }
}