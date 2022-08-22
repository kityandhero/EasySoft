using System.Collections.Generic;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods
{
    public static class Int64Extensions
    {
        #region In

        /// <summary>
        /// 包含于
        /// </summary>
        /// <param name="input"></param>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool In(this long input, params long[] array)
        {
            return CollectionAssist.In(input, array);
        }

        /// <summary>
        /// 包含于
        /// </summary>
        /// <param name="input"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In(this long input, ICollection<long> list)
        {
            return CollectionAssist.In(input, list);
        }

        #endregion In

        /// <summary>
        /// 转换long为int
        /// </summary>
        /// <param name="source">要转换的变量</param>
        /// <returns></returns>
        public static int ToInt(this long source)
        {
            return (int)source;
        }

        public static object ToObject(this long v) => v;
    }
}