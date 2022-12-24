using EasySoft.Core.Sql.Assists;
using EasySoft.Core.Sql.Common;
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
    ) where T : IEntity
    {
        var modelName = ReflectionAssist.GetClassName(entity);

        if (toLowerFirst) modelName = modelName.ToLowerFirst();

        var identification = ReflectionAssist.GetPropertyName(entity.GetPrimaryKeyLambda());

        return $"{modelName}{identification}";
    }

    /// <summary>
    /// GetKeyValue
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static long GetKeyValue<T>(this T entity) where T : IEntity
    {
        return entity.Id;
    }

    /// <summary>
    /// GetPrimaryKeyValue
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static long GetPrimaryKeyValue<T>(this T entity) where T : IEntity
    {
        return entity.Id;
    }

    /// <summary>
    /// SetPrimaryKeyValue
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="value"></param>
    /// <exception cref="NotImplementedException"></exception>
    public static void SetPrimaryKeyValue<T>(this T entity, long value) where T : IEntity
    {
        entity.Id = value;
    }

    /// <summary>
    /// GetTableName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetTableName<T>(this T entity) where T : IEntity
    {
        var tableAttribute = Tools.GetAdvanceTableAttribute(entity.GetType());

        return tableAttribute == null ? entity.GetType().Name : tableAttribute.Name;
    }

    /// <summary>
    /// GetPrimaryKeyLambda
    /// </summary>
    /// <returns></returns>
    public static Expression<Func<T, object>> GetPrimaryKeyLambda<T>(this T entity) where T : IEntity
    {
        return o => o.Id;
    }

    /// <summary>
    /// GetPrimaryKeyName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetPrimaryKeyName<T>(this T entity) where T : IEntity
    {
        var lambda = entity.GetPrimaryKeyLambda();

        var columnAttribute = Tools.GetAdvanceColumnAttribute(GetPropertyInfo(lambda));

        if (columnAttribute == null) return GetPropertyName(lambda);

        if (string.IsNullOrWhiteSpace(columnAttribute.Name))
            throw new Exception($"{nameof(columnAttribute)} disallow empty value");

        return columnAttribute.Name;
    }

    /// <summary>
    /// GetSqlSchemaName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlSchemaName<T>(this T entity) where T : IEntity
    {
        return "";
    }

    /// <summary>
    /// GetSqlFieldDecorateStart
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlFieldDecorateStart<T>(this T entity) where T : IEntity
    {
        return "[";
    }

    /// <summary>
    /// GetSqlFieldDecorateEnd
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlFieldDecorateEnd<T>(this T entity) where T : IEntity
    {
        return "]";
    }

    /// <summary>
    /// GetSqlFieldStringValueDecorateStart
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlFieldStringValueDecorateStart<T>(this T entity) where T : IEntity
    {
        return "'";
    }

    /// <summary>
    /// GetSqlFieldStringValueDecorateEnd
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlFieldStringValueDecorateEnd<T>(this T entity) where T : IEntity
    {
        return "'";
    }

    /// <summary>
    /// GetSqlSchemaTableName
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public static string GetSqlSchemaTableName<T>(this T entity) where T : IEntity
    {
        var schemaName = entity.GetSqlSchemaName();

        var tableAttribute = entity.GetType().GetCustomAttribute<TableAttribute>();

        if (tableAttribute == null)
            throw new Exception(
                "缺少AdvanceTableAttribute特性"
            );

        var name = tableAttribute.Name;

        return !string.IsNullOrWhiteSpace(schemaName) ? $"{schemaName}.{name}" : name;
    }

    private static PropertyInfo GetPropertyInfo<T>(
        Expression<Func<T, object>> propertyLambda
    ) where T : IEntity
    {
        if (propertyLambda.Body.NodeType == ExpressionType.MemberAccess)
        {
            dynamic me = propertyLambda.Body;

            if (me.Member != null)
                if (me.Member.PropertyType != null)
                {
                    var propertyInfo = me.Member as PropertyInfo;

                    if (propertyInfo == null) throw new ArgumentException("PropertyInfo is null'");

                    return propertyInfo;
                }
        }

        if (propertyLambda.Body.NodeType == ExpressionType.Convert)
        {
            if (propertyLambda.Body is not UnaryExpression cov)
                throw new ArgumentException("Cannot analyze type get name ");

            dynamic? me = cov.Operand as MemberExpression;

            if (me == null)
                throw new ArgumentException(
                    "You must pass a lambda of the form: ' Class=> Class.Property' or 'object => object.Property'"
                );

            if (me.Member == null) throw new ArgumentException("Cannot analyze type get name ");

            if (me.Member.PropertyType == null) throw new ArgumentException("Cannot analyze type get name ");

            var propertyInfo = me.Member as PropertyInfo;

            if (propertyInfo == null) throw new ArgumentException("PropertyInfo is null'");

            return propertyInfo;
        }

        throw new ArgumentException("Cannot analyze type get name ");
    }

    /// <summary>
    /// GetPropertyName
    /// </summary>
    /// <param name="expression"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPropertyName<T>(
        Expression<Func<T, object>> expression
    ) where T : IEntity
    {
        return ReflectionAssist.GetPropertyName(expression);
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static ExpandoObject ToObject<T>(this T entity, Func<T, ExpandoObject> func) where T : IEntity
    {
        var additionalData = func(entity);

        return entity.ToObject(additionalData);
    }

    /// <summary>
    /// 转换为属性首字母小写的Object
    /// </summary>
    /// <returns></returns>
    public static ExpandoObject ToObject<T>(this T entity, ExpandoObject? additionalData = null)
        where T : IEntity
    {
        var d = entity.ToExpandoObject();

        if (additionalData != null) d.Add(new KeyValuePair<string, object?>("additional", additionalData));

        d.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue().ToString()));

        var modelName = ReflectionAssist.GetClassName(entity).ToLowerFirst();

        var identification = ReflectionAssist.GetPropertyName(entity.GetPrimaryKeyLambda());

        var json = JsonConvertAssist.SerializeAndKeyToLower(d).Replace(
            $"\"{identification.ToLowerFirst()}\"",
            $"\"{modelName}{identification}\""
        );

        var jObject = JsonConvert.DeserializeObject<JObject>(json);

        if (jObject == null) throw new Exception("DeserializeObject result is null");

        return jObject.ToExpandoObject();
    }

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
    /// 转换为指定属性的首字母小写的Object
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expressions"></param>
    /// <returns></returns>
    public static object? ToSimpleObject<T>(
        this T entity,
        ICollection<Expression<Func<T, object>>> expressions
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
    public static object? ToSimpleObjectIgnore<T>(
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
            if (propertyList.Contains(item.Key)) continue;

            result.Add(item.Key.Equals("Id")
                ? new KeyValuePair<string, object?>(className + ".Id", item.Value)
                : item);
        }

        result.Add(new KeyValuePair<string, object?>("Key", entity.GetKeyValue()));

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
    ) where T : IEntity
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
    ) where T : IEntity
    {
        return list.Select(o => o.ToSimpleObjectIgnore(expressions)).ToListFilterNullable();
    }
}