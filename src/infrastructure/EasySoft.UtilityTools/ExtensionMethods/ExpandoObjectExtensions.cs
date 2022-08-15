using System;
using System.Collections.Generic;
using System.Dynamic;

namespace EasySoft.UtilityTools.ExtensionMethods
{
    public static class ExpandoObjectExtensions
    {
        /// <summary>
        /// 合并下级数据，将目标中的键值数据合并到源中并返回ExpandoObject
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static ExpandoObject MergeSubordinatesData(
            this ExpandoObject source,
            ExpandoObject target
        )
        {
            var data = source.Merge(target);

            var result = new ExpandoObject();

            foreach (var d in data)
            {
                result.TryAdd(d.Key, d.Value);
            }

            return result;
        }

        public static ExpandoObject Add(
            this ExpandoObject source,
            IEnumerable<KeyValuePair<string, object?>> keyValuePairCollection
        )
        {
            return source.AddKeyValuePairCollection(keyValuePairCollection);
        }

        public static ExpandoObject AddKeyValuePairCollection(
            this ExpandoObject source,
            IEnumerable<KeyValuePair<string, object?>> keyValuePairCollection
        )
        {
            foreach (var keyValuePair in keyValuePairCollection)
            {
                source.AddKeyValuePair(keyValuePair);
            }

            return source;
        }

        public static ExpandoObject Add(
            this ExpandoObject source,
            KeyValuePair<string, object?> keyValuePair
        )
        {
            return source.AddKeyValuePair(keyValuePair);
        }

        public static ExpandoObject AddKeyValuePair(
            this ExpandoObject source,
            KeyValuePair<string, object?> keyValuePair
        )
        {
            var resultAdd = source.TryAdd(keyValuePair.Key, keyValuePair.Value ?? "");

            if (!resultAdd)
            {
                throw new Exception("ExpandoObject TryAdd Fail");
            }

            return source;
        }

        public static ExpandoObject AddDictionary(
            this ExpandoObject source,
            IDictionary<string, object?> dictionary
        )
        {
            foreach (var item in dictionary)
            {
                var resultAdd = source.TryAdd(item.Key, item.Value ?? "");

                if (!resultAdd)
                {
                    throw new Exception("ExpandoObject TryAdd Fail");
                }
            }

            return source;
        }

        /// <summary>
        /// 新增键值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ExpandoObject AddKeyValue(
            this ExpandoObject source,
            string key,
            object value
        )
        {
            var temp = new ExpandoObject();

            var resultAdd = temp.TryAdd(key, value);

            if (!resultAdd)
            {
                throw new Exception("ExpandoObject TryAdd Fail");
            }

            var data = source.Merge(temp);

            var result = new ExpandoObject();

            foreach (var d in data)
            {
                result.TryAdd(d.Key, d.Value);
            }

            return result;
        }
    }
}