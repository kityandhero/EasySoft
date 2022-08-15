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

using System;
using System.Collections;
using System.Collections.Generic;

#endregion

namespace UtilityTools.Comparison
{
    /// <summary>
    /// Generic equality comparer
    /// 泛型相等比较
    /// </summary>
    /// <typeparam name="T">
    /// Data type
    /// 类型
    /// </typeparam>
    public class GenericEqualityComparer<T> : IEqualityComparer<T>
    {
        #region Functions

        /// <summary>
        /// Determines if the two items are equal
        ///  确定是否将指定的实例视为相等
        /// </summary>
        /// <param name="x">Object 1</param>
        /// <param name="y">Object 2</param>
        /// <returns>
        /// True if they are, false otherwise
        /// 相等返回true,否则返回false
        /// </returns>
        public bool Equals(T? x, T? y)
        {
            if (!typeof(T).IsValueType
                || (typeof(T).IsGenericType
                    && typeof(T).GetGenericTypeDefinition().IsAssignableFrom(typeof(Nullable<>))))
            {
                if (object.Equals(x, default(T)))
                {
                    return object.Equals(y, default(T));
                }

                if (object.Equals(y, default(T)))
                {
                    return false;
                }
            }

            if (x?.GetType() != y?.GetType())
            {
                return false;
            }

            if (x is IEnumerable enumerableX && y is IEnumerable enumerableY)
            {
                var comparer = new GenericEqualityComparer<object>();
                var xEnumerator = enumerableX.GetEnumerator();
                var yEnumerator = enumerableY.GetEnumerator();
                while (true)
                {
                    var xFinished = !xEnumerator.MoveNext();
                    var yFinished = !yEnumerator.MoveNext();

                    if (xFinished || yFinished)
                    {
                        return xFinished & yFinished;
                    }

                    if (!comparer.Equals(xEnumerator.Current, yEnumerator.Current))
                    {
                        return false;
                    }
                }
            }

            if (x is IEqualityComparer<T> tempEquality)
            {
                return tempEquality.Equals(y);
            }

            if (x is IComparable<T> tempComparable)
            {
                return tempComparable.CompareTo(y) == 0;
            }

            if (x is IComparable tempComparable2)
            {
                return tempComparable2.CompareTo(y) == 0;
            }

            return x != null && x.Equals(y);
        }

        /// <summary>
        /// Get hash code
        /// 用作特定类型的哈希函数。
        /// </summary>
        /// <param name="obj">Object to get the hash code of</param>
        /// <returns>The object's hash code</returns>
        public int GetHashCode(T obj)
        {
            return obj!.GetHashCode();
        }

        #endregion
    }
}