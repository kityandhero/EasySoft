using System;
using System.Collections.Generic;
using System.Linq;

namespace UtilityTools.Assists
{
    public static class CollectionAssist
    {
        /// <summary>
        /// 字符串包含于
        /// </summary>
        /// <param name="target"></param>
        /// <param name="array"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool In(string target, string[] array, StringComparison comparison = StringComparison.Ordinal)
        {
            var list = array.ToList();

            return In(target, list, comparison);
        }

        /// <summary>
        /// 字符串包含于
        /// </summary>
        /// <param name="target"></param>
        /// <param name="list"></param>
        /// <param name="comparison"></param>
        /// <returns></returns>
        public static bool In(
            string target,
            IEnumerable<string> list,
            StringComparison comparison = StringComparison.Ordinal
        )
        {
            if (comparison != StringComparison.OrdinalIgnoreCase)
            {
                return list.Contains(target);
            }

            var ignoreList = list.Select(item => item.ToLower()).ToList();

            return ignoreList.Contains(target.ToLower());
        }

        /// <summary>
        /// 包含于
        /// </summary>
        /// <param name="target"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In<T>(T target, params T[] array) where T : struct
        {
            var list = array.ToList();
            return In(target, list);
        }

        /// <summary>
        /// 包含于
        /// </summary>
        /// <param name="target"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(T target, ICollection<T> list) where T : struct
        {
            return list.Contains(target);
        }
    }
}