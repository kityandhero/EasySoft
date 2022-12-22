using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Common;

namespace EasySoft.Core.Sql.Extensions;

/// <summary>
/// EntityExtraExtension
/// </summary>
public static class EntityExtraExtension
{
    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <param name="nameList"></param>
    /// <param name="valueList"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static T BuildNameAndValueList<T>(
        this T model,
        out List<string> nameList,
        out List<string> valueList
    ) where T : IEntity
    {
        var names = new List<string>();
        var values = new List<string>();

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType().GetProperties().ForEach(p =>
        {
            if (!p.CanWrite) return;

            var attribute = Tools.GetAdvanceColumnAttribute(p);

            if (attribute == null) throw new Exception("AdvanceColumnAttribute is null");

            names.Add($"{fieldDecorateStart}{attribute.Name}{fieldDecorateEnd}");

            values.Add($"@{p.Name}");
        });

        nameList = names;
        valueList = values;

        return model;
    }

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static List<string> BuildNameValueList<T>(
        this T model
    ) where T : IEntity
    {
        var nameValueList = new List<string>();

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType().GetProperties().ForEach(p =>
        {
            if (!p.CanWrite) return;

            var columnName = TransferAssist.GetColumnName(p);

            if (string.IsNullOrWhiteSpace(columnName)) throw new Exception("AdvanceColumnAttribute is null");

            if (!p.Name.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueList.Add(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name}"
                );
        });

        return nameValueList;
    }

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static List<string> BuildNameValueList<T>(
        this T model,
        ICollection<Expression<Func<T>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var nameValueList = new List<string>();

        var listPropertyName = new List<string>();

        listPropertyLambda.ForEach(expression =>
        {
            var propertyName = ReflectionAssist.GetPropertyName(expression);

            listPropertyName.Add(propertyName);
        });

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType().GetProperties().ForEach(p =>
        {
            if (!p.CanWrite) return;

            if (!listPropertyName.Contains(p.Name)) return;

            var columnName = TransferAssist.GetColumnName(p);

            if (string.IsNullOrWhiteSpace(columnName)) throw new Exception("AdvanceColumnAttribute is null");

            if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueList.Add(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name}"
                );
        });

        return nameValueList;
    }

    /// <summary>
    /// BuildNameValueList
    /// </summary>
    /// <param name="model"></param>
    /// <param name="listPropertyLambda"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    internal static List<string> BuildNameValueList<T>(
        this T model,
        ICollection<Expression<Func<T, object>>> listPropertyLambda
    ) where T : IEntity, new()
    {
        if (listPropertyLambda == null || !listPropertyLambda.Any()) throw new Exception("缺少指定的更新属性");

        var nameValueList = new List<string>();

        var listPropertyName = new List<string>();

        listPropertyLambda.ForEach(expression =>
        {
            var propertyName = ReflectionAssist.GetPropertyName(expression);

            listPropertyName.Add(propertyName);
        });

        var fieldDecorateStart = model.GetSqlFieldDecorateStart();
        var fieldDecorateEnd = model.GetSqlFieldDecorateEnd();

        model.GetType().GetProperties().ForEach(p =>
        {
            if (!p.CanWrite) return;

            if (!listPropertyName.Contains(p.Name)) return;

            var columnName = TransferAssist.GetColumnName(p);

            if (string.IsNullOrWhiteSpace(columnName)) throw new Exception("AdvanceColumnAttribute is null");

            if (!columnName.ToLower().Equals(Constants.DefaultTablePrimaryKey))
                nameValueList.Add(
                    $"{fieldDecorateStart}{columnName}{fieldDecorateEnd} = @{p.Name}"
                );
        });

        return nameValueList;
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static object? ToObject<T>(this T entity) where T : IEntity
    {
        var d = entity.ToExpandoObject();

        d.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

        var modelName = ReflectionAssist.GetClassName(entity).ToLowerFirst();

        return JsonConvert.DeserializeObject(
            JsonConvertAssist.SerializeAndKeyToLower(d).Replace(
                "\"id\"",
                "\"" + modelName + "Id" + "\""
            )
        );
    }

    /// <summary>
    /// 转换为指定属性的首字母小写的Object
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expressions"></param>
    /// <returns></returns>
    public static object? ToSimpleObject<T>(this T entity, ICollection<Expression<Func<T, object>>> expressions)
        where T : IEntity
    {
        if (expressions.Count == 0) return entity.ToObject();

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
            if (!propertyList.Contains(item.Key)) continue;

            result.Add(item.Key.Equals("Id")
                ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                : item);
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

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
    ) where T : IEntity
    {
        if (expressions.Count == 0) return entity.ToObject();

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
            if (!propertyList.Contains(item.Key)) continue;

            result.Add(item.Key.Equals("Id")
                ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                : item);
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    /// <summary>
    /// 转换为排除指定属性的首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static object? ToSimpleObjectIgnore<T>(
        this T entity,
        ICollection<Expression<Func<T, object>>>? expressions
    ) where T : IEntity
    {
        if (expressions == null || expressions.Count == 0) return entity.ToObject();

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
            if (propertyList.Contains(item.Key)) continue;

            result.Add(item.Key.Equals("Id")
                ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                : item);
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    /// <summary>
    /// 转换为排除指定属性的首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static object? ToSimpleObjectIgnore<T>(this T entity, ICollection<Expression<Func<object>>> expressions)
        where T : IEntity
    {
        if (expressions.Count == 0) return entity.ToObject();

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
            if (propertyList.Contains(item.Key)) continue;

            result.Add(item.Key.Equals("Id")
                ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                : item);
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

        return JsonConvert.DeserializeObject(JsonConvertAssist.SerializeAndKeyToLower(result));
    }

    public static List<object> ToListObject<T>(this IEnumerable<T> list) where T : IEntity
    {
        return list.Select(o => (object)o.ToExpandoObject()).ToList();
    }

    public static List<object> ToListSimpleObject<T>(
        this IEnumerable<T> list,
        ICollection<Expression<Func<object>>> expressions
    ) where T : IEntity
    {
        return list.Select(o => o.ToSimpleObject(expressions)).ToListFilterNullable();
    }

    public static List<object> ToListSimpleObjectIgnore<T>(
        this IEnumerable<T> list,
        ICollection<Expression<Func<object>>> expressions
    ) where T : IEntity
    {
        return list.Select(o => o.ToSimpleObjectIgnore(expressions)).ToListFilterNullable();
    }
}