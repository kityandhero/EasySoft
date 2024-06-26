﻿using EasySoft.UtilityTools.Standard.Interfaces;

namespace EasySoft.Core.ExchangeRegulation.Extensions;

/// <summary>
/// QueueMessageStoreExtensions
/// </summary>
public static class QueueMessageStoreExtensions
{
    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static ExpandoObject ToObject<T>(
        this T entity,
        Func<T, ExpandoObject> func
    ) where T : IQueueMessageStore
    {
        // Action<BaseMethodEntity<T>> a = o => { };

        var additionalData = func(entity);

        return entity.ToObject(additionalData);
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static ExpandoObject ToObject<T>(
        this T entity,
        ExpandoObject? additionalData = null
    ) where T : IQueueMessageStore
    {
        var d = entity.ToExpandoObject();

        if (additionalData != null)
        {
            d.Add(new KeyValuePair<string, object?>("additional", additionalData));
        }

        d.Add(new KeyValuePair<string, object?>("Key", entity.GetId()));

        var modelName = ReflectionAssist.GetClassName(entity).ToLowerFirst();

        var identification = entity.GetIdentificationName();

        var json = JsonConvertAssist.SerializeAndKeyToLower(d)
            .Replace(
                $"\"{identification.ToLowerFirst()}\"",
                $"\"{modelName}{identification}\""
            );

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        return jObject?.ToExpandoObject() ?? new ExpandoObject();
    }

    /// <summary>
    /// 转换为指定属性的首字母小写的Object
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static ExpandoObject ToSimpleObject<T>(
        this T entity,
        params Expression<Func<T, object>>[] expression
    ) where T : IQueueMessageStore
    {
        if (expression.Length == 0)
        {
            throw new Exception("expression disallow empty");
        }

        var className = entity.GetType().Name;
        var eo = entity.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = expression.Select(ReflectionAssist.GetPropertyName).ToList();

        var identification = entity.GetIdentificationName();

        foreach (var item in eo)
        {
            if (!propertyList.Contains(item.Key))
            {
                continue;
            }

            if (item.Key.Equals(identification))
            {
                result.Add(new KeyValuePair<string, object?>(className + $"{identification}", item.Value));
            }
            else
            {
                result.Add(item);
            }
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetId()));

        var json = JsonConvertAssist.SerializeAndKeyToLower(result);

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        return jObject?.ToExpandoObject() ?? new ExpandoObject();
    }

    /// <summary>
    /// 转换为排除指定属性的首字母小写的Object
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expression"></param>
    /// <returns></returns>
    public static ExpandoObject ToSimpleObjectIgnore<T>(
        this T entity,
        params Expression<Func<T, object>>[] expression
    ) where T : IQueueMessageStore
    {
        if (expression.Length == 0)
        {
            throw new Exception("expression disallow empty");
        }

        var className = entity.GetType().Name;
        var eo = entity.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = expression.Select(ReflectionAssist.GetPropertyName).ToList();

        var identification = entity.GetIdentificationName();

        foreach (var item in eo)
        {
            if (propertyList.Contains(item.Key))
            {
                continue;
            }

            if (item.Key.Equals(identification))
            {
                result.Add(new KeyValuePair<string, object?>(className + $"{identification}", item.Value));
            }
            else
            {
                result.Add(item);
            }
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetId()));

        var jObject = JsonConvert.DeserializeObject<JObject>(JsonConvertAssist.SerializeAndKeyToLower(result));

        return jObject?.ToExpandoObject() ?? new ExpandoObject();
    }

    /// <summary>  
    /// ToObjectJson
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string ToObjectJson<T>(this T entity) where T : IQueueMessageStore
    {
        return JsonConvert.SerializeObject(entity.ToObject());
    }
}