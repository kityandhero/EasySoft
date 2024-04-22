using EasySoft.Core.Infrastructure.Entities.Interfaces;
using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Common;
using EasySoft.Core.Sql.Interfaces;
using Newtonsoft.Json.Linq;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// EntityExtraExtension
/// </summary>
public static class EntitySelfExtension
{
    /// <summary>
    /// GetIdentificationWithModelNamePrefix
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="toLowerFirst"></param>
    /// <returns></returns>
    public static string GetIdentificationWithModelNamePrefix<T>(
        this T entity,
        bool toLowerFirst = false
    ) where T : IEntitySelf<T>
    {
        var modelName = ReflectionAssist.GetClassName(entity);

        if (toLowerFirst)
        {
            modelName = modelName.ToLowerFirst();
        }

        var identification = ReflectionAssist.GetPropertyName(entity.GetPrimaryKeyLambda());

        return $"{modelName}{identification}";
    }

    /// <summary>
    /// GetPrimaryKeyName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetPrimaryKeyName<T>(this T entity) where T : IEntitySelf<T>
    {
        var lambda = entity.GetPrimaryKeyLambda();

        var columnAttribute = Tools.GetAdvanceColumnMapperAttribute(EntityAssist.GetPropertyInfo(lambda));

        if (columnAttribute == null)
        {
            return EntityAssist.GetPropertyName(lambda);
        }

        if (string.IsNullOrWhiteSpace(columnAttribute.Name))
        {
            throw new Exception($"{nameof(columnAttribute)} disallow empty value");
        }

        return columnAttribute.Name;
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static ExpandoObject ToObject<T>(
        this T entity,
        Func<T, ExpandoObject> func
    ) where T : IEntitySelf<T>
    {
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
    ) where T : IEntitySelf<T>
    {
        var d = entity.ToExpandoObject();

        if (additionalData != null)
        {
            d.Add(new KeyValuePair<string, object?>("additional", additionalData));
        }

        d.Add(new KeyValuePair<string, object?>("Key", entity.GetPrimaryKeyValue().ToString()));

        var modelName = ReflectionAssist.GetClassName(entity).ToLowerFirst();

        var identification = ReflectionAssist.GetPropertyName(entity.GetPrimaryKeyLambda());

        var json = JsonConvertAssist.SerializeAndKeyToLower(d)
            .Replace(
                $"\"{identification.ToLowerFirst()}\"",
                $"\"{modelName}{identification}\""
            );

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        if (jObject == null)
        {
            throw new Exception("DeserializeObject result is null");
        }

        return jObject.ToExpandoObject();
    }

    /// <summary>
    /// 转换为指定属性的首字母小写的Object
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expressions"></param>
    /// <returns></returns>
    public static object? ToSimpleObject<T>(
        this T entity,
        ICollection<Expression<Func<T, object>>> expressions
    ) where T : IEntitySelf<T>
    {
        if (expressions.Count == 0)
        {
            return entity.ToObject();
        }

        var className = entity.GetType().Name;
        var eo = entity.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = new List<string>();

        foreach (var o in expressions)
        {
            var p = ReflectionAssist.GetClassAndPropertyName(o);

            p = p.Replace(className + ".", "");

            propertyList.Add(p);
        }

        foreach (var item in eo)
        {
            if (!propertyList.Contains(item.Key))
            {
                continue;
            }

            result.Add(
                item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item
            );
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetPrimaryKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    /// <summary>
    /// 转换为指定属性的首字母小写的Object
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expressions"></param>
    /// <returns></returns>
    public static object? ToSimpleObject<T>(
        this T entity,
        ICollection<Expression<Func<object>>> expressions
    ) where T : IEntitySelf<T>
    {
        if (expressions.Count == 0)
        {
            return entity.ToObject();
        }

        var className = entity.GetType().Name;
        var eo = entity.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = new List<string>();

        foreach (var o in expressions)
        {
            var p = ReflectionAssist.GetClassAndPropertyName(o);

            p = p.Replace(className + ".", "");

            propertyList.Add(p);
        }

        foreach (var item in eo)
        {
            if (!propertyList.Contains(item.Key))
            {
                continue;
            }

            result.Add(
                item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item
            );
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetPrimaryKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    /// <summary>
    /// 转换为排除指定属性的首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static object? ToSimpleObjectIgnore<T>(
        this T entity,
        ICollection<Expression<Func<T, object>>>? expressions
    ) where T : IEntitySelf<T>
    {
        if (expressions == null || expressions.Count == 0)
        {
            return entity.ToObject();
        }

        var className = entity.GetType().Name;
        var eo = entity.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = new List<string>();

        foreach (var o in expressions)
        {
            var p = ReflectionAssist.GetClassAndPropertyName(o);

            p = p.Replace(className + ".", "");

            propertyList.Add(p);
        }

        foreach (var item in eo)
        {
            if (propertyList.Contains(item.Key))
            {
                continue;
            }

            result.Add(
                item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item
            );
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetPrimaryKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    /// <summary>
    /// 转换为排除指定属性的首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static object? ToSimpleObjectIgnore<T>(
        this T entity,
        ICollection<Expression<Func<object>>> expressions
    ) where T : IEntitySelf<T>
    {
        if (expressions.Count == 0)
        {
            return entity.ToObject();
        }

        var className = entity.GetType().Name;
        var eo = entity.ToExpandoObject(false);
        var result = new ExpandoObject();
        var propertyList = new List<string>();

        foreach (var o in expressions)
        {
            var p = ReflectionAssist.GetClassAndPropertyName(o);
            p = p.Replace(className + ".", "");
            propertyList.Add(p);
        }

        foreach (var item in eo)
        {
            if (propertyList.Contains(item.Key))
            {
                continue;
            }

            result.Add(
                item.Key.Equals("Id")
                    ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                    : item
            );
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetPrimaryKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    /// <summary>
    /// ToListObject
    /// </summary>
    /// <param name="list"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<object> ToListObject<T>(this IEnumerable<T> list) where T : IEntity
    {
        return list.Select(o => (object)o.ToExpandoObject()).ToList();
    }

    /// <summary>
    /// ToListSimpleObject
    /// </summary>
    /// <param name="list"></param>
    /// <param name="expressions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<object> ToListSimpleObject<T>(
        this IEnumerable<T> list,
        ICollection<Expression<Func<object>>> expressions
    ) where T : IEntitySelf<T>
    {
        return list.Select(o => o.ToSimpleObject(expressions)).ToListFilterNullable();
    }

    /// <summary>
    /// ToListSimpleObjectIgnore
    /// </summary>
    /// <param name="list"></param>
    /// <param name="expressions"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static List<object> ToListSimpleObjectIgnore<T>(
        this IEnumerable<T> list,
        ICollection<Expression<Func<object>>> expressions
    ) where T : IEntitySelf<T>
    {
        return list.Select(o => o.ToSimpleObjectIgnore(expressions)).ToListFilterNullable();
    }
}