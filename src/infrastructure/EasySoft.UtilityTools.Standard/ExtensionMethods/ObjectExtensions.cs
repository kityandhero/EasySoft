using System.Collections.Generic;
using System.Dynamic;
using EasySoft.UtilityTools.Standard.Assists;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static T ConvertTo<T>(this object? value, object? defaultValue = null) where T : new()
        {
            return ConvertAssist.ObjectTo<T>(value, defaultValue)!;
        }

        /// <summary>
        /// 转换为ExpandoObject
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keyFirstLetterToLower"></param>
        /// <returns></returns>
        public static ExpandoObject ToExpandoObject(this object? source, bool keyFirstLetterToLower = true)
        {
            var result = new ExpandoObject();

            var type = source?.GetType();
            var properties = type?.GetProperties();

            if (properties == null)
            {
                return result;
            }

            foreach (var p in properties)
            {
                result.AddKeyValuePair(
                    new KeyValuePair<string, object?>(
                        keyFirstLetterToLower ? p.Name.ToLowerFirst() : p.Name,
                        p.GetValue(source, null)
                    )
                );
            }

            return result;
        }
    }
}