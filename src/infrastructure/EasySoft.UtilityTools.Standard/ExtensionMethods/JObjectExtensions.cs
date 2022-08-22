using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using EasySoft.UtilityTools.Standard.Assists;
using Newtonsoft.Json.Linq;

namespace EasySoft.UtilityTools.Standard.ExtensionMethods;

public static class JObjectExtension
{
    /// <summary>
    /// CopyValueTo 将json逆序列化的JObject按照键的名称为目标实体对应的属性赋值（仅限类型可以转换成功的属性）
    /// </summary>
    /// <param name="source">    JObject</param>
    /// <param name="target">    目标实体</param>
    /// <param name="ignoreCase">忽略大小写</param>
    /// <typeparam name="T">目标类型</typeparam>
    public static T CopyValueTo<T>(this JObject source, T target, bool ignoreCase = false)
    {
        if (target == null)
        {
            throw new Exception("target disallow null");
        }

        var targetProperties = target.GetType().GetProperties();

        var stop = source.First;

        foreach (var tp in targetProperties)
        {
            do
            {
                var c = ignoreCase ? tp.Name.ToLower().Equals(stop?.Path.ToLower()) : tp.Name.Equals(stop?.Path);

                if (c)
                {
                    if (stop?.Value<JProperty>()?.Value is JValue v)
                    {
                        var jsonValue = v.Value;

                        var realValue = Convert.ChangeType(jsonValue, tp.PropertyType);

                        tp.SetValue(target, realValue, null);
                    }
                }

                stop = stop?.Next;

                if (stop == null)
                {
                    break;
                }
            } while (stop != source.Last);
        }

        return target;
    }

    /// <summary>
    /// ToNameValueCollection 转换Json对象为NameValueCollection
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static NameValueCollection ToNameValueCollection(this JObject source)
    {
        return ConvertAssist.JObjectToNameValueCollection(source);
    }

    /// <summary>
    /// ToDictionary 转换Json对象为IDictionary
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static IDictionary<string, object> ToDictionary(this JObject source)
    {
        return ConvertAssist.JObjectToDictionary(source);
    }

    /// <summary>
    /// ToDictionary 转换Json对象为IDictionary
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static ExpandoObject ToExpandoObject(this JObject source)
    {
        return ConvertAssist.JObjectToExpandoObject(source);
    }
}